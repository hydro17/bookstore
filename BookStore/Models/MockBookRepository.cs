using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
  public class MockBookRepository : IBookRepository
  {
    private IList<Book> _bookList;

    public MockBookRepository()
    {
      _bookList = new List<Book>
      {
        new Book{Id=1, Author="Author1", Title="Title1", Price=10.515m},
        new Book{Id=2, Author="Author2", Title="Title2", Price=5.5m},
        new Book{Id=3, Author="Author3", Title="Title3", Price=5.1111m},
        new Book{Id=4, Author="Author3", Title="Title3", Price=5.1111m},
        new Book{Id=5, Author="Author3", Title="Title3", Price=5.1551m},
        new Book{Id=6, Author="Author3", Title="Title3", Price=5.1111m},
        new Book{Id=7, Author="Author3", Title="Title3", Price=5.1111m}

        //new Book{Id=1, Author="Author1", Title="Title1", Price=10.515},
        //new Book{Id=2, Author="Author2", Title="Title2", Price=5.5},
        //new Book{Id=3, Author="Author3", Title="Title3", Price=5.1111},
        //new Book{Id=4, Author="Author3", Title="Title3", Price=5.1111},
        //new Book{Id=5, Author="Author3", Title="Title3", Price=5.1551},
        //new Book{Id=6, Author="Author3", Title="Title3", Price=5.1111},
        //new Book{Id=7, Author="Author3", Title="Title3", Price=5.1111},
      };
    }

    public Book Add(Book book)
    {
      book.Id = _bookList.Max(b => b.Id) + 1;
      _bookList.Add(book);

      return book;
    }

    public Book Delete(int id)
    {
      Book book = _bookList.FirstOrDefault(b => b.Id == id);

      if (book != null)
      {
        _bookList.Remove(book);
      }

      return book;
    }

    public IEnumerable<Book> GetAllBooks()
    {
      return _bookList;
    }

    public Book GetBook(int id)
    {
      return _bookList.FirstOrDefault(b => b.Id == id);
    }

    public Book Update(Book bookChanges)
    {
      Book book = _bookList.FirstOrDefault(b => b.Id == bookChanges.Id);

      if (book != null) {
        book.Title = bookChanges.Title;
        book.Author = bookChanges.Author;
        book.Description = bookChanges.Description;
        book.Price = bookChanges.Price;
        book.PhotoUniqueName = bookChanges.PhotoUniqueName;
      }

      return book;
    }
  }
}
