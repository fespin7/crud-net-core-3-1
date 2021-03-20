using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IEmployeeRepository EmployeeRepository { get; }
        IGenericRepository<EmployeeType> EmployeeTypeRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
