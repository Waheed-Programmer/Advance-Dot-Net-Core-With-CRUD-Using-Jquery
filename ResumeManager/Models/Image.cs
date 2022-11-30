using System.ComponentModel.DataAnnotations;

namespace ResumeManager.Models
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public string FileType { get; set; }

        public virtual UserApp UserApp { get; set; }
    }
}
