using Entity.Dtos.Book;
using Entity.Dtos.ComboBookDTO;
using Entity.Dtos.DetailComboBookDTO;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IDetailComboBookService
    {
        Task<ServiceResponse<IEnumerable<ListBookOfCombo>>> GetListInDetailOfComboBookId(int id);
        //Them moi combodetail khi da co san comboid 
        Task<ServiceResponse<int>> CreateNewDetailComboBook(int comboBookId, List<int> bookId);
        Task<ServiceResponse<int>> CreateNewDetailComboBookVer2(DetailComboBook detailComboBook);

        Task<ServiceResponse<string>> RemoveDetailComboBook(int detailComboBookId);
        Task<ServiceResponse<IEnumerable<DetailComboBookDtoShow>>> GetDetailOfComboBookIdWithPagination(int id, int page, int pageSize);
    }
}
