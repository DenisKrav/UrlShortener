using Autofac;
using AutoMapper;
using UrlShortener.BLL.Mappers;
using UrlShortener.Server.Mappers;

namespace UrlShortener.Server.DependencyResolve
{
    public class MappersModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserDTOProfile());

                cfg.AddProfile(new AuthViewModelProfile());

                cfg.AddProfile(new ShortUrlViewModelProfile());
                cfg.AddProfile(new ShortURLDTOProfile());
            })).SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper())
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}
