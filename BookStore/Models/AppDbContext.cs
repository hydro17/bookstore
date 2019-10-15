using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Book>().HasData(
        new Book
        {
          Id = 1,
          Title = "Wiedźmin Miecz przeznaczenia",
          Author = "Andrzej Sapkowski",
          Price = 28.99m,
          PhotoUniqueName = "3d4cdb83-8fb8-42aa-a05c-fb2f7d5d1e6d_wiedzmin-miecz-przeznaczenia.jpg"
        },
        new Book
        {
          Id = 2,
          Title = "Kongres futurologiczny",
          Author = "Stanisław Lem",
          Price = 18.89m,
          PhotoUniqueName = "lem-kongres-futurologiczny.jpg"
        },
        new Book
        {
          Id = 3,
          Title = "Czerwona księga",
          Author = "Carl Gustav Jung",
          Price = 87.99m,
          PhotoUniqueName = "czerwona-ksiega.jpg"
        },
        new Book
        {
          Id = 4,
          Title = "Dywizjon 303. Walka i codzienność",
          Author = "Richard King",
          Price = 25.69m,
          PhotoUniqueName = "dywizjon-303.jpg"
        },
        new Book
        {
          Id = 5,
          Title = "Historia bez cenzury 4",
          Author = "Wojciech Drewniak",
          Price = 28.99m,
          PhotoUniqueName = "historia-bez-cenzury.jpg"
        }
      );
    }
  }
}
