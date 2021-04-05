using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hospital.BL.Responses;
using Hospital.BL.Services.Interfaces;
using Hospital.DAL.Domains;
using Hospital.DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace Hospital.BL.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IUnitOfWork  unitOfWork,  IProductRepository productRepository, ICategoryRepository categoryRepository,  ILogger<ProductService>  logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public Task<IEnumerable<Product>> ListAsync()
        {
            return _productRepository.GetAllAsync();
        }

        public async Task<ProductResponse> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return new ProductResponse("product not found");
            }

            return new ProductResponse(product);
        }

        public async Task<IEnumerable<Product>> GetAllProductsByCategory(int id)
        {
            return await _productRepository.Get((x => x.CategoryId == id));
        }

        public async Task<ProductResponse> SaveAsync(Product product)
        {
            try
            {
                var existingCategory = await _categoryRepository.GetByIdAsync(product.CategoryId);
                if (existingCategory == null)
                {
                    return new ProductResponse("Category is invalid");
                }

                if (await IsProductAlreadyExistsWithCondition(x => x.Code ==  product.Code))
                {
                    return new ProductResponse("Code is already used by different product");
                }

                await _productRepository.AddAsync(product);
                await _unitOfWork.CommitAsync();
                return new ProductResponse(product);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ProductResponse($"An error occurred when saving the product: {ex.Message}");
            }

        }

        public async Task<ProductResponse> UpdateAsync(int id, Product product)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return new ProductResponse("Product not found.");
            }

            if (await IsProductAlreadyExistsWithCondition(x=>x.Code == product.Code && x.Id != id))
            {
                return new ProductResponse("Code is already used by different product");
            }

            try
            {
               existingProduct.Code = product.Code;
               existingProduct.Description = product.Description;
               existingProduct.ExpireDate = product.ExpireDate;
               existingProduct.UnitOfMeasurement = product.UnitOfMeasurement;
               existingProduct.Barcode = product.Barcode;

               await _productRepository.UpdateAsync(existingProduct);
               await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ProductResponse($"An error occurred when updating the product: {ex.Message})");
            }

            return new ProductResponse(product);
        }

        public async Task<ProductResponse> DeleteAsync(int id)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return new ProductResponse("Product not found.");
            }

            try
            {
                _productRepository.Delete(existingProduct);
                await _unitOfWork.CommitAsync();

                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ProductResponse($"An error occurred when deleting the product: {ex.Message})");
            }
        }

        private async Task<bool> IsProductAlreadyExistsWithCondition(Expression<Func<Product,bool>> predicate)
        {
            var isExist = await _productRepository.Get(predicate);
            return isExist.Any();
        }
    }
}
