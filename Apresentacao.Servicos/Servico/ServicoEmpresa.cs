using System;
using System.Linq;
using Apresentacao.Dominio.Entidades;
using Apresentacao.Dominio.Repositorio;
using Apresentacao.Servicos.Dto;

namespace Apresentacao.Servicos.Servico
{
    public class ServicoEmpresa
    {
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IRepositorioEmpresa _repositorioEmpresa;

        public ServicoEmpresa(
                                IUnidadeDeTrabalho unidadeDeTrabalho,
                                IRepositorioEmpresa repositorioEmpresa
                             )
        {
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _repositorioEmpresa = repositorioEmpresa;
        }
        
        public bool CadastrarEmpresa(DtoEmpresa dtoEmpresa)
        {
            try
            {
                if(dtoEmpresa.IdEmpresa == 0)
                {
                    _repositorioEmpresa.Adicionar(
                                                    new Empresa(
                                                                    dtoEmpresa.Nome,
                                                                    dtoEmpresa.NomeFantasia,
                                                                    dtoEmpresa.WebSite,
                                                                    dtoEmpresa.Cnpj
                                                                )
                                                 );
                }
                else
                {
                    var empresa = _repositorioEmpresa.ObterPorId(dtoEmpresa.IdEmpresa);
                    empresa.AdicionarNome(dtoEmpresa.Nome);
                    empresa.AdicionarNomeFantasia(dtoEmpresa.NomeFantasia);
                    empresa.AdicionarWebSite(dtoEmpresa.WebSite);
                    empresa.AdicionarCnpj(dtoEmpresa.Cnpj);
                    _repositorioEmpresa.Alterar(empresa);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DtoEmpresa ObterPorId(int idEmpresa)
        {
            return ObterDtoEmpresa(_repositorioEmpresa.ObterPorId(idEmpresa));
        }

        public DtoPesquisaEmpresa ListaEmpresas(string nome, int page = 1)
        {
            int quantidadeReg = 0;
            int quantidadePag = 0;
            var empresas = _repositorioEmpresa.ObterTodosOndeLazy(x => x.Nome.ToLower().Contains(nome.ToLower())).Page(page, 10, x => x.Id, true, out quantidadeReg, out  quantidadePag).ToList();

            var dto = new DtoPesquisaEmpresa();
            dto.PaginaSelecionada = 1;
            dto.Pesquisa = empresas.Select(ObterDtoEmpresa).ToList();
            dto.TotalRegistro = quantidadeReg;
            dto.TotalPagina = quantidadePag;

            return dto;
        }

        public DtoEmpresa ObterDtoEmpresa(Empresa empresa)
        {
            var dto = new DtoEmpresa();
            dto.IdEmpresa = empresa.Id;
            dto.Nome = empresa.Nome;
            dto.NomeFantasia = empresa.NomeFantasia;
            dto.WebSite = empresa.WebSite;
            dto.Cnpj = FormataCnpj(empresa.Cnpj);
            return dto;                
        }

        public string FormataCnpj(string cnpj)
        {
            if(cnpj.Length == 14)
                return cnpj.Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-");
            return cnpj;
        }

    }
}
