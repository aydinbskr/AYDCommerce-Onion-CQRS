using AYDCommerce.API.Application.Interfaces.Repositories;
using AYDCommerce.API.Domain.Common;
using AYDCommerce.API.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYDCommerce.API.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _dbContext;
        private DbSet<T> Table { get => _dbContext.Set<T>(); }
        public ReadRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CountAsync(System.Linq.Expressions.Expression<Func<T, bool>>? predicate = null)
        {
            Table.AsNoTracking();
            if (predicate is not null) Table.Where(predicate);

            return await Table.CountAsync();
        }

        public IQueryable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate, bool enableTracking = false)
        {
            IQueryable<T> query = enableTracking ? Table : Table.AsNoTracking();
            return query.Where(predicate);
        }

        public async Task<IList<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false)
        {
            IQueryable<T> query = enableTracking ? Table : Table.AsNoTracking();

            if (include is not null) query = include(query);
            if (predicate is not null) query = query.Where(predicate);
            if (orderBy is not null) query = orderBy(query);
                
            return await query.ToListAsync();
        }

        public async Task<IList<T>> GetAllByPagingAsync(System.Linq.Expressions.Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false, int currentPage = 1, int pageSize = 3)
        {
            IQueryable<T> query = enableTracking ? Table : Table.AsNoTracking();

            if (include is not null) query = include(query);
            if (predicate is not null) query = query.Where(predicate);
            if (orderBy is not null) query = orderBy(query);

            return await query.Skip((currentPage -1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T?> GetAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<T, object>>? include = null, bool enableTracking = false)
        {
            IQueryable<T> query = enableTracking ? Table : Table.AsNoTracking();

            if (include is not null) query = include(query);

            return await query.Where(predicate).FirstOrDefaultAsync();
        }
    }
}
