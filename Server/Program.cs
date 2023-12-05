using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Grpc.Core;
using GrpcApi;
using Autofac;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ContainerBuilder containerBuilder = new ContainerBuilder();
                containerBuilder.RegisterType<DbContext>().AsSelf().SingleInstance();
                containerBuilder.RegisterType<WorkerRepository>().As<IWorkerRepository>();
                containerBuilder.RegisterType<GrpcServiceImpl>().AsSelf();

                var container = containerBuilder.Build();

                var grpcService = container.Resolve<GrpcServiceImpl>();

                Grpc.Core.Server server = new Grpc.Core.Server
                {
                    Services = { WorkerIntegration.BindService(grpcService) },
                    Ports = { new ServerPort("localhost", 123456, ServerCredentials.Insecure) }
                };
                server.Start();

                Console.ReadLine();

                server.ShutdownAsync().Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
                throw;
            }

         
           
        }
    }
}
