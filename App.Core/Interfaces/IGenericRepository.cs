using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Interfaces
{
    public interface IGenericRepository<T> where T: class
    {
        IEnumerable<T> Read();
        T FindById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
