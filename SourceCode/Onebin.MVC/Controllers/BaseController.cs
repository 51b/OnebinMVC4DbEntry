using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Onebin.Extra.Dic;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Web;
using System.Security;
using System.Security.Permissions;
using Leafing.Data.Definition;
using Leafing.Data;
using Leafing.Core.Logging;
using Onebin.MVC.Filter;
using Onebin.MVC.Domain;
using Onebin.Extra.Dic;
using Onebin.Extra.Util;
using Leafing.Core;

namespace Onebin.MVC.Controllers
{
    [ExceptionFilter]
    public class BaseController : Controller
    {
        /// <summary>
        /// 当前系统登录用户
        /// </summary>
        public Account CurrentAccount
        {
            get { return Session[GlobalConstant.CURRENT_ACCOUNT] as Account; }
        }

        /// <summary>
        /// 角色密钥
        /// </summary>
        public string RoleKey
        {
            get { return Session[GlobalConstant.ROLE_KEY].ToString(); }
        }

        /// <summary>
        /// 用户菜单
        /// </summary>
        public IList<MenuItem> AccountMenu
        {
            get { return Session[GlobalConstant.ACCOUNT_MENU] as IList<MenuItem>; }
        }

        public void MessageTip(ResultStatus status, string message)
        {
            TempData["msgTip"] = message;
            TempData["msgTipStatus"] = (int)status;
        }

        #region 成员变量

        protected int pageIndex = 1;
        protected int pageSize = 15;

        #endregion

        /// <summary>
        /// 初始化基本页面信息
        /// </summary>
        protected void InitPageData(FormCollection formData)
        {
            pageIndex = int.Parse(formData["page"] ?? "1") - 1;
            pageSize = int.Parse(formData["rows"] ?? int.MaxValue.ToString());
        }

        public JsonResult GetPage<T>(FormCollection formData, OrderBy orderby, Condition condition = null) where T : class, IDbObject
        {
            condition = condition ?? Condition.Empty;
            InitPageData(formData);
            var ps = DbEntry.From<T>()
                .Where(condition)
                .OrderBy(orderby)
                .PageSize(pageSize)
                .GetPagedSelector();
            return Json(new EasyUiDataGrid()
                .SetRows(ps.GetResultCount(), ps.GetCurrentPage(pageIndex))
                .GetJsonModel());
        }

        /// <summary>
        /// 编辑（增删改）
        /// </summary>
        /// <typeparam name="T">Model的类型</typeparam>
        /// <typeparam name="TKey">Model的主键</typeparam>
        /// <param name="model">Model的实例</param>
        /// <param name="customDelegate">自定义方法委托</param>
        /// <param name="operation">操作描述</param>
        /// <returns></returns>
        public ReturnResult Edit<T, TKey>(T model, Action customDelegate, string operation = "操作")
            where T : DbObjectModel<T, TKey>, new()
            where TKey : struct
        {
            if (model == null)
            {
                return new ReturnResult(ResultStatus.Failure, "操作数据项不存在！");
            }
            try
            {
                DbEntry.UsingTransaction(customDelegate);
                return new ReturnResult(ResultStatus.Success, operation + "成功！").SetData(model);
            }
            catch (System.Exception ex)
            {
                Logger.System.Fatal(ex);
                return new ReturnResult(ResultStatus.Failure, operation + "失败，请稍候再试！");
            }
        }

        /// <summary>
        /// 编辑（增删改）
        /// </summary>
        /// <typeparam name="T">Model的类型</typeparam>
        /// <typeparam name="TKey">Model的主键</typeparam>
        /// <param name="model">Model的实例</param>
        /// <param name="actionType">操作类型</param>
        /// <param name="operation">操作描述</param>
        /// <returns>ReturnResult</returns>
        public ReturnResult Edit<T, TKey>(T model, ActionType actionType, string operation = "操作")
            where T : DbObjectModel<T, TKey>, new()
            where TKey : struct
        {
            Action customDelegate = delegate
            {
                if (actionType == ActionType.Delete)
                {
                    model.Delete();
                }
                else
                {
                    model.Save();
                }
            };
            return Edit<T, TKey>(model, customDelegate, operation);
        }

        /// <summary>
        /// 导出类型
        /// </summary>
        private class ExportType
        {
            public string ReportType { get; set; }
            public string OutputFormat { get; set; }
            public string ContentType { get; set; }
        }

        /// <summary>
        /// 导出Word报表
        /// </summary>
        /// <param name="dataSet">数据源</param>
        /// <param name="filePath">模板文件路径</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="reportParameters">参数</param>
        /// <returns>报表</returns>
        public ActionResult ExportWord(DataSet dataSet, string filePath, string fileName, List<ReportParameter> reportParameters = null)
        {
            ExportType exportType = new ExportType()
            {
                ReportType = "Word",
                OutputFormat = "Word",
                ContentType = "application/ms-word"
            };
            return Export(dataSet, filePath, fileName, exportType, reportParameters);
        }

        /// <summary>
        /// 导出Excel报表
        /// </summary>
        /// <param name="dataSet">数据源</param>
        /// <param name="filePath">模板文件路径</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="reportParameters">参数</param>
        /// <returns>报表</returns>
        public ActionResult ExportExcel(DataSet dataSet, string filePath, string fileName, List<ReportParameter> reportParameters = null)
        {
            ExportType exportType = new ExportType() 
            {
                ReportType = "Excel",
                OutputFormat = "Excel",
                ContentType = "application/ms-excel"
            };
            return Export(dataSet, filePath, fileName, exportType, reportParameters);
        }

        /// <summary>
        /// 报表导出
        /// </summary>
        /// <param name="dataSet">数据源</param>
        /// <param name="filePath">模板文件路径</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="exportType">导出类型</param>
        /// <param name="reportParameters">参数</param>
        /// <returns>报表</returns>
        private ActionResult Export(DataSet dataSet, string filePath, string fileName, ExportType exportType, List<ReportParameter> reportParameters)
        {
            LocalReport localReport = new LocalReport();
            localReport.SetBasePermissionsForSandboxAppDomain(new PermissionSet(PermissionState.Unrestricted));

            localReport.ReportPath = Server.MapPath(filePath);
            foreach (DataTable dt in dataSet.Tables)
            {
                ReportDataSource reportDataSource = new ReportDataSource(dt.TableName, dt);
                localReport.DataSources.Add(reportDataSource);
            }

            if (reportParameters != null && reportParameters.Count > 0)
            {
                foreach (var reportParameter in reportParameters)
                {
                    localReport.SetParameters(reportParameter);
                }
            }

            string reportType = exportType.ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =
           "<DeviceInfo>" +
           "  <OutputFormat>" + exportType.OutputFormat + "</OutputFormat>" +
           "  <PageWidth></PageWidth>" +
           "  <PageHeight></PageHeight>" +
           "  <MarginTop></MarginTop>" +
           "  <MarginLeft></MarginLeft>" +
           "  <MarginRight></MarginRight>" +
           "  <MarginBottom></MarginBottom>" +
           "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            Response.Clear();
            Response.ContentType = exportType.ContentType;
            Response.AddHeader("content-disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8) + "." + fileNameExtension);
            Response.BinaryWrite(renderedBytes);
            Response.End();

            return View();
        }
    }
}
