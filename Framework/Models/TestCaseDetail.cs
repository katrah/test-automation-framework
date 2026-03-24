using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Framework.Models
{
    [DataContract(Namespace = "")]
    public class TestCaseDetail : TestTimedExecution
    {
        private Guid testCaseDetailId = Guid.Empty;

        [Key]
        [DataMember(Order = 0)]
        public Guid TestCaseDetailId
        {
            get
            {
                if (testCaseDetailId == Guid.Empty)
                {
                    testCaseDetailId = Guid.NewGuid();
                }
                return testCaseDetailId;
            }
            set
            {
                testCaseDetailId = value;
            }
        }

        [DataMember(Order = 1)]
        [Required]
        public Guid TestSuiteDetailId { get; set; }

        [DataMember(Order = 2)]
        [Required]
        public Guid TestRunDetailId { get; set; }

        [DataMember(Order = 3)]
        [MaxLength(128)]
        [Display(Name = "Test Case")]
        [Required]
        public string Name { get; set; }

        [DataMember(Order = 4)]
        [Display(Name = "Partial Passes")]
        [Required]
        public int PartialPasses { get; set; }

        [DataMember(Order = 5)]
        [Display(Name = "Partial Failures")]
        [Required]
        public int PartialFailures { get; set; }

        [DataMember(Order = 6)]
        [Display(Name = "Asserts")]
        [Required]
        public int AssertCount { get; set; }

        [DataMember(Order = 7)]
        [MaxLength(16)]
        [Display(Name = "Result")]
        [Required]
        public string ResultState { get; set; }

        [DataMember(Order = 8)]
        [MaxLength(2048)]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [DataMember(Order = 9)]
        public List<TestLog> TestLogs { get; set; }

        [DataMember(Order = 10)]
        public TestPerformance Performance { get; set; }

        public TestCaseDetail()
        {
            TestLogs = new List<TestLog>();
            PartialPasses = 0;
            PartialFailures = 0;
            AssertCount = 0;
        }
    }
}
