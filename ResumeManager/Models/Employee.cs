using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeManager.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
        public string FullName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        
        [ForeignKey("Departments")]
        public int Did { get; set; }
        public virtual Department Departments { get; set; }
    }
}
