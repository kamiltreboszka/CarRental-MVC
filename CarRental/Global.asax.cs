using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CarRental.Properties;

namespace CarRental
{
    public class MvcApplication : System.Web.HttpApplication, ILogger<MvcApplication>
    {
        protected void Application_Start()
        {
            Stream log4net_stream = new MemoryStream(Resource.log4net);
            XmlConfigurator.Configure(log4net_stream);

            this.GetLog().Info("Application started");

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
