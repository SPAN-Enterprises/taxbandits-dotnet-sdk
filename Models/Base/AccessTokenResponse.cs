using DotNetCoreSDK.Models.Form941;
using DotNetCoreSDK.Models.Utilities;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DotNetCoreSDK.Models.Base
{
   
    public class AccessTokenResponse:BaseResponseStatus
    {
       
        public string AccessToken { get; set; }
       
        public string TokenType { get; set; }
     
        public int ExpiresIn { get; set; }
       
        public List<Error> Errors { get; set; }
    }
   
}