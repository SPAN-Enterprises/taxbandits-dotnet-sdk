using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DotNetCoreSDK.Models.Business_Model
{
    public class BusinessListResponse
    {

        /// <summary>
        /// Business Details of all the business
        /// </summary>
     
        public List<Business> Businesses { get; set; }
        /// <summary>
        /// Page number
        /// </summary>

        public int Page { get; set; }
        /// <summary>
        /// Total number of Businesses
        /// </summary>
       
        public int TotalRecords { get; set; }
        /// <summary>
        /// Total number of pages
        /// </summary>
      
        public int TotalPages { get; set; }
        /// <summary>
        /// Range: 10, 25, 50, 100
        /// </summary>
        public int PageSize { get; set; }
    }
}
