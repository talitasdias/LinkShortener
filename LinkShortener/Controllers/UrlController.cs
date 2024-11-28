using LinkShortener.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public UrlController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("Shortenlink")]
        public async Task<ActionResult<string>> ShortenLink(string longUrl)
        {
            try
            {
                var existingUrl = await _dbContext.urlMappings
                    .Where(x => x.LongUrl == longUrl).Select(x => x.ShortenUrl).FirstOrDefaultAsync();
                
                if (existingUrl != null)
                {
                    return Ok(existingUrl);
                }

                string baseUrl = "talita@estagiaria/";
                string guid = Guid.NewGuid().ToString().Substring(0, 1);
                string shortenUrl = $"{baseUrl}{guid}";

                UrlMappings urlMappings = new UrlMappings()
                {
                    ShortenUrl = shortenUrl,
                    LongUrl = longUrl
                };

                _dbContext.urlMappings.Add(urlMappings);
                _dbContext.SaveChanges();

                return Ok(shortenUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("GetUrl")]
        public async Task<ActionResult<string>> SearchLink(string shortenUrl)
        {
            try
            {
                string longUrl = await _dbContext.urlMappings
                    .Where(x => x.ShortenUrl == shortenUrl).Select(x => x.LongUrl).FirstOrDefaultAsync();

                if (string.IsNullOrEmpty(longUrl))
                {
                    return NotFound(new { message = "URL não encontrada." });
                }

                return new RedirectResult(longUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
