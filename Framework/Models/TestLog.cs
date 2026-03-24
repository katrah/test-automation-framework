using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Framework.Models
{
    [DataContract(Namespace = "")]
    public class TestLog
    {
        [Key]
        public int TestLogId { get; set; }

        public Guid? TestCaseDetailId { get; set; }

        public Guid? TestSuiteDetailId { get; set; }

        [DataMember(Order = 0)]
        [DataType(DataType.DateTime)]
        [Display(Name = "Time Stamp")]
        [Required]
        public DateTime TimeStamp { get; set; }

        [DataMember(Order = 1)]
        [MaxLength(4096)]
        [Display(Name = "Log")]
        [Required]
        public string LogText { get; set; }

        [DataMember(Order = 2)]
        [Display(Name = "Type")]
        [Required]
        public TestLogEntryType LogType { get; set; }

        [DataMember(Order = 3)]
        [Display(Name = "Caller")]
        [Required]
        public TestLogEntryCaller LogCaller { get; set; }
    }

    [Serializable]
    public enum TestLogEntryType
    {
        Out = 0,
        Error = 1,
        Trace = 2,
        Warn = 3,
        Pass = 4,
        Fail = 5,
        PerfItem = 6,
        PerfResult = 7
    }

    [Serializable]
    public enum TestLogEntryCaller
    {
        Test = 0,
        Setup = 1,
        Teardown = 2
    }
}
