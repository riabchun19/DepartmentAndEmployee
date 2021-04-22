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
        public async Task<ActionResult<IEnumerable<Department>>> Get()
        {
            return await db.Departments.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> Get(int id)
        {
            Department department = await db.Departments.FirstOrDefaultAsync(x => x.IdDep == id);
            if (department == null)
                return NotFound();
            return new ObjectResult(department);
        }

        [HttpPost]
        public async Task<ActionResult<Department>> Post(Department department)
        {
            if (department == null)
            {
                return BadRequest();
            }

            db.Departments.Add(department);
            await db.SaveChangesAsync();
            return Ok(department);
        }

        [HttpPut]
        public async Task<ActionResult<Department>> Put(Department department)
        {
            if (department == null)
            {
                return BadRequest();
            }
            if (!db.Departments.Any(x => x.IdDep == department.IdDep))
            {
                return NotFound();
            }

            db.Update(department);
            await db.SaveChangesAsync();
            return Ok(department);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> Delete(int id)
        {
            Department department = db.Departments.FirstOrDefault(x => x.IdDep == id);
            if (department == null)
            {
                return NotFound();
            }
            db.Departments.Remove(department);
            await db.SaveChangesAsync();
            return Ok(department);
        }
    }
}
