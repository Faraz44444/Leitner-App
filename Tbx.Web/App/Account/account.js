window.addEventListener("load", function () {
    var app = new Vue({
        el: '#app',
        data: {
            currentSiteId: 0,
            currentSupplierId: 0,
            details: {}
        },
        computed: {

        },
        watch: {
            'details.CssFontSize': function () {
                $("body").css("font-size", this.details.CssFontSize + "px");
                $("html").css("font-size", this.details.CssFontSize + "px");
            }
        },
        methods: {
            getDetails: function () {
                loadHandler.AddGlobalLoader();
                return apiService.GetList("account", null).then(function (response) {
                    app.details = response;
                    if (app.details.ForceUserInformationUpdate) {
                        app.details.FirstName = "";
                        app.details.LastName = "";
                        feedback.DisplayMessage("Info", "We need you to update your user information. Please fill inn required fields and click 'save' before proceeding.")
                    }
                }, function (error) {
                    feedback.DisplayError(error);
                }).always(function () {
                    loadHandler.RemoveGlobalLoader();
                });
            },
            save: function () {
                if (!formvalidation.Validate("detailsSection"))
                    return;

                loadHandler.AddGlobalLoader();
                return apiService.PostRequest("account", this.details).then(function (response) {                    
                    window.location.href = "/dashboard";
                }, function (error) {
                    feedback.DisplayError(error);
                }).always(function () {
                    loadHandler.RemoveGlobalLoader();
                });
            }
        },
        created: function () {
            this.currentSiteId = currentUser.CurrentSiteId;
            this.currentSupplierId = currentUser.CurrentSupplierId;
            this.getDetails();
        },
        mounted: function () {

        }
    })

}, false);
