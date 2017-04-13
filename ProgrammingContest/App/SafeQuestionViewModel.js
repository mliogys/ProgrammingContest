var SafeViewModel = function () {
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
    var cmdURL = "api/questions/safe";
    $.getJSON(cmdURL, function (data) {
        self.questions(ko.utils.arrayMap(data, function (question) {
            var observableQuestion = {
                orderNo: question.orderNo,
                questionText: question.questionText,
                questionImage: question.Image.length > 0 ? "/Images/" + question.Image : null,
            }

            self.watchModel(observableQuestion, self.modelChanged);
            return observableQuestion;
        }))
    });
}

ko.applyBindings(new SafeViewModel(), document.getElementById('safeQuestionsTable'));
