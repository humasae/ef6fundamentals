// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

//This sentence checks if database exists, and if not, creates it.
//using (PubContext context = new PubContext())
//{
//    context.Database.EnsureCreated();
//}

//Initialize Context in start of class for demo purposes instead of in every Method
PubContext _context = new PubContext();

//AddAuthorWithBooks();
//GetAuthorsWithBooks();
//AddAuthor();
//AddMoreAuthors();
//QueryFilters();
//SkipAndTakeAuthors();
//SortAuthors();
//RetrieveAndUpdateActor();
//RetrieveAndUpdateActorAsNotrackingFails();
//RetrieveAndUpdateActorAsNotracking();
//RetrieveAndUpdateActorContext();
SortAuthors();

void AddAuthor()
{
    var author = new Author { FirstName = "Arturo", LastName = "Pérez Reverte" };
    using var context = new PubContext();
    context.Authors.Add(author);
    //context.Authors.Add(new Author { FirstName = "Rodrigo", LastName = "Cortés" });
    context.SaveChanges();
}

void AddMoreAuthors()
{
    _context.Authors.Add(new Author { FirstName = "Brandon", LastName = "Sanderson" });
    _context.Authors.Add(new Author { FirstName = "Javier", LastName = "Negrete" });
    _context.Authors.Add(new Author { FirstName = "Joe", LastName = "Abercrombie" });
    _context.Authors.Add(new Author { FirstName = "Stephen", LastName = "King" });
    _context.Authors.Add(new Author { FirstName = "Arturo", LastName = "Pérez Reverte" });
    _context.Authors.Add(new Author { FirstName = "BB", LastName = "King" });
    _context.Authors.Add(new Author { FirstName = "JRR", LastName = "Tolkien" });
    _context.Authors.Add(new Author { FirstName = "Christopher", LastName = "Tolkien" });
    _context.Authors.Add(new Author { FirstName = "Dean", LastName = "Koontz" });
    _context.SaveChanges();
}

void AddAuthorWithBooks()
{
    var author = new Author { FirstName = "Juan", LastName = "Gómez Jurado" };
    author.Books.Add(new Book
    {
        Title = "Reina Roja",
        PublishDate = new DateTime(2018, 1, 1)
    });
    author.Books.Add(new Book
    {
        Title = "La Leyenda del Ladrón",
        PublishDate = new DateTime(2012, 1, 1)
    });
    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}

void GetAuthors()
{
    using var context = new PubContext();
    var authors = context.Authors.ToList();
    foreach (var author in authors)
    {
        Console.WriteLine(String.Format("--> {0} {1}", author.FirstName, author.LastName));
    }
}

void GetAuthorsWithBooks()
{
    using var context = new PubContext();
    var authors = context.Authors.Include(a => a.Books).ToList();
    foreach (var author in authors)
    {
        Console.WriteLine(String.Format("--> {0} {1}", author.FirstName, author.LastName));
        foreach(var book in author.Books)
        {
            Console.WriteLine(String.Format("* {0}", book.Title));
        }
    }
}

void QueryFilters()
{
    //var name = "Juan";
    //var authors = _context.Authors.Where(s => s.FirstName == name).ToList();
    var authors = _context.Authors
        .Where(a => EF.Functions.Like(a.FirstName, "J%")).ToList();

    foreach (var author in authors)
    {
        Console.WriteLine(String.Format("--> {0} {1}", author.FirstName, author.LastName));
    }

}

void SkipAndTakeAuthors() 
{
    var groupSize = 2;
    for(int i = 0; i < 5; i++)
    {
        var authors = _context.Authors.Skip(groupSize * i).Take(groupSize).ToList();
        Console.WriteLine($"Group {i}");
        foreach (var author in authors)
        {
            Console.WriteLine(String.Format("--> {0} {1}", author.FirstName, author.LastName));
        }
    }
}

void SortAuthors()
{
    //var authorsByLastName = _context.Authors.OrderBy(a => a.LastName).ToList();
    var authorsByLastName = _context.Authors
        .OrderBy(a => a.LastName)
        .ThenBy(a => a.FirstName).ToList();
    authorsByLastName.ForEach(a => Console.WriteLine($"{a.LastName}, {a.FirstName}"));

    var authorsDescending = _context.Authors
        .OrderByDescending(a => a.LastName)
        .ThenByDescending(a => a.FirstName).ToList();
    Console.WriteLine("Descending!!!:");
    authorsDescending.ForEach(a => Console.WriteLine($"{a.LastName}, {a.FirstName}"));
}

void QueryAggregate()
{
    var author = _context.Authors.OrderByDescending(a => a.LastName)
        .FirstOrDefault(a => a.LastName == "Tolkien");

}

void RetrieveAndUpdateActor()
{
    var author = _context.Authors.FirstOrDefault(a => a.AuthorId == 10);
    if(author != null)
    {
        author.FirstName += "*";
        Console.WriteLine($"Before: {_context.ChangeTracker.DebugView.ShortView}");
        //Only First name is updated
        _context.ChangeTracker.DetectChanges();
        Console.WriteLine($"After: {_context.ChangeTracker.DebugView.ShortView}");

        _context.SaveChanges();
    }
}

void RetrieveAndUpdateActorAsNotrackingFails()
{
    //Marked as no tracking,DBContext wont save the changes
    var author = _context.Authors.AsNoTracking().FirstOrDefault(a => a.AuthorId == 10);
    if (author != null)
    {
        author.FirstName += "+";
        Console.WriteLine($"Before: {_context.ChangeTracker.DebugView.ShortView}");
        _context.ChangeTracker.DetectChanges();
        Console.WriteLine($"After: {_context.ChangeTracker.DebugView.ShortView}");

        _context.SaveChanges();
    }
}

void RetrieveAndUpdateActorAsNotracking()
{
    var authorToUpdate = FindAuthor(10);
    if (!String.IsNullOrEmpty(authorToUpdate?.FirstName) && authorToUpdate.FirstName.Contains("Dean"))
    {
        authorToUpdate.FirstName += ":)";
        //All Author fields are updated
        SaveAuthor(authorToUpdate);
    }
}

Author FindAuthor(int authorId)
{
    // because of the using, at the end of the method, the context get disposed.
    // So, as no context longer exists, no tracking
    using var shortLivedContext = new PubContext();
    return shortLivedContext.Authors.Find(authorId);
}

void SaveAuthor(Author author)
{
    using var shortLivedContextToSave = new PubContext();
    //marks the author as Modified
    //Track via DbSet --> context.Authors.Update(...); DbSet indicates type
    shortLivedContextToSave.Authors.Update(author);
    shortLivedContextToSave.SaveChanges();
}

void RetrieveAndUpdateActorContext()
{
    using var shortLivedContextToSave = new PubContext();
    var authorToUpdate = FindAuthor(10);
    if (!String.IsNullOrEmpty(authorToUpdate?.FirstName) && authorToUpdate.FirstName.Contains("Dean"))
    {
        authorToUpdate.FirstName += "_context";
        //Track via DbContext --> context.Update(...); Context will discover type(s)
        shortLivedContextToSave.Update(authorToUpdate);
        shortLivedContextToSave.SaveChanges();
    }
}


/*
 CPM Commands:

- cretae first migration:
Add-Migration initial_migration -OutputDir ./migrations -StartupProject PublisherConsole

- generate sql scripts (saved in PublisherData\obj\Debug\net6.0, but you can save anywhere):
Script-Migration

- generate scripts from a determined migration:
Script-Migration initial_migration

- after renaming Author Id primary key to AuthorId:
add-migration authoridchange

- In order to apply changes to database:
Update-Database

 */