﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject.Web.WebApi;
using Ninject.Web.WebApi.WebHost;
using Ninject.Modules;
using Ninject;
using Logic;
using Ninject.Http;

namespace WebUI
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

           /* NinjectModule UIBinder = new Binder();
            NinjectModule LogicBinder = new LogicBinder();
            var kernel = new StandardKernel(LogicBinder, UIBinder);
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);*/
        }
    }
}
