using System;
using System.Linq;
using System.Linq.Expressions;

namespace Apresentacao.Servicos
{
    public static class PageExtension
    {
        public static IQueryable<T> Page<T, TResult>(this IQueryable<T> query, int pageNum, int pageSize, Expression<Func<T, TResult>> orderByProperty, bool isAscendingOrder, out int rowsCount, out int pageCount)
        {
            if (pageSize <= 0)
                pageSize = 10;

            rowsCount = query.Count();
            pageCount = 1;

            if (rowsCount <= pageSize || pageNum <= 0)
                pageNum = 1;

            if (rowsCount > pageSize)
                pageCount = (int)Math.Ceiling((rowsCount / (decimal)pageSize));

            int excludedRows = (pageNum - 1) * pageSize;

            if (isAscendingOrder)
                query = query.OrderBy(orderByProperty);
            else
                query = query.OrderByDescending(orderByProperty);

            return query.Skip(excludedRows).Take(pageSize);
        }
    }
}
