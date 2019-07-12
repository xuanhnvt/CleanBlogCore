using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Nhx.Core
{
    public class ExtendedPagedList<T> : PagedList<T>
    {
        private ExtendedPagedList()
        {

        }

        public static async Task<IPagedList<T>> Create(IQueryable<T> superset, int pageNumber, int pageSize)
        {
            var list = new ExtendedPagedList<T>();
            await list.Init(superset, pageNumber, pageSize);
            return (IPagedList<T>) list;
        }

        async Task Init(IQueryable<T> superset, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "PageNumber cannot be below 1.");

            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "PageSize cannot be less than 1.");

            this.TotalCount = superset == null ? 0 : await superset.CountAsync();
            this.PageSize = pageSize;
            this.PageIndex = pageNumber - 1;
            this.TotalPages = this.TotalCount > 0 ? (int)Math.Ceiling(this.TotalCount / (double)this.PageSize) : 0;
            if (superset == null || this.TotalCount <= 0)
                return;
            this.AddRange(pageNumber == 1 ? await superset.Skip(0).Take(pageSize).ToListAsync() : await superset.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync());
        }
    }

    public static class ExtendedPagedListExtensions
    {
        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> superset, int pageNumber, int pageSize)
        {
            return await ExtendedPagedList<T>.Create(superset, pageNumber, pageSize);
        }
    }
}
