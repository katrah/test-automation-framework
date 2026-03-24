using System.Linq;
//using NUnit.Framework;
using Framework.Models;

namespace Framework.Library
{
    /// <summary>
    /// Ultimate Base Class for All Integration Tests
    /// </summary>
    public class IntegrationTestBase
    {
        /// <summary>
        /// Default Configuration File if none specified
        /// </summary>
        internal const string DefaultConfigurationFile = "OG.config";

        /// <summary>
        /// Default Test Customer Instance Key if none specified
        /// </summary>
        internal const string DefaultCustomerInstanceKey = "Default";

        /// <summary>
        /// Context Instance
        /// </summary>
        public IntegrationTestContext Context;

        /// <summary>
        /// Performance Tracker
        /// </summary>
        public TestPerformanceHelper Performance
        {
            get
            {
                if (Context == null)
                {
                    throw new TestConfigurationException("TestPerformanceHelper Test requires a valid Test Context");
                }
                return Context.PerformanceHelper;
            }
        }

        /// <summary>
        /// Logger
        /// </summary>
        public TestController Test
        {
            get
            {
                if (Context == null)
                {
                    throw new TestConfigurationException("TestController Test requires a valid Test Context");
                }
                return Context.Controller;
            }
        }

        private readonly string contextCacheKey;
        private readonly string contextSourceFile;

        public IntegrationTestBase(string cachekey = null, string configurationXmlFilePath = DefaultConfigurationFile)
        {
            contextCacheKey = cachekey ?? configurationXmlFilePath ?? DefaultConfigurationFile;
            contextSourceFile = configurationXmlFilePath ?? DefaultConfigurationFile;
        }

        /// <summary>
        /// IMPORTANT: Nunit does not report Fixture failures properly, so DIY on the top most test fixture
        /// http://stackoverflow.com/questions/1411676/how-to-diagnose-testfixturesetup-failed
        /// </summary>         
        public virtual void TestFixtureSetUp()
        {

        }

        /// <summary>
        /// IMPORTANT: Nunit does not report Fixture failures properly, so DIY on the top most test fixture
        /// http://stackoverflow.com/questions/1411676/how-to-diagnose-testfixturesetup-failed
        /// </summary>        
        public virtual void TestFixtureTearDown()
        {

        }

        /// <summary>
        /// 
        /// </summary>          
        public virtual void SetUp()
        {
            Context = IntegrationTestContext.GetInstance(contextCacheKey, contextSourceFile);
        }

        /// <summary>
        /// 
        /// </summary>        
        public virtual void TearDown()
        {
            if (Performance.Instance.Tests.Count > 0)
            {
                foreach (TestPerformanceUnit perfTest in Performance.Instance.Tests.Where(perfTest => perfTest.Status == TestExecutionStatus.Started))
                {
                    Test.Warn("Performance Test incomplete (Key: {0})", perfTest.Key);
                }

                Test.Log("[{0}] {1}", TestLogEntryType.PerfResult, Performance.Serialize());
                if (Performance.Instance.TotalFailures > 0)
                {
                    if (Context.IsPerformanceRun)
                    {
                        Test.FailAndResume("{0} Performance Failure(s) found. Test Failed.", Performance.Instance.TotalFailures);
                    }
                    else
                    {
                        Test.Warn("{0} Performance Failure(s) found. Will not fail because of this since this is not a performance run.", Performance.Instance.TotalFailures);
                    }
                }
                else
                {
                    Test.PassAndResume("{0} Performance Pass(es) found.", Performance.Instance.TotalPasses);
                }

                int incompleteTests = Performance.Instance.Tests.Count - (Performance.Instance.TotalPasses + Performance.Instance.TotalFailures);
                if (incompleteTests > 0)
                {
                    Test.FailAndResume("{0} Incomplete Performance tests found. Test Case is incorrectly written", incompleteTests);
                }
            }

            // Get the PartialFailures from the Context before I null it
            int failures = Test.PartialFailures;
            Context = null;

            if (failures > 0)
            {
                //Assert.Fail("{0} Partial Failure(s) found. Test Failed.", failures);
            }
        }
    }
}
