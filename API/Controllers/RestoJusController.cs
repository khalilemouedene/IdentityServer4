using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RestoJusController : ControllerBase
    {
        private readonly IRestoJusService _restoJusService;

        public RestoJusController(IRestoJusService restoJusService)
        {
            _restoJusService = restoJusService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _restoJusService.List();
            return Ok(result);
        }
    }
}
