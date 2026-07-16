using laboratoriobioquimico.ar.com.laboratoriobioquimico.utiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace laboratoriobioquimico
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ActionApi1",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { inicio = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ActionApi2",
                routeTemplate: "api/{controller}/{action}/{key1}/{key2}",
                defaults: new { inicio = RouteParameter.Optional }
            );

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
                           .ContractResolver = new NHibernateContractResolver();

        }

    }
}
