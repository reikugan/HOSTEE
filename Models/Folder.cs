namespace HOSTEE.Models
{
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
