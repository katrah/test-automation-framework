using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data;

namespace Common.Tools
{
    public class ErrorHandling
    {
        public static string UnexpectedError(Exception ex, params string[] values)
        {
            string retVal = "";
            if (ex != null && ex.Message != "Exception of type 'System.Exception' was thrown.")
            {
                retVal = string.Concat(ex.Message, Environment.NewLine);
            }
            return string.Concat(retVal, string.Join(Environment.NewLine, values.Where(str => !string.IsNullOrEmpty(str))));
        }

        public static string UnexpectedError<T>(Exception ex, IEnumerable<T> responses) where T : ResponseBase
        {
            string retVal = "";
            if (ex != null && ex.Message != "Exception of type 'System.Exception' was thrown.")
            {
                retVal = string.Concat(ex.Message, Environment.NewLine);
            }
            return string.Concat(retVal, string.Join(Environment.NewLine, responses.Where(r => !string.IsNullOrEmpty(r.Msg))));
        }

        public static string UnexpectedError<T>(Exception ex, params IEnumerable<T>[] responses) where T : ResponseBase
        {
            string retVal = "";
            if (ex != null && ex.Message != "Exception of type 'System.Exception' was thrown.")
            {
                retVal = string.Concat(ex.Message, Environment.NewLine);
            }

            foreach (IEnumerable<T> response in responses)
            {
                retVal = string.Concat(retVal, string.Join(Environment.NewLine, response.Where(r => !string.IsNullOrEmpty(r.Msg))));
            }
            return retVal;
        }
    }
}
