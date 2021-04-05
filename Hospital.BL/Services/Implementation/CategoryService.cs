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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, ILogger<CategoryService> logger)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<CategoryResponse> GetByIdAsync(int id)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);

            if (existingCategory == null)
                return new CategoryResponse("Category not found.");

            return new CategoryResponse(existingCategory);
        }

        public async Task<CategoryResponse> SaveAsync(Category category)
        {
            try
            {
                if (await IsCategoryAlreadyExistsWithCondition(x => x.Code == category.Code))
                {
                    return new CategoryResponse("Code is already used by different category");
                }

                await _categoryRepository.AddAsync(category);
                await _unitOfWork.CommitAsync();

                return new CategoryResponse(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new CategoryResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<CategoryResponse> UpdateAsync(int id, Category category)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);

            if (existingCategory == null)
            {
                return new CategoryResponse("Category not found.");
            }

            if (await IsCategoryAlreadyExistsWithCondition(x =>x.Code == category.Code && x.Id !=id))
            {
                return new CategoryResponse("Code is already used by different category"); 
            }

            existingCategory.Code = category.Code;
            existingCategory.Description = category.Description;

            try
            {
                await _unitOfWork.CommitAsync();

                return new CategoryResponse(existingCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new CategoryResponse($"An error occurred when updating the category: {ex.Message}");
            }
        }

        public async Task<CategoryResponse> DeleteAsync(int id)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);

            if (existingCategory == null)
            {
                return new CategoryResponse("Category not found.");
            }

            try
            {
                _categoryRepository.Delete(existingCategory);
                await _unitOfWork.CommitAsync();

                return new CategoryResponse(existingCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new CategoryResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }

        private async Task<bool> IsCategoryAlreadyExistsWithCondition(Expression<Func<Category, bool>> predicate)
        {
            var isExist = await _categoryRepository.Get(predicate);
            return isExist.Any();
        }
    }
}
