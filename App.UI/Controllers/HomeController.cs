using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using App.UI.Models;
using App.Infrastructure.Context;
using App.Infrastructure.Repository;
using App.Core.Interfaces;
using App.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _uow;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            var employeeList = _uow.EmployeeRepository.Read();
            return View(employeeList);
        }

        public IActionResult AddOrEdit(int? id)
        {
            var employee = new Employee();
            var employeeTypeList = _uow.EmployeeTypeRepository.Read();

            if (id.HasValue && id.Value > 0)
            {
                employee = _uow.EmployeeRepository.FindById(id.Value);
            }

            ViewBag.EmployeeTypes = new SelectList(employeeTypeList, "TypeId", "Name");
            return View(employee);

        }

        [HttpPost]
        public JsonResult AddOrEdit(Employee employee)
        {
            if (employee.EmployeeId > 0)
            {
                var emp = _uow.EmployeeRepository.FindById(employee.EmployeeId);
                emp.Name = employee.Name;
                emp.TypeId = employee.TypeId;
                emp.Address = employee.Address;
                emp.EmploymentDate = employee.EmploymentDate;
                _uow.EmployeeRepository.Update(emp);
            }
            else
            {
                _uow.EmployeeRepository.Create(employee);
            }

            _uow.Save();

            return Json(new { result = true });
        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            _uow.EmployeeRepository.Delete(id);
            _uow.Save();

            return Json(new { result = true });
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
