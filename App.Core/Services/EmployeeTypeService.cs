using App.Core.Entities;
using App.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Services
{
    public class EmployeeTypeService : IEmployeeTypeService
    {
        private readonly IUnitOfWork _uow;
        public EmployeeTypeService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<EmployeeType>> List()
        {
            return await _uow.EmployeeTypeRepository.ReadAsync();
        }
    }
}
