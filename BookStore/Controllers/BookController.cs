using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
  public class BookController : Controller
  {
    private readonly IBookRepository _bookRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public BookController(IBookRepository bookRepository, IWebHostEnvironment webHostEnvironment)
    {
      this._bookRepository = bookRepository;
      this._webHostEnvironment = webHostEnvironment;
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
    public IActionResult Create(BookCreateViewModel bookCreateViewModel)
    {
      if (ModelState.IsValid)
      {

        string photoUniqueName = null;

        if (bookCreateViewModel.Photo != null)
        {
          string serverImagesFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
          photoUniqueName = Guid.NewGuid() + "_" + bookCreateViewModel.Photo.FileName;
          string serverImageLocation = Path.Combine(serverImagesFolder, photoUniqueName);
          bookCreateViewModel.Photo.CopyTo(new FileStream(serverImageLocation, FileMode.Create));
        }

        Book book = new Book();

        book.Title = bookCreateViewModel.Title;
        book.Author = bookCreateViewModel.Author;
        book.Price = bookCreateViewModel.Price;
        book.Description = bookCreateViewModel.Description;
        book.PhotoUniqueName = photoUniqueName;

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
