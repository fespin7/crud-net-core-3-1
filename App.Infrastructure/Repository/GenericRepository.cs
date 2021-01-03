using App.Core.Interfaces;
using App.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
