using App.Services.Products;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController] bunlar artık CustomBaseController'dan geliyor
    public class ProductsController(IProductService productService) : CustomBaseController
    {
        [HttpGet]
        public async Task <IActionResult> GetAll()=> CreateActionResult(await productService.GetAllListAsync());
        //burda(yukarıda) productResult'a 
        //serviceResult geldiği için products     
        //yerine serviceResult diye isimlendirdim.

        [HttpGet("{pageSize}/{pageNumber}")]
        public async Task<IActionResult> GetPagedAll(int pageNumber,int pageSize)
            => CreateActionResult(await productService.GetPagedAllListAsync(pageNumber,pageSize));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)=>CreateActionResult(await productService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request) =>CreateActionResult(await productService.CreateAsync(request));
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductRequest request) => CreateActionResult(await productService.UpdateAsync(id, request));
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)=>CreateActionResult(await productService.DeleteAsync(id));
        
    }
}
 