using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDBasico.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (Session["Erro"] != null)
                ViewBag.Erro = Session["Erro"].ToString();
            return View();
        }

        [HttpPost]
        public ActionResult Checarlogin()
        {
            var usuario = new Models.Usuario();
            usuario.Email = Request["Email"];
            usuario.Senha = Request["PassWord"];
            if(usuario.Login())
            {
                Session["Autorizado"] = "OK";
                Session.Remove("Erro");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["Erro"] = "Senha ou usuário inválidos";
                return RedirectToAction("Index", "Login");
            }
        }
    }
}