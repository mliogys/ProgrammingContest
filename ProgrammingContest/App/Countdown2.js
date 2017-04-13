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

        display.textContent = hours + ":" + minutes + ":" + seconds;

        if (duration <= 0) {            
            window.location.reload(true);
        }
    };
    timer();
    setInterval(timer, 1000);
}

window.onload = function () {    
    display = document.querySelector('#timeToEvent');
    startTimer(timeValue, display);
}

