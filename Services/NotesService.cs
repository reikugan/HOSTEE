using HOSTEE.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HOSTEE.Services
{
    public class NotesService : INotesService
    {
        private readonly AppDbContext _context;

        public NotesService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Note>> GetNotesAsync(string userId)
        {
            return await _context.Notes.Where(n => n.UserId == userId).ToListAsync();
        } 

        public async Task AddNoteAsync(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNoteAsync(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
            }
        }
    }
}
