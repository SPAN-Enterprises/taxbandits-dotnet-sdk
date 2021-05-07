using DotNetCoreSDK.Models.Form_941;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreSDK.Models.Form_941
{
    public class Form941
    {
        public Guid SubmissionId { get; set; }
        /// <summary>
        /// Form 941 Records
        /// </summary>
        public Form941Data Form941Records { get; set; }
    }
}
