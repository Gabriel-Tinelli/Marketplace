namespace Marketplace.ProductsAPI.Clients
{
    public class UserClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public UserClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["UserAPI:BaseUrl"];
        }

        public async Task<bool> UserExists(int userId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/user/{userId}");
            return response.IsSuccessStatusCode;
        }
    }
}