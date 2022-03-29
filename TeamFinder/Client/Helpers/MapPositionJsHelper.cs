using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using TeamFinder.Client.Pages.Components;
using TeamFinder.Shared.Models;

namespace TeamFinder.Client;

public class MapPositionJsHelper
{
    private readonly EventCallback<SportEvent> _onPosChange;

    public MapPositionJsHelper(SportEvent sportEvent, EventCallback<SportEvent> onPosChange)
    {
        _onPosChange = onPosChange;
        SportEvent = sportEvent;
    }
    SportEvent SportEvent { get; }
    
    [JSInvokable("UpdatePos")]
    public async Task UpdatePos(System.Text.Json.JsonElement element)
    {
        var p = parsePos(element);
        SportEvent.Location = new SportEventLocation()
        {
            Latitude = p.coords.x,
            Longitude = p.coords.y,
            City = p.name
        };
        await _onPosChange.InvokeAsync(SportEvent);
    }

    private GeocodePosition parsePos(JsonElement element)
    {
        var json = element.GetRawText();
        var pos = JsonConvert.DeserializeObject<GeocodeResult>(json);
        var p = pos.items.FirstOrDefault(pp => pp.type == "muni");
        if (p != null) return p;
        
        p = pos.items.FirstOrDefault(pp => pp.type == "osmm");
        return p ?? pos.items[3];
    }
}