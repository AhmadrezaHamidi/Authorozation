using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Extensions
{
    public static class EnumUtilities
    {
        public static T ConvertStringToEnum<T>(this string inputStr, T defaultValue)
        {
            try
            {
                var type = typeof(T);

                if (!type.IsEnum || string.IsNullOrEmpty(inputStr) || string.IsNullOrWhiteSpace(inputStr))
                    return defaultValue;

                var valueList = Enum.GetValues(type);
                var nameList = Enum.GetNames(type);

                for (int i = 0; i < nameList.Length; i++)
                {
                    var name = nameList[i];
                    var value = valueList.GetValue(i);
                    if (inputStr.ToLower() == name.ToLower())
                        return (T)Enum.Parse(type, name);
                }
                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
