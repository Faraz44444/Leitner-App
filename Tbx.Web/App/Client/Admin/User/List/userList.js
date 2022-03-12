window.addEventListener("load", function (event) {
    var filterTimeout = null;
    var app = new Vue({
        el: '#app',
        data: {
            filter: {
                Username: "",
                Email: "",
                FullName: "",
                SiteName: "",
                Active: null,
                OrderBy: 1,
                OrderByDirection: 1,
                ItemsPerPage: 50,
                CurrentPage: 1
            },
            items: [],
            roleList: []
        },
        computed: {

        },
        watch: {
            'filter.Username': function () {
                this.filterChangedTimeout();
            },
            'filter.Email': function () {
                this.filterChangedTimeout();
            },
            'filter.FullName': function () {
                this.filterChangedTimeout();
            },
            'filter.Active': function () {
                this.filterChanged();
            }
        },
        methods: {
            getList: function () {
                loadHandler.AddTableLoader("#datalist");
                return apiService.GetList("user", this.filter).then(function (response) {
                    app.items = app.items.concat(response.Items);
                    pagination.InfiniteScroll("datalist", response, app.fetchPage);
                }, function (error) {
                    feedback.DisplayError(error);
                }).always(function () {
                    loadHandler.RemoveTableLoader("#datalist");
                });
            },
            fetchPage: function () {
                this.filter.CurrentPage++;
                this.getList();
            },
            filterChanged: function () {
                this.items = [];
                this.filter.CurrentPage = 1
                this.getList();
            },
            filterChangedTimeout: function () {
                clearTimeout(filterTimeout);
                filterTimeout = setTimeout(function () {
                    app.filterChanged();
                }, 500);
            },
            orderBy: function (value) {
                this.filter = orderByHandler.Handle(this.filter, value);
                this.filterChanged();
            },
            getRoleList: function () {
                loadHandler.AddTableLoader("#rolelist");
                return apiService.GetList("role/all", null).then(function (response) {
                    app.roleList = response;
                }, function (error) {
                    feedback.DisplayError(error);
                }).always(function () {
                    loadHandler.RemoveTableLoader("#rolelist");
                });
            }
        },
        created: function () {
            this.getList();
            this.getRoleList();
        },
        mounted: function () {
            eventHandler.CalculateTableHeights();
        }
    })

}, false);
