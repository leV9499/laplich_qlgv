using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace QLGVFunction2.Service
{
    public class ValidateService
    {
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(email, pattern);
        }
        public static bool IsPhoneValid(string input)
        {
            input = input.Replace(" ", "");
            if (input.Length == 10 || input.Length == 11)
            {
                foreach (char c in input)
                {
                    if (!char.IsDigit(c))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string ConvertToDateFormat(string input)
        {
            DateTime date = DateTime.Parse(input);
            string formattedDate = date.ToString("yyyy-MM-dd");

            return formattedDate;
        }

        public static DateTime ConvertToDate(string birthdayConvert)
        {
            string[] formats = { "dd-MM-yyyy", "dd/MM/yyyy", "yyyy-MM-dd", "yyyy/MM/dd" };

            DateTime birthday;

            try
            {
                birthday = DateTime.TryParseExact(birthdayConvert, formats, null, System.Globalization.DateTimeStyles.None, out birthday) ? birthday : throw new ArgumentException();
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Chuỗi ngày tháng không hợp lệ: " + ex.Message);
            }

            return birthday;
        }
        public static bool IsValidDateFormat(string dateString)
        {
            if (DateTime.TryParse(dateString, out DateTime result))
            {
                string formattedDate = result.ToString("M/d/yyyy");
                return dateString == formattedDate;
            }
            else
            {
                return false;
            }
        }
        public static string ConvertDateFormat(string dateString)
        {
            string[] parts = dateString.Split('/');

            if (parts.Length != 3)
            {
                return null;
            }

            int month = int.Parse(parts[0]);
            int day = int.Parse(parts[1]);
            int year = int.Parse(parts[2]);

            string formattedDate = new DateTime(year, month, day).ToString("yyyy-MM-dd");

            return formattedDate;
        }
        public static string FormatDateTime(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        //public static string ConvertToNoSign(string str)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    foreach (char c in str)
        //    {
        //        if (CONST.VIETNAMDICT.ContainsKey(c))
        //        {
        //            sb.Append(CONST.VIETNAMDICT[c]);
        //        }
        //        else
        //        {
        //            if ((c >= 97 && c <= 122) || c == ' ')
        //                sb.Append(c);
        //        }
        //    }
        //    return sb.ToString();
        //}
        public static bool ContainsNonLetter(string str)
        {
            return !Regex.IsMatch(str, @"^[a-zA-Z]+$");
        }
        public static bool ContainsNumber(string str)
        {
            return str.Any(char.IsDigit);
        }
    }
}
