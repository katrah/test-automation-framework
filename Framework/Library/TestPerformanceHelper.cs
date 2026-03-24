using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Framework.Models;

namespace Framework.Library
{
    public sealed class TestPerformanceHelper
    {
        private static readonly object locker = new object();

        private readonly TestController controller;

        private readonly TestPerformance performance = new TestPerformance();
        public TestPerformance Instance { get { return performance; } }

        public TestPerformanceHelper(TestController testController)
        {
            if (testController == null)
            {
                throw new ArgumentNullException("testController");
            }
            controller = testController;
        }

        public void StartTest(string key, TimeSpan expectedExecutionTime)
        {
            if (performance.Tests.Any(test => test.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new TestFrameworkException("Performance Key {0} was already started or completed.", key);
            }

            (performance.Tests).Add(new TestPerformanceUnit
            {
                Key = key,
                Expected = expectedExecutionTime.TotalSeconds,
                StartTime = DateTime.Now,
                EndTime = DateTime.MaxValue,
                Status = TestExecutionStatus.Started
            });
            controller.LogPerf("START: Performance Test (Key: \"{0}\")", key);
        }

        public void EndTest(string key)
        {
            TestPerformanceUnit test = performance.Tests.SingleOrDefault(pt => pt.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
            if (test == null)
            {
                throw new TestFrameworkException("Performance Key {0} does not exist.", key);
            }

            controller.LogPerf("END: Performance Test (Key: \"{0}\")", key);
            test.EndTime = DateTime.Now;
            test.Success = test.ElapsedInSeconds.CompareTo(test.Expected) <= 0;
            test.Status = TestExecutionStatus.Finished;

            if (test.Success)
            {
                performance.TotalPasses++;
            }
            else
            {
                performance.TotalFailures++;
            }
        }

        public string Serialize()
        {
            lock (locker)
            {
                try
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(TestPerformance));
                    XmlWriterSettings settings = new XmlWriterSettings
                    {
                        Indent = true,
                        OmitXmlDeclaration = true,
                        Encoding = Encoding.UTF8
                    };

                    StringBuilder sb = new StringBuilder();
                    using (XmlWriter writer = XmlWriter.Create(sb, settings))
                    {
                        serializer.WriteObject(writer, Instance);
                    }
                    return sb.ToString();
                }
                catch (Exception e)
                {
                    throw new TestFrameworkException("Error in Serializing TestPerformance", e);
                }
            }
        }
    }
}
