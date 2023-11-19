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
                OrderBy: "CreatedAt",
                OrderByDirection: 2,
                ItemsPerPage: 50,
                MaterialId: 0,
                CategoryId: 0,
                CurrentPage: 1,
                TotalPages: 150,
                Loading: false,
            },
            showDetailsModal: false,
            showBatchDetailsModal: false,
            details: {
                MaterialId: 0,
                Title: null,
                IsPaidToPerson: false,
                IsDeposit: false,
            },
            batchDetails: {
                BatchId: 0,
                BatchNo: null
            },
            businessFilter: {},
            paymenrRecommendationFilter: {
                Loading: false,
                CurrentPage: 1,
                ItemsPerPage: 10
            },
            value: null,
            categories: [],
            batches: [],
            items: [],
        }

    },
    watch: {
        'filter.Title': function () {
            this.filterChagnedDelayed();
        },
    },
    computed: {

    },
    methods: {
        getList: function () {
            return apiHandler.Get("material", this.filter).then(response => {
                this.items = response.Items;
                this.filter.TotalPages = response.TotalPages;
                this.filter.Loading = false;
            })
        },
        getCategories: function () {
            var request = { Deleted: false };
            return apiHandler.Get("category/lookup", request).then(response => {
                this.categories = response;
                this.categories.unshift({ CategoryId: 0, Name: "All" })

            })
        },
        getBatches: function () {
            var request = { Deleted: false };
            return apiHandler.Get("batch/lookup", request).then(response => {
                this.batches = response;
            })
        },
        openMaterialDetailsModal: function (item) {
            if (item) {
                this.details = Object.assign({}, item);
            }
            else {
                this.paymentDetails = {
                    IsPaidToPerson: false,
                    IsDeposit: false
                };
            }
            this.showDetailsModal = true;
        },
        openBatchDetailsModal: function (item) {
            if (item) {
                this.batchDetails = Object.assign({}, item);
            }
            else {
                this.batchDetails = {
                    BatchId: 0,
                    BatchNo: null
                };
            }
            this.showBatchDetailsModal = true;
        },
        closeModal: function () {
            this.showDetailsModal = false;
            this.showBatchDetailsModal = false;
        },
        saveMaterial: function () {
            if (this.details.MaterialId > 0) {
                return apiHandler.Put("material", this.details.MaterialId, this.details).then(() => {
                    this.showDetailsModal = false;
                    this.getList();
                });
            }
            else {
                return apiHandler.Post("material", this.details).then(responce => {
                    this.getList();
                });
            }
        },
        saveBatch: function () {
            if (this.batchDetails.BatchId > 0) {
                return apiHandler.Put("batch", this.batchDetails.BatchId, this.batchDetails).then(responce => {
                    this.showBatchDetailsModal = false;
                    this.getBatches();
                });
            }
            else {
                return apiHandler.Post("batch", this.batchDetails).then(() => {
                    this.getBatches();
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
        this.getList();
        this.getCategories();
        this.getBatches();
    },
    mounted: function () {
    }
})
