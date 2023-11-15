var timeout = null;
var app = vueContext({
    el: "#app",
    data: function () {
        return {
            filter: {
                Name: "",
                OrderByDirection: "Asc",
                DateFrom: null,
                DateTo: null,
                OrderBy: "Date",
                OrderByDirection: 2,
                ItemsPerPage: 50,
                CategoryId: 0,
                BusinessId: 0,
                CurrentPage: 1,
                IsDeposit: null,
                IsPaidToPerson: null,
                TotalPages: 150,
                Loading: false,
                PaymentPriorityId: 0
            },
            showDetailsModal: false,
            paymentDetails: {
                Title: null,
                IsPaidToPerson: false,
                IsDeposit: false,
            },
            businessFilter: {},
            paymenrRecommendationFilter: {
                Loading: false,
                CurrentPage: 1,
                ItemsPerPage: 10
            },
            value: null,
            categories: [],
            businesses: [],
            priorities: [],
            recommendations: [],
            paymentPriorities: [],
            items: [],
            searchResultBusinesses: [],
            loadingAvailableBusinesses: false,
            showItems: false,
        }

    },
    watch: {
        'filter.Title': function () {
            this.filterChagnedDelayed();
        },
        'paymentDetails.Date': function () {
            console.log(this.paymentDetails.Date);
        },
        'filter.CategoryId': function () {
            this.filterChanged();
        },
        'filter.BusinessId': function () {
            this.filterChanged();
        },
        'filter.PaymentPriorityId': function () {
            this.filterChanged();
        },
        'filter.PriceFrom': function () {
            this.filterChanged();
        },
        'filter.PriceTo': function () {
            this.filterChanged();
        },
        'filter.IsDeposit': function () {
            this.filterChanged();
        },
        'filter.IsPaidToPerson': function () {
            this.filterChanged();
        },
        'filter.DateFrom': function () {
            this.filterChanged();
        },
        'filter.DateTo': function () {
            this.filterChanged();
        },
        'filter.OrderBy': function () {
            this.filterChanged();
        },
        'filter.OrderByDirection': function () {
            this.filterChanged();
        },
        'paymentDetails.BusinessName': function () {
            this.searchResultBusinesses = []
            this.scanBusiness();
        },
        'showItems': function () {
            console.log("show items changed !!")
        }
    },
    computed: {

    },
    methods: {
        getList: function () {
            if (this.filter.Loading) return;
            this.filter.Loading = true;
            return apiHandler.Get("payment", this.filter).then(response => {
                this.items = response.Items;
                this.filter.TotalPages = response.TotalPages;
                this.filter.Loading = false;
            });
        },
        getCategories: function () {
            var request = { Deleted: false };
            return apiHandler.Get("category/lookup", request).then(response => {
                this.categories = response;
                this.categories.unshift({ CategoryId: 0, Name: "All" })

            })
        },
        getBusinesses: function () {
            var request = { Deleted: false };
            return apiHandler.Get("business/lookup", {}).then(response => {
                this.businesses = response;
                this.businesses.unshift({ BusinessId: 0, Name: "All" })

            })
        },
        getPriorities: function () {
            var request = { Deleted: false };
            return apiHandler.Get("paymentpriority/lookup", {}).then(response => {
                this.priorities = response;
                this.priorities.unshift({ PaymentPriorityId: 0, Name: "All" })
            })
        },
        getPaymentPrioririties: function () {
            var request = { Deleted: false };
            return apiHandler.Get("paymentPriority/lookup", request).then(response => {
                this.paymentPriorities = response;
                this.paymentPriorities.unshift({ PaymentPriorityId: 0, Name: "All" })
            })
        },
        getPaymentRecommendations: function () {
            if (this.paymenrRecommendationFilter.Loading) return;
            this.paymenrRecommendationFilter.Loading = true;
            return apiHandler.Get("payment/recommendations", this.paymenrRecommendationFilter).then(response => {
                this.recommendations = response.Items;
                this.paymenrRecommendationFilter.TotalPages = response.TotalPages;
                this.paymenrRecommendationFilter.Loading = false;
            });
        },
        selectRecommendation: function (item) {
            this.paymentDetails.BusinessId = item.BusinessId;
            this.paymentDetails.CategoryId = item.CategoryId;
            this.paymentDetails.CategoryName = item.CategoryName;
            this.paymentDetails.Price = item.AveragePrice;
            this.paymentDetails.BusinessName = item.BusinessName;
        },
        openPaymentDetailsModal: function (item) {
            if (item) {
                this.paymentDetails = Object.assign({}, item);
                this.paymentDetails.Date = new Date(item.Date).toISOString().split('T')[0]
                console.log(new Date(item.Date).toISOString().split('T')[0]);
                let date = new Date(item.Date)
                this.paymentDetails.Date = date.getFullYear() + "-" + ("0" + (date.getMonth() + 1)).slice(-2) + "-" + date.getDate();
            }
            else {
                this.paymentDetails = {
                    IsPaidToPerson: false,
                    IsDeposit: false
                };
            }
            this.showDetailsModal = true;
        },
        closeModal: function () {
            this.showDetailsModal = false;
        },
        savePayment: function () {
            if (!formvalidation.Validate("paymentDetails")) return;

            if (this.paymentDetails.PaymentId > 0) {
                return apiHandler.Put("payment", this.paymentDetails.PaymentId, this.paymentDetails).then(responce => {
                    this.showDetailsModal = false;
                    this.getList();
                });
            }
            else {
                return apiHandler.Post("payment", this.paymentDetails).then(responce => {
                    this.getList();
                });
            }
        },
        scanBusiness: function () {
            if (!this.paymentDetails.BusinessName?.length) return;
            this.loadingAvailableBusinesses = true;

            this.businessFilter.Name = this.paymentDetails.BusinessName;
            return apiHandler.Get("business/lookup", this.businessFilter).then(response => {
                this.searchResultBusinesses = response;
                $("#searchBusinesses").focus();
                this.loadingAvailableBusinesses = false;
                this.$refs.businessLookup.showItems();
            });
        },
        selectBusiness: function (item) {
            this.paymentDetails.BusinessId = item.BusinessId;
            this.paymentDetails.BusinessName = item.Name;
        },
        filterChanged: function () {
            this.filter.CurrentPage = 1;
            this.getList();
        },
        filterChagnedDelayed: function () {
            clearTimeout(timeout);
            timeout = setTimeout(() => {
                this.filterChanged();
            }, 200);
        },
        fetchPage: function (event) {
            this.filter.CurrentPage = event.page;
            this.getList();
        },
        fetchRecommendationPage: function (event) {
            this.paymenrRecommendationFilter.CurrentPage = event.page;
            this.getPaymentRecommendations();
        },
        updateModelDatepickerValue: function (event) {
            this.paymentDetails.Date = event
        },
        setOrderByDirection: function (event) {
            this.filter.OrderByDirection = event;
        },
        setOrderBy: function (event) {
            this.filter.OrderBy = event;
        }
    },
    created: function () {
        this.getList();
        this.getCategories();
        this.getBusinesses();
        this.getPriorities();
        this.getPaymentPrioririties();
        this.getPaymentRecommendations();
    },
    mounted: function () {
    }
})
