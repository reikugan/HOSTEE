using HOSTEE.Models;

namespace HOSTEE.Services
{
    public interface INotesService
    {
        Task<List<Note>> GetNotesAsync(string userId);
        Task AddNoteAsync(Note note);
        Task DeleteNoteAsync(int id);
    }
}
