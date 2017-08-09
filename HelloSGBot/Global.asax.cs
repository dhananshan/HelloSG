using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Bot.Builder.Dialogs;
using NDBot.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace NDBot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);


            //TODO
            //var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            //var config = GlobalConfiguration.Configuration;

            
            // Register your Web API controllers.
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //builder.RegisterType<LUISService>()
            //       .As<ILUISService>()
            //       .InstancePerRequest();

            //builder.RegisterType<HTTPService>()
            //       .As<IHTTPService>()
            //       .InstancePerRequest();

            //var container = builder.Build();
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }
    }
}
