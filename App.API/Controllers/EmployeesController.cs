using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.DataTransferObjects;
using App.Core.Entities;
using App.Core.Interfaces;
using App.Core.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeTypeService _employeeTypeService;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeService employeeService, IEmployeeTypeService employeeTypeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _employeeTypeService = employeeTypeService;
            _mapper = mapper;
        }

        [HttpGet] // api/employees
        public async Task<IEnumerable<EmployeeDTO>> Get()
        {
            return await _employeeService.ListEmployee();
        }


        [HttpGet("{employeeId}")] // api/employees/1
        public async Task<EmployeeDTO> GetById(int employeeId)
        {
            var employee = await _employeeService.FindById(employeeId);
            return _mapper.Map<EmployeeDTO>(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Post(EmployeeDTO employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeService.Create(employee);
            return CreatedAtAction(nameof(GetById), new { employeeId = employee.EmployeeId }, employee);
        }

        [HttpPut]
        public EmployeeDTO Put(EmployeeDTO employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            _employeeService.Update(employee);
            return employeeDto;
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> Delete(int employeeId)
        {
            await _employeeService.Delete(employeeId);
            return NoContent();
        }

    }
}
