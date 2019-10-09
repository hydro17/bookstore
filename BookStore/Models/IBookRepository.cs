using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
  public interface IBookRepository
  {
    Book GetBook(int id);
    IEnumerable<Book> GetAllBooks();
    Book Add(Book book);
    Book Update(Book book);
    Book Delete(int id);
  }
}
