using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Onebin.Extra.Attr;

namespace Onebin.Tools
{
    public class GenerateSQL
    {
        private string DllName { get; set; }
        private string DllFilePath { get; set; }
        private string ScriptPath { get; set; }
        private ISqlStatement SqlStatement { get; set; }

        private IDictionary<string, MenuAttribute> menuDic = new Dictionary<string, MenuAttribute>();
        private IDictionary<string, PermissionAttribute> permissionDic = new Dictionary<string, PermissionAttribute>();
        private IList<Type> targetTypes = new List<Type>();

        public GenerateSQL(string path, string outputPath, string dllName, string outputName, ISqlStatement sqlStatement)
        {
            this.DllName = dllName;
            this.DllFilePath = path;
            this.SqlStatement = sqlStatement;
            this.ScriptPath = outputPath + @"\" + outputName;
        }

        public void CreateScript(out string msg)
        {
            try
            {
                DeleteUsedScript();
                CreateNewScript();

                msg = "脚本已生成，路径：" + ScriptPath;
            }
            catch (Exception ex)
            {
                msg = "生成失败，错误信息：" + ex.ToString();
            }
        }

        private void DeleteUsedScript()
        {
            if (File.Exists(ScriptPath))
            {
                File.Delete(ScriptPath);
            }
        }

        private void CreateNewScript()
        {
            FileStream fileStream = new FileStream(ScriptPath, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.GetEncoding("UTF-8"));

            WriteStream(streamWriter);

            streamWriter.Flush();
            streamWriter.Close();
            fileStream.Close();
        }

        private void WriteStream(StreamWriter streamWriter)
        {
            DirectoryInfo dir = new DirectoryInfo(DllFilePath);
            FileInfo[] files = dir.GetFiles();
            foreach (var file in files.Where(e => e.Name == DllName))
            {
                Assembly assembly = Assembly.LoadFrom(file.FullName);
                Type[] types = assembly.GetTypes();
                if (types == null)
                    continue;
                streamWriter.Write(SqlText(types));
            }

        }

        private string SqlText(Type[] types)
        {
            VerifyTypes(types);

            StringBuilder sqlText = new StringBuilder();

            sqlText.Append(SqlStatement.BeforeGen());

            foreach (Type type in targetTypes)
            {
                sqlText.Append(MenuSQL(type));
            }
            sqlText.Append(DelectUsesdMenu());

            foreach (Type type in targetTypes)
            {
                sqlText.Append(PermissionSQL(type));
            }
            sqlText.Append(DeleteUsedPermission());

            sqlText.Append(SqlStatement.AfterGen());

            return sqlText.ToString();
        }

        private void VerifyTypes(Type[] types)
        {
            foreach (Type type in types)
            {
                if (type.GetCustomAttributes(typeof(MenuAttribute), false).Count() > 0)
                {
                    targetTypes.Add(type);
                }
            }
        }

        private string MenuSQL(Type type)
        {
            StringBuilder sql = new StringBuilder();
            var attrs = type.GetCustomAttributes(typeof(MenuAttribute), false);
            foreach (MenuAttribute attr in attrs)
            {
                if (menuDic.ContainsKey(attr.Id))
                {
                    throw new Exception("存在重复的MenuAttribute.Id :" + attr.Id);
                }
                else
                {
                    menuDic.Add(attr.Id, attr);
                }
                sql.Append(string.Format(SqlStatement.GenMenu(), new object[] {
                    attr.Id,
                    attr.ParentId,
                    attr.Name,
                    attr.Url,
                    attr.Category,
                    attr.Level
                }));
            }
            return sql.ToString();
        }

        private string DelectUsesdMenu()
        {
            string sql = "";
            if (menuDic.Keys.Count > 0)
            {
                StringBuilder deleteTarget = new StringBuilder();
                foreach (var id in menuDic.Keys)
                {
                    deleteTarget.Append("'" + id + "',");
                }
                sql = string.Format(SqlStatement.DeleteMenu(), deleteTarget.ToString().Substring(0, deleteTarget.Length - 1));
            }
            return sql;
        }

        private string PermissionSQL(Type type)
        {
            StringBuilder sql = new StringBuilder();
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);

            foreach (var method in methods)
            {
                var attrs = method.GetCustomAttributes(typeof(PermissionAttribute), false);
                foreach (PermissionAttribute attr in attrs)
                {
                    if (attr.Id == null || attr.Dependent == true)
                    {
                        continue;
                    } 
                    else if (attr.MenuId == null){
                        throw new Exception("PermissionAttribute.Id :" + attr.Id + " 未指定MenuId");
                    }
                    else if (permissionDic.ContainsKey(attr.Id))
                    {
                        throw new Exception("存在重复的PermissionAttribute.Id :" + attr.Id);
                    }
                    else if (!menuDic.ContainsKey(attr.MenuId))
                    {
                        throw new Exception("不存在MenuAttribute.Id :" + attr.MenuId);
                    }
                    else
                    {
                        permissionDic.Add(attr.Id, attr);

                        sql.Append(string.Format(SqlStatement.GenPermission(), new object[] {
                            attr.Id,
                            attr.MenuId,
                            attr.Name,
                            attr.Action
                        }));
                    }
                }
            }
            return sql.ToString();
        }

        private string DeleteUsedPermission()
        {
            string sql = "";
            if (permissionDic.Keys.Count > 0)
            {
                StringBuilder deleteTarget = new StringBuilder();
                foreach (var id in permissionDic.Keys)
                {
                    deleteTarget.Append("'" + id + "',");
                }
                sql = string.Format(SqlStatement.DeletePermission(), deleteTarget.ToString().Substring(0, deleteTarget.Length - 1));
            }
            return sql;
        }

    }
}
