using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApplicationExample.Models;

namespace WebApplicationExample.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        UsersContext db;
        public DepartmentsController(UsersContext context)
        {
            db = context;
            if (!db.Departments.Any())
            {
                db.Departments.Add(new Department { NameDep = "NewDepartment" });
                db.Departments.Add(new Department { NameDep = "MyDepartment" });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Department> Get()
        {
            return db.Departments.ToArray();
        }

        [HttpGet("{id}")]
        public Department Get(int id)
        {
            Department departmentId = db.Departments.FirstOrDefault(x => x.IdDep == id);
            if (departmentId == null)
                return departmentId = null;  
            return departmentId;  
        }
    }
}
