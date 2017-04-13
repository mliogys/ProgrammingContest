var TeamViewModel = function () {
    var self = this;
    self.teams = ko.observableArray();

    $.getJSON('api/teams', function (data) {
        self.teams(data);
    });
}

ko.applyBindings(new TeamViewModel());