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
        Task<ServiceResponse<DetailComboBookDTO>> GetDetailComboBookyById(int id);
        Task<ServiceResponse<IEnumerable<DetailComboBookDTO>>> GetListInDetailOfComboBookId(int id);
        //Them moi combodetail khi da co san comboid 
        Task<ServiceResponse<int>> CreateNewDetailComboBook(int comboBookId, List<int> bookId);
        
        Task<ServiceResponse<string>> RemoveDetailComboBook(int detailComboBookId);
    }
}
