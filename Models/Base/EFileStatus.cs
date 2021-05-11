using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreSDK.Models.Base
{
    public class EFileStatus : EntityBase
    {
        public Guid SubmissionId { get; set; }
        public Guid RecordId { get; set; }
        public bool IsReturnTransmitted { get; set; }
    }
}
