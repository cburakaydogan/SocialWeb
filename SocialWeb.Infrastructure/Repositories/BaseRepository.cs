using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SocialWeb.Domain.Entities.Abstract;
using SocialWeb.Domain.Repositories;
using SocialWeb.Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Internal;
using AutoMapper.QueryableExtensions;

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
        public async Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true,
            int pageIndex = 1, int pageSize = 3)
        {
            IQueryable<T> query = table;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).Select(selector).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else
            {
                return await query.Select(selector).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
        }

        public async Task<TResult> GetFilteredFirstorDefault<TResult>(Expression<Func<T, TResult>> selector,
           Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           bool disableTracking = true)
        {
            IQueryable<T> query = table;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).Select(selector).FirstOrDefaultAsync();
            }
            else
            {
                return await query.Select(selector).FirstOrDefaultAsync();
            }
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