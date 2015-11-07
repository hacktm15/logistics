using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace LogisticsAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // GlobalConfiguration.Configuration.EnsureInitialized();
            GlobalConfiguration.Configure(WebApiConfig.Register);

        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Context.Request.Path.ToLower().Contains("odata/") )
            {
                Context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                Context.Response.AddHeader("Access-Control-Allow-Headers", "*");
                Context.Response.AddHeader("Access-Control-Allow-Methods", "*");
            }
        }
    }
}
