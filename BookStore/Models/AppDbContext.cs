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

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //  modelBuilder.Entity<Book>().HasData(
    //    new Book
    //    {
    //      Title = "Wiedźmin Miecz przeznaczenia",
    //      Author = "Andrzej Sapkowski",
    //      Price = 28.99m,
    //      PhotoUniqueName = "3d4cdb83-8fb8-42aa-a05c-fb2f7d5d1e6d_wiedzmin-miecz-przeznaczenia.jpg"
    //    }
    //  );
    //}
  }
}
