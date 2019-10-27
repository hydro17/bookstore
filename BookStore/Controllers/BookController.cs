
using BookStore.ModelBinders;
using BookStore.Models;
using BookStore.Models.Books;
using BookStore.Models.Carts;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
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
        private readonly ICartRepository _cartRepository;

        public BookController(IBookRepository bookRepository, IWebHostEnvironment webHostEnvironment, ICartRepository cartRepository)
        {
            this._bookRepository = bookRepository;
            this._webHostEnvironment = webHostEnvironment;
            this._cartRepository = cartRepository;
        }

        //--------------------------------------------------------------
        public IActionResult Index() => View(_bookRepository.GetAll());

        //--------------------------------------------------------------
        public IActionResult Details(int id) => View(_bookRepository.GetById(id));

        //--------------------------------------------------------------
        [HttpGet]
        public IActionResult Create() => View();

        //--------------------------------------------------------------
        [HttpPost]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
        public IActionResult Create(
            [Bind("Title,Author,Description,Price,Photo")]
            BookCreateViewModel bookCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookCreateViewModel);
            }

            string photoUniqueName = null;

            if (bookCreateViewModel.Photo != null)
            {
                string serverImagesFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                photoUniqueName = Guid.NewGuid() + "_" + bookCreateViewModel.Photo.FileName;
                string serverImageLocation = Path.Combine(serverImagesFolder, photoUniqueName);

                using (FileStream fileStream = new FileStream(serverImageLocation, FileMode.Create))
                {
                    bookCreateViewModel.Photo.CopyTo(fileStream);
                }
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

            return RedirectToAction(nameof(Details), new { id = book.Id });
        }

        //--------------------------------------------------------------
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Book book = _bookRepository.GetById(id);

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

            return RedirectToAction(nameof(Index));
        }

        //--------------------------------------------------------------
        [HttpPost]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
        public IActionResult Edit(
            [Bind("Id,Title,Author,Description,Price,Photo,ExistingPhotoUniqueName")]
            BookEditViewModel bookEditViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookEditViewModel);
            }

            string photoUniqueName;

            if (bookEditViewModel.Photo != null)
            {
                string serverImagesFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                photoUniqueName = Guid.NewGuid() + "_" + bookEditViewModel.Photo.FileName;
                string serverImageLocation = Path.Combine(serverImagesFolder, photoUniqueName);

                using (FileStream fileStream = new FileStream(serverImageLocation, FileMode.Create))
                {
                    bookEditViewModel.Photo.CopyTo(fileStream);
                }
            }
            else
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

        //--------------------------------------------------------------
        public IActionResult Delete(int id)
        {
            // If book to delete is in the shopping cart display error
            if (_cartRepository.GetCartItemById(id) != null)
            {
                // http response code 409 is for conflict that prevents the action from being performed
                Response.StatusCode = 409; 
                ViewData["whyBookIsNonRemovable"] = "in-cart";
                return View("NonRemovableBook", id);
            }

            Book deletedBoook = _bookRepository.Delete(id);

            if (deletedBoook == null)
            {
                // http response code 409 is for conflict that prevents the action from being performed
                Response.StatusCode = 409;
                return View("NonRemovableBook", id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
