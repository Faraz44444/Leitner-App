window.addEventListener("load", function () {
    var app = new Vue({
        el: '#app',
        data: {
            oldPassword: "",
            newPassword: "",
            confirmedPassword: ""
        },
        computed: {

        },
        watch: {
            oldPassword: function () {
                if (this.oldPassword.trim() == "")
                    return $("#oldPassword").removeClass("input-error").addClass("input-error");
                return $("#oldPassword").removeClass("input-error");
            },
            newPassword: function () {
                if (this.newPassword.trim() == "")
                    return $("#newPassword").removeClass("input-error").addClass("input-error");
                return $("#newPassword").removeClass("input-error");
            },
            confirmedPassword: function () {
                if (this.confirmedPassword.trim() == "")
                    return $("#confirmedPassword").removeClass("input-error").addClass("input-error");
                return $("#confirmedPassword").removeClass("input-error");
            }
        },
        methods: {
            save: function () {
                if (!formvalidation.Validate("detailsSection"))
                    return;

                if (this.newPassword != this.confirmedPassword)
                    return $("#confirmedPassword").removeClass("input-error").addClass("input-error");

                loadHandler.AddGlobalLoader();
                return apiService.PostRequest("account/updatePassword", { OldPassword: this.oldPassword, NewPassword: this.newPassword, ConfirmedNewPassword: this.confirmedPassword }).then(function (response) {
                    if (response.Success)
                        window.location.replace("/account");
                }, function (error) {
                    feedback.DisplayError(error);
                }).always(function () {
                    loadHandler.RemoveGlobalLoader();
                });
            }
        },
        created: function () {

        },
        mounted: function () {

        }
    })

}, false);
