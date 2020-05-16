using System;
using System.Security.Cryptography;
using System.IO;

namespace bookcity.common.Helper
{
	/// <summary>
	/// CEnCoder 的摘要说明。
	/// </summary>
	public static class CEnCoder
	{
        public static string ReplaceSingleToDoubleQuotation(string strInput)
        {
            strInput = strInput.Replace("'", "\"");
            return strInput;
        }

        public static string SpecPathString(string sPath)
        {
            if(sPath.EndsWith("\\"))
                return sPath;
            return sPath + "\\";

        }

		/// <summary>
		/// md5编码
		/// </summary>
		/// <param name="input">要编码的字符串</param>
		/// <returns>编码后的字符串</returns>
		public static string MD5(string input)
		{
			byte[] b = System.Text.Encoding.UTF8.GetBytes(input);
			b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
			string ret = "";
			for(int i = 0;i < b.Length;i ++)
				ret += b[i].ToString("x").PadLeft(2,'0');
			return ret;
		}

        /// <summary>
        /// 放止SQL注入
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string ReplaceSpecialCharacters(string str)
        {
            str = str.Trim();
            //str = str.Trim(new char[] { '(', ')' });
            //str = str.Replace("--", string.Empty);
            str = str.Replace("'", "''");
            if(str.IndexOf("and", StringComparison.OrdinalIgnoreCase)>=0)
            {
                string substring=str.Substring(str.IndexOf("and",StringComparison.OrdinalIgnoreCase),3);
                str.Replace(substring, string.Empty);
            }
            if(str.IndexOf("or", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                string substring = str.Substring(str.IndexOf("or", StringComparison.OrdinalIgnoreCase), 2);
                str.Replace(substring, string.Empty);
            }

            return str;
        }

        /// <summary>
        /// ToReplacementString替换前的字符串
        /// ToReplaceCharacter要被替换的字符
        /// ReplacedCharacter 替换成的字符
        /// 返回替换后的字符串
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string ReplaceSpecialCharacters(string ToReplacementString, string ToReplaceCharacter, string ReplacedCharacter)
        {
            return Convert.ToString(ToReplacementString).Trim().Replace(ToReplaceCharacter, ReplacedCharacter);
        }

        /// <summary>
        /// URL加密
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string EncryptURL(string url)
        {
            url = url.Trim();
            url = url.Replace("?", "question|");
            url = url.Replace("&", "and|");

            return url;
        }

        /// <summary>
        /// URL解密
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string DecryptURL(string url)
        {
            url = url.Trim();
            url = url.Replace("question|", "?");
            url = url.Replace("and|", "&");

            return url;
        }

        #region 加密解密
        private const string Key = "gt9a93si9pw=";
        private const string IV = "seyQJ8LhyWE=";

        public static string EncryptString(string Value)
        {
            CryptoStream cs = null;
            try
            {
                SymmetricAlgorithm mCSP = new DESCryptoServiceProvider();
                mCSP.Key = Convert.FromBase64String(Key);
                mCSP.IV = Convert.FromBase64String(IV);

                ICryptoTransform ct;
                MemoryStream ms;
                byte[] byt;

                ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV);

                byt = System.Text.Encoding.UTF8.GetBytes(Value);

                ms = new MemoryStream();
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();

                cs.Close();

                return Convert.ToBase64String(ms.ToArray());
            }
            catch
            {
                if(cs != null)
                    cs.Close();
                return null;
            }
        }

        public static string DecryptString(string Value)
        {
            CryptoStream cs = null;
            try
            {
                SymmetricAlgorithm mCSP = new DESCryptoServiceProvider();
                mCSP.Key = Convert.FromBase64String(Key);
                mCSP.IV = Convert.FromBase64String(IV);

                ICryptoTransform ct;
                MemoryStream ms;
                byte[] byt;

                ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);

                byt = Convert.FromBase64String(Value);

                ms = new MemoryStream();
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();

                cs.Close();

                return System.Text.Encoding.UTF8.GetString(ms.ToArray());

            }
            catch
            {
                if(cs != null)
                    cs.Close();
                return null;
            }
        }
        #endregion
    }
}
