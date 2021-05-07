using DotNetCoreSDK.Models.Utilities;
using DotNetCoreSDK.Models.BaseModels;
using DotNetCoreSDK.Models.Form_941;
using DotNetCoreSDK.Models.Form941CoreModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Nancy.Json;
using System.Linq;
using DotNetCoreSDK.Models.Business_Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotNetCoreSDK.Controllers
{
    public class Form941Controller : Controller
    {
        #region Pre Populate
        /// <summary>
        /// Pre Populate date
        /// </summary>
        /// <param name="form941"></param>
        private static void PrePopulate(Form941Data form941)
        {
            //Mapping Form 941 
            form941.Sequence = "Record1";
            form941.RecordId = null;
            //Mapping Return Header
            form941.ReturnHeader = new Form941ReturnHeader
            {
                ReturnType = null,
                Qtr = "Q1",
                TaxYr = "2018",
                Business = new Business
                {
                    BusinessId = null,
                    BusinessNm = "Test Business",
                    EINorSSN = "003333330",
                    IsEIN = true,
                    BusinessType = "ESTE",
                    ContactNm = "John Doe",
                    Email = "employer@company.com",
                    Fax = "1234567890",
                    TradeNm = "Kodak",
                    IsBusinessTerminated = false,
                    Phone = "1234566890",
                    PhoneExtn = "12345",
                    IsForeign = false,
                    USAddress = new USAddress
                    {
                        Address1 = "Address Line 1",
                        City = "Rockhill",
                        State = "SC",
                        ZipCd = "29730"
                    },
                    SigningAuthority = new DotNetCoreSDK.Models.BaseModels.SigningAuthority
                    {
                        BusinessMemberType = "ADMINISTRATOR",
                        Phone = "1234564390",
                        Name = "John"
                    },
                    KindOfEmployer = null,
                    KindOfPayer = null,
                    ForeignAddress = null,
                },
                BusinessStatusDetails = new BusinessStatusDetails
                {
                    IsBusinessClosed = false,
                    IsBusinessTransferred = false,
                    IsSeasonalEmployer = false,
                    BusinessClosedDetails = null,
                    BusinessTransferredDetails = null
                },
                IsThirdPartyDesignee = true,
                ThirdPartyDesignee = new ThirdPartyDesignee
                {
                    Name = "John Doe",
                    Phone = "1234567890",
                    PIN = "12345"
                },
                SignatureDetails = new SignatureDetails
                {
                    SignatureType = "REPORTING_AGENT",
                    ReportingAgentPIN = new ReportingAgentPIN { PIN = "12345" }
                }


            };

            //Mapping Return Data
            form941.ReturnData = new Form941ReturnData
            {
                DepositScheduleType = new DepositScheduleType
                {
                    DepositorType = DepositorType.MINTAXLIABILITY.ToString(),
                    TaxLiabilityTotalAmt = 200M
                },
                Form941 = new Form941Details
                {
                    EmployeeCnt = 3,
                    WagesAmt = 5750M,
                    FedIncomeTaxWHAmt = 130M,
                    WagesNotSubjToSSMedcrTaxInd = true,
                    Line5aInitialAmt = 564.51M,
                    Line5bInitialAmt = 0M,
                    Line5cInitialAmt = 0M,
                    Line5dInitialAmt = 0M,
                    Line5a = 70M,
                    Line5b = 0M,
                    Line5c = 0M,
                    Line5d = 0M,
                    Line5e = 70M,
                    TaxOnUnreportedTips3121qAmt = 0M,
                    CurrentQtrFractionsCentsAmt = 0M,
                    CurrentQuarterSickPaymentAmt = 0M,
                    CurrQtrTipGrpTermLifeInsAdjAmt = 0M,
                    Line12 = 200M,
                    Line11a = 0M,
                    Line14 = 0M,
                    Line15 = 0M,
                    Line6 = 200M,
                    Line10 = 200M,
                    TotTaxDepositAmt = 200M
                }
            };
        }
        #endregion

        #region Form 941 View Get Method

        public ActionResult Form941Return(bool? id)
        {
            Form941Data form941 = new Form941Data();
            bool _prePopulate = id ?? false;
            if (_prePopulate)
            {
                PrePopulate(form941);
            }
            return View(form941);
        }
        #endregion

        #region Form 941 Create Return Response Status
        [HttpPost]
        public ActionResult CreateForm941(Form941Data form941)
        {
            var responseJson = string.Empty;
            form941.Sequence = Constants.Sequence;
            form941.RecordId = null;
            form941.ReturnHeader.ReturnType = Constants.ReturnType;
            form941.ReturnHeader.Business.IsEIN = true;
            form941.ReturnHeader.Business.IsForeign = false;
            form941.ReturnData.Form941.IsPayrollTaxCredit = false;
            if (form941?.ReturnHeader?.ThirdPartyDesignee != null && (!string.IsNullOrEmpty(form941.ReturnHeader.ThirdPartyDesignee.Name) || !string.IsNullOrEmpty(form941.ReturnHeader.ThirdPartyDesignee.Phone) || !string.IsNullOrEmpty(form941.ReturnHeader.ThirdPartyDesignee.PIN)))
            {
                form941.ReturnHeader.IsThirdPartyDesignee = true;
            }
            if (form941?.ReturnHeader?.BusinessStatusDetails != null)
            {
                if (form941.ReturnHeader.BusinessStatusDetails.IsBusinessClosed == false)
                {
                    form941.ReturnHeader.BusinessStatusDetails.BusinessClosedDetails = new BusinessClosedDetails();
                }
                if (form941.ReturnHeader.BusinessStatusDetails.IsBusinessTransferred == false)
                {
                    form941.ReturnHeader.BusinessStatusDetails.BusinessTransferredDetails = new BusinessTransferredDetails();
                }
            }

            var form941Response = new Form941CreateReturnResponse();
            var form941ReturnList = new Form941CreateReturnRequest { Form941Records = new List<Form941Data>() };
            form941ReturnList.Form941Records.Add(form941);
            string ApiUrl = Utility.GetAppSettings(Constants.PublicAPIUrlWithJWT);
            // Generate JSON for Form 941
            var requestJson = JsonConvert.SerializeObject(form941ReturnList, Formatting.Indented);
            //Get Access token from OAuth API response 
            GetAccessToken AccessToken = new GetAccessToken(HttpContext);
            string GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();
            if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL to Create Form 941 Return
                    string requestUri = "Form941/Create";
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response
                    var apiResponse = apiClient.PostAsJsonAsync(requestUri, form941ReturnList).Result;
                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var createResponse = apiResponse.Content.ReadAsAsync<Form941CreateReturnResponse>().Result;
                        if (createResponse != null)
                        {
                            responseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to Form941CreateReturnResponse object
                            form941Response = new JavaScriptSerializer().Deserialize<Form941CreateReturnResponse>(responseJson);                         
                        }
                    }
                    else
                    {
                        var createResponse = apiResponse.Content.ReadAsAsync<Object>().Result;
                        responseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);

                        //Deserializing JSON (Error Response) to Form941CreateReturnResponse object
                        form941Response = new JavaScriptSerializer().Deserialize<Form941CreateReturnResponse>(responseJson);
                    }
                }
            }
            return PartialView("APIResponseStatus", form941Response);

        }
        #endregion

        #region Get Efile Status
        /// <summary>
        /// Function returns the Efile status of Form 941
        /// </summary>
        /// <param name="submissionId">SubmissionId is passed to get the efile status</param>
        /// <returns>Form941StatusResponse</returns>
        public ActionResult _GetEfileStatusResponse(Guid submissionId, Guid RecordId)
        {
            Form941StatusResponse efileStatusResponse = new Form941StatusResponse();
            if (submissionId != null && submissionId != Guid.Empty)
            {
                var efileRequest = new EfileStatusGetRequest { SubmissionId = submissionId };
                var GetEfileStatusForm941ResponseJSON = string.Empty;
                // Request JSON
                var requestJson = JsonConvert.SerializeObject(efileRequest, Formatting.Indented);
                if (submissionId != null && submissionId != Guid.Empty)
                {
                    //Get URLs from App.Config
                    string apiUrl = Utility.GetAppSettings(Constants.PublicAPIUrlWithJWT);
                    //Get Access token from GetAccessToken Class
                    GetAccessToken AccessToken = new GetAccessToken(HttpContext);
                    //Get Access token from OAuth API response
                    var GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();
                    //Access token is valid for one hour. After that call OAuth API again & get new Access token.

                    if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
                    {                      
                        using (var apiClient = new HttpClient())
                        {
                            //GET
                            var requestUri = "Form941/Status?SubmissionId=" + submissionId;
                            apiClient.BaseAddress = new Uri(apiUrl);
                            //Construct HTTP headers
                            //If Access token got expired, call OAuth API again & get new Access token.
                            OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                            //Read Response
                            var _response = apiClient.GetAsync(requestUri).Result;
                            if (_response != null && _response.IsSuccessStatusCode)
                            {

                                var createResponse = _response.Content.ReadAsAsync<Form941StatusResponse>().Result;
                                if (createResponse != null)
                                {
                                    GetEfileStatusForm941ResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                                    efileStatusResponse = new JavaScriptSerializer().Deserialize<Form941StatusResponse>(GetEfileStatusForm941ResponseJSON);
                                }
                            }
                            else
                            {
                                var createResponse = _response.Content.ReadAsAsync<Object>().Result;
                                GetEfileStatusForm941ResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                                efileStatusResponse = new JavaScriptSerializer().Deserialize<Form941StatusResponse>(GetEfileStatusForm941ResponseJSON);
                            }
                        }
                    }

                }
            }
            return PartialView(efileStatusResponse);
        }
        #endregion

        #region Transmit Return
        /// <summary>
        /// Function transmit the Form 941 Return to Efile
        /// </summary>
        /// <param name="submissionId">SubmissionId passed to transmit the 941 return</param>
        /// <returns>TransmitFormW2Response</returns>
        public ActionResult _TransmitReturn(Guid submissionId)
        {
            TransmitForm transmitForm941 = new TransmitForm();
            var transmitForm941Response = new TransmitForm941Response();
            var transmitForm941ResponseJSON = string.Empty;
            if (submissionId != null && submissionId != Guid.Empty)
            {                               
                transmitForm941.SubmissionId = submissionId;
                // Generate JSON for TransmitForm 941
                var requestJson = JsonConvert.SerializeObject(transmitForm941, Formatting.Indented);
                if (transmitForm941 != null)
                {
                    //Get URLs from App.Config                       
                    string ApiUrl = Utility.GetAppSettings(Constants.PublicAPIUrlWithJWT);
                    //Get Access token from GetAccessToken Class
                    GetAccessToken AccessToken = new GetAccessToken(HttpContext);
                    var GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();
                    if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
                    {                       
                        using (var apiClient = new HttpClient())
                        {
                            //API URL to Transmit Form 941 Return
                            string requestUri = "Form941/Transmit";

                            apiClient.BaseAddress = new Uri(ApiUrl);
                            //Construct HTTP headers
                            //If Access token got expired, call OAuth API again & get new Access token.
                            OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);

                            //Get Response
                            var _response = apiClient.PostAsJsonAsync(requestUri, transmitForm941).Result;
                            if (_response != null && _response.IsSuccessStatusCode)
                            {
                                //Read Response
                                var createResponse = _response.Content.ReadAsAsync<TransmitForm941Response>().Result;
                                if (createResponse != null)
                                {
                                    transmitForm941ResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                                    transmitForm941Response = new JavaScriptSerializer().Deserialize<TransmitForm941Response>(transmitForm941ResponseJSON);
                                    if (transmitForm941Response.SubmissionId != null && transmitForm941Response.SubmissionId != Guid.Empty && transmitForm941Response.StatusCode == (int)StatusCodeList.Success)
                                    {
                                        //Updating Filing Status (Transmitted) for a specific SubmissionId in Session 
                                        //bool Isupdated = APISession.UpdateForm941ReturnFilingStatus(transmitForm941Response.SubmissionId);
                                        //if (Isupdated)
                                        //{
                                        //    transmitForm941Response.IsReturnTransmitted = true;
                                        //}

                                    }
                                }
                            }
                            else
                            {
                                var createResponse = _response.Content.ReadAsAsync<Object>().Result;
                                transmitForm941ResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                                transmitForm941Response = new JavaScriptSerializer().Deserialize<TransmitForm941Response>(transmitForm941ResponseJSON);
                            }
                        }
                    }



                }
            }
            return PartialView(transmitForm941Response);
        }
        #endregion

        #region Get Form 941
        /// <summary>
        /// Function get the Form 941 Return to Efile
        /// </summary>
        /// <param name="submissionId">SubmissionId passed to get the 941 return</param>
        /// <returns>Form941GetReturnResponse</returns>
        public ActionResult GetForm941(Guid submissionId)
        {
            var getReturnResponse = new Form941GetReturnResponse();
            var getReturnResponseJSON = string.Empty;
            if (submissionId != null && submissionId != Guid.Empty)
            {
                //Get URLs from App.Config
                string ApiUrl = Utility.GetAppSettings(Constants.PublicAPIUrlWithJWT);

                //Get Access token from GetAccessToken Class
                GetAccessToken AccessToken = new GetAccessToken(HttpContext);
                //Get Access token from OAuth API response
                var GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();
                if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
                {                   
                    using (var apiClient = new HttpClient())
                    {
                        //API URL to Get Form 941 Return
                        string requestUri = "Form941/Get?submissionId=" + submissionId;

                        apiClient.BaseAddress = new Uri(ApiUrl);
                        //Construct HTTP headers
                        OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                        //Get Response
                        var _response = apiClient.GetAsync(requestUri).Result;
                        if (_response != null && _response.IsSuccessStatusCode)
                        {
                            //Read Response
                            var createResponse = _response.Content.ReadAsAsync<Form941GetReturnResponse>().Result;
                            if (createResponse != null)
                            {
                                getReturnResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                                getReturnResponse = new JavaScriptSerializer().Deserialize<Form941GetReturnResponse>(getReturnResponseJSON);
                                if (getReturnResponse != null && getReturnResponse.StatusCode == (int)StatusCodeList.Success)
                                {
                                    ViewData["GetResponseJSON"] = getReturnResponseJSON;
                                    return PartialView();
                                }
                            }
                        }
                        else
                        {
                            var createResponse = _response.Content.ReadAsAsync<Object>().Result;
                            getReturnResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                            getReturnResponse = new JavaScriptSerializer().Deserialize<Form941GetReturnResponse>(getReturnResponseJSON);
                        }

                    }
                }



            }
            return PartialView(getReturnResponse);
        }
        #endregion

        #region Delete Return
        /// <summary>
        /// Function delete the Form 941 Return to Efile
        /// </summary>
        /// <param name="submissionId">SubmissionId passed to delete the 941 return</param>
        /// <returns>DeleteReturnResponse</returns>
        public ActionResult Delete(Guid submissionId)
        {
            var deleteReturnRequest = new DeleteReturnRequest();
            var deleteReturnResponse = new DeleteReturnResponse();
            var deleteReturnResponseJSON = string.Empty;
            if (submissionId != null && submissionId != Guid.Empty)
            {
                deleteReturnRequest.SubmissionId = submissionId;
                if (deleteReturnRequest.SubmissionId != null)
                {
                    //Get URLs from App.Config                  
                    string ApiUrl = Utility.GetAppSettings(Constants.PublicAPIUrlWithJWT);
                    GetAccessToken AccessToken = new GetAccessToken(HttpContext);
                    var GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();
                    if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
                    {
                        
                        using (var apiClient = new HttpClient())
                        {
                            //API URL to Transmit Form 941 Return
                            string requestUri = "Form941/Delete?submissionId=" + submissionId;

                            apiClient.BaseAddress = new Uri(ApiUrl);
                            //Construct HTTP headers
                            //If Access token got expired, call OAuth API again & get new Access token.
                            OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);

                            //Get Response
                            var _response = apiClient.DeleteAsync(requestUri).Result;
                            if (_response != null && _response.IsSuccessStatusCode)
                            {
                                //Read Response
                                var createResponse = _response.Content.ReadAsAsync<DeleteReturnResponse>().Result;
                                if (createResponse != null)
                                {
                                    deleteReturnResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                                    deleteReturnResponse = new JavaScriptSerializer().Deserialize<DeleteReturnResponse>(deleteReturnResponseJSON);
                                }
                            }
                            else
                            {
                                var createResponse = _response.Content.ReadAsAsync<Object>().Result;
                                deleteReturnResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                                deleteReturnResponse = new JavaScriptSerializer().Deserialize<DeleteReturnResponse>(deleteReturnResponseJSON);
                            }

                        }
                    }

                }
            }
            return PartialView(deleteReturnResponse);
        }
        #endregion

        #region Get List Form 941
        /// <summary>
        /// Function get the Form 941 Return to Efile
        /// </summary>
        /// <param name="submissionId">SubmissionId passed to get the 941 return</param>
        /// <returns>Form941GetReturnResponse</returns>
        public ActionResult GetListForm941()
        {
            var ListReturnRepsone = new Form941ListReturnResponse();
            var Form941ListDetails = new List<Form941ListData>();
            var getReturnResponseJSON = string.Empty;
            //Get URLs from App.Config
            string ApiUrl = Utility.GetAppSettings(Constants.PublicAPIUrlWithJWT);
            //Get Access token from GetAccessToken Class
            GetAccessToken AccessToken = new GetAccessToken(HttpContext);
            //Get Access token from OAuth API response
            var GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();
            if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
            {                
                using (var apiClient = new HttpClient())
                {
                    //API URL to Get Form 941 Return
                    string requestUri = "Form941/List?Page=0&PageSize=10";

                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Get Response
                    var _response = apiClient.GetAsync(requestUri).Result;
                    if (_response != null && _response.IsSuccessStatusCode)
                    {
                        //Read Response
                        var createResponse = _response.Content.ReadAsAsync<Form941ListReturnResponse>().Result;
                        if (createResponse != null)
                        {
                            getReturnResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                            ListReturnRepsone = new JavaScriptSerializer().Deserialize<Form941ListReturnResponse>(getReturnResponseJSON);
                            Form941ListDetails = ListReturnRepsone.Form941Records;
                        }
                    }
                    else
                    {
                        var createResponse = _response.Content.ReadAsAsync<Object>().Result;
                        getReturnResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                        ListReturnRepsone = new JavaScriptSerializer().Deserialize<Form941ListReturnResponse>(getReturnResponseJSON);
                    }

                }
            }

            return PartialView("../Efile/_GetForm941EFileStatus", Form941ListDetails);
        }
        #endregion

        #region  Business Members List
        [HttpGet]
        public ActionResult BusinessMembersList(string id)
        {
            var businessType = id;
            if (!string.IsNullOrWhiteSpace(businessType))
            {
                var businessMembersList = new List<SelectListItem>();
                if (businessType == BusinessType.ESTE.ToString())
                {
                    businessMembersList = Enum.GetValues(typeof(EstateBusinessMembers)).Cast<EstateBusinessMembers>().Select(x => new SelectListItem { Text = Utility.GetEnumDisplayName(x), Value = (x).ToString() }).ToList();
                }
                else if (businessType == BusinessType.PART.ToString())
                {
                    businessMembersList = Enum.GetValues(typeof(PartnershipBusinessMembers)).Cast<PartnershipBusinessMembers>().Select(x => new SelectListItem { Text = Utility.GetEnumDisplayName(x), Value = (x).ToString() }).ToList();
                }
                else if (businessType == BusinessType.CORP.ToString())
                {
                    businessMembersList = Enum.GetValues(typeof(CorporationBusinessMembers)).Cast<CorporationBusinessMembers>().Select(x => new SelectListItem { Text = Utility.GetEnumDisplayName(x), Value = (x).ToString() }).ToList();
                }
                else if (businessType == BusinessType.EORG.ToString())
                {
                    businessMembersList = Enum.GetValues(typeof(EstateBusinessMembers)).Cast<EstateBusinessMembers>().Select(x => new SelectListItem { Text = Utility.GetEnumDisplayName(x), Value = (x).ToString() }).ToList();
                }
                else if (businessType == BusinessType.SPRO.ToString())
                {
                    businessMembersList = Enum.GetValues(typeof(SoleProprietorshipBusinessMembers)).Cast<SoleProprietorshipBusinessMembers>().Select(x => new SelectListItem { Text = Utility.GetEnumDisplayName(x), Value = (x).ToString() }).ToList();
                }
                return new JsonResult(new { data = businessMembersList });
            }
            return new JsonResult(false) ;
        }
        #endregion

    }
}
