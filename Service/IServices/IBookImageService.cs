using Entity.Dtos.BookImage;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IBookImageService
    {
        Task<ServiceResponse<IEnumerable<BookImageDto>>> GetBookImageWithPagination(int page, int pageSize);
        Task<ServiceResponse<IEnumerable<BookImageDto>>> GetBookImageByBookId(int bookId);
        Task<ServiceResponse<BookImageDto>> GetBookImageById(int id);
        Task<ServiceResponse<int>> CountBookImages();
        Task<ServiceResponse<string>> DeleteBookImage(int id);
        Task<ServiceResponse<int>> CreateNewBookImage(BookImage bookImage);
        Task<ServiceResponse<int>> CreateNewEBookImage(BookImage bookImage);
        Task<ServiceResponse<BookImage>> UpdateBookImage(int id, BookImage bookImage);
    }
}
