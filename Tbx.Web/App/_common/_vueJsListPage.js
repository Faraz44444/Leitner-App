window.addEventListener("load", function (event) {
    var filterTimeout = null;
    var app = new Vue({
        el: '#app',
        data: {
            filter: {

                OrderBy: 1,
                OrderByDirection: 1,
                ItemsPerPage: 50,
                CurrentPage: 1
            },
            items: []
        },
        computed: {

        },
        watch: {
            'filter.InputProperty': function () {
                this.filterChangedTimeout();
            },
            'filter.Select': function () {
                this.filterChanged();
            }
        },
        methods: {
            getList: function () {
                loadHandler.AddTableLoader("#datalist");
                return apiService.GetList("url", this.filter).then(function (response) {
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
                this.filter.CurrentPage = 1;
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
            }
        },
        created: function () {
            this.getList();
        },
        mounted: function () {

        }
    })

}, false);
