
var map;
var sessionKey = $(location).attr('pathname').split('/')[2];
var cookieUsernameKey = 'gps_username_' + sessionKey;

function initialize() {

    var myLatlng = new google.maps.LatLng(47.377677, 8.526544);
    var mapOptions = {
        zoom: 2,
        center: myLatlng
    }
    map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);

    $.getJSON("/api/locations/" + sessionKey, function(data) {
        addPoints(data);
    });
};
google.maps.event.addDomListener(window, 'load', initialize);


// Reference the auto-generated proxy for the hub.
var mapHub = $.connection.mapHub;

// Define client methods (can be called by server)
mapHub.client.updatePosition = function(data) {
    addPoints(data);
};

// Initialize
$.connection.hub.start().done(function() {

    if ($.cookie(cookieUsernameKey) === undefined) {
        var username = prompt('Enter your name:', 'User');
        $.cookie(cookieUsernameKey, username, { expires: 7 });
    }
    $('#displayname').text($.cookie(cookieUsernameKey));

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function(position) {
            propagatePosition(position);

            map.panTo(new google.maps.LatLng(position.coords.latitude, position.coords.longitude));
            map.setZoom(10);
        }, handleError);

        navigator.geolocation.watchPosition(function(position) {
            propagatePosition(position);

            map.panTo(new google.maps.LatLng(position.coords.latitude, position.coords.longitude));
        }, handleError);
    };
});

function handleError(error) {
    var msg = '';
    switch (error.code) {
        case 0: //   0: unknown error
        case 2: //   2: position unavailable (error response from locaton provider)
        case 3: //   3: timed out
        default:
            msg = 'An unknown error occured while trying to find your location. Please try again later.';
            break;
        case 1: //   1: permission denied
            msg = 'Please enable and allow location services on your device for this webiste.';
            break;
    }
    alert(msg);
}

function propagatePosition(position) {
    var speed = position.coords.speed != null ? position.coords.speed : 0;
    var lat = position.coords.latitude;
    var lng = position.coords.longitude;
    var name = $.cookie(cookieUsernameKey);
    if (!(lat === 0.0 && lng === 0.0)) {
        mapHub.server.propagatePosition({ username: name, lat: lat, lng: lng, speed: speed }, sessionKey);
    }
}

function addPoints(data) {
    if (data.length > 0) {
        addGoogleMapPoints(data);
    }
}

function addInfoWindow(marker, message) {
    var infoWindow = new google.maps.InfoWindow({
        content: message
    });

    google.maps.event.addListener(marker, 'click', function() {
        infoWindow.open(map, marker);
    });
}

var markersArray = [];

google.maps.Map.prototype.clearMarkers = function() {
    for (var i = 0; i < markersArray.length; i++) {
        markersArray[i].setMap(null);
    }
    markersArray.length = [];
};

// To add Google Map Points
function addGoogleMapPoints(loc) {
    map.clearMarkers();
    for (i = 0; i < loc.length; i++) {
        var pnt = loc[i];

        var marker = new google.maps.Marker({
            position: new google.maps.LatLng(pnt.Lat, pnt.Lng),
            map: map,
            title: 'Click me!',
            clickable: true
        });
        markersArray.push(marker);

        var contentString = '<div id="content">' +
            '<div id="siteNotice">' +
            '</div>' +
            '<h1 id="firstHeading" class="firstHeading">' + pnt.Username + '</h1>' +
            '<div id="bodyContent">' +
            '<p><b>Position</b><br/>' +
            'Latitude: ' + pnt.Lat + ' - Longitude: ' + pnt.Lng + ' - Speed: ' + pnt.Speed +
            '</p>' +
            '</div>' +
            '</div>';

        addInfoWindow(marker, contentString);
    }
}