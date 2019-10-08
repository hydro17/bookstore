using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
  public class BookController : Controller
  {
    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository)
    {
      this._bookRepository = bookRepository;
    }

    public IActionResult Index()
    {
      IEnumerable<Book> allBooks = _bookRepository.GetAllBooks();
      return View(allBooks);
    }
  }
}
