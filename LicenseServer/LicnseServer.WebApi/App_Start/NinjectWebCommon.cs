﻿using System.Configuration;
using System.Web.Http;
using LicenseServer.Core.BO;
using LicenseServer.Core.Interfaces.BO;
using LicenseServer.Data.Repositorios;
using MongoDB.Driver;
using Ninject.Extensions.Conventions;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(LicnseServer.WebApi.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(LicnseServer.WebApi.App_Start.NinjectWebCommon), "Stop")]

namespace LicnseServer.WebApi.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                GlobalConfiguration.Configuration.DependencyResolver = new Ninject.WebApi.DependencyResolver.NinjectDependencyResolver(kernel);
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            var CONNSTR = ConfigurationManager.ConnectionStrings["MONGOLAB_URI"].ConnectionString;
            kernel.Bind(config => config.FromThisAssembly().SelectAllClasses().BindDefaultInterfaces().Configure(x => x.InRequestScope()));
            kernel.Bind(config => config.FromAssemblyContaining<Licenses>().SelectAllClasses().BindDefaultInterfaces().Configure(x => x.InRequestScope()));
            kernel.Bind(config => config.FromAssemblyContaining<LicenseBO>().SelectAllClasses().BindDefaultInterfaces().Configure(x => x.InRequestScope()));

            kernel.Bind<MongoServer>().ToMethod((x) =>
            {
                // Create server settings to pass connection string, timeout, etc
                var settings = new MongoServerSettings();
                settings.Server = new MongoServerAddress(CONNSTR);
                // Create server object to communicate with our server
                var server = new MongoServer(settings);
                return server;
            }).InRequestScope();
            kernel.Bind<MongoDatabase>().ToMethod((x) =>
            {
                var server = x.Kernel.Get<MongoServer>();
                return server.GetDatabase("Licenses");
            }).InRequestScope();
        }
    }
}