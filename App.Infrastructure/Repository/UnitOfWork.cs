using App.Core.Entities;
using App.Core.Interfaces;
using App.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ShoppingContext _context;
        private IGenericRepository<Employee> _employeeRepository;
        private IGenericRepository<EmployeeType> _employeeTypeRepository;

        public UnitOfWork(ShoppingContext context)
        {
            _context = context;
        }

        public IGenericRepository<Employee> EmployeeRepository {

            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new GenericRepository<Employee>(_context);

                return _employeeRepository;
            }
        
        }

        public IGenericRepository<EmployeeType> EmployeeTypeRepository
        {

            get
            {
                if (_employeeTypeRepository == null)
                    _employeeTypeRepository = new GenericRepository<EmployeeType>(_context);

                return _employeeTypeRepository;
            }

        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
