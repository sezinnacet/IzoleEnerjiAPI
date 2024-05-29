using Business.Abstract;
using Entities.Concrete;
using Entities.Concrete.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IzoleEnerjiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService productService = productService;

        [HttpGet]
        [Route("GetAllProductsList")]
        public IActionResult GetAllProductsList()
        {
            try
            {
                var result = productService.GetAllProductsList();
                return new JsonResult(new Result<List<CategoryProductViewModel>> { ResultObject = result });
            }
            catch (Exception ex)
            {

                return new JsonResult(new Result<UserViewModel> { Message = ex.Message, Success = false });
            }
        }

    }
}
