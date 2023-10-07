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
    internal class WriteRepository<T> : IWriteRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _dbContext;
        private DbSet<T> Table { get => _dbContext.Set<T>(); }
        public WriteRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public async Task AddRangeAsync(IList<T> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public async Task HardDeleteAsync(T entity)
        {
            await Task.Run(() => Table.Remove(entity));
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => Table.Update(entity));
            return entity;
        }

        public async Task HardDeleteRangeAsync(IList<T> entities)
        {
            await Task.Run(() => Table.RemoveRange(entities));
        }
    }
}
