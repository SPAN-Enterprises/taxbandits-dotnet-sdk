using DotNetCoreSDK.Models.Base;
using DotNetCoreSDK.Models.Business;
using DotNetCoreSDK.Models.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DotNetCoreSDK.Controllers
{
    public class BusinessController : Controller
    {
        public IActionResult Business()
        {
            return View();
        }

        #region Business Create Return Response Status

        [HttpPost]
        public ActionResult CreateBusiness(BusinessCreateRequest FormBusiness)
        {
            var responseJson = string.Empty;
            var BusinessResponse = new BusinessCreateReturnResponse();

            string ApiUrl = Utility.GetAppSettings(Constants.PublicAPIUrlWithJWT);
            // Generate JSON for Business
            var requestJson = JsonConvert.SerializeObject(FormBusiness, Formatting.Indented);
            //Get Access token from OAuth API response 
            GetAccessToken AccessToken = new GetAccessToken(HttpContext);
            string GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();
            if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL to Business Create
                    string requestUri = "Business/Create";
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response
                    var apiResponse = apiClient.PostAsJsonAsync(requestUri, FormBusiness).Result;
                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var createResponse = apiResponse.Content.ReadAsAsync<BusinessCreateReturnResponse>().Result;
                        if (createResponse != null)
                        {
                            responseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to BusinessCreateReturnResponse object
                            BusinessResponse = new JavaScriptSerializer().Deserialize<BusinessCreateReturnResponse>(responseJson);
                        }
                    }
                    else
                    {
                        var createResponse = apiResponse.Content.ReadAsAsync<Object>().Result;
                        responseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);

                        //Deserializing JSON (Error Response) to BusinessCreateReturnResponse object
                        BusinessResponse = new JavaScriptSerializer().Deserialize<BusinessCreateReturnResponse>(responseJson);
                    }
                }
            }

            return PartialView("_BusinessReturnResponse", BusinessResponse);

        }
        #endregion

        #region Get List Business 
        /// <summary>
        /// Function get the Business Return
        /// </summary>     
        /// <returns>BusinessReturnList</returns>
        public ActionResult GetBusinessList()
        {
            var ListReturnRepsone = new BusinessListResponse();
            var Business = new List<Business>();
            var getReturnResponseJSON = string.Empty;
            DateTime aDate = DateTime.Now;
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
                    //API URL to Get Business List Return
                    string requestUri = "Business/List?Page=0&PageSize=10";

                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Get Response
                    var _response = apiClient.GetAsync(requestUri).Result;
                    if (_response != null && _response.IsSuccessStatusCode)
                    {
                        //Read Response
                        var createResponse = _response.Content.ReadAsAsync<BusinessListResponse>().Result;
                        if (createResponse != null)
                        {
                            getReturnResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                            ListReturnRepsone = new JavaScriptSerializer().Deserialize<BusinessListResponse>(getReturnResponseJSON);
                            Business = ListReturnRepsone.Businesses;
                        }
                    }
                    else
                    {
                        var createResponse = _response.Content.ReadAsAsync<Object>().Result;
                        getReturnResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                        ListReturnRepsone = new JavaScriptSerializer().Deserialize<BusinessListResponse>(getReturnResponseJSON);
                    }

                }

            }
            return PartialView("_GetBusinessList", Business);
        }
        #endregion

        #region Delete Return
        /// <summary>
        /// Function delete the Business return using  BusinessId
        /// </summary>
        /// <param name="BusinessId">BusinessId passed to delete the Business return</param>
        /// <returns>DeleteReturnResponse</returns>
        public ActionResult Delete(Guid BusinessId)
        {
            var BusinessDeleteReturnRequest = new BusinessDeleteRequest();
            var BusinessDeleteReturnResponse = new BusinessDeleteResponse();

            var deleteReturnResponseJSON = string.Empty;
            if (BusinessId != null && BusinessId != Guid.Empty)
            {
                if (BusinessId != null)
                {
                    //Get URLs from App.Config                  
                    string ApiUrl = Utility.GetAppSettings(Constants.PublicAPIUrlWithJWT);
                    GetAccessToken AccessToken = new GetAccessToken(HttpContext);
                    var GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();
                    if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
                    {

                        using (var apiClient = new HttpClient())
                        {
                            //API URL to Delete  Business Return using  BusinessId
                            string requestUri = "Business/Delete?BusinessId=" + BusinessId;

                            apiClient.BaseAddress = new Uri(ApiUrl);
                            //Construct HTTP headers                       
                            OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);

                            //Get Response
                            var _response = apiClient.DeleteAsync(requestUri).Result;
                            if (_response != null && _response.IsSuccessStatusCode)
                            {
                                //Read Response
                                var createResponse = _response.Content.ReadAsAsync<BusinessDeleteResponse>().Result;
                                if (createResponse != null)
                                {
                                    deleteReturnResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                                    BusinessDeleteReturnResponse = new JavaScriptSerializer().Deserialize<BusinessDeleteResponse>(deleteReturnResponseJSON);
                                }
                            }
                            else
                            {
                                var createResponse = _response.Content.ReadAsAsync<Object>().Result;
                                deleteReturnResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                                BusinessDeleteReturnResponse = new JavaScriptSerializer().Deserialize<BusinessDeleteResponse>(deleteReturnResponseJSON);
                            }

                        }
                    }

                }
            }
            return PartialView("_BusinessDeleteStatus", BusinessDeleteReturnResponse);
        }
        #endregion

        #region Get Business By BusinessID
        /// <summary>
        /// Function get the single Business return using BusinessId
        /// </summary>
        /// <param name="BusinessId">BusinessId passed to get the single Business return</param>
        /// <returns>BusinessGetReturnResponse</returns>
        public ActionResult GetBusinessByBusinessId(Guid BusinessId, string EinOrSSN)
        {
            var BusinessGetReturnResponse = new BusinessGetResponse();
            var BusinessGetReturnResponseJSON = string.Empty;
            if (BusinessId != null && BusinessId != Guid.Empty)
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
                        //API URL to Get Business Return using BusinessId and EIN
                        string requestUri = "Business/Get?BusinessId=" + BusinessId + "&EIN=" + EinOrSSN;

                        apiClient.BaseAddress = new Uri(ApiUrl);
                        //Construct HTTP headers
                        OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                        //Get Response
                        var _response = apiClient.GetAsync(requestUri).Result;
                        if (_response != null && _response.IsSuccessStatusCode)
                        {
                            //Read Response
                            var createResponse = _response.Content.ReadAsAsync<BusinessGetResponse>().Result;
                            if (createResponse != null)
                            {
                                BusinessGetReturnResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                                BusinessGetReturnResponse = new JavaScriptSerializer().Deserialize<BusinessGetResponse>(BusinessGetReturnResponseJSON);
                                if (BusinessGetReturnResponse != null && BusinessGetReturnResponse.StatusCode == (int)StatusCodeList.Success)
                                {
                                    ViewData["BusinessGetResponseJSON"] = BusinessGetReturnResponseJSON;
                                    return PartialView("_GetSIngleBusiness");
                                }
                            }
                        }
                        else
                        {
                            var createResponse = _response.Content.ReadAsAsync<Object>().Result;
                            BusinessGetReturnResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                            BusinessGetReturnResponse = new JavaScriptSerializer().Deserialize<BusinessGetResponse>(BusinessGetReturnResponseJSON);
                        }

                    }
                }



            }
            return PartialView("_GetSIngleBusiness", BusinessGetReturnResponse);
        }
        #endregion

        #region  Business Members Type  based on Business Type
        [HttpGet]
        public ActionResult BusinessMembersType(string SelectedBusinessTypeVal)
        {
            var businessType = SelectedBusinessTypeVal;
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
            return new JsonResult(false);
        }
        #endregion


        #region Get Business Address Book 
        /// <summary>
        /// Function get the Business Return
        /// </summary>     
        /// <returns>BusinessReturnList</returns>
        public ActionResult GetBusinessAddressBook()
        {
            var ListReturnRepsone = new BusinessListResponse();
            var Business = new List<Business>();
            var getReturnResponseJSON = string.Empty;
            DateTime aDate = DateTime.Now;
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
                    //API URL to Get Business Return
                    string requestUri = "Business/List?Page=0&PageSize=10";

                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Get Response
                    var _response = apiClient.GetAsync(requestUri).Result;
                    if (_response != null && _response.IsSuccessStatusCode)
                    {
                        //Read Response
                        var createResponse = _response.Content.ReadAsAsync<BusinessListResponse>().Result;
                        if (createResponse != null)
                        {
                            getReturnResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                            ListReturnRepsone = new JavaScriptSerializer().Deserialize<BusinessListResponse>(getReturnResponseJSON);
                            Business = ListReturnRepsone.Businesses;
                        }
                    }
                    else
                    {
                        var createResponse = _response.Content.ReadAsAsync<Object>().Result;
                        getReturnResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                        ListReturnRepsone = new JavaScriptSerializer().Deserialize<BusinessListResponse>(getReturnResponseJSON);
                    }

                }
            }
            return new JsonResult(new { data = Business });
        }
        #endregion


        #region Get Single Business In Json 
        /// <summary>
        /// Function get the Business Return to Efile
        /// </summary>
        /// <param name="BusinessId">BusinessId passed to get the single Business return</param>
        /// <returns>BusinessGetReturnResponse</returns>
        public ActionResult GetSingeBusinessInJsonFormat(Guid BusinessId, string EinOrSSN)
        {
            var BusinessGetReturnResponse = new BusinessGetResponse();
            var Businesslist = new List<Business>();
            var BusinessGetReturnResponseJSON = string.Empty;
            if (EinOrSSN != null && EinOrSSN != string.Empty)
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
                        string requestUri = "Business/Get?BusinessId=" + BusinessId + " & EIN = " + EinOrSSN;

                        apiClient.BaseAddress = new Uri(ApiUrl);
                        //Construct HTTP headers
                        OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                        //Get Response
                        var _response = apiClient.GetAsync(requestUri).Result;
                        if (_response != null && _response.IsSuccessStatusCode)
                        {
                            //Read Response
                            var createResponse = _response.Content.ReadAsAsync<BusinessGetResponse>().Result;
                            if (createResponse != null)
                            {
                                BusinessGetReturnResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                                BusinessGetReturnResponse = new JavaScriptSerializer().Deserialize<BusinessGetResponse>(BusinessGetReturnResponseJSON);
                                if (BusinessGetReturnResponse != null && BusinessGetReturnResponse.StatusCode == (int)StatusCodeList.Success)
                                {


                                }

                            }
                        }
                        else
                        {
                            var createResponse = _response.Content.ReadAsAsync<Object>().Result;
                            BusinessGetReturnResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                            BusinessGetReturnResponse = new JavaScriptSerializer().Deserialize<BusinessGetResponse>(BusinessGetReturnResponseJSON);
                        }

                    }
                }
            }
            return new JsonResult(new { Data = BusinessGetReturnResponse });
        }
        #endregion
    }
}
