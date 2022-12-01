using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeManager.Models
{
    public class UserInterest
    {
        public int UserInterestId { get; set; }


        [ForeignKey("UserApp")]
        public int UserId { get; set; }
        public virtual UserApp UserApp { get; set; }
        
        
        [ForeignKey("Interest")]
        public int InterestId { get; set; }
        public virtual Interest Interest { get; set; }
    }
}
