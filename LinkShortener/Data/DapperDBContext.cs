/*namespace LinkShortener.Data
{
    using Dapper;
    using System.Data.SQLite;
    using System.Security.Policy;

    public class DapperDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SQLiteConnection");
        }

        public SQLiteConnection CreateConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        public async Task<bool> InsertUrl(string shortenUrl, string longUrl)
        {
            const string query = "INSERT INTO URL_MAPPINGS (shorten_url, long_url) VALUES (@ShortenUrl, @LongUrl)";

            try
            {
                using (var connection = CreateConnection())
                {
                    var parameters = new { ShortenUrl = shortenUrl, LongUrl = longUrl };
                    int rowsAffected = await connection.ExecuteAsync(query, parameters);
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir URL: {ex.Message}");
                return false;
            }
        }

        public async Task<string> GetUrl(string shortenUrl)
        {
            const string query = "SELECT long_url FROM URL_MAPPINGS WHERE shorten_url = @ShortenUrl";

            try
            {
                using (var connection = CreateConnection())
                {
                    var parameters = new { ShortenUrl = shortenUrl };
                    string longUrl = await connection.QuerySingleOrDefaultAsync<string>(query, parameters);
                    return longUrl;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar URL: {ex.Message}");
                throw;
            }
        }

        public async Task<string> CheckUrlExists(string longUrl)
        {
            const string query = "SELECT shorten_url FROM URL_MAPPINGS WHERE long_url = @LongUrl";

            try
            {
                using (var connection = CreateConnection())
                {
                    var parameters = new { LongUrl = longUrl };
                    string shortenUrl = await connection.QuerySingleOrDefaultAsync<string>(query, parameters);
                    return shortenUrl;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao verificar URL: {ex.Message}");
                throw;
            }
        }
    }
}*/
