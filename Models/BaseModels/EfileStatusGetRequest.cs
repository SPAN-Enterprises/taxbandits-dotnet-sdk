using System;
using System.Collections.Generic;

namespace DotNetCoreSDK.Models.BaseModels
{
    public class EfileStatusGetRequest
    {
        public Guid SubmissionId { get; set; }
        public List<Guid> RecordIds { get; set; }
    }
}