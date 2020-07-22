using System.Web;
using System.Web.Mvc;

namespace FGTrack_API.WEB_API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
