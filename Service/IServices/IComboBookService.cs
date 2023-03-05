using Entity.Dtos.ComboBookDTO;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IComboBookService
    {
        Task<ServiceResponse<IEnumerable<ComboBookDTO>>> GetComboBookWithPagination(int page, int pageSize);
        Task<ServiceResponse<ComboBookDTO>> GetComboBookyById(int id);
        Task<ServiceResponse<int>> CountComboBooks();
        Task<ServiceResponse<string>> DisableOrEnableComboBook(int id);
        Task<ServiceResponse<int>> CreateNewComboBook(ComboBook comboBoo, List<int> bookId);
        Task<ServiceResponse<ComboBook>> UpdateComboBook(ComboBook comboBook);

        Task<ServiceResponse<int>> CreateNewComboBookVer2(ComboBook comboBoo);
    }
}
