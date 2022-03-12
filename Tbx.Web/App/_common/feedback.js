var feedback = (function () {
    'use strict';
    let hideModalTimeout;
    return {
        DisplayError: displayError,
        DisplayMessage: displayMessage
    };

    function displayError(error, hideAfterMs = 0) {
        var title = "Warning";
        var message = "<p>An error occured during this operation.We are sorry for the inconvenience.</p><p>Please contact system provider.</p>";

        if (error && error.responseJSON) {
            let replaceMessage = true;
            if (error.responseJSON.ExceptionMessage && typeof error.responseJSON.ExceptionMessage === "string") {
                message = error.responseJSON.ExceptionMessage;
                replaceMessage = false;
            }
            if (error.responseJSON.Message && typeof error.responseJSON.Message === "string") {
                if (replaceMessage) {
                    message = error.responseJSON.Message;
                } else {
                    message += "<p></p>" + error.responseJSON.Message;
                }
            }
            if (error.responseJSON.Title && typeof error.responseJSON.Title === "string") {
                title = error.responseJSON.Title;
            }
        }

        if (error && error.responseJSON && error.responseJSON.ExceptionMessage)
            message = error.responseJSON.ExceptionMessage;
        if (error && error.responseJSON && error.responseJSON.Title)
            title = error.responseJSON.Title;

        var _message = "<p><strong>" + message + "</strong></p>";

        $("#globalModal .modal-title").html(title);
        $("#globalModal .modal-body").html(_message);
        $("#globalModal").modal("show");

        if (hideAfterMs > 0) {
            clearTimeout(hideModalTimeout);
            hideModalTimeout = setTimeout(function () {
                $("#globalModal").modal("hide");
            }, hideAfterMs);
        }
    }

    function displayMessage(title, message, hideAfterMs = 0) {
        var _message = "<p><strong>" + message + "</strong></p>";

        $("#globalModal .modal-title").html(title);
        $("#globalModal .modal-body").html(_message);
        $("#globalModal").modal("show");

        if (hideAfterMs > 0) {
            clearTimeout(hideModalTimeout);
            hideModalTimeout = setTimeout(function () {
                $("#globalModal").modal("hide");
            }, hideAfterMs);
        }
    }

})();