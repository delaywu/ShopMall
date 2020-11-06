using Autofac;
using ShopMall.Site.Domain;
using System;
using System.Linq;

namespace ShopMall.Site.WebApi
{
    public class AutofacShopMallModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        { 
            builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly()).PropertiesAutowired();

            var baseType = typeof(IDependency);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterAssemblyTypes(assemblies)
                         .Where(s => baseType.IsAssignableFrom(s) && !s.IsAbstract)
                         .AsSelf()
                         .AsImplementedInterfaces()
                         .PropertiesAutowired()
                         .InstancePerLifetimeScope();
        }
    }
}
