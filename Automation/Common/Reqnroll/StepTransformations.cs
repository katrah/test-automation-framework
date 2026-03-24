using System;
using Reqnroll;

namespace Automation.Common.SpecFlow
{
    [Binding]
    public class StepTransformations
    {
        #region Boolean
        [StepArgumentTransformation(@"(?i)(succeeds?|fails?)$")]
        [StepArgumentTransformation(@"(?i)(check|uncheck)$")]
        public bool SucceedFailTransform(string boolString)
        {
            if (boolString.StartsWith("succeed", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (boolString.Equals("check", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
