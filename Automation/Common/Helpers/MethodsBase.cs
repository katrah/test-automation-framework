using System.Collections.Generic;
using Framework.Library;

namespace Automation.Common.Helpers
{
    public abstract class MethodsBase
    {
        private readonly Dictionary<string, int> testKeyMap = new Dictionary<string, int>();

        public string MakeKey(string key)
        {
            if (!testKeyMap.ContainsKey(key))
            {
                testKeyMap.Add(key, 0);
            }
            testKeyMap[key]++;
            return string.Format("{0}-{1}", key, testKeyMap[key]);
        }

        public string GetKey(string key)
        {
            int ctr;
            if (testKeyMap.TryGetValue(key, out ctr))
            {
                return string.Format("{0}-{1}", key, testKeyMap[key]);
            }
            throw new TestFrameworkException("Performance Key {0} does not exist.", key);
        }
    }
}
