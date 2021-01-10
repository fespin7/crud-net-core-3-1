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
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeTypeService _employeeTypeService;

        public HomeController(ILogger<HomeController> logger, IEmployeeService employeeService, IEmployeeTypeService employeeTypeService)
        {
            _logger = logger;
            _employeeService = employeeService;
            _employeeTypeService = employeeTypeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var employeeList = await _employeeService.List();
            return View(employeeList);
        }

        public async Task<IActionResult> AddOrEdit(int? id)
        {
            var employee = new Employee();
            var employeeTypeList = await _employeeTypeService.List();

            if (id.HasValue && id.Value > 0)
            {
                employee = await _employeeService.FindById(id.Value);
            }

            ViewBag.EmployeeTypes = new SelectList(employeeTypeList, "TypeId", "Name");
            return View(employee);

        }

        [HttpPost]
        public async Task<JsonResult> AddOrEdit(Employee employee)
        {
            if (employee.EmployeeId > 0)
            {
                _employeeService.Update(employee);
            }
            else
            {
                await _employeeService.Create(employee);
            }

            return Json(new { result = true });
        }


        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await  _employeeService.Delete(id);
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
