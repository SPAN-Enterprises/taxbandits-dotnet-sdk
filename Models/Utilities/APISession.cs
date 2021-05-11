using DotNetCoreSDK.Models.Base;
using DotNetCoreSDK.Models.Form941;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetCoreSDK.Models.Utilities
{
    public class APISession
    {
        private  ISession Session;
        public APISession(HttpContext HttpContext)
        {
            Session = HttpContext.Session;
        }

        #region Form 941

        #region Session Property for Form 941 Return
        public   List<Form941ReturnResponse> Form941ReturnResponses
        {
            get
            {
                if (Session !=null && Session.GetString("Form941ReturnResponses") != null)
                {                                    
                    return JsonConvert.DeserializeObject<List<Form941ReturnResponse>>(Session.GetString("Form941ReturnResponses"));               
                }
                else
                {
                    return new List<Form941ReturnResponse>();
                }
            }
            set
            {
                if (value != null)
                {

                    Session.SetString("Form941ReturnResponses", JsonConvert.SerializeObject(value));
                }
            }
        }
        #endregion

        #region Add Form 941 API Response
        /// <summary>
        /// Add Form 941 API Response
        /// </summary>
        /// <param name="returnResponse"></param>
        public  void AddForm941APIResponse(Form941ReturnResponse returnResponse)
        {

            if (returnResponse != null && returnResponse.SubmissionId != Guid.Empty)
            {
                List<Form941ReturnResponse> returnResponses = Form941ReturnResponses;
                returnResponses.Add(returnResponse);
                Form941ReturnResponses = returnResponses;
            }
        }
        #endregion

        #region Delete Form 941 API Response
        /// <summary>
        /// Delete Form 941 API Response
        /// </summary>
        /// <param name="returnResponse"></param>
        public  void DeleteForm941APIResponse(Guid? submissionId)
        {
            if (submissionId != null && submissionId != Guid.Empty)
            {
                var form941SessionResponse = Form941ReturnResponses;
                if (form941SessionResponse != null)
                {
                    var form941return = form941SessionResponse.Where(a => a.SubmissionId == submissionId).FirstOrDefault();
                    form941SessionResponse.Remove(form941return);
                    Form941ReturnResponses = form941SessionResponse;
                }
            }
        }
        #endregion

        #region Get Form941 RecordIDs by SubmissionId
        /// <summary>
        ///  Get Form941 RecordIDs by SubmissionId
        /// </summary>
        /// <param name="submissionId"></param>
        /// <returns></returns>
        public  TransmitForm GetForm941RecordIdsBySubmissionId(Guid submissionId)
        {
            TransmitForm transmitForm = new TransmitForm();
            List<Form941ReturnResponse> returnResponses = Form941ReturnResponses;
            if (submissionId != Guid.Empty)
            {
                transmitForm.SubmissionId = submissionId;
                if (returnResponses != null && returnResponses.Count > 0)
                {
                    var returnResponse = returnResponses.Where(r => r.SubmissionId == submissionId).SingleOrDefault();
                    if (returnResponse != null && returnResponse.Form941Records != null
                        && returnResponse.Form941Records.SuccessRecords != null && returnResponse.Form941Records.SuccessRecords.Count > 0)
                    {
                        var recordIds = returnResponse.Form941Records.SuccessRecords.Select(r => r.RecordId).ToList();
                        if (recordIds != null && recordIds.Any())
                        {
                            transmitForm.RecordIds = new List<Guid>();
                            foreach (var recordId in recordIds)
                            {
                                transmitForm.RecordIds.Add(recordId ?? Guid.Empty);
                            }
                        }
                    }
                }
            }
            return transmitForm;
        }
        #endregion

        #region Get Coma seperated Form 941 RecordIDs by SubmissionId
        /// <summary>
        ///  Get Coma seperated RecordIDs by SubmissionId
        /// </summary>
        /// <param name="submissionId"></param>
        /// <returns></returns>
        public  string GetComaseperatedForm941RecordIdsBySubmissionId(Guid submissionId)
        {
            string recordIdStr = string.Empty;
            List<Form941ReturnResponse> returnResponses = Form941ReturnResponses;
            if (submissionId != Guid.Empty)
            {
                if (returnResponses != null && returnResponses.Count > 0)
                {
                    var returnResponse = returnResponses.Where(r => r.SubmissionId == submissionId).SingleOrDefault();
                    if (returnResponse != null && returnResponse.Form941Records != null
                        && returnResponse.Form941Records.SuccessRecords != null && returnResponse.Form941Records.SuccessRecords.Count > 0)
                    {
                        var recordIds = returnResponse.Form941Records.SuccessRecords.Select(r => r.RecordId).ToList();
                        if (recordIds != null && recordIds.Any())
                        {
                            foreach (var recordId in recordIds)
                            {
                                recordIdStr = !string.IsNullOrEmpty(recordIdStr) ? ("," + (recordId ?? Guid.Empty).ToString()) : (recordId ?? Guid.Empty).ToString();
                            }
                        }
                    }
                }
            }
            return recordIdStr;
        }
        #endregion

        #region Form941 Return Update Filing Status
        /// <summary>
        /// Update Filing Status
        /// </summary>
        /// <param name="submissionId"></param>
        /// <returns></returns>
        public  bool UpdateForm941ReturnFilingStatus(Guid submissionId)
        {
            bool isUpdated = false;
            if (submissionId != Guid.Empty)
            {
                var form941SessionResponse = Form941ReturnResponses;
                if (form941SessionResponse != null && form941SessionResponse.Count > 0)
                {                 
                    var apiResponse = form941SessionResponse.Where(a => a.SubmissionId == submissionId).FirstOrDefault();
                    apiResponse.IsReturnTransmitted = true;
                    Form941ReturnResponses = form941SessionResponse;
                    isUpdated = true;
                    
                }

            }

            return isUpdated;
        }
        #endregion

        #region Get EFileStatus 941 API Response 
        /// <summary>
        ///  Get API Response 
        /// </summary>
        /// <returns></returns>

        public List<EFileStatus> GetAPIResponse()
        {
            List<EFileStatus> Form941list = new List<EFileStatus>();
            List<Form941ReturnResponse> returnResponses = Form941ReturnResponses;
            if (returnResponses != null && returnResponses.Count > 0)
            {
                var apiResponse = returnResponses.Where(a => a.StatusCode == (int)StatusCodeList.Success && a.SubmissionId != Guid.Empty).ToList();
                foreach (var submission in apiResponse)
                {
                    var eFileStatus = new EFileStatus();
                    eFileStatus.SubmissionId = submission.SubmissionId;
                    eFileStatus.IsReturnTransmitted = submission.IsReturnTransmitted;
                    Form941list.Add(eFileStatus);
                }
            }
            return Form941list;
        }

        #endregion

        #region Get Form 941 API Response 
        /// <summary>
        ///  Get API Response 
        /// </summary>
        /// <returns></returns>
        public List<EFileStatus> GetForm941APIResponse()
        {
            List<EFileStatus> efileStatusList = new List<EFileStatus>();
            List<Form941ReturnResponse> returnResponses = Form941ReturnResponses;
            if (returnResponses != null && returnResponses.Count > 0)
            {
                var apiResponse = returnResponses.Where(a => a.StatusCode == (int)StatusCodeList.Success && a.SubmissionId != Guid.Empty).ToList();
                foreach (var submission in apiResponse)
                {
                    var Form941 = new EFileStatus();
                    Form941.SubmissionId = submission.SubmissionId;
                    Form941.IsReturnTransmitted = submission.IsReturnTransmitted;
                    efileStatusList.Add(Form941);
                }
            }
            return efileStatusList;
        }
        #endregion




        #endregion
     
    }

}