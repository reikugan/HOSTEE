namespace HOSTEE.Models
{
    public class Folder
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string userId { get; set; }
        public string Name { get; set; }
        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
