using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Onebin.Extra.Util
{
    /// <summary>
    /// 使用MD5加密的工具类
    /// </summary>
    public class MD5Encryption
    {
        /// <summary>
        ///  使用MD5进行加密
        /// </summary>
        /// <returns>加密后的字符串</returns>
        public static string Encode(string source)
        {
            MD5 md5Hasher = MD5.Create();
            //计算哈希值
            byte[] hashData = md5Hasher.ComputeHash(Encoding.Default.GetBytes(source));
            StringBuilder strBuilder = new StringBuilder();
            //将哈希字节数组转换成为字符串
            for (int i = 0; i < hashData.Length; i++)
            {
                strBuilder.Append(hashData[i].ToString("x2"));
            }
            // 返回哈希字符串
            return strBuilder.ToString();
        }

        /// <summary>
        /// 检查一个普通字符串的Md5，与传递的Md5字符串是否相同
        /// </summary>
        /// <param name="strInput">普通字符串</param>
        /// <param name="strHash">Md5字符串</param>
        /// <returns>返回是否相同</returns> 
        public static bool VerifyMd5Hash(string strInput, string strHash) {
            //获取输入的普通字符串对应的哈希字符串
            string strhashOfInput = Encode(strInput);

            // 创建StringComparer，用于比较字符串
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            //比较哈希字符串是否相等
            return 0 == comparer.Compare(strhashOfInput, strHash);
        }
    }
}
