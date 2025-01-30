using System.ComponentModel.DataAnnotations;
namespace HOSTEE.Models
{
    public class Note
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int? FolderId { get; set; }
        public Folder Folder { get; set; }
    }
}
