
namespace Automation.Common.Helpers
{
    public static class InputParser
    {
        public static int TranslatePastPresentFuture(string pastPresentFuture)
        {
            switch (pastPresentFuture.ToLower())
            {
                case "next":
                    return 1;

                case "current":
                    return 0;

                case "previous":
                    return -1;

                default:
                    return 0;
            }
        }
    }
}
