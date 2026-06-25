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
            HttpCookie cookie = Request.Cookies["AgenciaAuto"];
            if (cookie == null)
            {
                if (Session["Erro"] != null)
                    ViewBag.Erro = Session["Erro"].ToString();
                return View();
            }
            else
            {
                Response.Redirect("/Home/Index");
                return null;
            }
            
        }

        [HttpPost]
        public ActionResult Checarlogin()
        {
            var usuario = new Models.Usuario();
            usuario.Email = Request["Email"];
            usuario.Senha = Request["PassWord"];
            if(usuario.Login())
            {
                SalvarCookie("AgenciaAuto", Request["Email"]);
                //Session["Autorizado"] = "OK";
                Session.Remove("Erro");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["Erro"] = "Senha ou usuário inválidos";
                return RedirectToAction("Index", "Login");
            }
        }

        private void SalvarCookie(string nomecookie, string nomeusuario)
        {
            HttpCookie cookie = new HttpCookie(nomecookie);
            cookie.Values.Add("nomeUsuario", nomeusuario);
            //tempo de duração 10 horas
            cookie.Expires = DateTime.Now.AddHours(10);
            //true somente pode ser lido pelo asp net
            cookie.HttpOnly = true;
            Response.AppendCookie(cookie);
        }
    }
}