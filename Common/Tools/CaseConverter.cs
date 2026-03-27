using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace Common.Tools
{
    public class CaseConverter
    {
        public static string RandomCase(string str)
        {
            if (str != null)
            {
                Random randomizer = new Random();
                IEnumerable<char> final = str.Select(x => 
                    randomizer.Next() %2 == 0
                        ? (char.IsUpper(x) ? x.ToString().ToLower().First() : x.ToString().ToUpper().First())
                        : x);
                return new string(final.ToArray());
            }
            return str;
        }
        
        public static string PascalCase(string str)
        {
            TextInfo info = CultureInfo.CurrentCulture.TextInfo;
            str = info.ToTitleCase(str).Replace(" ", string.Empty);
            return str;
        }
    }
}