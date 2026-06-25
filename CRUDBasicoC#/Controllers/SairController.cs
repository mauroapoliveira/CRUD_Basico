using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDBasico.Controllers
{
    public class SairController : Controller
    {
        // GET: Sair
        public ActionResult Index()
        {
            Session.Abandon();
            Response.Cookies["AgenciaAuto"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Index", "Login");
        }
    }
}