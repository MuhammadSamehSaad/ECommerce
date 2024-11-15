using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Entites;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        //api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnfDto>>> GetProducts()
        {
            var spec = new ProductWithBrandAndCategorySpecifications();


            var products = await _productRepo.GetAllWithSpecAsync(spec);

            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnfDto>>(products));
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


    }
}
