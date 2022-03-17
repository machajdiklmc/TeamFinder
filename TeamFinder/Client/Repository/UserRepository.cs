using System.Net.Http.Json;
using TeamFinder.Shared;
using TeamFinder.Shared.Models;

namespace TeamFinder.Client.Repository
{
    public class UserRepository
    {
        private readonly HttpClient _http;

        public UserRepository(HttpClient httpClient)
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
