using BookStore.Models;
using BookStore.Models.Books;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Extensions
{
  public static class ModelBuilderExtensions
  {
    public static void Seed(this ModelBuilder modelBuilder)
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
        },
        new Book
        {
            Id = 6,
            Title = "Mitologia. Wierzenia i podania Greków i Rzymian",
            Author = "Jan Parandowski",
            Price = 32.99m,
            PhotoUniqueName = "mitologia.jpg"
        },
        new Book
        {
            Id = 7,
            Title = "Władca pierścieni, Dwie wieże",
            Author = "J.R.R. Tolkien",
            Price = 55.59m,
            PhotoUniqueName = "the-two-towers.jpg"
        },
        new Book
        {
            Id = 8,
            Title = "Leonardo da Vinci",
            Author = "Walter Isaacson",
            Price = 34.85m,
            PhotoUniqueName = "lonardo-da-vinci.jpg"
        },
        new Book
        {
            Id = 9,
            Title = "Harry Potter i Kamień Filozoficzny",
            Author = "J.K. Rowling",
            Price = 23.99m,
            PhotoUniqueName = "harry-potter-kamien-filozoficzny.jpg"
        },
        new Book
        {
            Id = 10,
            Title = "Olmekowie Najstarsza tajemnica Meksyku",
            Author = "Childress David Hatcher",
            Price = 20.99m,
            PhotoUniqueName = "olmekowie-najstarsza-tajemnica-meksyku.jpg"
        }
      );
    }
  }
}
