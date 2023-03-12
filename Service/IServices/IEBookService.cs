using Entity.Dtos.Book;
using Entity.Dtos.EBook;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IEBookService
    {
        Task<ServiceResponse<IEnumerable<EBookDto>>> GetEBooksWithPagination(int page, int pageSize);
        Task<ServiceResponse<int>> CountAll();
        Task<ServiceResponse<EBookDto>> GetEBookById(int id);
        /*Task<ServiceResponse<EBookDto>> GetEBookByBookId(int bookId);*/
        Task<ServiceResponse<int>> CreateNewEBook(EBookDto eBookDto);
        Task<ServiceResponse<string>> ChangeInformationOfEBook(int id, EBookDtoForUpdate eBookDtoForUpdate);

        Task<ServiceResponse<IEnumerable<EBookDtoForAdmin>>> GetEBookWithPagination(int page, int pageSize);

        Task<ServiceResponse<int>> CountEBooks();

        Task<ServiceResponse<IEnumerable<EBookShowDtoVer2>>> GetEBookForCusWithPagination(int page, int pageSize);

        Task<ServiceResponse<int>> CountEBooksForCus();
    }
}
