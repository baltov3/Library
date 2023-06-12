﻿using Library.Contracts;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public async Task<IActionResult> All()
        {
            var model = await bookService.GetAllBooksAync();
            return View(model);
        }

        public async Task<IActionResult> Mine()
        {
            var model = await bookService.GetMyBooksAync(GetUserId());
            return View(model);
        }

        public async Task<IActionResult> AddToCollection(int id)
        {
            var book = await bookService.GetBookByIdAync(id);
            if (book == null)
            {
                return RedirectToAction(nameof(All));
            }

            var userId = GetUserId();
            await bookService.AddBookToCoolectionAsync(userId, book);
            return RedirectToAction(nameof(All));
        }


        public async Task<IActionResult> RemoveFromCollection(int id)
        {
            var book = await bookService.GetBookByIdAync(id);
            if (book == null)
            {
                return RedirectToAction(nameof(Mine));
            }

            var userId = GetUserId();
            await bookService.RemoveBookFromCoolectionAsync(userId, book);
            return RedirectToAction(nameof(Mine));




        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddBookViewModel model = await bookService.GetNewAddBookModelAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel model)
        {
            decimal rating;
            if (!decimal.TryParse(model.Rating, out rating) || rating<0||rating>10 )
            {
                ModelState.AddModelError(nameof(model.Rating),"invalid rating");
                return View(model);
            }
            if (ModelState.IsValid==false)
            {
                return View(model);
            }
            await bookService.AddBookAync(model);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            AddBookViewModel model = await bookService.GetNewAddBookModelAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            AddBookViewModel? book = await bookService.GetBookByIdForEditAsync(id);

            if (book == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(book);
        }

        public async Task<IActionResult> Edit(int id, AddBookViewModel model)
        {
            decimal rating;

            if (!decimal.TryParse(model.Rating, out rating) || rating < 0 || rating > 10)
            {
                ModelState.AddModelError(nameof(model.Rating), "Rating must be a number between 0 and 10.");

                return View(model);
            }

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await bookService.EditBookAsync(model, id);

            return RedirectToAction(nameof(All));
        }
    }
}
