using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;

namespace Garaio_Assessment.Configuration
{
    public class GaraioAssessment_Config
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "Garaio-Assessment-Api",
                routeTemplate: "api/{controller}",
                defaults: new { }
            );

            GlobalConfiguration.Configuration.Formatters.Clear();

            JsonMediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
            jsonFormatter.Indent = true;
            GlobalConfiguration.Configuration.Formatters.Add(jsonFormatter); // json

            //GlobalConfiguration.Configuration.Formatters.Add(new System.Net.Http.Formatting.XmlMediaTypeFormatter()); // xml
        }
    }
}