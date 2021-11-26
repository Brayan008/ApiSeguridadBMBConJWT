using ApiSeguridadConJWT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ApiSeguridadConJWT
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // AÑADE EL HANDLER DE VALIDACIÓN DE TOKENS
            config.MessageHandlers.Add(new ValidarTokenHandler());
        }
    }
}
