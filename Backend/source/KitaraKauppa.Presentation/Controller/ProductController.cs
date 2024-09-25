using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitaraKauppa.Core.Products;
using KitaraKauppa.Service.ProductsServices;
using KitaraKauppa.Service.ProductsServices.Dtos;
using KitaraKauppa.Service.Repositories.Products;
using KitaraKauppa.Service.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KitaraKauppa.Presentation.Controller
{
    [ApiController]
    [Route("api/v1/[controller]s")]
    public class ProductController : ControllerBase
    {
        private readonly IProductManagement _productManagement;
        private readonly IProductDefinitionManagement _productDefinitionManagement;

        public ProductController(IProductManagement productManagement, IProductDefinitionManagement productDefinitionManagement)
        {
            _productManagement = productManagement;
            _productDefinitionManagement = productDefinitionManagement;
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var newProductValidation = createProductDto.Validate();

            if (!newProductValidation.Succeeded)
                return BadRequest(newProductValidation);

            var newProduct = await _productManagement.CreateProduct(createProductDto);
            Response.StatusCode = 201;

            return Created(nameof(CreateProduct), newProduct);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetProductById([FromRoute] Guid id)
        {
            if(id == Guid.Empty)
                return BadRequest(new KKResult<string>().Fail("Id cannot be empty"));

            var product = await _productManagement.GetProductById(id);
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductDto updateProductDto)
        {
            var validate = updateProductDto.Validate();
            if (!validate.Succeeded)
                return BadRequest(validate);

            var result = await _productManagement.UpdateProduct(id, updateProductDto);

            if (!result.Succeeded)
                return BadRequest(new KKResult<string>().Fail(result.Value));

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteProductById([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(new KKResult<string>().Fail("Id cannot be empty"));

            var result = await _productManagement.DeleteProductById(id);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryOptions pqdto)
        {
            var validate = pqdto.Validate();
            if (!validate.Succeeded)
                return BadRequest(validate);

            var products = await _productManagement.GetAllProducts(pqdto);

            return Ok(products);
        }

        [HttpGet("Definition")]
        public async Task<IActionResult> GetProductDefinitions()
        {
            var productDefinition = await  _productDefinitionManagement.GetDefinitions();
            return Ok(productDefinition);
        }

    }











}