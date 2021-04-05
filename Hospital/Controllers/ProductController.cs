using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.API.Dtos.Product;
using Hospital.BL.Responses;
using Hospital.BL.Services.Interfaces;
using Hospital.DAL.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Hospital.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;


        public ProductController(IProductService productService, IMapper mapper, ILogger<ProductController> logger)
        {
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Lists all products.
        /// </summary>
        /// <returns>List of products.</returns>
        [HttpGet(Name = "GetAllProductsAsync")]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAllProductsAsync()
        {
           var products = await _productService.ListAsync();
           var productsDto = _mapper.Map<IEnumerable<ProductReadDto>>(products);
           return Ok(productsDto);
        }

        /// <summary>
        /// Gets products by id.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <returns>Gets category by id.</returns>
        [HttpGet("{id}", Name = "GetProductByIdAsync")]
        public async Task<ActionResult<ProductReadDto>> GetProductByIdAsync(int id)
        {
            _logger.LogInformation($"Invoking {nameof(GetProductByIdAsync)} method with arguments: id={id}");
            var result = await _productService.GetByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return  Ok(_mapper.Map<ProductReadDto>(result.Resource));
        }

        /// <summary>
        /// Gets all products by category id.
        /// </summary>
        /// <param name="categoryId">Category id.</param>
        /// <returns>Gets all products by category id..</returns>
        [HttpGet("GetAllProductsByCategoryAsync/{categoryId}", Name = "GetAllProductsByCategoryAsync")]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAllProductsByCategoryAsync(int categoryId)
        {
            _logger.LogInformation(
                $"Invoking {nameof(GetAllProductsByCategoryAsync)} method with arguments: id={categoryId}");
            var products = await _productService.GetAllProductsByCategory(categoryId);
            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(products));
        }

        /// <summary>
        /// Saves a new product.
        /// </summary>
        /// <param name="productDto">Product data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost(Name = "AddNewProductAsync")]
        public async Task<ActionResult<ProductReadDto>> AddNewProductAsync(ProductCreateDto productDto)
        {
            _logger.LogInformation(
                $"Invoking {nameof(GetAllProductsByCategoryAsync)} method with arguments: productDto={JsonConvert.SerializeObject(productDto)}");
            var newProduct = _mapper.Map<Product>(productDto);
            var result = await _productService.SaveAsync(newProduct);
            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var productReadDto = _mapper.Map<ProductReadDto>(result.Resource);

            return CreatedAtRoute(nameof(GetProductByIdAsync), new {id = newProduct.Id}, _mapper.Map<ProductReadDto>(productReadDto));
        }

        /// <summary>
        /// Updates an existing product according to an identifier.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <param name="productDto">Updated product data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPut("{id}", Name = "UpdateProductAsync")]
        public async Task<ActionResult> UpdateProductAsync(int id,  ProductUpdateDto productDto)
        {
            _logger.LogInformation(
                $"Invoking {nameof(GetAllProductsByCategoryAsync)} method with arguments id={id}, productDto={JsonConvert.SerializeObject(productDto)}");

            var product = _mapper.Map<Product>(productDto);
            var result = await _productService.UpdateAsync(id, product);
            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var productResource = _mapper.Map<Product, ProductReadDto>(result.Resource);
            return Ok(productResource);
        }

    }
}
