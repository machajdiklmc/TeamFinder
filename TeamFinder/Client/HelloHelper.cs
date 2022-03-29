using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using TeamFinder.Client.Pages.Components;
using TeamFinder.Shared.Models;

namespace TeamFinder.Client;

public class HelloHelper
{
    private readonly EventCallback<SportEvent> _onPosChange;

    public HelloHelper(string name, SportEvent sportEvent, EventCallback<SportEvent> onPosChange)
    {
        _onPosChange = onPosChange;
        Name = name;
        SportEvent = sportEvent;
    }

    string Name { get; set; }
    SportEvent SportEvent { get; }

    [JSInvokable]
    public string GetHelloMessage() => $"Hello, {Name}!";
    
    [JSInvokable("UpdatePos")]
    public async Task UpdatePos(System.Text.Json.JsonElement element)
    {
        var json = element.GetRawText();
        var pos = JsonConvert.DeserializeObject<Map.GeocodeResult>(json);
        var p = pos.items.FirstOrDefault(pp => pp.type == "muni");
        Console.WriteLine(p.coords.x + " " + p.coords.y + " " + p.name);
        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        SportEvent.Location = new SportEventLocation()
        {
            Latitude = p.coords.x,
            Longitude = p.coords.y,
            City = p.name
        };
        await _onPosChange.InvokeAsync(SportEvent);
    }
}