using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Framework.Models
{
    [DataContract(Namespace = "")]
    public sealed class TestPerformanceUnit : TestTimedExecution
    {
        [Key]
        public int TestPerformanceUnitId { get; set; }

        [Required]
        public int TestPerformanceId { get; set; }

        [DataMember(Order = 0)]
        [MaxLength(128)]
        [Display(Name = "Perf Key")]
        [Required]
        public string Key { get; set; }

        [DataMember(Order = 1)]
        [Display(Name = "Success")]
        [Required]
        public bool Success { get; set; }

        [DataMember(Order = 2)]
        [Display(Name = "Target (seconds)")]
        [Required]
        public double Expected { get; set; }
    }
}