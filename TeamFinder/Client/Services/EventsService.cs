using System.Net.Http.Json;
using TeamFinder.Shared;
using TeamFinder.Shared.Models;
using TeamFinder.Shared.Requests;

namespace TeamFinder.Client.Services;

public class EventsService : IEventsService
{
    private readonly HttpClient _http;

    public EventsService(HttpClient http)
    {
        _http = http;
    }
    public async Task<bool> AddEvent(SportEvent sportEvent, string ownerId)
    {
        sportEvent.OwnerId = ownerId;
        sportEvent.Id = Guid.NewGuid();
        var response = await _http.PostAsJsonAsync(Endpoints.AddEvent, sportEvent);
        return await response.Content.ReadFromJsonAsync<bool>();
    }

    public async Task<bool> LeaveEvent(UserEventsRequest request)
    {
        var responseMessage = await _http.PostAsJsonAsync(Endpoints.LeaveEvent, request);
        return await responseMessage.Content.ReadFromJsonAsync<bool>();
    }
    
    public async Task<bool> JoinEvent(UserEventsRequest request)
    {
        var responseMessage = await _http.PostAsJsonAsync(Endpoints.JoinEvent, request);
        return await responseMessage.Content.ReadFromJsonAsync<bool>();
    }
    public async Task<SportEvent?> GetEvent(string sportEventId)
    {
        var response = await _http.PostAsJsonAsync(Endpoints.GetEvent, sportEventId);
        return await response.Content.ReadFromJsonAsync<SportEvent?>();
    }

    public async Task<List<UserEvents>?> GetAllUsersInEvent(Guid sportEventId)
    {
        var response = await _http.PostAsJsonAsync(Endpoints.GetAllUsersInEvent, sportEventId);
        return await response.Content.ReadFromJsonAsync<List<UserEvents>>();
    }
    
    public async Task<List<UserEvents>?> GetUserEvents(GetUserEventsRequest request)
    {
        var response = await _http.PostAsJsonAsync(Endpoints.GetUserEvents, request);
        return await response.Content.ReadFromJsonAsync<List<UserEvents>>();
    }

    public async Task<List<SportEvent>?> GetEvents(GetEventsRequest? request = null)
    {
        var response = await _http.PostAsJsonAsync(Endpoints.GetAllEvents, request ?? new GetEventsRequest(GetEventsRequestOrderBy.Date));
        return await response.Content.ReadFromJsonAsync<List<SportEvent>>();
    }
}

public interface IEventsService
{
    Task<bool> AddEvent(SportEvent sportEvent, string ownerId);
    Task<bool> LeaveEvent(UserEventsRequest request);
    Task<bool> JoinEvent(UserEventsRequest request);
    Task<SportEvent?> GetEvent(string sportEventId);
    Task<List<UserEvents>?> GetAllUsersInEvent(Guid sportEventId);
    Task<List<UserEvents>?> GetUserEvents(GetUserEventsRequest request);
    Task<List<SportEvent>?> GetEvents(GetEventsRequest? request);
}