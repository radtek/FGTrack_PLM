using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using FGTrack_API.WEB_API.Security;

namespace FGTrack_API.WEB_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.MessageHandlers.Add(new BasicAuthMessageHandler()
            {
                PrincipalProvider = new DummyPrincipalProvider()
            });

            config.Filters.Add(new AuthorizeAttribute());

            // Web API routes
            //config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ControllerOnly",
                routeTemplate: "api/{controller}"
            );

            // Controllers with Actions
            config.Routes.MapHttpRoute(
                name: "ControllerAndAction",
                routeTemplate: "api/{controller}/{action}"
            );
        }
    }
}
