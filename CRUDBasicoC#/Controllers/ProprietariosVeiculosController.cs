using CRUDBasico.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDBasico.Controllers
{
    public class ProprietariosVeiculosController : Controller
    {
        public ActionResult Index(int id)
        {
            //HttpCookie cookie = Request.Cookies["Veiculo"];
           
            bool dd = true;

            //if (cookie != null)
            if (dd != false)
                    
            {
                ViewBag.Title = "Veiculos do Proprietário";
                ViewBag.Message = "Relação de veiculos de ";
                var proprietario = new ProprietariosVeiculosViewModel();
                var lista = proprietario.GetVeiculos(id);
                return View(lista);
            }
            else
            {
                Response.Redirect("/Login/Index");
                return null;
            }
        }

    }
}