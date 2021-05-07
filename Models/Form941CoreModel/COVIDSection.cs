using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreSDK.Models.Form941CoreModel
{
    public class COVIDSection
    {
        [JsonProperty(PropertyName = "QualSickLeaveWagesAmt_Col1")]
        /// <summary>
        /// Gets or sets the Line5a1 Initial Amount
        /// </summary>
        /// <value>
        /// Line5b Initial Amount
        /// </value>
        public decimal Line5a1InitialAmount { get; set; }

        [JsonProperty(PropertyName = "TaxOnQualSickLeaveWagesAmt_Col2")] 
        public decimal Line5a1 { get; set; }

        [JsonProperty(PropertyName = "QualFamilyLeaveWagesAmt_Col1")]
        /// <summary>
        /// Gets or sets the Line5a2 Initial Amount
        /// </summary>
        /// <value>
        /// Line5a2 Initial Amount
        /// </value>
        public decimal Line5a2InitialAmount { get; set; }

        [JsonProperty(PropertyName = "TaxOnQualFamilyLeaveWagesAmt_Col2")]
        public decimal Line5a2 { get; set; }

        [JsonProperty(PropertyName = "NonRfdCrQualSickAndFamilyWagesAmt")]
        /// <summary>
        /// Gets or sets the line 11b amount
        /// </summary>
        /// <value>
        /// Line 11b amount
        /// </value>
        public decimal Line11b { get; set; }



        [JsonProperty(PropertyName = "NonRfdEeRetentionCrAmt")]
        /// <summary>
        /// Gets or sets the line 11c amount
        /// </summary>
        /// <value>
        /// Line 11c amount
        /// </value>
        public decimal Line11c { get; set; }



        [JsonProperty(PropertyName = "TotlNonRfdCrAmt")]
        /// <summary>
        /// Gets or sets the line 11d amount
        /// </summary>
        /// <value>
        /// Line 11d amount
        /// </value>

        public decimal Line11d { get; set; }
   

        [JsonProperty(PropertyName = "RfdCrQualSickAndFamilyWagesAmt")]
        /// <summary>
        /// Gets or sets Line13c amount
        /// </summary>
        /// <value>
        /// Line13c amount mount
        /// </value>
        public decimal Line13c { get; set; }


        [JsonProperty(PropertyName = "RfdEeRetentionCrAmt")]
        /// <summary>
        /// Gets or sets Line13d amount
        /// </summary>
        /// <value>
        /// Line13d amount mount
        /// </value>
        public decimal Line13d { get; set; }


        [JsonProperty(PropertyName = "TotDepositDeferralAndRfdCrAmt")]
        /// <summary>
        /// Gets or sets Line13e amount
        /// </summary>
        /// <value>
        /// Line13e amount mount
        /// </value>
        public decimal Line13e { get; set; }

        [JsonProperty(PropertyName = "TotAdvRcvd7200")]
        /// <summary>
        /// Gets or sets Line13e amount
        /// </summary>
        /// <value>
        /// Line13e amount mount
        /// </value>
        public decimal Line13f { get; set; }

        [JsonProperty(PropertyName = "TotDepositsDeferralsAndRfdCrLessAdvAmt")]
        /// <summary>
        /// Gets or sets Line13e amount
        /// </summary>
        /// <value>
        /// Line13e amount mount
        /// </value>
        public decimal Line13g { get; set; }


        [JsonProperty(PropertyName = "QualHealthPlanExpToSickLeaveWagesAmt")]
        /// <summary>
        /// Gets or sets Line13e amount
        /// </summary>
        /// <value>
        /// Line13e amount mount
        /// </value>
        public decimal TaxOnQualHealthPlanExpToSickLeaveWagesAmt { get; set; }


        [JsonProperty(PropertyName = "QualHealthPlanExpToFamilyLeaveWagesAmt")]
        /// <summary>
        /// Gets or sets Line13e amount
        /// </summary>
        /// <value>
        /// Line13e amount mount
        /// </value>
        public decimal TaxOnQualHealthPlanExpToFamilyLeaveWagesAmt { get; set; }

        [JsonProperty(PropertyName = "QualWagesForEeRetentionCrAmt")]
        /// <summary>
        /// Gets or sets Line13e amount
        /// </summary>
        /// <value>
        /// Line13e amount mount
        /// </value>
        public decimal TaxOnQualWagesForEeRetentionCrAmt { get; set; }


        [JsonProperty(PropertyName = "QualHealthPlanExpToWagesAmt")]
        /// <summary>
        /// Gets or sets Line13e amount
        /// </summary>
        /// <value>
        /// Line13e amount mount
        /// </value>
        public decimal TaxOnQualHealthPlanExpToWagesAmt { get; set; }

        [JsonProperty(PropertyName = "Form5884CCr")]
        /// <summary>
        /// Gets or sets Line13e amount
        /// </summary>
        /// <value>
        /// Line13e amount mount
        /// </value>
        public decimal TaxOnForm5884CCr { get; set; }


    }
}
