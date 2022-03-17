function loadBingMap(latitude, longitude) {
    var map = new Microsoft.Maps.Map(document.getElementById('map'), {
        center: new Microsoft.Maps.Location(latitude, longitude),
    });
    var pushpin = new Microsoft.Maps.Pushpin(map.getCenter(), null);
    map.entities.push(pushpin);
    return "";
}