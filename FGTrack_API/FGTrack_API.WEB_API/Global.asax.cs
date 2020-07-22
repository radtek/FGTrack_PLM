using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FGTrack_API.WEB_API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            HTN.BITS.FGTRACK.BLL.GlobalDB.Instance.SetConnectionKey = "FGTrack_API.ConnectionString";

            HTN.BITS.FGTRACK.BLL.GlobalDB.Instance.Init();
            HTN.BITS.FGTRACK.BLL.GlobalDB.Instance.Connect();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
            HTN.BITS.FGTRACK.BLL.GlobalDB.Instance.Disconenct();
            HTN.BITS.FGTRACK.BLL.GlobalDB.Instance.Release();
        }
    }
}
