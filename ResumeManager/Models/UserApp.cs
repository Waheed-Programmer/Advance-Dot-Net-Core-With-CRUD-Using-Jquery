using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeManager.Models
{
    public class UserApp
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }



        [ForeignKey("Cities")]
        public int CityId { get; set; }
        public virtual Cities Cities { get; set; }
        
        
        [NotMapped]
        public SelectList Genderlist { get; set; }

        [NotMapped]
        public List<SelectListItem> Interestlist { get; set; }

        

        [ForeignKey("Gender")]
        public int GenderId { get; set; }
        public virtual Gender Gender { get; set; }




        [ForeignKey("Image")]
        public int? ImageId { get; set; }
        public virtual Image Image { get; set; }

        public virtual IEnumerable<UserInterest> UserInterests { get; set; }

    }
}
