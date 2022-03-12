window.addEventListener("load", function () {
    var app = new Vue({
        el: '#app',
        data: {
            title: "User Details",
            id: 0,
            details: { Active: true },
            userRoles: [],
            siteList: [],
            roleList: []
        },
        computed: {

        },
        watch: {

        },
        methods: {
            getDetails: function () {
                if (this.id < 0)
                    return;
                loadHandler.AddGlobalLoader();
                return apiService.GetById("user", this.id).then(function (response) {
                    app.details = response;
                }, function (error) {
                    feedback.DisplayError(error);
                }).always(function () {
                    loadHandler.RemoveGlobalLoader();
                });
            },
            getUserRoles: function () {
                if (this.id < 0)
                    return;
                return apiService.GetList("user/" + this.id + "/role", null).then(function (response) {
                    return app.userRoles = response;
                }, function (error) {
                    feedback.DisplayError(error);
                });
            },
            save: function () {
                if (!formvalidation.Validate("detailsSection"))
                    return;

                loadHandler.AddGlobalLoader();
                if (this.id == 0) {
                    return apiService.PostRequest("user", this.details).then(function (response) {
                        if (response.Success) {
                            app.id = response.Id;
                            for (i = 0; i < app.userRoles.length; i++) {
                                app.userRoles[i].UserId = response.Id;
                            }
                            app.saveUserRoles()
                        }
                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        loadHandler.RemoveGlobalLoader();
                    });
                } else {
                    return apiService.PostRequest("user/" + this.id, this.details).then(function (response) {
                        if (response.Success) {
                            app.saveUserRoles();
                        }
                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        loadHandler.RemoveGlobalLoader();
                    });
                }
            },
            saveUserRoles: function () {
                if (this.id == 0)
                    return;

                loadHandler.AddGlobalLoader();
                return apiService.PostRequest("user/" + this.id + "/role", { UserSiteList: this.userRoles }).then(function (response) {
                    if (response.Success)
                        window.location.replace("/admin/users");
                }, function (error) {
                    feedback.DisplayError(error);
                }).always(function () {
                    loadHandler.RemoveGlobalLoader();
                });
            }
        },
        created: function () {
            this.id = parseInt($("#app").data("entityId"));

            lookup.RoleList(name = "", false).then(function (response) {
                app.roleList = response;
            });


            if (isNaN(this.id)) {
                this.id = 0;
                this.title = "New User"
                lookup.SiteList().then(function (response) {
                    app.siteList = response;
                    for (i = 0; i < app.siteList.length; i++) {
                        app.userRoles.push({
                            UserId: 0,
                            SiteId: app.siteList[i].Id,
                            SiteName: app.siteList[i].Name,
                            RoleId: 0
                        });
                    }
                });
                return;
            }

            this.getUserRoles().then(function () {
                lookup.SiteList().then(function (response) {
                    app.siteList = response;                    
                    for (i = 0; i < app.siteList.length; i++) {
                        if (!app.userRoles.some(function (x) { return x.SiteId == app.siteList[i].Id })) {
                            app.userRoles.splice(i, 0, {
                                UserId: app.details.UserId,
                                SiteId: app.siteList[i].Id,
                                SiteName: app.siteList[i].Name,
                                RoleId: 0
                            });
                        }
                    }
                    //app.userRoles = app.userRoles.sort(function (a, b) { (a.SiteName > b.SiteName) ? 1 : ((b.SiteName > a.SiteName) ? -1 : 0)});
                });
            });
            this.getDetails();
        },
        mounted: function () {

        }
    })

}, false);
