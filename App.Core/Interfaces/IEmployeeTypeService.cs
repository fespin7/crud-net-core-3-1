using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Interfaces
{
    public interface IEmployeeTypeService
    {
        Task<IEnumerable<EmployeeType>> List();
    }
}
