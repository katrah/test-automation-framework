
namespace Framework.Library.Extensions
{
    public static class Primitives
    {
        public static bool Between(this decimal num, decimal lower, decimal upper)
        {
            return lower <= num && num <= upper;
        }

        public static bool Between(this double num, double lower, double upper)
        {
            return lower <= num && num <= upper;
        }

        public static bool Between(this float num, float lower, float upper)
        {
            return lower <= num && num <= upper;
        }

        public static bool Between(this int num, int lower, int upper)
        {
            return lower <= num && num <= upper;
        }

        public static bool Between(this long num, long lower, long upper)
        {
            return lower <= num && num <= upper;
        }
    }
}
