using DotNetCoreSDK.Models.Base;
using DotNetCoreSDK.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreSDK.Models.Business
{
    public class BusinessCreateReturnResponse 
    {

        public int StatusCode { get; set; }
        /// <summary>
        /// It will return the name of status code.
        /// </summary>

        public string StatusName { get; set; }
        /// <summary>
        /// It will return the detailed message of status code.
        /// </summary> [DataMember]
        public string StatusMessage { get; set; }
        public Guid BusinessId { get; set; }
        public bool IsEIN { get; set; }
        public string EINorSSN { get; set; }
        public string BusinessNm { get; set; }
        public List<Error> Errors { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
    }
}
