using System;
using Framework.Models;

namespace Framework.Library
{
    [Serializable]
    public sealed class TestController
    {
        public TestController()
        {
            PartialPasses = 0;
            PartialFailures = 0;
        }

        public static void StaticLog(string message, params object[] args)
        {
            // If the message contains {} characters that are not part of a string format like {0}
            // (for example, if you are printing out a JSON string), the WriteLine method that
            // passes in args will puke.  So, if you have {} characters in your string and your
            // string isn't a string format string (JSON), then you don't have any args to pass in
            // because the args are only used with a string format.  Therefore, or therefive
            // (whatever it takes), just use the WriteLine method that takes a string that isn't
            // expected to be a formatted string.
            if (args.Length == 0)
            {
                Console.Out.WriteLine(message);
            }
            else
            {
                Console.Out.WriteLine(message, args);
            }
        }

        public static void StaticLog(string message, TestLogEntryType logEntryType, params object[] args)
        {
            StaticLog(string.Format("[{0}] {1}", logEntryType, message), args);
        }

        #region Public Members (Exposed to Test Cases)
        public void Log(string message, params object[] args)
        {
            // If the message contains {} characters that are not part of a string format like {0}
            // (for example, if you are printing out a JSON string), the WriteLine method that
            // passes in args will puke.  So, if you have {} characters in your string and your
            // string isn't a string format string (JSON), then you don't have any args to pass in
            // because the args are only used with a string format.  Therefore, or therefive
            // (whatever it takes), just use the WriteLine method that takes a string that isn't
            // expected to be a formatted string.
            if (args.Length == 0)
            {
                Console.Out.WriteLine(message);
            }
            else
            {
                Console.Out.WriteLine(message, args);
            }
        }

        public void Warn(string message, params object[] args)
        {
            Log(string.Format("[{0}] {1}", TestLogEntryType.Warn, message), args);
        }

        internal void LogPerf(string message, params object[] args)
        {
            Log(string.Format("[{0}] {1}", TestLogEntryType.PerfItem, message), args);
        }

        public int PartialPasses { get; private set; }
        public int PartialFailures { get; private set; }

        public void PassAndResume(string message, params object[] args)
        {
            if (message != null)
            {
                Log(string.Format("[{0}] {1}", TestLogEntryType.Pass, message), args);
            }
            PartialPasses++;
        }

        public void FailAndResume(string message, params object[] args)
        {
            Log(string.Format("[{0}] {1}", TestLogEntryType.Fail, message), args);
            PartialFailures++;
        }

        public void AssertAndResume(bool condition, string message, params object[] args)
        {
            if (condition)
            {
                PassAndResume(message, args);
            }
            else
            {
                FailAndResume(message, args);
            }
        }
        #endregion
    }
}
