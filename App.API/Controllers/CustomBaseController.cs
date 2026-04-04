using App.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction] //bu ve alttaki generic olmayan versiyonları yardımcı method. Swagger bunları 
                    //endpoint olarak algılamasın diye.
        public IActionResult CreateActionResult<T>(ServiceResult<T> result)
        {
            if (result.Status == HttpStatusCode.NoContent)
            {
                return new ObjectResult(null)
                {
                    StatusCode = result.Status.GetHashCode()
                };
            }
            return new ObjectResult(result) { StatusCode = result.Status.GetHashCode() };
        }
        [NonAction]
        public IActionResult CreateActionResult(ServiceResult  result)
        {
            if (result.Status == HttpStatusCode.NoContent)
            {
                return new ObjectResult(null)
                {
                    StatusCode = result.Status.GetHashCode()
                };
            }
            return new ObjectResult(result) { StatusCode = result.Status.GetHashCode() };
        }
    }
}
