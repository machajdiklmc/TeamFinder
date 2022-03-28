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

function positionalMap(lat, long, id, instance)
{
    console.log(instance);
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
        instance.invokeMethodAsync("UpdatePos", results);
        //DotNet.invokeMethodAsync("TeamFinder.Client", 'UpdatePos', results);
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
        console.log('EEEEEEEEE')
        
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