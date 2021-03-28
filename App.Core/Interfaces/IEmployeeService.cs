using App.Core.DataTransferObjects;
using App.Core.Entities;
using App.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeViewModel>> ListEmployeeViewModel();
        Task<IEnumerable<EmployeeDTO>> ListEmployee();
        Task<IEnumerable<Employee>> List();
        Task<Employee> FindById(int id);
        Task Create(Employee entity);
        void Update(Employee entity);
        Task Delete(int id);

    }
}
