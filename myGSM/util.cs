using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace myGSM
{
    class util
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="msg"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string formatMsg(string phone, string msg, string time)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"发件人：+{phone}  ");

            DateTime dt = DateTime.ParseExact(time, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
            String localTime = dt.ToLocalTime().ToString("G");
            sb.Append($"时间：{localTime}  ");
            sb.Append($"\r\n内容：{msg}\r\n  ");

            return sb.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="msg"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string formatMsg(String index, string phone, string msg, string time)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"序号：{index}  ");
            sb.Append($"发件人：+{phone}  ");

            DateTime dt = DateTime.ParseExact(time, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
            String localTime = dt.ToLocalTime().ToString("G");
            sb.Append($"时间：{localTime}  ");
            sb.Append($"\r\n内容：{msg}\r\n  ");

            return sb.ToString();
        }
    }
}
