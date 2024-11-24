using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entites;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductCategory> _categoryRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo,
            IGenericRepository<ProductCategory> categoryRepo,
            IGenericRepository<ProductBrand> brandRepo,
            IMapper mapper)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        //api/Products
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnfDto>>> GetProducts([FromQuery] ProductSpecParams Params)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(Params);

            var products = await _productRepo.GetAllWithSpecAsync(spec);

            var mappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnfDto>>(products);

            var countSpec = new ProductWithFilterationForCountAsync(Params);

            var count = await _productRepo.GetCountWithSpecAsync(countSpec);

            return Ok(new Pagination<ProductToReturnfDto>(Params.PageSize, Params.PageIndex, mappedProducts, count));
        }

        //api/Products/1
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnfDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnfDto>> GetProduct(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);
            //var product = _productRepo.GetAsync(id);
            var product = await _productRepo.GetWithSpecAsync(spec);


            if (product == null)
                return NotFound(new ApiResponse(404));//404

            return Ok(_mapper.Map<Product, ProductToReturnfDto>(product));//200
        }

        //Get All Categories
        //BaseUrl/api/Products/Categories
        [HttpGet("Categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
        {

            var categories = await _categoryRepo.GetAllAsync();

            return Ok(categories);
        }

        //Get All Brands
        //BaseUrl/api/Products/Brands
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandRepo.GetAllAsync();
            return Ok(brands);
        }

    }
}
