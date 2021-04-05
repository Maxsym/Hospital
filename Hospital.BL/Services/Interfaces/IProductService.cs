using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hospital.BL.Responses;
using Hospital.DAL.Domains;

namespace Hospital.BL.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListAsync();
        Task<ProductResponse> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsByCategory(int id);
        Task<ProductResponse> SaveAsync(Product product);
        Task<ProductResponse> UpdateAsync(int id, Product product);
        Task<ProductResponse> DeleteAsync(int id);
    }
}
