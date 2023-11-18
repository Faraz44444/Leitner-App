/******/ (() => { // webpackBootstrap
var __webpack_exports__ = {};
/*!************************!*\
  !*** ./Pages/index.js ***!
  \************************/
﻿var timeout = null;
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
            loadingCategories: false,
            loadingPaymentsOverview: false,
            sums: {
                Incomes: {},
                Expenditures: {},
                Savings: {}
            },
            categories: [],
            paymentsOverview: {
                Incomes: {},
                Expenditures: {}
            },
            overviewChart: null
        }

    },
    watch: {
    },
    computed: {
    },
    methods: {
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
    },
    mounted: function () {
    }
})


/******/ })()
;
//# sourceMappingURL=dashboard.bundle.js.map