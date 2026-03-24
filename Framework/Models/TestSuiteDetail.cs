using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Framework.Models
{
    [DataContract(Namespace = "")]
    public sealed class TestSuiteDetail : TestTimedExecution
    {
        private Guid testSuiteDetailId = Guid.Empty;

        [Key]
        [DataMember(Order = 0)]
        public Guid TestSuiteDetailId
        {
            get
            {
                if (testSuiteDetailId == Guid.Empty)
                {
                    testSuiteDetailId = Guid.NewGuid();
                }
                return testSuiteDetailId;
            }
            set
            {
                testSuiteDetailId = value;
            }
        }

        [DataMember(Order = 1)]
        [Required]
        public Guid TestRunDetailId { get; set; }

        [DataMember(Order = 2)]
        [MaxLength(128)]
        [Display(Name = "Test Suite")]
        [Required]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        [Display(Name = "Tests")]
        [Required]
        public int TestCount { get; set; }

        [DataMember(Order = 4)]
        [Display(Name = "Passes")]
        [Required]
        public int PassCount { get; set; }

        [DataMember(Order = 5)]
        [Display(Name = "Failures")]
        [Required]
        public int FailCount { get; set; }

        [DataMember(Order = 6)]
        [Display(Name = "Pass Rate")]
        [Required]
        public double PassRate
        {
            get
            {
                return TestCount == 0 ? 0.00f : Math.Round((PassCount * 100.00f) / (TestCount), 2);
            }

            // for serialization purposes only (value is derived)
            private set { }
        }

        [DataMember(Order = 7)]
        public List<TestCaseDetail> TestCases { get; set; }

        [DataMember(Order = 8)]
        public List<TestLog> TestLogs { get; set; }

        public TestSuiteDetail()
        {
            TestCount = 0;
            PassCount = 0;
            FailCount = 0;
            TestCases = new List<TestCaseDetail>();
            TestLogs = new List<TestLog>();
        }
    }
}
