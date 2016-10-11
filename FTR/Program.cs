using System;
using System.Linq;
using System.Reflection;
using Autofac;
using FTR.Services;
using Serilog;

namespace FTR
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IContainer container = null;
            try
            {
                container = ConfigureAutofac();

                var gameMamager = container.Resolve<IGameManager>();

                gameMamager.StartGame();
            }
            catch (Exception exception)
            {
                var logger = container.Resolve<ILogger>();
                logger.Error(exception, "An error occurred");
                Console.WriteLine("An unexpected error occurred. Please see the log for details.");
                Environment.Exit(1);
            }
        }


        private static void RegisterNamespaceInAssembly(ContainerBuilder builder, string namespaceEndingWith,
            Assembly assembly)
        {
            var typesToRegister = assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsInterface)
                .Where(x => !string.IsNullOrEmpty(x.Namespace) && x.Namespace.EndsWith(namespaceEndingWith))
                .ToList();

            typesToRegister.ForEach(x => builder.RegisterType(x).AsImplementedInterfaces());
        }

        private static void ConfigureLogging(ContainerBuilder builder)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.File("log.txt")
                .CreateLogger();

            builder.RegisterInstance(logger).AsImplementedInterfaces();
        }

        private static IContainer ConfigureAutofac()
        {
            var builder = new ContainerBuilder();
            RegisterNamespaceInAssembly(builder, "Services", Assembly.GetExecutingAssembly());
            builder.RegisterType<GameState>().SingleInstance().AsImplementedInterfaces();
            ConfigureLogging(builder);
            return builder.Build();
        }
    }
}