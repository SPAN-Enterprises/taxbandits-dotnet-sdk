using DotNetCoreSDK.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreSDK.Models.Business_Model
{
    public class BusinessGetResponse :  BaseResponseStatus
    {

        /// <summary>
        /// Business Details
        /// </summary>

        public Business Business { get; set; }

        /// <summary>
        /// It will show the detailed information about the error.
        /// </summary>     
        public List<Error> Errors { get; set; }
    }
}
