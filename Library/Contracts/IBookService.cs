using Library.Models;

namespace Library.Contracts
{
    public interface IBookService
    {
        Task AddBookAync(AddBookViewModel model);
        Task AddBookToCoolectionAsync(string userId, BookViewModel book);
        Task<IEnumerable<AllBookViewModel>> GetAllBooksAync();

        Task<AddBookViewModel?> GetBookByIdForEditAsync(int id);
        Task EditBookAsync(AddBookViewModel model, int id);
        Task<BookViewModel?> GetBookByIdAync(int id);
        Task<IEnumerable<AllBookViewModel>> GetMyBooksAync(string userId);
        Task<AddBookViewModel> GetNewAddBookModelAsync();
        Task RemoveBookFromCoolectionAsync(string userId, BookViewModel book);
    }
}
