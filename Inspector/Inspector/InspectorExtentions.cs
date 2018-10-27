using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WonderTools.Inspector
{
    public static partial class InspectorExtentions
    {
        public static void AddInspector(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<InspectorRepository>();
            serviceCollection.AddTransient<InspectorMiddleWare>();
            serviceCollection.AddSingleton<InspectorOptions>();
        }

        private static T Get<T>(IApplicationBuilder builder)
        {
            var t = builder.ApplicationServices.GetService<T>();
            if (t == null) throw new Exception("Jit logger has to be added while configuring service.");
            return t;
        }

        public static IApplicationBuilder UseInspector
            (this IApplicationBuilder builder, Action<InspectorConfigurator> configAction)
        {

            var repository = Get<InspectorRepository>(builder);
            var options = Get<InspectorOptions>(builder);
            var configurator = new InspectorConfigurator(repository, options);
            configAction.Invoke(configurator);

            var middleware = builder.ApplicationServices.GetService<InspectorMiddleWare>();
            builder.Use(middleware.Process);

            //ConfigureOptions(builder, configureAction);
            //loggerFactory.AddProvider(new JitLoggerProvider(repository));
            return builder;
        }
    }
}