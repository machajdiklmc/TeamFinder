function initMap(long,lat, id)
{
    var center = SMap.Coords.fromWGS84(long, lat);
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
        map.setCenter(SMap.Coords.fromWGS84(long, lat),true);
    }, pointer);
    map.addControl(pointer);
    pointer.setCoords(SMap.Coords.fromWGS84(long, lat));
}

function simpleMap(long,lat,uniqueId)
{
    var center = SMap.Coords.fromWGS84(long, lat);
    var map = new SMap(JAK.gel(String(uniqueId)), center, 13);
    map.addDefaultLayer(SMap.DEF_BASE).enable();

    var layer = new SMap.Layer.Marker();
    map.addLayer(layer);
    layer.enable();

    var options = {};
    var marker = new SMap.Marker(center, "myMarker", options);
    layer.addMarker(marker);

}