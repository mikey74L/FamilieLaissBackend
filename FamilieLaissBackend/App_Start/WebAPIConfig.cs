using FamilieLaissBackend.Data.Interface;
using FamilieLaissBackend.Data.UnitOfWork;
using FamilieLaissBackend.Data.Validator;
using Newtonsoft.Json.Serialization;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        //Den DI-Container erzeugen
        var container = new Container();

        //Den Scope auf WebAPI-Request setzen
        container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

        //Die benötigten Typen registrieren
        container.Register<iUnitOfWorkData, UnitOfWorkData>(Lifestyle.Scoped);
        container.Register<iBreezeValidator, BreezeValidator>(Lifestyle.Scoped);
        //container.Register<iMessageRepository, MessageRepository>(Lifestyle.Scoped);

        //Die Web-API-Controller registrieren
        container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

        //Überprüfen des Containers
        //container.Verify();

        //Den Dependency Resolver für die Web-API zuweisen
        System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

        // Web API routes
        config.MapHttpAttributeRoutes();

        config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
        );

        var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
        jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    }
}