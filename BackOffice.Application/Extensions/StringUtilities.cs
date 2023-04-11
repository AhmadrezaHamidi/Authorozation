using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BackOffice.Application.Extensions
{
    public static class StringUtilities
    {
        public static string CleanIranMobilePhoneNumber(this string mobileNumber)
        {
            var result = mobileNumber;
            try
            {
                result = result.Trim().Replace(" ", "").Replace("-", "").ConvertPersianDigitsAndArabicAlphebet();
                if (result.StartsWith("+98"))
                    result = result.Replace("+98", "0");
                if (result.StartsWith("0098"))
                    result = "+" + result.Replace("+0098", "0");
                if (result.StartsWith("98"))
                    result = "+00" + result.Replace("+0098", "0");
                if (result.StartsWith("00"))
                    result = "+" + result.Replace("+00", "0");
                if (!result.IsNumberOnly())
                    result = result.OnlyNumberAccept();
            }
            catch
            {
            }
            return result;
        }

        public static string ConvertPersianDigitsAndArabicAlphebet(this string temp)
        {
            try
            {
                var result = temp.Trim().ConvertPersianDigits();
                result = result.Replace("ي", "ی").Replace("ك", "ک");
                return result;
            }
            catch
            {
                return temp;
            }
        }

        public static string ConvertPersianDigits(this string text)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(text) || string.IsNullOrEmpty(text))
                    return "";

                var textStr = StripHTML(text);

                return textStr.Replace("٠", "0").Replace("۰", "0")
                    .Replace("١", "1").Replace("۱", "1")
                    .Replace("٢", "2").Replace("۲", "2")
                    .Replace("٣", "3").Replace("۳", "3")
                    .Replace("٤", "4").Replace("۴", "4")
                    .Replace("٥", "5").Replace("۵", "5")
                    .Replace("٦", "6").Replace("۶", "6")
                    .Replace("٧", "7").Replace("۷", "7")
                    .Replace("٨", "8").Replace("۸", "8")
                    .Replace("٩", "9").Replace("۹", "9");
            }
            catch
            {
                return text;
            }
        }

        public static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }

        public static bool IsNumberOnly(this string numberStr, bool emptyStringIsNumber = true)
        {
            if (string.IsNullOrWhiteSpace(numberStr) || string.IsNullOrEmpty(numberStr))
                return emptyStringIsNumber;

            var numberCharArray = "0123456789".ToCharArray();
            var charArray = numberStr.Trim().ToCharArray();
            foreach (var charData in charArray)
                if (!numberCharArray.Contains(charData))
                    return false;

            return true;
        }

        public static string OnlyNumberAccept(this string str)
        {
            if (string.IsNullOrWhiteSpace(str) || string.IsNullOrEmpty(str))
                return "";

            var numberCharArray = "0123456789".ToCharArray();
            var charArray = str.ConvertPersianDigits().ToCharArray();
            var result = "";
            foreach (var charData in charArray)
                if (numberCharArray.Contains(charData))
                    result += charData;

            return result;
        }
    }
}
