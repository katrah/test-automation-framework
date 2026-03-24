using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Framework.Models
{
    [DataContract(Namespace = "")]
    public abstract class TestTimedExecution
    {
        [DataMember(Order = 0)]
        [DataType(DataType.DateTime)]
        [Required]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [DataMember(Order = 1)]
        [DataType(DataType.DateTime)]
        [Required]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        [DataMember(Order = 2)]
        [Required]
        [Display(Name = "Duration (seconds)")]
        public double ElapsedInSeconds
        {
            get { return EndTime.Subtract(StartTime).TotalSeconds; }
            private set { } // for serialization purposes only (value is derived)
        }

        [DataMember(Order = 3)]
        [Display(Name = "Status")]
        [Required]
        public TestExecutionStatus Status { get; set; }
    }

    [Serializable]
    public enum TestExecutionStatus
    {
        Started = 0,
        Finished = 1
    }
}
