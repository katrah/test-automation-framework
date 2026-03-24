using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Framework.Models
{
    [DataContract(Namespace = "")]
    public class TestRunDetail : TestTimedExecution
    {
        private Guid testRunDetailId = Guid.Empty;

        [Key]
        [DataMember(Order = 0)]
        public Guid TestRunDetailId
        {
            get
            {
                if (testRunDetailId == Guid.Empty)
                {
                    testRunDetailId = Guid.NewGuid();
                }
                return testRunDetailId;
            }
            set
            {
                testRunDetailId = value;
            }
        }

        [DataMember(Order = 1)]
        [MaxLength(1024)]
        [Required]
        [Display(Name = "Test File")]
        public string ProjectName { get; set; }

        [DataMember(Order = 2)]
        [MaxLength(256)]
        [Required]
        [Display(Name = "Machine Name")]
        public string MachineName { get; set; }

        [DataMember(Order = 3)]
        [Required]
        [Display(Name = "Total Tests")]
        public int TestCount { get; set; }

        [DataMember(Order = 4)]
        [Required]
        [Display(Name = "Passes")]
        public int PassCount { get; set; }

        [DataMember(Order = 5)]
        [Required]
        [Display(Name = "Failures")]
        public int FailCount { get; set; }

        [DataMember(Order = 6)]
        [Required]
        [Display(Name = "Pass Rate")]
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
        public ICollection<TestSuiteDetail> TestSuites { get; set; }

        public TestRunDetail()
        {
            TestCount = 0;
            PassCount = 0;
            FailCount = 0;
            TestSuites = new List<TestSuiteDetail>();
        }
    }
}
