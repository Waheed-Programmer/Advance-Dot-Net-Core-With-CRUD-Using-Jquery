using System.ComponentModel.DataAnnotations;

namespace ResumeManager.Models
{
    public class Gender
    {
        [Key]
        public int GenderId { get; set; }
        public string GenderName { get; set; }

        public virtual List<UserApp> UserApps { get; set; }
    }
}
