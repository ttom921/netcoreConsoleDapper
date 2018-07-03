using Autofac;
using AutofacSerilogIntegration;
using Gomo.Domain;
using Gomo.Repository;
using GomoService;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

            //Database connection
            var connectionString = Configuration["ConnectionStrings:GomoDatabase"];
            builder.Register(c => new MySqlConnection(connectionString)).As<IDbConnection>().InstancePerLifetimeScope();

            // sql config
            //builder.Register(c => new SqlGeneratorConfig() { SqlProvider = SqlProvider.MySQL, UseQuotationMarks = true }).As<SqlGeneratorConfig>().SingleInstance();
            //Sql generators
            builder.Register(c => new SqlGenerator<Employee>(SqlProvider.MySQL, true) { }).As<ISqlGenerator<Employee>>().SingleInstance();
            //builder.RegisterType<SqlGenerator<Employee>>().As<ISqlGenerator<Employee>>().SingleInstance();

            //Repository
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>().InstancePerLifetimeScope();
            return builder.Build();
        }
    }
}
