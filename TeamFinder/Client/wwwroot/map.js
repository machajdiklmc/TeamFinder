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
    pointer.addListener("pointer-click", function() {
        map.setCenter(SMap.Coords.fromWGS84(lat, long),true);
    }, pointer);
    map.addControl(pointer);
    pointer.setCoords(SMap.Coords.fromWGS84(lat, long));
}

var dotNetHelperGlobal;
function positionalMap(dotnetHelper, lat, long, id)
{
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
    
    var sync = new SMap.Control.Sync({bottomSpace:0});
    map.addControl(sync);
    
    var pointer = new SMap.Control.Pointer({
        type: SMap.Control.Pointer.TYPES.RED,
        snapHUDtoScreen: 0
    });
    pointer.addListener("pointer-click", function() {
        map.setCenter(SMap.Coords.fromWGS84(lat, long),true);
    }, pointer);
    map.addControl(pointer);
    pointer.setCoords(SMap.Coords.fromWGS84(lat, long));
    
    function sendData(geocoder)
    {
        var results = geocoder.getResults();
        dotNetHelperGlobal.invokeMethodAsync('UpdatePos', results); //send location data to FE
    }
    
    var click = function(signal) {
        var event = signal.data.event;
        console.log(mark);
        var coords = SMap.Coords.fromEvent(event, map);
        mark.setCoords(coords); //set new marker location
        new SMap.Geocoder.Reverse(coords, sendData); //call geocoder reverse to send data of location
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
    signals.addListener(window, "map-click", click);
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