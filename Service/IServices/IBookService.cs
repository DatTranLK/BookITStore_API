using Entity.Dtos.Book;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IBookService
    {
        Task<ServiceResponse<IEnumerable<BookDto>>> GetBooksWithPagination(int page, int pageSize);
        Task<ServiceResponse<int>> CountAll();
        Task<ServiceResponse<BookDto>> GetBookById(int id);
        Task<ServiceResponse<string>> DisableOrEnableBook(int id);
        Task<ServiceResponse<int>> CreateNewPhysicalBook(Book book);
        Task<ServiceResponse<Book>> UpdatePhysicalBook(int id, Book book);
        Task<ServiceResponse<int>> CreateNewPhysicalBookAndEBook(BookDtoForPhysicalAndEBook bookDtoForPhysicalAndEBook);

        Task<ServiceResponse<IEnumerable<BookShowDto>>> GetBooksShowWithPagination(int page, int pageSize);
        Task<ServiceResponse<int>> CountBooksShow();

        Task<ServiceResponse<IEnumerable<BookShowDtoVer2>>> GetBooksShowVer2WithPagination(int page, int pageSize);
        Task<ServiceResponse<int>> CountBooksShowVer2();
    }
}
