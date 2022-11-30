using System.ComponentModel.DataAnnotations;

namespace ResumeManager.Models
{
    public class Cities
    {
        [Key]
        public int CityId { get; set; }
        public string CityName { get; set; }

        public virtual List<UserApp> UserApps { get; set; }
    }
}
