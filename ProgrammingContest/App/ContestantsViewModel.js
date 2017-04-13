var ContestantsViewModel = function () {
    var self = this;
    self.contestants = ko.observableArray();

    var userID = getCookieValue();
    var cmdURL = "api/teams/" + userID + "/contestants";
    $.getJSON(cmdURL, function (data) {
        self.contestants(data);
    });
}

ko.applyBindings(new ContestantsViewModel(), document.getElementById('members'));

function getCookieValue() {
    var b = new Array();
    var c = new Array();
    b = document.cookie.split('=');
    c = b[2].split(':');
    return c[0];
}