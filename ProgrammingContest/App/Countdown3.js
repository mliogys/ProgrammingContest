function startTimer(duration, display) {
    var hours, minutes, seconds;

    function timer() {
        duration = duration - 1;

        hours = (duration / 3600) | 0;
        minutes = ((duration % 3600) / 60) | 0;
        seconds = (duration % 60) | 0;

        hours = hours < 10 ? "0" + hours : hours;
        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        display.textContent = "Iki rungties pabaigos liko: " + hours + ":" + minutes + ":" + seconds;

        if (duration <= 0) {
            window.location.reload(true);
        }
    };
    timer();
    setInterval(timer, 1000);
}

window.onload = function () {
    var teamID = getTeamID();
    $.ajax({
        url: 'api/times/eventending/2',
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (seconds) {
            display = document.querySelector('#time');
            startTimer(seconds, display);
        }
    });

    $.ajax({
        url: 'api/teams/' + teamID + '/points',
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (points) {
            display = document.querySelector('#points');
            display.textContent = "Taškai: " + points;
        }
    });
};

function getTeamID() {
    var b = new Array();
    var c = new Array();
    b = document.cookie.split('=');
    if (b.length === 3) {
        c = b[2].split(':');
        return c[0];
    }
    else {
        window.location.reload(true);
    }
}