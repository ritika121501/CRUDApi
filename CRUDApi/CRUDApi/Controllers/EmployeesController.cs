using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDApi.Data;
using CRUDApi.Model;

namespace CRUDApi.Controllers
{
    //Icollection, Ienumerable, Ilist
    //----IEnumerable---
    //iterates through the list of items/objects
    //Deferred Execution -- it does not go to database - not a suitable candudate for queries
    //from db

    //---ICollection---
    //Implement IEnumerable
    //Additional features are are to Add/Remove items to the list of items/objects

    //--ILIST---
    //Implements ICollection
    //Additional features to Add/Remove items but it also provides access to add items at a specific
    //position. Indexes are supported with IList
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly CRUDApiContext _context;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(CRUDApiContext context, ILogger<EmployeesController> logger)
        {
            _context = context;
            _logger = logger;

        }
        // ProductWithPriceGreaterThan20 - List<Products>
        // ProductWhoseQuantityIsLessThan5 - List<Product>
        // ProductWhichAreEdible - List<Product>

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            IEnumerable<Employee> employees_enu = _context.Employee;
            ICollection<Employee> employee_col = _context.Employee.ToList();
            IList<Employee> employee_li = _context.Employee.ToList();

            foreach (Employee employee in employee_col)
            {
                _logger.Log(LogLevel.Information, "Collection -" + employee.FirstName);
            }

            foreach (Employee employee in employee_li)
            {
                _logger.Log(LogLevel.Information, "List -" + employee.FirstName);
            }

            foreach (Employee employee in employees_enu)
            {
                _logger.Log(LogLevel.Information, "Enumerable -" + employee.FirstName);
            }

            return null;
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
          if (_context.Employee == null)
          {
              return NotFound();
          }
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpGet("GetEmployeeSalaryHigherThan4000")]
        public List<Employee> GetEmployeeSalaryHigherThan4000()
        {
            List<Employee> listOfEmployees = new List<Employee>();

            Employee employee = new Employee();
            employee.EmployeeId = 1;
            employee.FirstName = "Ritika";
            employee.LastName = "Jha";
            employee.Salary = 4010;
            listOfEmployees.Add(employee);

            Employee employee1 = new Employee();
            employee1.EmployeeId = 2;
            employee1.FirstName = "John";
            employee1.LastName = "Jacob";
            employee1.Salary = 4050;
            listOfEmployees.Add(employee1);

            Employee employee2 = new Employee();
            employee2.EmployeeId = 3;
            employee2.FirstName = "Test";
            employee2.LastName = "Ann";
            employee2.Salary = 5000;
            listOfEmployees.Add(employee2);

            return listOfEmployees;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
          if (_context.Employee == null)
          {
              return Problem("Entity set 'CRUDApiContext.Employee'  is null.");
          }
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employee?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
