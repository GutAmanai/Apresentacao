using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Apresentacao.Dominio.Entidades;
using Apresentacao.Dominio.Repositorio;
using NHibernate;
using NHibernate.Linq;

namespace Apresentacao.Persistencia.Repositorio
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : EntidadeBase
    {
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        protected ISession _sessao
        {
            get { return ((UnidadeDeTrabalho)_unidadeDeTrabalho).Sessao; }
        }

        public RepositorioBase(IUnidadeDeTrabalho unidadeDeTrabalho)
        {
            _unidadeDeTrabalho = unidadeDeTrabalho;
        }

        public virtual int Count(Expression<Func<T, bool>> expressao)
        {
            return _sessao.Query<T>().Count(expressao);
        }

        public virtual bool Any(Expression<Func<T, bool>> expressao)
        {
            return _sessao.Query<T>().Any(expressao);
        }

        public virtual bool Contains(T entidade)
        {
            return _sessao.Query<T>().Contains(entidade);
        }

        public virtual T ObterPorId(int id)
        {
            return _sessao.Get<T>(id);
        }

        public virtual ICollection<T> ObterTodos()
        {
            return _sessao.Query<T>().ToList();
        }

        public virtual IQueryable<T> ObterTodosOndeLazy(Expression<Func<T, bool>> expressao)
        {
            return _sessao.Query<T>().Where(expressao);
        }

        public IQueryable<T> ObterTodosLazy()
        {
            return _sessao.Query<T>();
        }

        public virtual IEnumerable<T> ObterTodosOnde(Expression<Func<T, bool>> expressao)
        {
            return _sessao.Query<T>().Where(expressao).ToList();
        }

        public virtual void Adicionar(T entidade)
        {           
            _unidadeDeTrabalho.Inicializar();
            _sessao.Save(entidade);
            _unidadeDeTrabalho.Salvar();
            _unidadeDeTrabalho.Dispose();
        }

        public virtual void Remover(T entidade)
        {
            _unidadeDeTrabalho.Inicializar();
            _sessao.Delete(entidade);
            _unidadeDeTrabalho.Salvar();
            _unidadeDeTrabalho.Dispose();
        }

        public void Alterar(T entidade)
        {
            _unidadeDeTrabalho.Inicializar();
            _sessao.Update(entidade);
            _unidadeDeTrabalho.Salvar();
            _unidadeDeTrabalho.Dispose();
        }
    }
}
