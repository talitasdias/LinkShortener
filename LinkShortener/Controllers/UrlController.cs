using Microsoft.AspNetCore.Mvc;

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
                string baseUrl = "talita@estagiaria/";

                string guid = Guid.NewGuid().ToString().Substring(0, 1);
                string shortenUrl = baseUrl + guid;

                return Ok(shortenUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
