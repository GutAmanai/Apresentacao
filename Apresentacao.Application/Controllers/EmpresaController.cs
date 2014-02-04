using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Apresentacao.Infra;
using Apresentacao.Servicos.Dto;
using Apresentacao.Servicos.Servico;

namespace Apresentacao.Application.Controllers
{
    public class EmpresaController : Controller
    {
        public JavaScriptSerializer js = new JavaScriptSerializer();
        public ServicoEmpresa ServicoEmpresa;

        public EmpresaController()
        {
            ServicoEmpresa = Fabrica.Instancia.Obter<ServicoEmpresa>();
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        public ActionResult SalvarCadastro(string configuracao)
        {
            var dto = js.Deserialize<DtoEmpresa>(configuracao);
            if(ServicoEmpresa.CadastrarEmpresa(dto))
                return Json(true);
            else
                return Json(false);        
        }

        public ActionResult ListaEmpresas(string nome, int page)
        {
            return Json(ServicoEmpresa.ListaEmpresas(nome, page));
        }
    }
}
