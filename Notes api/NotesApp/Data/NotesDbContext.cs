using Microsoft.EntityFrameworkCore;
using NotesApp.Models.Entities;

namespace NotesApp.Data
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions options) : base(options)
        {
        }

        // create a dbset with type Note with get and set
        // this is how you create a table in the database
        public DbSet<Note> Notes { get; set; }
    }
}
