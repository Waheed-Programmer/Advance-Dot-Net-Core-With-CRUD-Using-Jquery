using System.ComponentModel.DataAnnotations;

namespace ResumeManager.Models
{
    public class Interest
    {
        [Key]
        public int InterestId { get; set; }
        public string InterestName { get; set; }

        public virtual IEnumerable<UserInterest> UserInterests { get; set; }
    }
}
