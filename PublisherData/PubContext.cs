using Microsoft.EntityFrameworkCore;
using PublisherDomain;

namespace PublisherData
{
    public class PubContext:DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = PubDatabase"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, FirstName = "Rodrigo", LastName = "Cortés" }
            );

            var authorList = new Author[]
            {
                new Author { Id = 2, FirstName = "Brandon", LastName = "Sanderson" },
                new Author { Id = 3, FirstName = "Javier", LastName = "Negrete" },
                new Author { Id = 4, FirstName = "Joe", LastName = "Abercrombie" },
                new Author { Id = 5, FirstName = "Stephen", LastName = "King" },
                new Author { Id = 6, FirstName = "Arturo", LastName = "Pérez Reverte" },
                new Author { Id = 7, FirstName = "BB", LastName = "King" },
                new Author { Id = 8, FirstName = "JRR", LastName = "Tolkien" },
                new Author { Id = 9, FirstName = "Christopher", LastName = "Tolkien" },
                new Author { Id = 10, FirstName = "Dean", LastName = "Koontz" },
                new Author { Id = 11, FirstName = "Juan", LastName = "Gómez Jurado" }
            };
            modelBuilder.Entity<Author>().HasData(authorList);
        }
    }
}