using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CRUDBasico
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "VeiculosSalvar",
                "Veiculos/Salvar",
                new { controller = "veiculos", action = "Salvar" }
            );

        routes.MapRoute(
                "VeiculosAdicionar",
                "Veiculos/Adicionar",
                new { controller = "veiculos", action = "Adicionar" }
            );
            routes.MapRoute(
                "VeiculosAlterar",
                "Veiculos/Alterar/:id",
                new { controller = "veiculos", action = "Alterar", id = 0 }
            );
            routes.MapRoute(
                "VeiculosExcluir",
                "Veiculos/Excluir/:id",
                new { controller = "veiculos", action = "Excluir", id = 0 }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
