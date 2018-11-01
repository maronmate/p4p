using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Cors;
using System.Web.Http;
using System.Web.Http.Cors;


[assembly: OwinStartup(typeof(P4Papi.Startup))]
namespace P4Papi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            //var cors = new EnableCorsAttribute("*", "*", "*");
            //  config.EnableCors(cors);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(app);
            WebApiConfig.Register(config);            
            app.UseWebApi(config);
        }
    }
}