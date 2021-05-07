using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreSDK.Models.Form941CoreModel
{
    public class SignatureDetails
    {
        /// <summary>
        /// Gets or sets the Signature Type
        /// </summary>
        public string SignatureType { get; set; }

        /// <summary>
        /// Online Signature PIN
        /// </summary>
        public OnlineSignaturePIN OnlineSignaturePIN { get; set; }

        /// <summary>
        /// Reporting Agent PIN
        /// </summary>
        public ReportingAgentPIN ReportingAgentPIN { get; set; }
    }

    public class OnlineSignaturePIN
    {
        /// <summary>
        /// Online Signature PIN
        /// </summary>
        public string PIN { get; set; }
    }

    public class ReportingAgentPIN
    {
        /// <summary>
        /// Reporting Agent PIN
        /// </summary>
        public string PIN { get; set; }
    }

}
