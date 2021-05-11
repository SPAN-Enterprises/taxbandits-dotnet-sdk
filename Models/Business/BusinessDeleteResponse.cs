using DotNetCoreSDK.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreSDK.Models.Business
{
    public class BusinessDeleteResponse: BaseResponseStatus
    {
        /// <summary>
        /// It will check whether the business has any return. If any return is associated it will not allow to delete the business
        /// </summary>

        public Guid BusinessId { get; set; }

        public List<Error> Errors { get; set; }
    }
}
