window.addEventListener("load", function (event) {
    var filterTimeout = null;
    var localStorageKey = "filterArticleList";
    var app = new Vue({
        el: "#app",
        data: {
            payment: {
                isLoading: false,
                filter: {
                    Title: "",
                    PaymentPriorityName: "",
                    BusinessName: "",
                    IsDeposit: null,
                    IsPaidToPerson: null,
                    Type: null,
                    Month: null,
                    Price: 0,
                    Date: null,
                    CreatedByUserId: null,
                    CreatedAt: null,
                    CurrentPage: 1,
                    ItemsPerPage: 50,
                    OrderBy: 6,
                    OrderByDirection: 2
                },
                categoriesFilter: {

                },
                items: [],
                categories: [],
                isLoading: false,
                // MODAL
                modal: undefined,
                details: {
                    PaymentId: 0,
                    PaymentPriorityId: 3,
                },
                showCreatdByAndCreatedAt: false

            },
            paymentPriority: {
                filter: {},
                items: []
            },
            business: {
                filter: {
                    BusinessName: ""
                },
                items: [],
                searchResultBusinesses: [],
                loadingAvailableBusinesses: false
            },
            category: {
                details: {
                    CategoryName: "",
                    CategoryPriority: null,
                }
            },
        },
        methods: {
            fetchPayments: function () {
                this.payment.isLoading = true;

                return apiService.GetList("payment", this.payment.filter)
                    .then(data => {
                        this.payment.items = data.Items;
                        this.payment.TotalNumberOfItems = data.TotalNumberOfItems;
                        this.payment.CurrentPage = data.CurrentPage;

                        pagination.InfiniteScroll("datalist", data, app.fetchPage, app.payment.isLoading);
                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.payment.isLoading = false;
                    });
            },

            savePayment: function () {
                if (this.payment.details.PaymentId < 1 ) {
                    this.payment.isLoading = true;

                    return apiService.PostRequest("payment", this.payment.details)
                        .then((response) => {
                            if (response) {
                                window.location.reload();
                            }
                        }, function (error) {
                            feedback.DisplayError(error);
                        }).always(function () {
                            app.payment.isLoading = false;
                        });
                } else {
                    return apiService.PostRequest("payment/" + this.payment.details.PaymentId, this.payment.details)
                        .then((response) => {
                            if (response) {
                                window.location.reload();
                            }
                        }, function (error) {
                            feedback.DisplayError(error);
                        }).always(function () {
                            app.payment.isLoading = false;
                        });
                }

            },

            createNewPayment: function () {
                this.payment.details = {};
                this.payment.modal.show();
            },
            showPaymentDetails: function (payment) {
            },

            getNextPage: function () {
                this.payment.filter.CurrentPage++;
                this.fetchPayments();
            },
            filterChanged: function () {
                this.payment.items = [];
                this.payment.filter.CurrentPage = 1;
                this.fetchPayments();
            },
            fetchPage: function () {
                this.filter.CurrentPage++;
                this.getList();
            },
            filterChangedTimeout: function () {
                clearTimeout(filterTimeout);
                filterTimeout = setTimeout(function () {
                    app.filterChanged();
                }, 500);
            },
            orderBy: function (value) {
                this.payment.filter = orderByHandler.Handle(this.payment.filter, value);
                this.filterChanged();
            },
            openModal: function (item) {
                if (item) {
                    this.payment.details = Object.assign(this.payment.details, item);
                    this.$forceUpdate();
                } else {
                    this.payment.details = {
                        PaymentId: 0
                    };
                    this.payment.details.Date = Date.now();
                }
                $("#modalNewPayment").modal("show");
            },
            scanBusiness: function () {
                if (this.payment.details.BusinessName.length < 1) return;

                this.business.loadingAvailableBusinesses = true
                loadHandler.AddGlobalLoader();

                this.business.filter.BusinessName = this.payment.details.BusinessName;
                return apiService.GetList("business/lookup", this.business.filter).then(response => {
                    this.business.searchResultBusinesses = response;
                    $("#searchBusinesses").focus();
                }, (error) => {
                    feedback.DisplayError(error);
                }).always(() => {
                    loadHandler.RemoveGlobalLoader();
                    this.business.loadingAvailableBusinesses = false
                });
            },
            selectBusinesss: function (item) {
                this.payment.details.BusinessId = item.BusinessId;
                this.payment.details.BusinessName = item.BusinessName;
            },
            saveCategory: function () {
                this.category.isLoading = true;

                apiService.PostRequest("category", this.category.details)
                    .then(function (response) {
                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        loadHandler.RemoveGlobalLoader();
                        app.loadingItems = false;
                    });
                apiService.GetList("business/lookup", this.business.filter).then(data => {
                    this.business.items = data;
                }, function (error) {
                    feedback.DisplayError(error);
                }).always(function () {
                    app.business.isLoading = false;
                });

            }
        },
        computed: {

        },
        filters: {
            price: function (price) {
                return Intl.NumberFormat('en-US').format(price);
            },
            moment: function (date) {
                return moment(date).format('DD.MM.YY HH:mm');
            }

        },
        watch: {
            'payment.filter.Title': function () {
                this.payment.filter.CurrentPage = 1;
                this.fetchPayments();
            },
            'payment.filter.PaymentPriorityName': function () {
                this.payment.filter.CurrentPage = 1;
                this.fetchPayments();
            },
            'payment.filter.BusinessName': function () {
                this.payment.filter.CurrentPage = 1;
                this.fetchPayments();
            },

            'payment.filter.CategoryName': function () {
                this.payment.filter.CurrentPage = 1;
                this.fetchPayments();
            },

            'payment.filter.IsDeposit': function () {
                this.payment.filter.CurrentPage = 1;
                this.fetchPayments();
            },
            'payment.filter.IsPaidToPerson': function () {
                this.payment.filter.CurrentPage = 1;
                this.fetchPayments();
            },
            'payment.filter.Price': function () {
                this.payment.filter.CurrentPage = 1;
                this.fetchPayments();
            },
            'payment.filter.Date': function () {
                this.payment.filter.CurrentPage = 1;
                this.fetchPayments();
            },
            'payment.filter.CreatedAt': function () {
                this.payment.filter.CurrentPage = 1;
                this.fetchPayments();
            },


            'payment.isLoading': function (value) {
                if (this.loadingItems) {
                    loadHandler.AddTableLoader("#datalist");
                } else {
                    loadHandler.RemoveTableLoader("#datalist");
                }
            }
        },
        mounted: function () {
            this.fetchPayments();

            apiService.GetList("category/lookup", this.payment.categoriesFilter).then(data => {
                this.payment.categories = data;
            }, function (error) {
                feedback.DisplayError(error);
            }).always(function () {
                app.business.isLoading = false;
            });
            apiService.GetList("business/lookup", this.business.filter).then(data => {
                this.business.items = data;
            }, function (error) {
                feedback.DisplayError(error);
            }).always(function () {
                app.business.isLoading = false;
            });
            apiService.GetList("paymentpriority/lookup", this.paymentPriority.filter).then(data => {
                this.paymentPriority.items = data;
            }, function (error) {
                feedback.DisplayError(error);
            }).always(function () {
                app.business.isLoading = false;
            });
        },
    });

}, false);
