var formvalidation = (function () {
    'use strict';

    function validate(formId) {
        var valid = true;

        $(getFormId(formId)).find("input, select, textarea").each(function (idx, el) {
            $(el).removeClass("input-error");

            if (!$(el)[0].checkValidity()) {
                $(el).addClass("input-error");
                valid = false;
            }
            else if ($(el).is("select[required]") && el.value == 0) {
                $(el).addClass("input-error");
                valid = false;
            }


        });

        return valid;
    }
    function removeInputErrorClass(formId) {
        $(getFormId(formId)).find("input, select, textarea").each(function (idx, el) {
            $(el).removeClass("input-error");
        });
    }

    function getFormId(formId) {
        if (!formId)
            return;
        if (formId.charAt(0) !== "#")
            formId = "#" + formId;

        return formId;
    }

    return {
        Validate: validate,
        removeInputErrorClass: removeInputErrorClass
    }
})();