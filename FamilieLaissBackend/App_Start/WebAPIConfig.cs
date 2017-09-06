using AutoMapper;
using FamilieLaissAzureOperations.Interface;
using FamilieLaissAzureOperations.Repository;
using FamilieLaissBackend.Data.Interface;
using FamilieLaissBackend.Data.Model;
using FamilieLaissBackend.Data.UnitOfWork;
using FamilieLaissBackend.Model.FacetGroup;
using FamilieLaissBackend.Model.FacetValue;
using FamilieLaissBackend.Model.MediaGroup;
using FamilieLaissBackend.Model.MediaItem;
using FamilieLaissBackend.Model.MediaItemFacet;
using FamilieLaissBackend.Model.UploadPictureImageProperty;
using FamilieLaissBackend.Model.UploadPictureItem;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;

public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        //Den DI-Container erzeugen
        var container = new Container();

        //Den Scope auf WebAPI-Request setzen
        container.Options.DefaultScopedLifestyle = new SimpleInjector.Lifestyles.AsyncScopedLifestyle();

        //Die benötigten Typen registrieren
        container.Register<iUnitOfWorkData, UnitOfWorkData>(Lifestyle.Scoped);
        container.Register<iAzureStorageOperations, AzureStorageRepository>(Lifestyle.Scoped);
        //container.Register<iMessageRepository, MessageRepository>(Lifestyle.Scoped);

        //Die Web-API-Controller registrieren
        container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

        //Überprüfen des Containers
        //container.Verify();

        //Den Dependency Resolver für die Web-API zuweisen
        System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

        //Die Automapper Mappings definieren
        Mapper.Initialize(cfg =>
        {
            cfg.CreateMap<FacetGroupUpdateDTO, FacetGroup>();
            cfg.CreateMap<FacetGroupInsertDTO, FacetGroup>();
            cfg.CreateMap<FacetValueUpdateDTO, FacetValue>();
            cfg.CreateMap<FacetValueInsertDTO, FacetValue>();
            cfg.CreateMap<MediaGroupInsertDTO, MediaGroup>();
            cfg.CreateMap<MediaGroupUpdateDTO, MediaGroup>();
            cfg.CreateMap<MediaItemInsertDTO, MediaItem>();
            cfg.CreateMap<MediaItemUpdateDTO, MediaItem>();
            cfg.CreateMap<MediaItemFacetInsertDTO, MediaItemFacet>();
            cfg.CreateMap<UploadPictureItemInsertDTO, UploadPictureItem>();
            cfg.CreateMap<UploadPictureItemUpdateDTO, UploadPictureItem>();
            cfg.CreateMap<UploadPictureImagePropertyInsertDTO, UploadPictureImageProperty>();
            cfg.CreateMap<UploadPictureImagePropertyUpdateDTO, UploadPictureImageProperty>();
        });

        // Web API routes
        config.MapHttpAttributeRoutes();

        config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "FamilieLaissAPI/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
        );

        //OData - Query Extensions zu jedem IQueryable hinzufügen 
        System.Web.Http.OData.Extensions.HttpConfigurationExtensions.AddODataQueryFilter(config);

        //Folgendes einkommentieren wenn CamelCase bei JSON verlangt wird ist dann aber global
        //Es kann auch das Attribut [JsonProperty(PropertyName="myFoo")] verwendet werden
        //var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
        //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        //Hiermit wird zirkuläre Referenzen ignoriert und nicht aufgelöst wenn es sich bei einem Child-Objekt um das selbe Objekt
        //handelt was ausgegeben werden soll
        config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    }
}