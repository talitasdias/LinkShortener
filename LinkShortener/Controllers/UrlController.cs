using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LinkShortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private const string _cacheKey = "LinksDict";
        public UrlController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpPost("Shortenlink")]
        public ActionResult<string> ShortenLink(string longUrl)
        {
            try
            {
                var dataDict = _memoryCache.Get<Dictionary<string, string>>(_cacheKey) ?? new Dictionary<string, string>();

                string baseUrl = "talita@estagiaria/";
                string guid = Guid.NewGuid().ToString().Substring(0, 1);
                string shortenUrl = baseUrl + guid;

                dataDict[shortenUrl] = longUrl;

                _memoryCache.Set(_cacheKey, dataDict);

                return Ok(shortenUrl);
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
                var dataDict = _memoryCache.Get<Dictionary<string, string>>(_cacheKey);

                if (dataDict == null || !dataDict.ContainsKey(shortenUrl))
                    return NotFound();
                else
                    return Ok(dataDict[shortenUrl]);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
