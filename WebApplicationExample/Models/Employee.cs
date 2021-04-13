using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationExample.Models
{ 
    public class Employee
    {
        [Key]
        public int IdEmp { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; } 
        public int Department { get; set; }
    }
}
