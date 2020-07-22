using HTN.BITS.FGTRACK.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace FGTrack_API.WEB_API.Security
{
    public class DummyPrincipalProvider : IProvidePrincipal
    {
        public IPrincipal CreatePrincipal(string username, string password)
        {
            bool isAuthen = false;
            string userRole = string.Empty;

            using (AuthenticationBLL authenBll = new AuthenticationBLL())
            {
                isAuthen = authenBll.CheckApiAuthen(username, password, out userRole);
            }

            if (isAuthen)
            {
                var identity = new GenericIdentity(username);

                //get roles for user
                IPrincipal principal = new GenericPrincipal(identity, new[] { userRole });
                return principal;
            }
            else
            {
                return null;
            }
        }
    }
}