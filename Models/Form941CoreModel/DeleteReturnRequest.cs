using System;
using System.Collections.Generic;
namespace DotNetCoreSDK.Models.Form941CoreModel
{
    public class DeleteReturnRequest 
    {
        public Guid SubmissionId { get; set; }
        public List<Guid> RecordIds { get; set; }
    }
}