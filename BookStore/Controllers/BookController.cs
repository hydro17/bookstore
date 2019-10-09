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

    public IActionResult Details(int id)
    {
      Book book = _bookRepository.GetBook(id);
      return View(book);
    }

    [HttpGet]
    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Create(Book book)
    {
      if (ModelState.IsValid)
      {
        _bookRepository.Add(book);
        return RedirectToAction("Details", new { id = book.Id });
      }

      return View();
    }

    public IActionResult Delete(int id)
    {
      _bookRepository.Delete(id);
      return RedirectToAction("index");
    }
  }
}
