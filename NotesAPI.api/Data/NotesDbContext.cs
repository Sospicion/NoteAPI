using Microsoft.EntityFrameworkCore;
using NotesAPI.api.Models.DomainModel;

namespace NotesAPI.api.Data
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Note> Notes { get; set; }
    }
}
