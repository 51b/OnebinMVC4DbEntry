using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace Onebin.Extra.Util
{
    public class IPAddress
    {
        public static string GetIPAddress()
        {
            var result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(result))
            {
                //可能有代理 
                if (result.IndexOf(".") == -1)    //没有“.”肯定是非IPv4格式 
                    result = null;
                else
                {
                    if (result.IndexOf(",") != -1)
                    {
                        //有“,”，估计多个代理。取第一个不是内网的IP。 
                        result = result.Replace(" ", "").Replace("'", "");
                        string[] temparyip = result.Split(",;".ToCharArray());
                        foreach (var t in temparyip)
                        {
                            if (IsIpAddress(t)
                                && t.Substring(0, 3) != "10."
                                && t.Substring(0, 7) != "192.168"
                                && t.Substring(0, 7) != "172.16.")
                            {
                                return t;    //找到不是内网的地址 
                            }
                        }
                    }
                    else if (IsIpAddress(result)) //代理即是IP格式 
                        return result;
                    else
                        result = null;    //代理中的内容 非IP，取IP 
                }

            }

            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.UserHostAddress;

            return result;
        }

        /// <summary>
        /// 判断是否是IP地址格式 0.0.0.0
        /// </summary>
        /// <param name="str">待判断的IP地址</param>
        /// <returns>true or false</returns>
        public static bool IsIpAddress(string str)
        {
            if (string.IsNullOrEmpty(str) || str.Length < 6 || str.Length > 15)
                return false;

            var regex = new Regex(@"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$", RegexOptions.IgnoreCase);
            return regex.IsMatch(str);
        }
    }
}
