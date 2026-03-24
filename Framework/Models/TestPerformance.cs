using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Framework.Models
{
    [DataContract(Namespace = "")]
    public sealed class TestPerformance
    {
        [Key]
        public int TestPerformanceId { get; set; }

        [Required]
        public Guid TestCaseDetailId { get; set; }

        [DataMember(Order = 0)]
        [Display(Name = "Total Passes")]
        [Required]
        public int TotalPasses { get; set; }

        [DataMember(Order = 1)]
        [Display(Name = "Total Failures")]
        [Required]
        public int TotalFailures { get; set; }

        [DataMember(Order = 2)]
        public ICollection<TestPerformanceUnit> Tests { get; set; }

        internal TestPerformance()
        {
            TotalPasses = 0;
            TotalFailures = 0;
            Tests = new List<TestPerformanceUnit>();
        }
    }
}
