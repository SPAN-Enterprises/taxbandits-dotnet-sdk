using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreSDK.Models.Business_Model
{
    public class BusinessDeleteRequest
    {
        /// <summary>
        /// It will check whether the business has any return. If any return is associated it will not allow to delete the business
        /// </summary>
       
        public Guid BusinessId { get; set; }

        /// <summary>
        /// It is used to delete business based on EIN or SSN (Both formatted and unformatted EIN/SSN)
        /// </summary>
      
        public string EinOrSSN { get; set; }
        /// <summary>
        /// It will not check whether the business having return or not and it will delete the business immediately.
        /// </summary>
       
        public bool IsForceDelete { get; set; }
    }
}
