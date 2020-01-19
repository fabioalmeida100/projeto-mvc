using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using Newtonsoft.Json;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        [HttpPost]
        public ActionResult Incluir(BeneficiarioModel model)
        {
            BoBeneficiario bo = new BoBeneficiario();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                try
                {
                    model.Id = bo.Gravar(new Beneficiario()
                    {   
                        Id = model.Id,
                        Nome = model.Nome,                    
                        CPF = model.CPF,
                        IdCliente = (model.IdCliente == 0 ? long.Parse(TempData["Cliente"].ToString()) : model.IdCliente)

                    });

                    TempData["Cliente"] = model.IdCliente;

                    return Json("Cadastro efetuado com sucesso");
                }
                catch (Exception ex)
                {
                    Response.StatusCode = 400;
                    return Json(ex.Message);
                }

            }
        }

        [HttpGet]
        public ActionResult Incluir(long id)
        {
            TempData["Cliente"] = id;
            return View();
        }

        [HttpGet]
        public JsonResult Listar()
        {

            BoBeneficiario bo = new BoBeneficiario();
            var listaBeneficiario = bo.Listar(long.Parse(TempData["Cliente"].ToString()));

            return Json(listaBeneficiario, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetByID(int id)
        {
            BoBeneficiario bo = new BoBeneficiario();
            return Json(bo.Consultar(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            BoBeneficiario bo = new BoBeneficiario();
            bo.Excluir(id);
            TempData["Cliente"] = Url.RequestContext.RouteData.Values["id"].ToString();

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
