using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialWeb.Domain.Entities.Abstract;
using SocialWeb.Domain.Repositories;
using SocialWeb.Infrastructure.Context;

namespace SocialWeb.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class, IBaseEntity
    {
        private readonly ApplicationDbContext _context;
        protected DbSet<T> table;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        public async Task Add(T entity)
        {
            await table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
           table.Remove(entity);
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await table.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<List<T>> Get(Expression<Func<T, bool>> predicate)
        {
           return await table.Where(predicate).ToListAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await table.ToListAsync();
        }
        public async Task<bool> Any(Expression<Func<T, bool>> predicate)
        {
            return await table.AnyAsync(predicate);
        }

        public async Task<T> GetById(int id)
        {
            return await table.FindAsync(id);
        }

        public void Update(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
        }
    }
}