using DotNetCoreSDK.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreSDK.Models.Form941
{
    public class Form941ListReturnResponse: EntityBase
    {
       
        /// </summary>
        public List<Form941ListData> Form941Records { get; set; }
        /// <summary>
        /// It will show the detailed information about the error.
        /// </summary>
        public List<Error> Errors { get; set; }
        /// <summary>
        /// It will return the status codes like 200, 300, etc., Refer Status Codes.
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// It will return the name of status code.
        /// </summary>
        public string StatusMessage { get; set; }
        /// <summary>
        /// It will return the detailed message of status code.
        /// </summary>
        public string StatusName { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
    }

    public class Form941ListData : Form941ListReturnResponse
    {
        /// <summary>
        /// Submission Id
        /// </summary>
        public Guid SubmissionId { get; set; }

        /// <summary>
        /// Gets or sets the business identifier.
        /// </summary>
        /// <value>
        /// Unique business identifier.
        /// </value>
        public Guid? BusinessId { get; set; }
        /// <summary>
        /// Gets or sets the name of the business.
        /// </summary>
        /// <value>
        /// The business name
        /// </value>
        public string BusinessNm { get; set; }

        /// <summary>
        /// Gets or sets the Employer Identification Number or Social Security Number
        /// </summary>
        /// <value>
        /// Employer Identification Number or Social Security Number
        /// </value>
        public string EINorSSN { get; set; }

        /// <summary>
        /// Get or sets the Type of business entity.
        /// </summary>
        /// <value>
        /// Type of business entity. The value must be ESTE, PART, CORP, EORG, SPRO.
        /// </value>
        public string BusinessType { get; set; }

        /// <summary>
        /// Gets or sets the Record Id
        /// </summary>
        /// <value>
        /// Record Identifier
        /// </value>
        public Guid? RecordId { get; set; }

        /// <summary>
        ///  Gets or sets the form 941 tax year 
        /// </summary>
        /// <value>
        /// Form 941 taxyear 
        /// </value>
        public string TaxYr { get; set; }

        /// <summary>
        /// Gets or sets the form 941 quarter
        /// </summary>
        /// <value>
        /// Form 941 Filing Quarter
        /// </value>
        public string Qtr { get; set; }

        public string IRSPaymentType { get; set; }

        public string EFileStatus { get; set; }
    }
}
