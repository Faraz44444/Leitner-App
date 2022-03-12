window.addEventListener("load", function (event) {
    var filterTimeout = null;
    var localStorageKey = "filterArticleList";
    var app = new Vue({
        el: "#app",
        data: {
            business: {
                isLoading: false,
                filter: {
                    BusinessName: "",
                    BusinessPriority: null,
                    createdFrom: null,
                    createdTo: null,
                    CurrentPage: 1,
                    ItemsPerPage: 50,
                    OrderBy: 1,
                    OrderByDesc: true
                },
                details: {
                    Month: 1,
                },

                items: [],
                showCreatedAt: false
            }
        },
        methods: {
            fetchBusinesss: function () {
                this.business.isLoading = true;

                return apiService.GetList("business", this.business.filter)
                    .then(data => {
                        this.business.items = data.Items;
                        this.business.TotalNumberOfItems = data.TotalNumberOfItems;
                        this.business.CurrentPage = data.CurrentPage;

                        pagination.InfiniteScroll(this.$refs.businessTable, data, this.getNextPage);
                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.business.isLoading= false;
                    });
            },

            saveBusiness: function () {
                //if (this.business.details.BusinessId > 0) return;

                this.business.isLoading = true;

                return apiService.PostRequest("business", this.business.details)
                    .then(function (response) {
                        if (response) {
                            window.location.reload();
                        }
                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        loadHandler.RemoveGlobalLoader();
                        app.loadingItems = false;
                    });

            },

            createNewBusiness: function () {
                this.business.details = {};
                this.business.modal.show();
            },
            showBusinessDetails: function (business) {
                this.business.details = business;
                this.business.modal.show();
            },

            getNextPage: function () {
                this.business.filter.CurrentPage++;
                this.fetchBusinesss();
            },
            filterChanged: function () {
                this.business.items = [];
                this.business.filter.CurrentPage = 1;
                this.fetchBusinesss();
            },
            filterChangedTimeout: function () {
                clearTimeout(filterTimeout);
                filterTimeout = setTimeout(this.filterChanged, 500);
            },
            orderBy: function (value) {
                this.business.filter = orderByHandler.Handle(this.business.filter, value);
                this.filterChanged();
            },
            openModal: function () {
                $("#modalNewBusiness").modal("show");

            }
        },
        computed: {

        },
        filters: {
            moment: function (date) {
                return moment(date).format('DD.MM.YY HH:mm');
            }

        },
        watch: {
            'business.filter.BusinessName': function () {
                this.business.filter.CurrentPage = 1;
                this.fetchBusinesss();
            },
            'business.filter.CreatedAt': function () {
                this.business.filter.CurrentPage = 1;
                this.fetchBusinesss();
            },


            'business.isLoading': function (value) {
                if (this.loadingItems) {
                    loadHandler.AddTableLoader("#datalist");
                } else {
                    loadHandler.RemoveTableLoader("#datalist");
                }
            }
        },
        mounted: function () {
            this.fetchBusinesss();
        },
    });

}, false);
