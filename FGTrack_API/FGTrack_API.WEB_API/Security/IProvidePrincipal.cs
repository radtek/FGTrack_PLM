using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace FGTrack_API.WEB_API.Security
{
    public interface IProvidePrincipal
    {
        IPrincipal CreatePrincipal(string username, string password);
    }
}