using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using DotNetCoreSDK.Models.Base;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace DotNetCoreSDK.Models.Utilities
{
    public class GetAccessToken
    {
        private ISession Session;
        public GetAccessToken(HttpContext HttpContext)
        {
            Session = HttpContext.Session;
        }

        #region Get Generated Access Token
       public string GetGeneratedAccessToken()
        {
            var GeneratedAccesToken = string.Empty;

            //Checking whether session having access token or not
            if (Session != null && Session.GetString("AccessToken") != null)
            {
                //if the sessison having the accestoken, then check whethere it is expired or not
                bool IsJWTExpired = GetAccessToken.CheckIsJWTExpired(Session.GetString("AccessToken"));

                if (IsJWTExpired)
                {
                    //If it is expired, Calling AccessTokenGenerator to regenerate it.
                    GeneratedAccesToken = AccessTokenGenerator();                    
                }
                else
                {
                    //if it not, getting the access token from session.
                    GeneratedAccesToken = Session.GetString("AccessToken");                   
                }
            }
            else
            {
                //If session is null, then calling accesstokengenerator.
                GeneratedAccesToken = AccessTokenGenerator();               
            }
            return GeneratedAccesToken;
        }
        #endregion

        #region Access Token Generation
        public string AccessTokenGenerator()
        {
            
            var AccessToken = string.Empty;
            var response = new HttpResponseMessage();
            //Get URLs from App.Config
            string oAuthApiUrl = Utility.GetAppSettings(Constants.OAuthApiUrl);         
            using (var oAuthClient = new HttpClient())
            {
                oAuthClient.BaseAddress = new Uri(oAuthApiUrl);
                //Generate JWS and get access token (JWT)
                OAuthGenerator.GenerateJWSAndGetAccessToken(oAuthClient);
                //Read OAuth API response
                response = oAuthClient.GetAsync(oAuthApiUrl).Result;
                if (response != null && response.IsSuccessStatusCode)
                {
                    var oauthApiResponse = response.Content.ReadAsAsync<AccessTokenResponse>().Result;
                    if (oauthApiResponse != null && oauthApiResponse.StatusCode == 200)
                    {
                        //Get Access token from OAuth API response
                        //Access token is valid for one hour
                        AccessToken = oauthApiResponse.AccessToken;
                        Session.SetString("AccessToken", AccessToken);

                    }
                }               
                return AccessToken;
            }

        }

        #endregion

        #region Checking JWT Expired Or Not
        public static bool CheckIsJWTExpired(string JWT)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = new JwtSecurityToken();
            token = tokenHandler.ReadJwtToken(JWT);
            bool isJWTExpired = false;
            if (token != null && token.Claims != null && token.Claims.Any())
            {
                if (token.Claims.Any(x => x.Type == JwtRegisteredClaimNames.Exp))
                {
                    int expiryTime = Utility.GetInt(token.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Exp).Value);

                    int currentTime = Utility.GetInt(OAuthGenerator.ConvertToUnixEpochString(DateTime.Now));

                    if (expiryTime < currentTime)
                    {
                        isJWTExpired = true;
                    }
                }
            }
            return isJWTExpired;

        }
        #endregion



        #region Get Generated Access Token
        public ReturnAcessTokenResponse GetGeneratedAccessTokenTemp()
        {
            var GeneratedAccesToken = new ReturnAcessTokenResponse();

            //Checking whether session having access token or not
            if (Session != null && Session.GetString("AccessToken") != null)
            {
                //if the sessison having the accestoken, then check whethere it is expired or not
                bool IsJWTExpired = GetAccessToken.CheckIsJWTExpired(Session.GetString("AccessToken"));

                if (IsJWTExpired)
                {
                    //If it is expired, Calling AccessTokenGenerator to regenerate it.
                    GeneratedAccesToken = AccessTokenGeneratorTemp();
                }
                else
                {
                    //if it not, getting the access token from session.
                    GeneratedAccesToken.AccessToken = Session.GetString("AccessToken");
                   
                }
            }
            else
            {
                //If session is null, then calling accesstokengenerator.
                GeneratedAccesToken = AccessTokenGeneratorTemp();

            }
            return GeneratedAccesToken;
        }
        #endregion

        #region Access Token Generation
        public ReturnAcessTokenResponse AccessTokenGeneratorTemp()
        {

            var GetReturnedAccessTokenResponse = new ReturnAcessTokenResponse();
            var response = new HttpResponseMessage();
            //Get URLs from App.Config
            string oAuthApiUrl = Utility.GetAppSettings(Constants.OAuthApiUrl);

            using (var oAuthClient = new HttpClient())
            {
                oAuthClient.BaseAddress = new Uri(oAuthApiUrl);
                //Generate JWS and get access token (JWT)
                OAuthGenerator.GenerateJWSAndGetAccessToken(oAuthClient);
                //Read OAuth API response
                response = oAuthClient.GetAsync(oAuthApiUrl).Result;
                if (response != null && response.IsSuccessStatusCode)
                {
                    var oauthApiResponse = response.Content.ReadAsAsync<AccessTokenResponse>().Result;
                    if (oauthApiResponse != null && oauthApiResponse.StatusCode == 200)
                    {
                        //Get Access token from OAuth API response
                        //Access token is valid for one hour
                        GetReturnedAccessTokenResponse.AccessToken = oauthApiResponse.AccessToken;
                        Session.SetString("AccessToken", GetReturnedAccessTokenResponse.AccessToken);

                    }
                }
                else
                {
                    GetReturnedAccessTokenResponse.ResponseMessage = response;

                }
                return GetReturnedAccessTokenResponse;
            }

        }

        #endregion

        public class ReturnAcessTokenResponse
        {
            public int StatusCode { get; set; }
            public HttpResponseMessage ResponseMessage { get; set; }
            public string AccessToken { get; set; }

        }
    }

}
