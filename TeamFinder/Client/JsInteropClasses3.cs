using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using TeamFinder.Client.Pages.Components;
using TeamFinder.Shared.Models;

namespace TeamFinder.Client;

public class JsInteropClasses3 : IDisposable
{
    public SportEvent SportEvent { get; set; }
    private readonly IJSRuntime js;
    private readonly string _id;
    private readonly EventCallback<SportEvent> _onPosChange;
    private DotNetObjectReference<HelloHelper> objRef;

    public JsInteropClasses3(IJSRuntime js, SportEvent sportEvent, string id, EventCallback<SportEvent> onPosChange)
    {
        SportEvent = sportEvent;
        this.js = js;
        _id = id;
        _onPosChange = onPosChange;
    }
    
    

    public async Task InitPositionalMap(string name)
    {
        objRef = DotNetObjectReference.Create(new HelloHelper(name,SportEvent, _onPosChange));

       // return js.InvokeAsync<string>("sayHello1", objRef);
        await js.InvokeVoidAsync("positionalMap", objRef, SportEvent.Location.Latitude, SportEvent.Location.Longitude,_id);
    }

    public void Dispose()
    {
        objRef?.Dispose();
    }
}