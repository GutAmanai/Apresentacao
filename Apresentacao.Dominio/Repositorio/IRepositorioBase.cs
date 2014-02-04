using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Apresentacao.Dominio.Entidades;

namespace Apresentacao.Dominio.Repositorio
{
    public interface IRepositorioBase<T> where T : EntidadeBase
    {
        int Count(Expression<Func<T, bool>> expressao);
        bool Any(Expression<Func<T, bool>> expressao);
        bool Contains(T entidade);
        T ObterPorId(int id);
        ICollection<T> ObterTodos();
        IQueryable<T> ObterTodosOndeLazy(Expression<Func<T, bool>> expressao);
        IQueryable<T> ObterTodosLazy();
        IEnumerable<T> ObterTodosOnde(Expression<Func<T, bool>> expressao);
        void Adicionar(T entidade);
        void Remover(T entidade);
        void Alterar(T entidade);
    }
}
