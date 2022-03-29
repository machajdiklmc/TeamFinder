function interactiveMap(lat, long, id)
{
    var center = SMap.Coords.fromWGS84(lat, long);
    var map = new SMap(JAK.gel(String(id)), center, 12);
    map.addDefaultLayer(SMap.DEF_BASE).enable();
    map.addDefaultControls();

    var layer = new SMap.Layer.Marker();
    map.addLayer(layer);
    layer.enable();

    var options = {};
    var marker = new SMap.Marker(center, "myMarker", options);
    layer.addMarker(marker);
    
    var pointer = new SMap.Control.Pointer({
        type: SMap.Control.Pointer.TYPES.RED,
        snapHUDtoScreen: 20
    });
    /*
    pointer.addListener("pointer-click", function() {
        map.setCenter(SMap.Coords.fromWGS84(lat, long),true);
    }, pointer);*/
    map.addControl(pointer);
    pointer.setCoords(SMap.Coords.fromWGS84(lat, long));
}
window.sayHello1 = (dotNetHelper) => {
    return dotNetHelper.invokeMethodAsync('GetHelloMessage');
};

var dotNetHelperGlobal;
function positionalMap(dotnetHelper, lat, long, id)
{
    console.log(dotnetHelper);
    dotNetHelperGlobal = dotnetHelper;
    var center = SMap.Coords.fromWGS84(lat, long);
    var map = new SMap(JAK.gel(String(id)), center, 12);
    map.addDefaultLayer(SMap.DEF_BASE).enable();
    map.addDefaultControls();

    var layer = new SMap.Layer.Marker();
    map.addLayer(layer);
    layer.enable();

    var mark = new SMap.Marker(center);
    mark.decorate(SMap.Marker.Feature.Draggable);
    layer.addMarker(mark);

    function sendData(geocoder)
    {
        var results = geocoder.getResults();
        let loc = results.items.find(e => e.type == "muni");
        if(loc === undefined)
        {
            loc = results.items.find(e => e.type == "ward");
            if (loc === undefined)
                loc = results.items[0];
        }
       // dotNetHelper.invokeMethodAsync('GetHelloMessage');
        dotNetHelperGlobal.invokeMethodAsync('UpdatePos', results);
        console.log(results);
       /* let latitudeEl = document.getElementById('latitude');
        let longitudeEl = document.getElementById('longitude');
        let cityEl = document.getElementById('city');
        latitudeEl.value = results.coords.x;
        longitudeEl.value = results.coords.y;
        cityEl.value = loc.name;
        latitudeEl.dispatchEvent(new Event('change'));
        longitudeEl.dispatchEvent(new Event('change'));
        cityEl.dispatchEvent(new Event('change'));*/
        
    }
    
    function start(e) {
        var node = e.target.getContainer();
        node[SMap.LAYER_MARKER].style.cursor = "help";
    }

    function stop(e) {
        var node = e.target.getContainer();
        node[SMap.LAYER_MARKER].style.cursor = "";
        var coords = e.target.getCoords();
        new SMap.Geocoder.Reverse(coords, sendData);
    }
    
    var signals = map.getSignals();
    signals.addListener(window, "marker-drag-stop", stop);
    signals.addListener(window, "marker-drag-start", start);
}

function simpleMap(lat, long, uniqueId)
{
    var center = SMap.Coords.fromWGS84(lat, long);
    var map = new SMap(JAK.gel(String(uniqueId)), center, 13);
    map.addDefaultLayer(SMap.DEF_BASE).enable();

    var layer = new SMap.Layer.Marker();
    map.addLayer(layer);
    layer.enable();

    var options = {};
    var marker = new SMap.Marker(center, "myMarker", options);
    layer.addMarker(marker);

}