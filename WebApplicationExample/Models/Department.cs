using System.ComponentModel.DataAnnotations;

namespace WebApplicationExample.Models
{
    public class Department
    {
        [Key]
        public int IdDep { get; set; }
        public string NameDep { get; set; }
    }
}
