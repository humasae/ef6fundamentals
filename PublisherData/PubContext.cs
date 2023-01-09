﻿using Microsoft.EntityFrameworkCore;
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
                new Author { AuthorId = 1, FirstName = "Rodrigo", LastName = "Cortés" }
            );

            var authorList = new Author[]
            {
                new Author { AuthorId = 2, FirstName = "Brandon", LastName = "Sanderson" },
                new Author { AuthorId = 3, FirstName = "Javier", LastName = "Negrete" },
                new Author { AuthorId = 4, FirstName = "Joe", LastName = "Abercrombie" },
                new Author { AuthorId = 5, FirstName = "Stephen", LastName = "King" },
                new Author { AuthorId = 6, FirstName = "Arturo", LastName = "Pérez Reverte" },
                new Author { AuthorId = 12, FirstName = "Arturo", LastName = "González Campos" },
                new Author { AuthorId = 7, FirstName = "BB", LastName = "King" },
                new Author { AuthorId = 8, FirstName = "JRR", LastName = "Tolkien" },
                new Author { AuthorId = 9, FirstName = "Christopher", LastName = "Tolkien" },
                new Author { AuthorId = 10, FirstName = "Dean", LastName = "Koontz" },
                new Author { AuthorId = 11, FirstName = "Juan", LastName = "Gómez Jurado" }
            };
            modelBuilder.Entity<Author>().HasData(authorList);

            var someBooks = new Book[]
            {
                new Book { BookId=1, AuthorId= 2, Title="El Imperio Final"},
                new Book { BookId=2, AuthorId= 12, Title="Enhorabuena por tu Fracaso"},
                new Book { BookId=3, AuthorId= 11, Title="Cicatriz"},
                new Book { BookId=4, AuthorId= 11, Title="El Espía de Dios"}

            };
            modelBuilder.Entity<Book>().HasData(someBooks);
        }
    }
}