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
        'filter.Name': function () {
            this.filterChagnedDelayed();
        },
        'PaymentTotalDetails.Date': function () {
            console.log(this.PaymentTotalDetails.Date);
        }
    },
    computed: {
        monthlyExpectedExpenditures: function () {
            const formatter = new Intl.NumberFormat('fr-FR', {
                style: 'currency',
                currency: 'NOK',
            });
            if (this.categories.length > 0)
                return formatter.format(this.categories.filter(x => x.Name != "Saving").map(x => x.MonthlyLimit).reduce((particalSum, a) => particalSum + a, 0));
            else return 0;
        }
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
        getPaymentsSum: function () {
            var today = new Date();
            var lastDayOfMonth = new Date(today.getFullYear(), today.getMonth() + 1, 0);
            var test1 = today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + 1;
            var test2 = today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + lastDayOfMonth.getDate();
            var req = {
                DateFrom: test1,
                DateTo: test2,
            }
            apiHandler.Get("reports/paymentssum", req).then(response => {
                this.sums = response;
            })
        },
        getCategories: function () {
            this.loadingCategories = true;
            var req = { HasMonthlyLimit: true }
            return apiHandler.Get("category/lookup", req).then(response => {
                this.categories = response;
                this.loadingCategories = false;
            });
        },
        getPaymentsOverview: function () {
            if (this.loadingPaymentsOverview) return
            this.loadingPaymentsOverview = true;

            var today = new Date();
            var req = {
                IsDeposit: true,
                Date: today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + 1
            };
            apiHandler.Get("reports/paymentsoverview", req).then(response => {
                this.paymentsOverview.Incomes = response;
            }).then(response2 => {
                req.IsDeposit = false;
                return apiHandler.Get("reports/paymentsoverview", req).then(response3 => {
                    this.paymentsOverview.Expenditures = response3;
                    this.loadingPaymentsOverview = false;
                    this.loadOverviewChart();
                });
            })
        },
        loadOverviewChart: function () {
            let incomes = this.paymentsOverview.Incomes.map(x => x.Price)
            let expenditures = this.paymentsOverview.Expenditures.map(x => x.Price)
            let data = {
                labels: this.paymentsOverview.Incomes.map(x => x.MonthFormatted),
                datasets: [
                    {
                        label: 'Expenditure',
                        data: expenditures,
                        borderColor: 'rgba(255, 99, 132, 1)',
                        backgroundColor: 'rgba(255, 99, 132, 0.2)',
                        borderWidth: 2,
                        borderSkipped: false,
                    },
                    {
                        label: 'incomes',
                        data: incomes,
                        borderColor: 'rgba(100, 99, 255, 1)',
                        backgroundColor: 'rgba(100, 99, 255, 0.2)',
                        borderWidth: 2,
                        borderSkipped: false,
                    }],
            };

            let config = {
                type: 'bar',
                data: data,
                options: {
                    responsive: true,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: 'Overview'
                        }
                    }
                },
            };

            var ctx = $("#overviewChart");
            if (this.overviewChart) this.overviewChart.destroy();
            this.overviewChart = new Chart(ctx, config);
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
        this.getPaymentsSum();
        this.getCategories();
        this.getPaymentsOverview();
    },
    mounted: function () {
        $("#datepickere").datepicker();
    }
})

