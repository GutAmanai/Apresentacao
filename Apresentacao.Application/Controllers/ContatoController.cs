using System.Web.Mvc;
using System.Web.Script.Serialization;
using Apresentacao.Infra;
using Apresentacao.Servicos.Dto;
using Apresentacao.Servicos.Servico;

namespace Apresentacao.Application.Controllers
{
    public class ContatoController : Controller
    {
        public JavaScriptSerializer js = new JavaScriptSerializer();
        public ServicoContato ServicoContato;
        public ServicoEmpresa ServicoEmpresa;

        public ContatoController()
        {
            ServicoContato = Fabrica.Instancia.Obter<ServicoContato>();
            ServicoEmpresa = Fabrica.Instancia.Obter<ServicoEmpresa>();
        }

        public ActionResult Cadastrar(int idEmpresa)
        {
            return View(ServicoEmpresa.ObterPorId(idEmpresa));
        }

        public ActionResult SalvarCadastro(string configuracao)
        {
            var dto = js.Deserialize<DtoContato>(configuracao);
            if (ServicoContato.CadastrarContato(dto))
                return Json(true);
            else
                return Json(false);
        }

        public ActionResult ListaContatos(string nome,int idEmpresa, int page)
        {
            return Json(ServicoContato.ListaEmpresas(nome, idEmpresa, page));
        }
    }
}
