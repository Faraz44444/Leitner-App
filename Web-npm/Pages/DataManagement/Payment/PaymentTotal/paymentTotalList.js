var timeout = null;
var app = vueContext({
    el: "#app",
    components: {
        'c-checkbox': compCheckbox
    },

    data: function () {
        return {
            filter: {
                Name: "",
                OrderByDirection: 2,
                OrderBy: "Date",
                ItemsPerPage: 50,
                CurrentPage: 1,
                TotalPages: 150,
                Loading: false
            },
            PaymentTotalDetails: {
                IsDeposit: false,
            },
            businessFilter: {

            },
            showItems: false,
            items: [],
            searchResultBusinesses: [],
            loadingAvailableBusinesses: false,
            businesses: [],
            showDetailsModal: false
        }

    },
    watch: {
        'filter.Title': function () {
            this.filterChanged();
        },
        'filter.BusinessId': function () {
            this.filterChanged();
        },
        'filter.IsDeposit': function () {
            this.filterChanged();
        },
        'filter.PriceFrom': function () {
            this.filterChanged();
        },
        'filter.PriceTo': function () {
            this.filterChanged();
        },
        'filter.DateFrom': function () {
            this.filterChanged();
        },
        'filter.DateTo': function () {
            this.filterChanged();
        },
    },
    computed: {

    },
    methods: {
        getList: function () {
            if (this.filter.Loading) return;
            this.filter.Loading = true;
            return apiHandler.Get("paymentTotal", this.filter).then(response => {
                this.items = response.Items;
                this.filter.TotalPages = response.TotalPages;
                this.filter.Loading = false;
            });
        },
        getBusinesses: function () {
            var request = { Deleted: false };
            return apiHandler.Get("business/lookup", {}).then(response => {
                this.businesses = response;
            })
        },
        openPaymentDetailsModal: function (item) {
            if (item) {
                this.PaymentTotalDetails = Object.assign({}, item);
            }
            else {
                this.PaymentTotalDetails = {
                    IsPaidToPerson: false,
                    IsDeposit: false
                };
            }
            this.showDetailsModal = true;
        },
        savePayment: function () {
            if (!formvalidation.Validate("PaymentTotalDetails")) return;

            if (this.PaymentTotalDetails.PaymentId > 0)
                return apiHandler.Post("paymentTotal/" + this.PaymentTotalDetails.PaymentId, this.PaymentTotalDetails).then(responce => {
                    this.getList();
                    this.showDetailsModal = true;
                });
            else
                return apiHandler.Post("paymentTotal", this.PaymentTotalDetails).then(responce => { this.getList(); this.showDetailsModal = true; });
        },
        scanBusiness: function () {
            if (this.PaymentTotalDetails.BusinessName.length < 1) return;
            this.loadingAvailableBusinesses = true;

            this.businessFilter.Name = this.PaymentTotalDetails.BusinessName;
            return apiHandler.Get("business/lookup", this.businessFilter).then(response => {
                this.searchResultBusinesses = response;
                $("#searchBusinesses").focus();
                this.loadingAvailableBusinesses = false;
                this.showItems = true;
            });
        },
        selectBusiness: function (item) {
            this.PaymentTotalDetails.BusinessId = item.BusinessId;
            this.PaymentTotalDetails.BusinessName = item.Name;
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
        closeModal: function () {
            this.showDetailsModal = false;
        },
    },
    created: function () {
        this.getList();
        this.getBusinesses();

    },
    mounted: function () {
        $("#datepickere").datepicker();
    }
})

