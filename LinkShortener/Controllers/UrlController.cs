using LinkShortener.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LinkShortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlController : ControllerBase
    {
        private readonly DapperDBContext _dapperDB;
        public UrlController(DapperDBContext dapperDB)
        {
            _dapperDB = dapperDB;
        }

        [HttpPost("Shortenlink")]
        public async Task<ActionResult<string>> ShortenLink(string longUrl)
        {
            try
            {
                var existingUrl = await _dapperDB.CheckUrlExists(longUrl);
                if (existingUrl != null)
                {
                    return Ok(existingUrl);
                }

                string baseUrl = "talita@estagiaria/";
                string guid = Guid.NewGuid().ToString().Substring(0, 1);
                string shortenUrl = $"{baseUrl}{guid}";

                bool isInserted = await _dapperDB.InsertUrl(shortenUrl, longUrl);
                if (!isInserted)
                {
                    return StatusCode(500, new { message = "Falha ao inserir URL no banco de dados." });
                }

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
                string longUrl = await _dapperDB.GetUrl(shortenUrl);
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
