using System;
using System.Linq;
using Apresentacao.Dominio.Entidades;
using Apresentacao.Dominio.Repositorio;
using Apresentacao.Servicos.Dto;

namespace Apresentacao.Servicos.Servico
{
    public class ServicoContato
    {
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IRepositorioEmpresa _repositorioEmpresa;
        private readonly IRepositorioContato _repositorioContato;

        public ServicoContato(
                                IUnidadeDeTrabalho unidadeDeTrabalho,
                                IRepositorioEmpresa repositorioEmpresa,
                                IRepositorioContato repositorioContato
                             )
        {
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _repositorioEmpresa = repositorioEmpresa;
            _repositorioContato = repositorioContato;
        }
        
        public bool CadastrarContato(DtoContato dtoContato)
        {
            try
            {                
                var empresa = _repositorioEmpresa.ObterPorId(dtoContato.IdEmpresa);

                if(dtoContato.IdContato == 0)
                {
                    var contato = new Contato(empresa);
                    contato.AdicionarNome(dtoContato.Nome);
                    contato.AdicionarEmail(dtoContato.Email);
                    contato.AdicionarTelefone(dtoContato.Telefone);
                    contato.AdicionarCelular(dtoContato.Celular);                    
                    empresa.AdicionarContatos(contato);                    
                }
                else
                {
                    var contato = empresa.Contatos.FirstOrDefault(x => x.Id == dtoContato.IdContato);
                    if(contato != null)
                    {
                        contato.AdicionarNome(dtoContato.Nome);
                        contato.AdicionarEmail(dtoContato.Email);
                        contato.AdicionarTelefone(dtoContato.Telefone);
                        contato.AdicionarCelular(dtoContato.Celular);
                        empresa.AdicionarContatos(contato);
                    }
                }
                _repositorioEmpresa.Alterar(empresa);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DtoContato ObterPorId(int idContato)
        {
            return ObterDtoContato(_repositorioContato.ObterPorId(idContato));
        }

        public DtoPesquisaContato ListaEmpresas(string nome, int idEmpresa, int page = 1)
        {
            int quantidadeReg = 0;
            int quantidadePag = 0;
            var contatos = _repositorioContato.ObterTodosOndeLazy(x => x.Nome.ToLower().Contains(nome.ToLower()) && x.Empresa.Id == idEmpresa).Page(page, 10, x => x.Id, true, out quantidadeReg, out  quantidadePag).ToList();

            var dto = new DtoPesquisaContato();
            dto.PaginaSelecionada = 1;
            dto.Pesquisa = contatos.Select(ObterDtoContato).ToList();
            dto.TotalRegistro = quantidadeReg;
            dto.TotalPagina = quantidadePag;

            return dto;
        }

        private DtoContato ObterDtoContato(Contato contato)
        {
            var dto = new DtoContato();
            dto.IdEmpresa = contato.Empresa.Id;
            dto.IdContato = contato.Id;
            dto.Nome = contato.Nome;
            dto.Telefone = contato.Telefone;
            dto.Celular = contato.Celular;
            dto.Email = contato.Email;
            
            return dto;                
        }

    }
}
