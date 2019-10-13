using BookStore.ModelBinders;
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

    //--------------------------------------------------------------
    public IActionResult Index()
    {
      IEnumerable<Book> allBooks = _bookRepository.GetAllBooks();
      return View(allBooks);
    }

    //--------------------------------------------------------------
    public IActionResult Details(int id)
    {
      Book book = _bookRepository.GetBook(id);
      return View(book);
    }

    //--------------------------------------------------------------
    [HttpGet]
    public IActionResult Create() => View();

    //--------------------------------------------------------------
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

        Book book = new Book
        {
          Title = bookCreateViewModel.Title,
          Author = bookCreateViewModel.Author,
          Price = bookCreateViewModel.Price,
          Description = bookCreateViewModel.Description,
          PhotoUniqueName = photoUniqueName
        };

        _bookRepository.Add(book);

        return RedirectToAction("Details", new { id = book.Id });
      }

      return View();
    }

    //--------------------------------------------------------------
    [HttpGet]
    public IActionResult Edit(int id)
    {
      Book book = _bookRepository.GetBook(id);

      if (book != null)
      {
        BookEditViewModel bookEditViewModel = new BookEditViewModel
        {
          Id = book.Id,
          Title = book.Title,
          Author = book.Author,
          Price = book.Price,
          Description = book.Description,
          ExistingPhotoUniqueName = book.PhotoUniqueName
        };

        return View(bookEditViewModel);
      }

      return RedirectToAction("index");
    }

    //--------------------------------------------------------------
    [HttpPost]
    public IActionResult Edit(BookEditViewModel bookEditViewModel)
    {
      if (ModelState.IsValid)
      {
        string photoUniqueName = null;

        if (bookEditViewModel.Photo != null)
        {
          string serverImagesFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
          photoUniqueName = Guid.NewGuid() + "_" + bookEditViewModel.Photo.FileName;
          string serverImageLocation = Path.Combine(serverImagesFolder, photoUniqueName);
          bookEditViewModel.Photo.CopyTo(new FileStream(serverImageLocation, FileMode.Create));
        } else
        {
          photoUniqueName = bookEditViewModel.ExistingPhotoUniqueName;
        }

        Book book = new Book
        {
          Id = bookEditViewModel.Id,
          Title = bookEditViewModel.Title,
          Author = bookEditViewModel.Author,
          Price = bookEditViewModel.Price,
          Description = bookEditViewModel.Description,
          PhotoUniqueName = photoUniqueName
        };

        _bookRepository.Update(book);

        return RedirectToAction("Details", new { id = book.Id });
      }

      return View();
    }

    //--------------------------------------------------------------
    public IActionResult Delete(int id)
    {
      _bookRepository.Delete(id);
      return RedirectToAction("index");
    }
  }
}
