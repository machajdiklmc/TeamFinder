using System.Net.Http.Json;
using TeamFinder.Shared;
using TeamFinder.Shared.Models;

namespace TeamFinder.Client.Services
{
    public class UserService
    {
        private readonly HttpClient _http;

        public UserService(HttpClient httpClient)
        {
            _http = httpClient;
        }
        public async Task<User> GetUser(string id)
        {
            var request = await _http.PostAsJsonAsync(Endpoints.GetUser, id);
            return await request.Content.ReadFromJsonAsync<User>() ?? new User() { Id = id};
        }
    }
}
