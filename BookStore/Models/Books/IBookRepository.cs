using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Books
{
  public interface IBookRepository
  {
    Book GetById(int id);
    IEnumerable<Book> GetAll();
    Book Add(Book book);
    Book Update(Book book);
    Book Delete(int id);
  }
}
