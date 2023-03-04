using Entity.Dtos.Category;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface ICategoryService
    {
        Task<ServiceResponse<IEnumerable<CategoryDto>>> GetCategoriesWithPagination(int page, int pageSize);
        Task<ServiceResponse<CategoryDto>> GetCategoryById(int id);
        Task<ServiceResponse<int>> CountCategories();
        Task<ServiceResponse<string>> DisableOrEnableCategory(int id);
        Task<ServiceResponse<int>> CreateNewCategory(Category category);
        Task<ServiceResponse<Category>> UpdateCategory(int id, Category category);

        Task<ServiceResponse<IEnumerable<CategoryDto>>> GetCategoriesForCusWithPagination(int page, int pageSize);
        Task<ServiceResponse<int>> CountCategoriesForCus();
    }
}
