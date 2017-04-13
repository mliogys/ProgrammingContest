function Register() {  
    var loginID = $("#loginID").val();   
    var cmdURL = "api/teams/" + loginID;

    $.ajax({
        url: cmdURL,
        async: false,
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            var d = data.ID + ':' + data.Title;
            var d1 = new Date(), d2 = new Date(d1);
            d2.setMinutes(d1.getMinutes() + 480);
            var str = "TeamCookie=team=" + d + ";expires=" + d2.toUTCString() + ";path=/";
            document.cookie = str;
        }
    });
    
}

function check(clickedButton) {
    var questionID = clickedButton.id.split('_')[1];
    var teamID = getCookieValue();

    if (teamID === 0) {
        window.location.reload(true);
    }
    else {
        var answer = $("#t_" + questionID).val();

        $.ajax({
            type: "POST",
            url: "api/teams/" + teamID + "/questions/" + questionID + "/answer",
            dataType: "json",
            async: false,
            data: { Text: answer },
            success: function (data) {
                if (data.correct === true) {
                    $("#t_" + questionID).val("Įvykdyta");
                    $("#t_" + questionID).attr("disabled", "disabled");
                    clickedButton.disabled = true;
                    if (data.reload === true) {
                        setTimeout(function () {
                            window.location.reload(true);
                        }, 2000);
                    }
                }
                else {
                    $("#t_" + questionID).val("Atsakymas neteisingas");
                    $("#t_" + questionID).addClass('color-scheme2');
                }
            }
        });
    }
}

function getCookieValue() {
    var b = new Array();
    var c = new Array();
    b = document.cookie.split('=');
    if (b.length === 3) {
        c = b[2].split(':');
        return c[0];
    }
    else {
        return 0;
    }
}

function clearError(inputBox) {
    var id = '#' + inputBox.id;
    if ($(id).hasClass('color-scheme2')) {
        $(id).removeClass('color-scheme2');
        $(id).val("");
    }   
}

var questionID = 0;

function check2(clickedButton) {
    var currentID = clickedButton.id.split('_')[1];
    var isVisible = $("#answerDiv").is(":visible");

    if (currentID !== questionID) {
        if (isVisible !== true) {
            $("#answerDiv").slideDown();
            
        }
        document.location.href = '#answerDiv';
        questionID = currentID;
        $.ajax({
            url: "api/questions/" + questionID + "/hint",
            //async: false,
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (data) {
                $('#hintLabel').text(data.hint);
                if (data.image.length > 0) {
                    $('#hintImage').attr("src", "/Images/" + data.image);
                } else {
                    $('#hintImage').removeAttr("src");
                }
            }
        });
    } else {
        if (isVisible === true) {
            if ($("#answerBox").hasClass('color-scheme2')) {
                $("#answerBox").removeClass('color-scheme2');
            }
            $("#answerBox").val("");
            $("#answerDiv").slideUp();
            
        } else {
            $("#answerDiv").slideDown();
            document.location.href = '#answerDiv';
        }
    }
}

function checkAnswer() {
    var teamID = getCookieValue();

    if (teamID === 0) {
        window.location.reload(true);
    }
    else {
        var answer = $("#answerBox").val();

        $.ajax({
            type: "POST",
            url: "api/teams/" + teamID + "/questions/" + questionID + "/answer",
            dataType: "json",
            async: false,
            data: { Text: answer },
            success: function (data) {
                if (data.correct === true) {
                    $("#t_" + questionID).text("Išspręsta");
                    $("#t_" + questionID).removeClass("color-unsolved").addClass("color-solved");
                    $("#b_" + questionID).attr("disabled", "disabled");
                    $("#answerDiv").slideUp();
                    $("#answerBox").val("");
                    if (data.reload === true) {
                        setTimeout(function () {
                            window.location.reload(true);
                        }, 2000);
                    }
                }
                else {
                    $("#answerBox").val("Atsakymas neteisingas");
                    $("#answerBox").addClass('color-scheme2');
                }
            }
        });
    }
}

function checkSafeCode() {
    var teamID = getCookieValue();

    if (teamID === 0) {
        window.location.reload(true);
    }
    else {
        var answer = $("#codeBox").val();

        $.ajax({
            type: "POST",
            url: "api/teams/" + teamID + "/safe",
            dataType: "json",
            async: false,
            data: { Text: answer },
            success: function (data) {
                if (data.correct === true) {
                    $("#codeBox").val("Kodas teisingas");
                    $("#codeButton").attr("disabled", "disabled");
                    if (data.reload === true) {
                        setTimeout(function () {
                            window.location.reload(true);
                        }, 2000);
                    }
                }
                else {
                    $("#codeBox").val("Atsakymas neteisingas");
                    $("#codeBox").addClass('color-scheme2');
                }
            }
        });
    }
}

function clicked() {
    //$("#teamMembers").slideToggle();
    if ($("#teamMembers").hasClass("hidden") === true) {
        $("#teamMembers").removeClass("hidden");
        $("#teamMembers").slideDown();
    } else {
        $("#teamMembers").addClass("hidden");
        $("#teamMembers").slideUp();
    }
}