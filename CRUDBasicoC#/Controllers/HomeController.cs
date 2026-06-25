using CRUDBasico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDBasico.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies["AgenciaAuto"];
            if (cookie != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Veiculo()
         {
            ViewBag.Title = "Página Consulta Veiculo";
            ViewBag.Message = "Lista de Veiculos";
            HttpCookie cookie = Request.Cookies["AgenciaAuto"];
            if (cookie != null)
            {
                var lista = Veiculos.Get_Carros();
                ViewBag.Lista = lista;
                return View();
            }
            else
            {
                //possivel trocar essas duas linhas pela seguinte de baixo
                //Response.Redirect("/Login/Index");
                //return null;
                return RedirectToAction("Index", "Login");
            }            
        }

        public ActionResult Contact()
        {
            HttpCookie cookie = Request.Cookies["AgenciaAuto"];
            if (cookie != null)
            {
                ViewBag.Message = "Your contact page.";
                return View();
            }
            else
            {
                //possivel trocar essas duas linhas pela seguinte de baixo
                //Response.Redirect("/Login/Index");
                //return null;
                return RedirectToAction("Index", "Login");
            }
        }
    }
}