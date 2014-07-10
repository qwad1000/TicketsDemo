[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TicketsDemo.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TicketsDemo.App_Start.NinjectWebCommon), "Stop")]

namespace TicketsDemo.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using TicketsDemo.CSV;
    using TicketsDemo.CSV.Repositories;
    using TicketsDemo.Data.Repositories;
    using TicketsDemo.Domain.DefaultImplementations;
    using TicketsDemo.Domain.DefaultImplementations.PriceCalculationStrategy;
    using TicketsDemo.Domain.Interfaces;
    using TicketsDemo.EF.Repositories;

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
            kernel.Bind<ITicketRepository>().To<TicketRepository>();
            kernel.Bind<ITrainRepository>().To<TrainRepository>();

            //kernel.Bind<ITrainRepository>().ToMethod(x => 
            //    new CSVTrainRepository(HttpContext.Current.Server.MapPath("~/App_Data"),x.Kernel.Get<ICSVReader>()));

            kernel.Bind<IRunRepository>().To<RunRepository>();
            kernel.Bind<IReservationRepository>().To<ReservationRepository>();

            kernel.Bind<ISchedule>().To<Schedule>();
            kernel.Bind<ITicketService>().To<TicketService>();
            kernel.Bind<IReservationService>().To<ReservationService>();

            //todo factory
            kernel.Bind<IPriceCalculationStrategy>().To<DefaultPriceCalculationStrategy>();

            kernel.Bind<ICSVReader>().To<CSVReader>();

            //binding my own logger
            kernel.Bind<ILogger>().To<FileLogger>();
        }        
    }
}
