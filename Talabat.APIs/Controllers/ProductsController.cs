 using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.core;
using Talabat.core.Entities;
using Talabat.core.Repositiories;
using Talabat.core.Sepecifitction;

namespace Talabat.APIs.Controllers
{

    public class ProductsController : ApiBaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductsController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
       // [Authorize]
        [HttpGet]//api/products
        public async Task<ActionResult<Pagination<productToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productSpec)
         {
            var spec =new ProductSpecifications(productSpec);
            var Products = await unitOfWork.Repository<Product>().GetAllwithSpecAsync(spec);
            var CountSpec = new ProductWithFilrerationCountSpecification(productSpec);
            var count = await unitOfWork.Repository<Product>().GetCountWithSpecAsync(CountSpec);
            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<productToReturnDto>>(Products);        // var Products = await productRepo.GetAllAsync();
            
            //OkObjectResult result = new OkObjectResult(Products); //helper method =>contentResult =>content


            return Ok(new Pagination<productToReturnDto>(productSpec.PageIndex,productSpec.PageSize,count,data)); //200 succcess json
        }

        [ProducesResponseType(typeof(productToReturnDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<productToReturnDto>> GetProduct(int id)
        {
            var spec =new ProductSpecifications(id);
            var Product = await unitOfWork.Repository<Product>().GetByIdwithSpecAsync(spec);
            if (Product is null) return NotFound(new ApiErrorResponse(404));
            var MappedProduct = mapper.Map<Product, productToReturnDto>(Product);

            //OkObjectResult result = new OkObjectResult(Products); //helper method =>contentResult =>content


            return Ok(MappedProduct); //200 succcess json
        }
        [HttpGet("brands")] //api/products/brands
        public  async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var Brands =await unitOfWork.Repository<ProductBrand>().GetAllAsync();

            if (Brands is null) return NotFound(404);

            return Ok(Brands);
        }
        [HttpGet("types")] //api/products/types
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllTypes()
        {
            var Types = await unitOfWork.Repository<ProductType>().GetAllAsync();

            if (Types is null) return NotFound(404);

            return Ok(Types);
        }








    }
}
