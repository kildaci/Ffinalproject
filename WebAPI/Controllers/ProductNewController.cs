using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductNewController : ControllerBase
    {
        IProductNewService _productNewService;
        public ProductNewController(IProductNewService productNewService)
        {
            _productNewService = productNewService;
        }
        [HttpGet("getall")]
        public IActionResult Get()
        {
            var result = _productNewService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
