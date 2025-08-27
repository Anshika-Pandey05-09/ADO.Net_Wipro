using Day33_EmployeeDirectoryMvcDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Day33_EmployeeDirectoryMvcDemo.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeDirectoryDbContext _context;

        public EmployeesController(EmployeeDirectoryDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var employees = _context.Employees.ToList();
            return View(employees);
        }
    }
}