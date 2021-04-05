using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.API.Dtos.Category;
using Hospital.BL.Responses;
using Hospital.BL.Services.Interfaces;
using Hospital.DAL.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hospital.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, IMapper mapper, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Lists all categories.
        /// </summary>
        /// <returns>List of categories.</returns>
        [HttpGet(Name = "GetAllCategoriesAsync")]
        public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetAllCategoriesAsync()
        {
            var categories = await _categoryService.ListAsync();
            var categoriesDto = _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
            return Ok(categoriesDto);
        }



        /// <summary>
        /// Gets category by id.
        /// </summary>
        /// <param name="id">Category id.</param>
        /// <returns>Gets category by id.</returns>
        [HttpGet("{id}", Name = "GetCategoryByIdAsync")]
        public async Task<ActionResult<CategoryReadDto>> GetCategoryByIdAsync(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(_mapper.Map<CategoryReadDto>(result.Resource));
        }

        /// <summary>
        /// Saves a new category.
        /// </summary>
        /// <param name="categoryDto">Category data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost(Name = "AddNewCategoryAsync")]
        public async Task<ActionResult<CategoryReadDto>> AddNewCategoryAsync(CategoryCreateDto categoryDto)
        {
            var category = _mapper.Map<CategoryCreateDto, Category>(categoryDto);
            var result = await _categoryService.SaveAsync(category);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return CreatedAtRoute(nameof(GetCategoryByIdAsync), new { id = category.Id }, _mapper.Map<CategoryReadDto>(result.Resource));
        }

        /// <summary>
        /// Updates an existing category according to an identifier.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <param name="categoryDto">Updated category data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPut("{id}", Name = "UpdateCategoryAsync")]

        public async Task<ActionResult> UpdateCategoryAsync(int id, CategoryUpdateDto categoryDto)
        {
            var category = _mapper.Map<CategoryUpdateDto, Category>(categoryDto);
            var result = await _categoryService.UpdateAsync(id, category);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var categoryResource = _mapper.Map<Category, CategoryUpdateDto>(result.Resource);
            return Ok(categoryResource);
        }

        /// <summary>
        /// Deletes a given category according to an identifier.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <returns>Response for the request.</returns>
        [HttpDelete("{id}", Name = "DeleteCategoryAsync")]
        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {
            var result = await _categoryService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var categoryResource = _mapper.Map<Category, CategoryReadDto>(result.Resource);
            return Ok(categoryResource);
        }

    }
}
