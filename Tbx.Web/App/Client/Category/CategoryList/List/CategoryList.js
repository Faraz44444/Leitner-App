window.addEventListener("load", function (event) {
    var filterTimeout = null;
    var localStorageKey = "filterArticleList";
    var app = new Vue({
        el: "#app",
        data: {
            category: {
                isLoading: false,
                filter: {
                    CategoryName: "",
                    CategoryPriority: null,
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
            fetchCategorys: function () {
                this.category.isLoading = true;

                return apiService.GetList("category", this.category.filter)
                    .then(data => {
                        this.category.items = data.Items;
                        this.category.TotalNumberOfItems = data.TotalNumberOfItems;
                        this.category.CurrentPage = data.CurrentPage;

                        pagination.InfiniteScroll(this.$refs.categoryTable, data, this.getNextPage);
                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.category.isLoading= false;
                    });
            },

            saveCategory: function () {
                //if (this.category.details.CategoryId > 0) return;

                this.category.isLoading = true;

                return apiService.PostRequest("category", this.category.details)
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

            createNewCategory: function () {
                this.category.details = {};
                this.category.modal.show();
            },
            showCategoryDetails: function (category) {
                this.category.details = category;
                this.category.modal.show();
            },

            getNextPage: function () {
                this.category.filter.CurrentPage++;
                this.fetchCategorys();
            },
            filterChanged: function () {
                this.category.items = [];
                this.category.filter.CurrentPage = 1;
                this.fetchCategorys();
            },
            filterChangedTimeout: function () {
                clearTimeout(filterTimeout);
                filterTimeout = setTimeout(this.filterChanged, 500);
            },
            orderBy: function (value) {
                this.category.filter = orderByHandler.Handle(this.category.filter, value);
                this.filterChanged();
            },
            openModal: function (item) {
                $("#modalNewCategory").modal("show");

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
            'category.filter.CategoryName': function () {
                this.category.filter.CurrentPage = 1;
                this.fetchCategorys();
            },
            'category.filter.WeeklyLimit': function () {
                this.category.filter.CurrentPage = 1;
                this.fetchCategorys();
            },
            'category.filter.MonthlyLimit': function () {
                this.category.filter.CurrentPage = 1;
                this.fetchCategorys();
            },
            'category.filter.CategoryPriority': function () {
                this.category.filter.CurrentPage = 1;
                this.fetchCategorys();
            },
            'category.filter.IsPaidToPerson': function () {
                this.category.filter.CurrentPage = 1;
                this.fetchCategorys();
            },

            'category.filter.Type': function () {
                this.category.filter.CurrentPage = 1;
                this.fetchCategorys();
            },

            'category.filter.Month': function () {
                this.category.filter.CurrentPage = 1;
                this.fetchCategorys();
            },
            'category.filter.Price': function () {
                this.category.filter.CurrentPage = 1;
                this.fetchCategorys();
            },
            'category.filter.Date': function () {
                this.category.filter.CurrentPage = 1;
                this.fetchCategorys();
            },
            'category.filter.CreatedAt': function () {
                this.category.filter.CurrentPage = 1;
                this.fetchCategorys();
            },


            'category.isLoading': function (value) {
                if (this.loadingItems) {
                    loadHandler.AddTableLoader("#datalist");
                } else {
                    loadHandler.RemoveTableLoader("#datalist");
                }
            }
        },
        mounted: function () {
            this.fetchCategorys();
        },
    });

}, false);
