using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcCrudApp.Data;
using MvcCrudApp.Models;
using MvcCrudApp.Models.Domain;

namespace MvcCrudApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MvcDbContext _dbContext;

        public EmployeesController(MvcDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel employeeDto)
        {
            Employee emp = new Employee{
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Salary = employeeDto.Salary,
                DateOfBirth = employeeDto.DateOfBirth,
                Department = employeeDto.Department
            };
            await _dbContext.Employees.AddAsync(emp);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Read
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Employee> emps = await _dbContext.Employees.ToListAsync();

            return View(emps);
        }

        // Update
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var emp = await _dbContext.Employees.SingleOrDefaultAsync(e => e.Id == id);
            if(emp != null)
            {
                UpdateEmployeeViewModel updated = new UpdateEmployeeViewModel{
                    Id = emp.Id,
                    Name = emp.Name,
                    Email = emp.Email,
                    Salary = emp.Salary,
                    DateOfBirth = emp.DateOfBirth,
                    Department = emp.Department
                };

                return await Task.Run(() => View("View",updated));
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel emp)
        {
            Employee employee = await _dbContext.Employees.FindAsync(emp.Id);
            if(employee != null)
            {
                employee.Name = emp.Name;
                employee.Email = emp.Email;
                employee.Salary = emp.Salary;
                employee.DateOfBirth = emp.DateOfBirth;
                employee.Department = emp.Department;

                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        } 

        // Delete
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel emp)
        {
            var empl = await _dbContext.Employees.FindAsync(emp.Id);
            if(empl != null)
            {
                _dbContext.Employees.Remove(empl);
                await _dbContext.SaveChangesAsync();
            }
             return RedirectToAction("Index");
        }
    }
}