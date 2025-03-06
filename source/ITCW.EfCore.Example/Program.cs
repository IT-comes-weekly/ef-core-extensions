using ITCW.EfCore.Example.Database;
using ITCW.EfCore.Example.Database.Models;
using ITCW.EfCore.Example.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseSqlite("Data Source=Application.db;"));

builder.Services.AddScoped<IBookRepository, BookRepository>();

using var host = builder.Build();
using var serviceScope = host.Services.CreateScope();

var bookRepo = serviceScope.ServiceProvider.GetService<IBookRepository>() ?? throw new NullReferenceException();

await bookRepo.AddAsync(
    new Book()
    {
        Id = Guid.CreateVersion7(),
        Name = "Book 1",
    },
    false);

await bookRepo.SaveChangesAsync();

await bookRepo.AddAsync(
    new Book()
    {
        Id = Guid.CreateVersion7(),
        Name = "Book 2",
    });

var books = await bookRepo.GetAsync(asNoTracking: false);

foreach (var book in books)
{
    Console.WriteLine(book.Name);
}

await bookRepo.DeleteRangeAsync(books);
