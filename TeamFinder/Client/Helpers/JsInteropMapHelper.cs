using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using TeamFinder.Client.Pages.Components;
using TeamFinder.Shared.Models;

namespace TeamFinder.Client;

public class JsInteropMapHelper : IDisposable
{
    public SportEvent SportEvent { get; set; }
    private readonly IJSRuntime js;
    private readonly string _id;
    private readonly EventCallback<SportEvent> _onPosChange;
    private DotNetObjectReference<MapPositionJsHelper> objRef;

    public JsInteropMapHelper(IJSRuntime js, SportEvent sportEvent, string id, EventCallback<SportEvent> onPosChange)
    {
        SportEvent = sportEvent;
        this.js = js;
        _id = id;
        _onPosChange = onPosChange;
    }
    
    

    public async Task InitPositionalMap()
    {
        objRef = DotNetObjectReference.Create(new MapPositionJsHelper(SportEvent, _onPosChange));
        await js.InvokeVoidAsync("positionalMap", objRef, SportEvent.Location.Latitude, SportEvent.Location.Longitude,_id);
    }

    public void Dispose()
    {
        objRef?.Dispose();
    }
}