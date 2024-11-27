using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LinkShortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlController : ControllerBase
    {
        public UrlController()
        {
        }

        [HttpPost("Shortenlink")]
        public ActionResult<string> ShortenLink(string longUrl)
        {
            try
            {

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet()]
        public ActionResult<string> SearchLink(string shortenUrl)
        {
            try
            {
                return new RedirectResult(shortenUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
