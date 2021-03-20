using App.Core.Entities;
using App.Core.Interfaces;
using App.Core.ViewModels;
using App.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly ShoppingContext _context;

        public EmployeeRepository(ShoppingContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeViewModel>> ListEmployeeViewModel()
        {
            return await _context.Employee.Include(employee => employee.Type)
                            .Select(r => new EmployeeViewModel
                            {
                                EmployeeId = r.EmployeeId,
                                Name = r.Name,
                                Address = r.Address,
                                EmploymentDate = r.EmploymentDate,
                                TypeName = r.Type.Name
                            }).ToListAsync();
        }
    }
}
