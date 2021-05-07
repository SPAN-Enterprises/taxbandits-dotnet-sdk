using System;
using System.Net.Http.Headers;
using System.Web;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace DotNetCoreSDK.Models.Utilities
{
    public class PublicAPIClient : HttpClient
    {
        public PublicAPIClient()
        {

            if (!string.IsNullOrWhiteSpace(Utility.GetAppSettings("PublicAPIUrl")))
            {
                string apiURL = Utility.GetAppSettings("PublicAPIUrl");              
                Uri apiURI = new Uri(apiURL);
                this.BaseAddress = apiURI;
            }
            else
            {
                this.BaseAddress = new Uri(BaseAddress.GetLeftPart(UriPartial.Authority));
            }

            this.DefaultRequestHeaders.Accept.Clear();

            this.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}