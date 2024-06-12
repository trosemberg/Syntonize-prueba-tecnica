using Autofac.Core;
using AutoMapper;
using System.Data.Entity;
using System.Web.Http;
using TechTest.Data;
using TechTest.Mapper;
using TechTest.Models;
using TechTest.Repositories;
using TechTest.Repositories.Interface;
using TechTest.Services;
using TechTest.Services.Interface;
using Unity;
using Unity.Injection;
using Unity.WebApi;

namespace TechTest
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperModelToDTO>();
            });

            IMapper mapper = configuration.CreateMapper();

            container.RegisterInstance(mapper);
            container.RegisterServices();
            container.RegisterRepositories();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        public static void RegisterServices(this UnityContainer container)
        {
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IRolesService, RolesService>();
        }

        public static void RegisterRepositories(this UnityContainer container)
        {
            container.RegisterType<IRepository<Users>, Repository<Users>>();
            container.RegisterType<IRepository<Roles>, Repository<Roles>>();
        }
    }
}