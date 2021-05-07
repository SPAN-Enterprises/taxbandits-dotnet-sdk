using DotNetCoreSDK.Models.Form_941;
using DotNetCoreSDK.Models.Utilities;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DotNetCoreSDK.Models.BaseModels
{
   
    public class AccessTokenResponse:BaseResponseStatus
    {
       
        public string AccessToken { get; set; }
       
        public string TokenType { get; set; }
     
        public int ExpiresIn { get; set; }
       
        public List<Error> Errors { get; set; }
    }
   
}