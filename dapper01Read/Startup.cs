using Autofac;
using AutofacSerilogIntegration;
using GomoService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GomoApp
{
    public class Startup
    {
        IConfigurationRoot Configuration { get; }
        public Startup()
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        internal IContainer Configure()
        {
            var builder = new ContainerBuilder();
            //註冊log
            builder.RegisterLogger();
            //將service註冊
            builder.Register(x => this.Configuration).As<IConfigurationRoot>();
            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<BatchService>().As<IBatchService>();

            return builder.Build();
        }
    }
}
