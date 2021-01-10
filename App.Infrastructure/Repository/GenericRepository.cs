using App.Core.Interfaces;
using App.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ShoppingContext _context;
        private readonly DbSet<T> dbSet;

        public GenericRepository(ShoppingContext context)
        {
            _context = context;
            dbSet = context.Set<T>();
        }

        public IEnumerable<T> Read()
        {
            return dbSet.ToList();
        }

        public T FindById(int id)
        {
            return dbSet.Find(id);
        }
        
        public void Create(T entity)
        {
           dbSet.Add(entity);
        }

        public void Delete(int id)
        {
            var entity = FindById(id);
           dbSet.Remove(entity);
        }


        public void Update(T entity)
        {
           dbSet.Update(entity);
        }

        public async Task<IEnumerable<T>> ReadAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await FindByIdAsync(id);
            dbSet.Remove(entity);
        }
    }
}
