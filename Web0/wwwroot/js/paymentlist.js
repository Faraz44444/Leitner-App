var timeout = null;
var app = vueContext({
    el: "#app",
    components: {
        'c-check-box': compCheckbox
    },
    data: function () {
        return {
            filter: {
                Name: "",
                OrderByDirection: "Asc",
                OrderBy: 2,
                ItemsPerPage: 50,
                CurrentPage: 1,
                TotalPages: 150,
                Loading: false
            },
            paymentDetails: {
                IsPaidToPerson: false,
                IsDeposit: false,
            },
            businessFilter: {

            },
            categories: [],
            paymentPriorities: [],
            items: [],
            searchResultBusinesses: [],
            loadingAvailableBusinesses: false
        }

    },
    watch: {
        'filter.Name': function () {
            this.filterChagnedDelayed();
        },
        'paymentDetails.Date': function () {
            console.log(this.paymentDetails.Date);
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
            })
        },
        getPaymentPrioririties: function () {
            var request = { Deleted: false };
            return apiHandler.Get("paymentPriority/lookup", request).then(response => {
                this.paymentPriorities = response;
            })
        },
        openPaymentDetailsModal: function (item) {
            formvalidation.removeInputErrorClass("paymentDetails");

            if (item) {
                this.paymentDetails = Object.assign({}, item);
            }
            else {
                this.paymentDetails = {
                    IsPaidToPerson: false,
                    IsDeposit: false
                };
            }
            var myModal = new bootstrap.Modal(document.getElementById('paymentDetails'), {
                keyboard: false
            })
            myModal.show();
        },
        savePayment: function () {
            if (!formvalidation.Validate("paymentDetails")) return;
            this.paymentDetails.Date = $("#datepickere").datepicker('getDate');

            if (this.paymentDetails.PaymentId > 0) {
                return apiHandler.Post("payment/" + this.paymentDetails.PaymentId, this.paymentDetails).then(responce => {
                });
            }
            else {
                return apiHandler.Post("payment", this.paymentDetails).then(responce => { });
            }
            this.getList();
        },
        scanBusiness: function () {
            if (this.paymentDetails.BusinessName.length < 1) return;
            this.loadingAvailableBusinesses = true;

            this.businessFilter.BusinessName = this.paymentDetails.BusinessName;
            return apiHandler.Get("business/lookup", this.businessFilter).then(response => {
                this.searchResultBusinesses = response;
                $("#searchBusinesses").focus();
            }, (error) => {
                feedback.DisplayError(error);
            });
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
        }
    },
    created: function () {
        this.getList();
        this.getCategories();
        this.getPaymentPrioririties();
    },
    mounted: function () {
        $("#datepickere").datepicker();
    }
})
