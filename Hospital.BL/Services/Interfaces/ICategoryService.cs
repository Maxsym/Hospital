using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hospital.BL.Responses;
using Hospital.DAL.Domains;

namespace Hospital.BL.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> ListAsync();
        Task<CategoryResponse> GetByIdAsync(int id);
        Task<CategoryResponse> SaveAsync(Category category);
        Task<CategoryResponse> UpdateAsync(int id, Category category);
        Task<CategoryResponse> DeleteAsync(int id);
    }
}
