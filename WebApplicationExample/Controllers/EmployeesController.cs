using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationExample.Models;

namespace WebApplicationExample.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        UsersContext db;
        public EmployeesController(UsersContext context)
        {
            db = context;
            if (!db.Employees.Any())
            {
                db.Employees.Add(new Employee { Name = "Tom", Age = 26, Salary = 1200, Department = 2 });
                db.Employees.Add(new Employee { Name = "Alice", Age = 31, Salary = 1300, Department = 1 });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Get()
        {
            return await db.Employees.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            Employee employee = await db.Employees.FirstOrDefaultAsync(x => x.IdEmp == id);
            if (employee == null)
                return NotFound();
            return new ObjectResult(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Post(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }

            db.Employees.Add(employee);
            await db.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpPut]
        public async Task<ActionResult<Employee>> Put(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            if (!db.Employees.Any(x => x.IdEmp == employee.IdEmp))
            {
                return NotFound();
            }

            db.Update(employee);
            await db.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> Delete(int id)
        {
            Employee employee = db.Employees.FirstOrDefault(x => x.IdEmp == id);
            if (employee == null)
            {
                return NotFound();
            }
            db.Employees.Remove(employee);
            await db.SaveChangesAsync();
            return Ok(employee);
        }
    }
}
