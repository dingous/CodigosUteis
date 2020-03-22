using Newtonsoft.Json;
using PagueVeloz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace PagueVeloz.Controllers
{
	public class EmpresaController : Controller
	{

		[HttpPost]
		public ActionResult Cadastrar(EMPRESA objEmpresa)
		{

			if (ModelState.IsValid)
			{
				using (PagueVelozEntities contexto = new PagueVelozEntities())
				{
					contexto.EMPRESA.Add(objEmpresa);

					contexto.SaveChanges();
				}

				return RedirectToAction("/Cadastrar");
			}
			else
			{
				return RedirectToAction("/Cadastrar");
			}
		}

		public ActionResult Cadastrar()
		{
			var contexto = new PagueVelozEntities();

			var Estados = contexto.ESTADO.ToList();

			var model = new EMPRESA();
			model.EMP_Id = 0;		

			this.ViewData["UF_Id"] = new SelectList(Estados, "UF_Id", "UF_Sigla");

			return View(model);
		}

		public ActionResult Listar(bool _search, Int64 nd, int rows, int page, string sidx, string sord)
		{
			var contexto = new PagueVelozEntities();

			var Empresas = contexto.EMPRESA.ToList().OrderByDescending(a => a.EMP_Id).Select(s => new
			{

				Codigo = s.EMP_Id,
				CNPJ = s.EMP_CNPJ,
				NomeFantasia = s.EMP_NomeFantasia,
				Sigla = s.ESTADO.UF_Sigla

			})
				//.Skip(page).Take(rows)
				.ToList();


			return Json(Empresas, JsonRequestBehavior.AllowGet);

		}


	}
}