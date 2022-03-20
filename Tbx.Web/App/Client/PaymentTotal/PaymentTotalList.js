window.addEventListener("load", function (event) {
    var filterTimeout = null;
    var localStorageKey = "filterPaymentTotalList";
    var app = new Vue({
        el: "#app",
        data: {
            paymentTotal: {
                isLoading: false,
                filter: {
                    Title: "",
                    Price: null,
                    Date: null,
                    IsDeposit: 0,
                    CreatedAt: 0,
                    CreatedByFullName: 0,
                    CurrentPage: 1,
                    ItemsPerPage: 100,
                    OrderBy: 1,
                    OrderByDesc: true
                },
                details: {
                },

                items: [],
                showCreatedAt: false
            },
            business: {
                filter: {
                    BusinessName: ""
                },
                items: [],
                searchResultBusinesses: [],
                loadingAvailableBusinesses: false
            },
        },
        methods: {
            fetchPaymentTotals: function () {
                this.paymentTotal.isLoading = true;

                return apiService.GetList("paymenttotal", this.paymentTotal.filter)
                    .then(data => {
                        this.paymentTotal.items = data.Items;
                        this.paymentTotal.TotalNumberOfItems = data.TotalNumberOfItems;
                        this.paymentTotal.CurrentPage = data.CurrentPage;

                        pagination.InfiniteScroll(this.$refs.categoryTable, data, this.getNextPage);
                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.paymentTotal.isLoading = false;
                    });
            },

            savePaymentTotal: function () {
                //if (this.paymentTotal.details.PaymentTotalId > 0) return;

                this.paymentTotal.isLoading = true;

                return apiService.PostRequest("paymentTotal", this.paymentTotal.details)
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

            createNewPaymentTotal: function () {
                this.paymentTotal.details = {};
                this.paymentTotal.modal.show();
            },
            showPaymentTotalDetails: function (paymentTotal) {
                this.paymentTotal.details = paymentTotal;
                this.paymentTotal.modal.show();
            },

            getNextPage: function () {
                this.paymentTotal.filter.CurrentPage++;
                this.fetchPaymentTotals();
            },
            filterChanged: function () {
                this.paymentTotal.items = [];
                this.paymentTotal.filter.CurrentPage = 1;
                this.fetchPaymentTotals();
            },
            filterChangedTimeout: function () {
                clearTimeout(filterTimeout);
                filterTimeout = setTimeout(this.filterChanged, 500);
            },
            orderBy: function (value) {
                this.paymentTotal.filter = orderByHandler.Handle(this.paymentTotal.filter, value);
                this.filterChanged();
            },
            openModal: function (item) {
                $("#modalNewPaymentTotal").modal("show");

            },
            scanBusiness: function () {
                if (this.paymentTotal.details.BusinessName.length < 1) return;

                this.business.loadingAvailableBusinesses = true
                loadHandler.AddGlobalLoader();

                this.business.filter.BusinessName = this.paymentTotal.details.BusinessName;
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
                this.paymentTotal.details.BusinessId = item.BusinessId;
                this.paymentTotal.details.BusinessName = item.BusinessName;
            },

        },
        computed: {

        },
        filters: {
            moment: function (date) {
                return moment(date).format('DD.MM.YY HH:mm');
            }

        },
        watch: {
            'paymentTotal.filter.Title': function () {
                this.filterChangedTimeout();
            },
            'paymentTotal.filter.Price': function () {
                this.filterChangedTimeout();
            },
            'paymentTotal.filter.Date': function () {
                this.filterChanged();
            },
            'paymentTotal.filter.IsDeposit': function () {
                this.filterChanged();
            },
            'paymentTotal.filter.CreatedAt': function () {
                this.filterChangedTimeout();
            },
            'paymentTotal.filter.CreatedByFullName': function () {
                this.fetchPaymentTotals();
            },


            'paymentTotal.isLoading': function (value) {
                if (this.loadingItems) {
                    loadHandler.AddTableLoader("#datalist");
                } else {
                    loadHandler.RemoveTableLoader("#datalist");
                }
            }
        },
        mounted: function () {
            this.fetchPaymentTotals();
            apiService.GetList("business/lookup", this.business.filter).then(data => {
                this.business.items = data;
            }, function (error) {
                feedback.DisplayError(error);
            }).always(function () {
                app.business.isLoading = false;
            });
        },
    });

}, false);
