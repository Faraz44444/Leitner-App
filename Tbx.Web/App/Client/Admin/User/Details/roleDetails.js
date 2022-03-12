window.addEventListener("load", function () {
    var app = new Vue({
        el: '#app',
        data: {
            title: "Role Details",
            id: 0,
            details: {},
            availablePermissions: [
                { isLocked: false, selected: false, PermissionType: 2000, PermissionGroup: "Requisition", PermissionName: "Requisition" },

                { isLocked: false, selected: false, PermissionType: 2024, PermissionGroup: "Report", PermissionName: "Inventory Report" },
                { isLocked: false, selected: false, PermissionType: 2025, PermissionGroup: "Report", PermissionName: "Loan Report" },
                { isLocked: false, selected: false, PermissionType: 2026, PermissionGroup: "Report", PermissionName: "Sale Report" },

                { isLocked: false, selected: false, PermissionType: 2001, PermissionGroup: "Article", PermissionName: "Article List" },
                { isLocked: false, selected: false, PermissionType: 2002, PermissionGroup: "Article", PermissionName: "Article Group" },
                { isLocked: false, selected: false, PermissionType: 2021, PermissionGroup: "Article", PermissionName: "Article Prefix" },
                { isLocked: false, selected: false, PermissionType: 2022, PermissionGroup: "Article", PermissionName: "Maintenance" },
                { isLocked: false, selected: false, PermissionType: 2023, PermissionGroup: "Article", PermissionName: "Serie List" },

                { isLocked: false, selected: false, PermissionType: 2019, PermissionGroup: "Loan Settings", PermissionName: "Loan Agreements" },
                { isLocked: false, selected: false, PermissionType: 2009, PermissionGroup: "Loan Settings", PermissionName: "Suppliers" },
                { isLocked: false, selected: false, PermissionType: 2020, PermissionGroup: "Loan Settings", PermissionName: "Employees" },

                { isLocked: false, selected: false, PermissionType: 2003, PermissionGroup: "Admin", PermissionName: "Sites" },
                { isLocked: false, selected: false, PermissionType: 2004, PermissionGroup: "Admin", PermissionName: "Warehouses" },
                { isLocked: false, selected: false, PermissionType: 2005, PermissionGroup: "Admin", PermissionName: "Departments" },
                { isLocked: false, selected: false, PermissionType: 2006, PermissionGroup: "Admin", PermissionName: "Client" },
                { isLocked: false, selected: false, PermissionType: 2007, PermissionGroup: "Admin", PermissionName: "Users" },
                { isLocked: false, selected: false, PermissionType: 2008, PermissionGroup: "Admin", PermissionName: "Dimensions" },
                { isLocked: false, selected: false, PermissionType: 2010, PermissionGroup: "Admin", PermissionName: "Units" },
                //{ isLocked: false, selected: false, PermissionType: 2011, PermissionGroup: "Admin", PermissionName: "Delivery Terms" },
                //{ isLocked: false, selected: false, PermissionType: 2012, PermissionGroup: "Admin", PermissionName: "Destinations" },
                //{ isLocked: false, selected: false, PermissionType: 2013, PermissionGroup: "Admin", PermissionName: "Invoice Addresses" },
                //{ isLocked: false, selected: false, PermissionType: 2014, PermissionGroup: "Admin", PermissionName: "Payment Terms" },

            ]
        },
        computed: {
            groupAvailablePermissions() {
                return dataManipulation.GroupBy(this.availablePermissions, "PermissionGroup");
            }
        },
        watch: {

        },
        methods: {
            getDetails: function () {
                if (this.id < 0)
                    return;
                loadHandler.AddGlobalLoader();
                return apiService.GetById("role", this.id).then(function (response) {
                    app.details = response;

                    for (x = 0; x < app.availablePermissions.length; x++) {
                        for (i = 0; i < app.details.PermissionList.length; i++) {
                            if (app.availablePermissions[x].PermissionType === app.details.PermissionList[i].PermissionType) {
                                app.availablePermissions[x].selected = true;
                                app.availablePermissions[x].isLocked = app.details.PermissionList[i].IsLocked;
                            }
                        }
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

                loadHandler.RemoveGlobalLoader()

                this.details.PermissionList = [];
                for (i = 0; i < this.availablePermissions.length; i++) {
                    if (this.availablePermissions[i].selected)
                        this.details.PermissionList.push(this.availablePermissions[i]);
                }

                if (this.id == 0) {
                    return apiService.PostRequest("role", this.details).then(function (response) {
                        if (response.Success)
                            window.location.replace("/admin/users");
                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        loadHandler.RemoveGlobalLoader()
                    });
                } else {
                    return apiService.PostRequest("role/" + this.id, this.details).then(function (response) {
                        if (response.Success)
                            window.location.replace("/admin/users");
                    }, function (error) {
                            feedback.DisplayError(error);
                    }).always(function () {
                        loadHandler.RemoveGlobalLoader();
                    });
                }
            },
            selectPermissions: function (permissions) {
                var select = !permissions.every(x => x.selected)
                permissions.forEach(x => x.selected = select);
            }
        },
        created: function () {
            this.id = parseInt($("#app").data("entityId"));

            if (isNaN(this.id)) {
                this.id = 0;
                this.title = "New Role"
                return;
            }
            this.getDetails();
        },
        mounted: function () {

        }
    })

}, false);
