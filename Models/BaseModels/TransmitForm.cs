﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreSDK.Models.BaseModels
{
    public class TransmitForm
    {
        public Guid SubmissionId { get; set; }
        public List<Guid> RecordIds { get; set; }
    }
}
