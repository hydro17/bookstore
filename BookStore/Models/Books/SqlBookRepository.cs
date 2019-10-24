using BookStore.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Books
{
    public class SqlBookRepository : IBookRepository
    {
        private readonly AppDbContext _dbContext;

        public SqlBookRepository(AppDbContext context)
        {
            this._dbContext = context;
        }

        public Book Add(Book book)
        {
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

            return book;
        }

        public Book Delete(int id)
        {
            Book book = _dbContext.Books.Find(id);

            // TODO: add checking if the book to delete is a part of an existing order
            try
            {
                if (book != null)
                {
                    _dbContext.Books.Remove(book);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return null;
            }

            return book;
        }

        public IEnumerable<Book> GetAll() => _dbContext.Books;

        public Book GetById(int id) => _dbContext.Books.Find(id);

        public Book Update(Book bookChanges)
        {
            EntityEntry book = _dbContext.Books.Attach(bookChanges);
            book.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();

            return bookChanges;
        }
    }
}
