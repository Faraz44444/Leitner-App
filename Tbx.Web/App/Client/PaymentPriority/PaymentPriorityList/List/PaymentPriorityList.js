window.addEventListener("load", function (event) {
    var filterTimeout = null;
    var localStorageKey = "filterArticleList";
    var app = new Vue({
        el: "#app",
        data: {
            paymentPriority: {
                isLoading: false,
                filter: {
                    PaymentPriorityName: "",
                    PaymentPriorityPriority: null,
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
            fetchPaymentPrioritys: function () {
                this.paymentPriority.isLoading = true;

                return apiService.GetList("paymentpriority", this.paymentPriority.filter)
                    .then(data => {
                        this.paymentPriority.items = data.Items;
                        this.paymentPriority.TotalNumberOfItems = data.TotalNumberOfItems;
                        this.paymentPriority.CurrentPage = data.CurrentPage;

                        pagination.InfiniteScroll(this.$refs.categoryTable, data, this.getNextPage);
                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.paymentPriority.isLoading= false;
                    });
            },

            savePaymentPriority: function () {
                //if (this.paymentPriority.details.PaymentPriorityId > 0) return;

                this.paymentPriority.isLoading = true;

                return apiService.PostRequest("paymentpriority", this.paymentPriority.details)
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

            createNewPaymentPriority: function () {
                this.paymentPriority.details = {};
                this.paymentPriority.modal.show();
            },
            showPaymentPriorityDetails: function (paymentPriority) {
                this.paymentPriority.details = paymentPriority;
                this.paymentPriority.modal.show();
            },

            getNextPage: function () {
                this.paymentPriority.filter.CurrentPage++;
                this.fetchPaymentPrioritys();
            },
            filterChanged: function () {
                this.paymentPriority.items = [];
                this.paymentPriority.filter.CurrentPage = 1;
                this.fetchPaymentPrioritys();
            },
            filterChangedTimeout: function () {
                clearTimeout(filterTimeout);
                filterTimeout = setTimeout(this.filterChanged, 500);
            },
            orderBy: function (value) {
                this.paymentPriority.filter = orderByHandler.Handle(this.paymentPriority.filter, value);
                this.filterChanged();
            },
            openModal: function () {
                $("#modalNewPaymentPriority").modal("show");

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
            'paymentPriority.filter.PaymentPriorityName': function () {
                this.paymentPriority.filter.CurrentPage = 1;
                this.fetchPaymentPrioritys();
            },
            'paymentPriority.filter.CreatedAt': function () {
                this.paymentPriority.filter.CurrentPage = 1;
                this.fetchPaymentPrioritys();
            },


            'paymentPriority.isLoading': function (value) {
                if (this.loadingItems) {
                    loadHandler.AddTableLoader("#datalist");
                } else {
                    loadHandler.RemoveTableLoader("#datalist");
                }
            }
        },
        mounted: function () {
            this.fetchPaymentPrioritys();
        },
    });

}, false);
