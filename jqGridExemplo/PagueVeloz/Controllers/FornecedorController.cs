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
	public class FornecedorController : Controller
	{

		[HttpPost]
		public ActionResult Cadastrar(FORNECEDOR objFornecedor)
		{
			PagueVelozEntities contexto = new PagueVelozEntities();

			var objUF = contexto.EMPRESA.FirstOrDefault(a => a.EMP_Id == objFornecedor.EMP_Id);

			objFornecedor.TELEFONE = objFornecedor.TELEFONE.Where(a => a.TEL_DDD != 0 && a.TEL_Numero != 0).ToList();

			objFornecedor.FND_DataCadastro = DateTime.Now;

			if (objFornecedor.FND_TipoPessoa == "F")
			{
				if (objUF.ESTADO.UF_Sigla == "PR")
				{
					if (objFornecedor.FND_DataNascimento != null)
					{
						if (((DateTime.Now - objFornecedor.FND_DataNascimento.Value).TotalDays / 360) < 18)
						{
							ViewBag.JavaScriptFunction = "$('#boxMsg').show()";
							ViewBag.Msg = "Não é possível cadastrar fornecedor pessoa física de menor para o estado do Paraná.";

							var Empresas = contexto.EMPRESA.ToList().AsEnumerable()
			.Select(s => new
			{
				s.EMP_Id,
				EMP_NomeFantasia = $"({s.EMP_CNPJ}) - {s.EMP_NomeFantasia}"
			}).ToList();

							var model = new FORNECEDOR();
							model.FND_Id = 0;

							var lstTipoPessoa = new[]
							{
							new { Tipo = "F", Descricao = "Física" },
							new { Tipo = "J", Descricao = "Jurídica" }

						}.ToList();


							this.ViewData["EMP_Id"] = new SelectList(Empresas, "EMP_Id", "EMP_NomeFantasia");

							this.ViewData["FND_TipoPessoa"] = new SelectList(lstTipoPessoa, "Tipo", "Descricao");

							return View(objFornecedor);
						}


					}
				}
			}

			if (ModelState.IsValid)
			{

				contexto.FORNECEDOR.Add(objFornecedor);

				contexto.SaveChanges();


				return RedirectToAction("/Cadastrar");
			}
			else
			{

				var Empresas = contexto.EMPRESA.ToList().AsEnumerable()
				.Select(s => new
				{
					s.EMP_Id,
					EMP_NomeFantasia = $"({s.EMP_CNPJ}) - {s.EMP_NomeFantasia}"
				}).ToList();

				var model = new FORNECEDOR();

				model.FND_Id = 0;


				var lstTipoPessoa = new[]
				{
				new { Tipo = "F", Descricao = "Física" },
				new { Tipo = "J", Descricao = "Jurídica" }

			}.ToList();



				this.ViewData["EMP_Id"] = new SelectList(Empresas, "EMP_Id", "EMP_NomeFantasia");

				this.ViewData["FND_TipoPessoa"] = new SelectList(lstTipoPessoa, "Tipo", "Descricao");




				return View(objFornecedor);
			}
		}

		public ActionResult Cadastrar()
		{
			var contexto = new PagueVelozEntities();

			var Empresas = contexto.EMPRESA.ToList().AsEnumerable()
				.Select(s => new
				{
					EMP_Id = s.EMP_Id,
					EMP_NomeFantasia = $"({s.EMP_CNPJ}) - {s.EMP_NomeFantasia}"
				}).ToList();

			var model = new FORNECEDOR();
			model.FND_Id = 0;


			var lstTipoPessoa = new[]
			{
				new { Tipo = "F", Descricao = "Física" },
				new { Tipo = "J", Descricao = "Jurídica" }
			}.ToList();

			this.ViewData["EMP_Id"] = new SelectList(Empresas, "EMP_Id", "EMP_NomeFantasia");
			this.TempData["EMP_Id"] = this.ViewData["EMP_Id"];


			this.ViewData["FND_TipoPessoa"] = new SelectList(lstTipoPessoa, "Tipo", "Descricao");
			this.TempData["FND_TipoPessoa"] = this.ViewData["FND_TipoPessoa"];

			return View(model);
		}

		public ActionResult Listar(bool _search, Int64 nd, int rows, int page, string sidx, string sord, string filtro, string filtroDataCadastro)
		{
			var contexto = new PagueVelozEntities();

			//contexto.Configuration.LazyLoadingEnabled = false;
			//contexto.Configuration.ProxyCreationEnabled = false;		  		 

			var Empresas = contexto.FORNECEDOR
				.OrderByDescending(a => a.FND_Id)
				.Select(s => new
				{
					Codigo = s.FND_Id,
					Nome = s.FND_Nome,
					CNPJ = s.FND_Documento,
					DataCadastro = s.FND_DataCadastro,
					TipoPessoa = s.FND_TipoPessoa == "F" ? "Física" : "Jurídica",
					DataNascimento = s.FND_DataNascimento,
					Empresa = s.EMPRESA.EMP_NomeFantasia

				})
				.ToList();

			

			if (!String.IsNullOrWhiteSpace(filtroDataCadastro))
			{
				DateTime data = Convert.ToDateTime(filtroDataCadastro);

				Empresas = Empresas
					.Where(a => a.DataNascimento != null)
					.Where(a => a.DataNascimento.Value.Date == data.Date)
					.Select(a => { return a; })
					.ToList();
			}
			else if (!String.IsNullOrWhiteSpace(filtro))
			{
				filtro = filtro.ToUpper();

				Empresas = Empresas.Where(a => a.Nome.ToUpper().Contains(filtro)
				||
				a.CNPJ.Contains(filtro)

				).ToList();
			}

			return Json(Empresas, JsonRequestBehavior.AllowGet);

		}


	}

	public class Retorno
	{

		public int Codigo { get; set; }
		public string Nome { get; set; }
		public string CNPJ { get; set; }
		public DateTime? DataCadastro { get; set; }
		public string TipoPessoa { get; set; }
		public DateTime? DataNascimento { get; set; }
		public string Empresa { get; set; }
		public List<TELEFONE> Telefone { get; set; }
		public string Telefones { get; set; }
	}
}