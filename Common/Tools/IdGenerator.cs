using System;
using System.Text;

namespace Common.Tools
{
    public class IdGenerator
    {
        public static string AlphaNum(int length)
        {
            const string pool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            StringBuilder rs = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                rs.Append(pool[(int)(random.NextDouble() * pool.Length)]);
            }
            return rs.ToString();
        }
    }
}
