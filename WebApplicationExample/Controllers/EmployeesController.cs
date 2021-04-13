using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<Employee> Get()
        {
            return db.Employees.ToArray();
        }

        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            Employee employ = db.Employees.FirstOrDefault(x => x.IdEmp == id);
            if (employ == null)
                return employ = null;
            return employ;
        }
    }
}
