using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SocialWeb.Domain.Entities.Abstract;

namespace SocialWeb.Domain.Repositories
{
    public interface IRepository<T> where T : IBaseEntity
    {
        Task<List<T>> GetAll();
        Task<List<T>> Get(Expression<Func<T, bool>> predicate);
        Task<T> GetById(int id);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<bool> Any(Expression<Func<T, bool>> predicate);
    }
}