using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
  public class SqlBookRepository : IBookRepository
  {
    private readonly AppDbContext _context;

    public SqlBookRepository(AppDbContext context)
    {
      this._context = context;
    }

    public Book Add(Book book)
    {
      _context.Books.Add(book);
      _context.SaveChanges();

      return book;
    }

    public Book Delete(int id)
    {
      Book book = _context.Books.Find(id);

      if (book != null)
      {
        _context.Books.Remove(book);
        _context.SaveChanges();
      }

      return book;
    }

    public IEnumerable<Book> GetAllBooks() => _context.Books;

    public Book GetBook(int id) => _context.Books.Find(id);

    public Book Update(Book bookChanges)
    {
      var book = _context.Books.Attach(bookChanges);
      book.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
      _context.SaveChanges();

      return bookChanges;
    }
  }
}
