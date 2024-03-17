using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace ShipwayUnitOfWork.Helper
{
    public class Utils
    {
        public static string MD5(string password)
        {
            if (Utils.IsMD5(password))
            {
                return password;
            }
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = md5.ComputeHash(encoder.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        public static bool IsMD5(string str)
        {
            var regex = new Regex(@"[0-9a-fA-F]{32}");
            if (regex.IsMatch(str))
            {
                return true;
            }
            return false;
        }

        public static string SummaryString(string str, int? size = null)
        {
            size = size ?? 15;
            str = str.Length >= size ? str.Substring(0, size ?? 15) + "..." : str;
            return str;
        }

        public static string ConvertMoney(int money)
        {
            money = money * 1000;
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
            string a = money.ToString("#,###", cul.NumberFormat);
            return a;
        }
    }
}