using App.Core.Entities;
using App.Core.Exceptions;
using App.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _uow;
        public EmployeeService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<Employee>> List()
        {
            return await _uow.EmployeeRepository.ReadAsync();
        }

        public async Task<Employee> FindById(int id)
        {
            return await _uow.EmployeeRepository.FindByIdAsync(id);
        }

        public async Task Create(Employee employee)
        {
            await _uow.EmployeeRepository.CreateAsync(employee);
            await _uow.SaveAsync();
        }

        public async Task Delete(int id)
        {
            await _uow.EmployeeRepository.DeleteAsync(id);
            await _uow.SaveAsync();
        }

        public void Update(Employee employee)
        {
            //Only employees with three months or more in the company can change their Employee type
            var originalEmployee = _uow.EmployeeRepository.FindById(employee.EmployeeId);

            if(originalEmployee != null)
            {
                if(originalEmployee.TypeId != employee.TypeId)
                {
                    if ((DateTime.Now - originalEmployee.EmploymentDate.Value).TotalDays < 90)
                        throw new BusinessException("Only employees with more than 3 months can change their type");
                }
            }
            else
            {
                throw new AppException(HttpStatusCode.NotFound, "Employee doesn't exist");
            }

            originalEmployee.Name = employee.Name;
            originalEmployee.TypeId = employee.TypeId;
            originalEmployee.Phone = employee.Phone;
            originalEmployee.Address = employee.Address;
            _uow.EmployeeRepository.Update(originalEmployee);
            _uow.Save();
        }
    }
}
