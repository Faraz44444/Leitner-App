$(document).ready(function () {
    $("#txtUsername").focus()

    $("document").on("keydown", function (event) {
        if (event.shiftKey && event.key == "Enter") event.preventDefault(); 
    });

    $("#btnForgottenPassword").on("click", function () {
        $("#resetPasswordUsername").val("");

        let username = $("#txtUsername").val();
        $("#resetPasswordUsername").val(username);

        $("#resetPasswordModal").modal("show");


        // Due to to the tabindex trap on the modal do we have to manually do the tabbing in js
        $("#resetPasswordUsername")
            .on("keydown", function (event) {
                if (event.key == "Enter") {
                    // Support enter
                    event.preventDefault();
                    $("#BtnResetPassword").click();
                }
                else if (event.key == "Tab") {
                    event.preventDefault();

                    if (event.shiftKey)
                        $("#btnModalClose").focus();
                    else
                        $("#BtnResetPassword").focus();
                }
            })
            .focus();

        $("#BtnResetPassword")
            .on("keydown", function (event) { 
                if (event.key == "Tab") {
                    event.preventDefault();

                    if (event.shiftKey)
                        $("#resetPasswordUsername").focus();
                    else
                        $("#btnClose").focus();
                }
            });

        $("#btnClose")
            .on("keydown", function (event) { 
                if (event.key == "Tab") {
                    event.preventDefault();

                    if (event.shiftKey)
                        $("#BtnResetPassword").focus();
                    else
                        $("#btnModalClose").focus();
                }
            });


    });
    $("#BtnResetPassword").on("click", function () {
        $("#resetPasswordModal").modal("hide");
    });
});