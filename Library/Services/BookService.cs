using Library.Contracts;
using Library.Data;
using Library.Data.Models;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext dbContext;

        public BookService(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddBookAync(AddBookViewModel model)
        {
           Book book = new Book()
           {
               Title = model.Title,
               Description = model.Description,
               ImageUrl=model.Url,
               CategoryId= model.CategoryId,
               Author = model.Author,
               Rating=decimal.Parse(model.Rating)
           };
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddBookToCoolectionAsync(string userId, BookViewModel book)
        {
           bool alreadyAdded = await dbContext.IdentityUserBooks.AnyAsync(x=>x.CategoryId == userId&&book.Id==x.BookId);
            if (alreadyAdded==false)
            {
                var userBook = new IdentityUserBook()
                {
                    CategoryId = userId,
                    BookId = book.Id,
                };
                await dbContext.IdentityUserBooks.AddAsync(userBook);
                await dbContext.SaveChangesAsync();
            }
           
        }

        public async Task<IEnumerable<AllBookViewModel>> GetAllBooksAync()
        {
            IEnumerable<AllBookViewModel> allBookViewModels = await dbContext.Books.Select(
                b => new AllBookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author=b.Author,
                    ImageUrl=b.ImageUrl,
                    Rating=b.Rating,
                    Category = b.Category.Name,
                   
                    
                }).ToListAsync();
            return allBookViewModels;
        }

        public async Task<BookViewModel?> GetBookByIdAync(int id)
        {
            return await dbContext.Books.Where(b=>b.Id==id).Select(
                b=> new BookViewModel()
                {
                    Id=b.Id,
                    Title=b.Title,
                    Author=b.Author,
                    ImageUrl=b.ImageUrl,
                    Rating=b.Rating,
                    Description=b.Description,
                    CategoryId=b.CategoryId
                }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AllBookViewModel>> GetMyBooksAync(string userId)
        {
            IEnumerable<AllBookViewModel> allBookViewModels = await dbContext.IdentityUserBooks.Where(b=>b.CategoryId==userId).Select(
                b => new AllBookViewModel()
                {
                    Id = b.Book.Id,
                    Title = b.Book.Title,
                    Author = b.Book.Author,
                    ImageUrl = b.Book.ImageUrl,
                    Rating = b.Book.Rating,
                    Category = b.Book.Category.Name,
                    Description = b.Book.Description

                }).ToListAsync();
            return allBookViewModels;
        }

        public async Task<AddBookViewModel> GetNewAddBookModelAsync()
        {
            var categories = await dbContext.Categories.Select(c => new CategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name
            }).ToListAsync();
            var model = new AddBookViewModel()
            {
                Categories = categories
            };
            return model;
        }

        public async Task RemoveBookFromCoolectionAsync(string userId, BookViewModel book)
        {

            var userBook = await dbContext.IdentityUserBooks.FirstOrDefaultAsync(x => x.BookId == book.Id && userId == x.CategoryId);
                
                if (userBook!=null)
                {
                    dbContext.IdentityUserBooks.Remove(userBook);
                    await dbContext.SaveChangesAsync();
                }
                
            
        }
    }
}
