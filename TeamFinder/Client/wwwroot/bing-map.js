function createMap(lat,lon,id) {
    Loader.async = true;
    Loader.load(null, null, createMap);
    var center = SMap.Coords.fromWGS84(14.41790, 50.12655);
    var m = new SMap(JAK.gel(id), center, 13);
    m.addDefaultLayer(SMap.DEF_BASE).enable();
}