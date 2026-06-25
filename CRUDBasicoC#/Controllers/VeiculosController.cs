using CRUDBasico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDBasico.Controllers
{
    public class VeiculosController : Controller
    {
        // GET: Veiculos
        public ActionResult Adicionar()
        {
            HttpCookie cookie = Request.Cookies["AgenciaAuto"];
            if (cookie != null)
            {
                ViewBag.Title = "Adicionar veiculo";
                ViewBag.Message = "veiculo";
                return View();
            }
            else
            {
                Response.Redirect("/Login/Index");
                return null;
            }
        }
        public ActionResult Alterar(int id)
        {
            HttpCookie cookie = Request.Cookies["AgenciaAuto"];
            if (cookie != null)
            {
                ViewBag.Title = "Veiculo";
                ViewBag.Message = "Alterar veiculo";
                var veiculo = new Veiculos();
                veiculo.GetVeiculo(id);
                return View(veiculo);
            }
            else
            {
                Response.Redirect("/Login/Index");
                return null;
            }
        }

        public ActionResult Excluir(int id)
        {
            HttpCookie cookie = Request.Cookies["AgenciaAuto"];
            if (cookie != null)
            {
                ViewBag.Title = "Exclusão de veiculo";
                ViewBag.Message = "";
                var veiculo = new Veiculos();
                veiculo.GetVeiculo(id);
                ViewBag.Veiculo = veiculo;
                return View();
            }
            else
            {
                Response.Redirect("/Login/Index");
                return null;
            }
        }

        [HttpPost]
        public ActionResult Salvar(Veiculos veiculo)
        {
            
            if (ModelState.IsValid)
            {
                veiculo.Salvar(veiculo);
                return RedirectToAction("Veiculo", "Home");
            }
            else
            {
                ViewBag.Title = "Veiculos";

                if (Convert.ToInt32("0" + Request["id"]) == 0)
                {
                    ViewBag.Message = "Adicionar Veículo";
                    return View("Adicionar");
                }
                else 
                {
                    ViewBag.veiculo = veiculo;
                    ViewBag.Message = "Alterar Veículo: " + veiculo.Id;
                    return View("Alterar");
                }
            }
        }

        [HttpPost]
        public void Excluir()
        {
            HttpCookie cookie = Request.Cookies["AgenciaAuto"];
            if (cookie != null)
            {
                var veiculo = new Veiculos();
                veiculo.Id = Convert.ToInt32("0" + Request["id"]);
                veiculo.Excluir();
            }
            else
            {
                Response.Redirect("/Home/Veiculo");
            }

        }
    }
}