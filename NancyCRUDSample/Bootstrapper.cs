namespace NancyCRUDSample
{
    using Nancy;
    using Nancy.TinyIoc;
    using NancyCRUDSample.Models.Context;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        // The bootstrapper enables you to reconfigure the composition of the framework,
        // by overriding the various methods and properties.
        // For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper
        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            container.Register<IApplicationDbContext, ApplicationDbContext>();
        }
    }
}