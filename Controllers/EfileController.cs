using DotNetCoreSDK.Models.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreSDK.Controllers
{
    public class EfileController : Controller
    {
        #region  EFile Status
        /// <summary>
        /// Function loads list of all SubmissionIds created in Form941Return page
        /// </summary>
        /// <returns>List of all SubmissionIds</returns>
        public ActionResult EFileStatus()
        {
            var APISession = new APISession(HttpContext);
            return View(APISession.GetAPIResponse());
        }
        #endregion

        #region Get Form 941 EFile Status
        /// <summary>
        /// Get Form 941 EFile Status
        /// </summary>
        /// <returns></returns>
        public ActionResult _GetForm941EFileStatus()
        {
            //var APISession = new APISession(HttpContext);
            return PartialView();
        }
        #endregion
    }
}
