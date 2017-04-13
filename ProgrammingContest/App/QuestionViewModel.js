var QuestionsViewModel = function () {
    var self = this;
    self.questions = ko.observableArray();

    self.watchModel = function (model, callback) {
        for (var key in model) {
            if (model.hasOwnProperty(key) && ko.isObservable(model[key])) {
                self.subscribeToProperty(model, key, function (key, val) {
                    callback(model, key, val);
                });
            }
        }
    }

    self.subscribeToProperty = function (model, key, callback) {
        model[key].subscribe(function (val) {
            callback(key, val);
        });
    }

    self.modelChanged = function (model, key, val) {
    }

    var userID = getCookieValue();
    var cmdURL = "api/questions/" + userID;
    $.getJSON(cmdURL, function (data) {
        self.questions(ko.utils.arrayMap(data, function (question) {
            var observableQuestion = {
                orderNo: question.orderNo,
                questionID: question.questionID,
                questionText: question.questionText,
                isSolved: question.isSolved,
                questionImage: question.Image.length > 0 ? "/Images/" + question.Image: null,
                answer: question.answer, //ko.observable(question.answer),
                textBox: 't_' + question.questionID,
                button: 'b_' + question.questionID,
                cssClass: question.isSolved === true ? "color-solved" : "color-unsolved"
            }
            
            self.watchModel(observableQuestion, self.modelChanged);
            return observableQuestion;
        }))
    });
}

ko.applyBindings(new QuestionsViewModel(), document.getElementById('questionsTable'));

function getCookieValue() {
    var b = new Array();
    var c = new Array();
    b = document.cookie.split('=');
    c = b[2].split(':');
    return c[0];
}