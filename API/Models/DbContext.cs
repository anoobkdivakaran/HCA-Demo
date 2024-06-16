using Microsoft.EntityFrameworkCore;

namespace HCA.Models
{
    public class BookDbContext:DbContext
    {
        // Construtor
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }

        //DbSet: Books table
        public DbSet<Book> Books { get; set; }
    }
}
