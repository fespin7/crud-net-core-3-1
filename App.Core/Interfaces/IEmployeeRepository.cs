using App.Core.DataTransferObjects;
using App.Core.Entities;
using App.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Interfaces
{
    public interface IEmployeeRepository: IGenericRepository<Employee>
    {
        Task<IEnumerable<EmployeeViewModel>> ListEmployeeViewModel();
        Task<IEnumerable<EmployeeDTO>> ListEmployee();
    }
}
