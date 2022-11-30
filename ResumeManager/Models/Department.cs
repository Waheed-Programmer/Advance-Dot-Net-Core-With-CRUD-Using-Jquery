using System.ComponentModel.DataAnnotations;

namespace ResumeManager.Models
{
    public class Department
    {
        [Key]
        public int Did { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Employee> Employees { get; set; }
    }
}
