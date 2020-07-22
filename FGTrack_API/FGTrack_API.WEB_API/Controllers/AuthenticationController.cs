using HTN.BITS.FGTRACK.BEL;
using HTN.BITS.FGTRACK.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FGTrack_API.WEB_API.Controllers
{
    public class AuthenticationController : ApiController
    {
        [Route("CheckValidationUser")]
        [HttpGet]
        public string CheckValidationUser(string userid, string ipaddress, string serialno, string scanversion) //
        {
            string result = string.Empty;

            using (AuthenticationBLL authenBll = new AuthenticationBLL())
            {
                result = authenBll.CheckValidationUser(userid, ipaddress, serialno, scanversion);
            }

            return result;
        }
    }
}