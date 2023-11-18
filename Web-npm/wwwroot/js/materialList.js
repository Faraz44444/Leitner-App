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
        getMaterials: function () {
            var request = { Deleted: false };
            return apiHandler.Get("material", request).then(response => {
                this.materials = response;
                this.categories.unshift({ CategoryId: 0, Name: "All" })

            })
        },
        getCategories: function () {
            var request = { Deleted: false };
            return apiHandler.Get("category/lookup", request).then(response => {
                this.categories = response;
                this.categories.unshift({ CategoryId: 0, Name: "All" })

            })
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
        this.getMaterials();
        this.getCategories();
    },
    mounted: function () {
    }
})
