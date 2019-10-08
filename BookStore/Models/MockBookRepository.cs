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
        new Book{Id=3, Author="Author3", Title="Title3", Price=5.1111m},
        new Book{Id=3, Author="Author3", Title="Title3", Price=5.1111m},
        new Book{Id=3, Author="Author3", Title="Title3", Price=5.1111m},
        new Book{Id=3, Author="Author3", Title="Title3", Price=5.1111m},
        new Book{Id=3, Author="Author3", Title="Title3", Price=5.1111m}
      };
    }

    public IEnumerable<Book> GetAllBooks()
    {
      return _bookList;
    }

    public Book GetBook(int id)
    {
      return _bookList.Where(b => b.Id == id).FirstOrDefault();
    }
  }
}
