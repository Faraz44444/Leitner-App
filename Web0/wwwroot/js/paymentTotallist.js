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
            PaymentTotalDetails: {
                IsDeposit: false,
            },
            businessFilter: {

            },
            items: [],
            searchResultBusinesses: [],
            loadingAvailableBusinesses: false
        }

    },
    watch: {
        'filter.Name': function () {
            this.filterChagnedDelayed();
        },
        'PaymentTotalDetails.Date': function () {
            console.log(this.PaymentTotalDetails.Date);
        }
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
        openPaymentDetailsModal: function (item) {
            formvalidation.removeInputErrorClass("PaymentTotalDetails");

            if (item) {
                this.PaymentTotalDetails = Object.assign({}, item);
            }
            else {
                this.PaymentTotalDetails = {
                    IsPaidToPerson: false,
                    IsDeposit: false
                };
            }
            var myModal = new bootstrap.Modal(document.getElementById('PaymentTotalDetails'), {
                keyboard: false
            })
            myModal.show();
        },
        savePayment: function () {
            if (!formvalidation.Validate("PaymentTotalDetails")) return;
            this.PaymentTotalDetails.Date = $("#datepickere").datepicker('getDate');

            if (this.PaymentTotalDetails.PaymentId > 0) {
                return apiHandler.Post("paymentTotal/" + this.PaymentTotalDetails.PaymentId, this.PaymentTotalDetails).then(responce => {
                });
            }
            else {
                return apiHandler.Post("paymentTotal/", this.PaymentTotalDetails).then(responce => { });
            }
            this.getList();
        },
        scanBusiness: function () {
            if (this.PaymentTotalDetails.BusinessName.length < 1) return;
            this.loadingAvailableBusinesses = true;
            loadHandler.AddGlobalLoader();

            this.businessFilter.BusinessName = this.PaymentTotalDetails.BusinessName;
            return apiService.GetList("business/lookup", this.businessFilter).then(response => {
                this.searchResultBusinesses = response;
                $("#searchBusinesses").focus();
            }, (error) => {
                feedback.DisplayError(error);
            }).always(() => {
                loadHandler.RemoveGlobalLoader();
                this.loadingAvailableBusinesses = false
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
    },
    mounted: function () {
        $("#datepickere").datepicker();
    }
})

