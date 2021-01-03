using App.Core.Entities;
using App.Core.Interfaces;
using App.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Infrastructure.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ShoppingContext context) : base(context)
        {
        }
    }
}
