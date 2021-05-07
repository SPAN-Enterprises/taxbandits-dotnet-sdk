using DotNetCoreSDK.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreSDK.Models.Form_941
{
    public class Form941ReturnResponse : FilingStatus 
    {
        public Guid SubmissionId { get; set; }
        public FormRecords Form941Records { get; set; }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string StatusName { get; set; }
        public List<Error> Errors { get; set; }
    }
    public class FormRecords
    {
        public List<FormRecordSuccessStatus> SuccessRecords { get; set; }
        public List<FormRecordErrorStatus> ErrorRecords { get; set; }
    }

    public class FormRecordErrorStatus
    {
        public string Sequence { get; set; }
        public List<Error> Errors { get; set; }
    }
    public class FormRecordSuccessStatus
    {
        public string Sequence { get; set; }
        public Guid? RecordId { get; set; }
        public string RecordStatus { get; set; }
        public string CreatedTs { get; set; }
        public string UpdatedTs { get; set; }
    }

    public class FilingStatus
    {
        public bool IsReturnTransmitted { get; set; }
    }
}
