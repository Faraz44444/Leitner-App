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
            datesFilter: {
                Loading: false
            },
            detailItemsFilter: {
                Loading: false
            },
            overalItems: [],
            detailItems: [],
            detailsExpendituresSum: 0,
            detailsIncomesSum: 0,

            searchResultBusinesses: [],
            dataDates: { FirstDate: null, LastDate: null },
            years: [],
            months: [],
            selectedYear: null,
            selectedMonth: null,
            selectedItems: [],
            chkShowCreatdByAndCreatedAt: false,
            MonthNames: ["January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"],
            overviewChart: null

        }
    },
    watch: {
        'selectedYear': function () {
            this.setMonths();

        },
        'selectedMonth': function () {
            if (this.selectedMonth) {

                this.selectedItems = [];
                this.selectedItems = this.overalItems.filter(x => {
                    return x.Year == this.selectedYear && x.Month == this.selectedMonth + 1;
                })
                this.getDetailItems();
                this.loadOverviewChart();
            }
        },
        'detailItemsFilter.Title': function () {
            this.filterChagnedDelayed()
        },
        'detailItemsFilter.PaymentPriorityName': function () {
            this.filterChagnedDelayed()
        },
        'detailItemsFilter.BusinessName': function () {
            this.filterChagnedDelayed()
        },
        'detailItemsFilter.Price': function () {
            this.filterChagnedDelayed()
        },
        'detailItemsFilter.IsDeposit': function () {
            this.filterChanged()
        },
        'detailItemsFilter.IsPaidToPerson': function () {
            this.filterChanged()
        },
        'detailItemsFilter.CategoryName': function () {
            this.filterChagnedDelayed()
        },
    },
    computed: {
        SelectedDateExpendituresSum: function () {
            let price = this.selectedItems.filter(x => x.IsDeposit == false).map(x => x.Price).reduce((x, y) => x + y, 0);
            return Intl.NumberFormat('en-US').format(price);
        },
        SelectedDateIncomeSum: function () {
            let price = this.selectedItems.filter(x => x.IsDeposit == true).map(x => x.Price).reduce((x, y) => x + y, 0);
            return Intl.NumberFormat('en-US').format(price);
        },
        SelectedDateSaving: function () {
            let price = this.selectedItems.filter(x => x.IsDeposit == true).map(x => x.Price).reduce((x, y) => x + y, 0) -
                this.selectedItems.filter(x => x.IsDeposit == false).map(x => x.Price).reduce((x, y) => x + y, 0);
            return Intl.NumberFormat('en-US').format(price);

        }
    },
    methods: {
        getList: function () {
            if (this.filter.Loading) return;
            this.filter.Loading = true;
            return apiHandler.Get("reports/monthlyoverview", this.filter).then(response => {
                this.overalItems = response;
                this.filter.Loading = false;
            });
        },
        getDetailItems: function () {
            this.detailItemsFilter.Loading = true;
            var selectedMonth = this.selectedMonth + 1;
            let lastDayOfSelectedMonth = new Date(this.selectedYear, selectedMonth, 0).getDate();
            this.detailItemsFilter.DateFrom = this.selectedYear + "-" + (selectedMonth) + "-" + 1
            this.detailItemsFilter.DateTo = this.selectedYear + "-" + selectedMonth + "-" + lastDayOfSelectedMonth

            return apiHandler.Get("payment/lookup", this.detailItemsFilter).then(response => {
                this.detailsExpendituresSum = Intl.NumberFormat('en-US').format(response.filter(x => x.IsDeposit == false).map(x => x.Price).reduce((x, y) => x + y, 0));
                this.detailsIncomesSum = Intl.NumberFormat('en-US').format(response.filter(x => x.IsDeposit == true).map(x => x.Price).reduce((x, y) => x + y, 0));
                this.detailItems = response;
                this.detailItemsFilter.Loading = false;

                //pagination.InfiniteScroll("datalist", response, app.fetchPage);
            });
        },
        getDates: function () {
            if (this.datesFilter.Loading) return;
            this.datesFilter.Loading = true;
            apiHandler.Get("reports/monthlyoverview/lastDate", this.filter).then(response => {
                this.dataDates.LastDate = response;
                if (!this.selectedYear) {
                    this.selectedYear = new Date(response).getFullYear();
                    this.selectedMonth = new Date(response).getMonth()+1;
                }
                this.datesFilter.Loading = false;
                apiHandler.Get("reports/monthlyoverview/firstDate", this.filter).then(response => {
                    this.dataDates.FirstDate = response;
                    this.datesFilter.Loading = false;
                    this.years = this.computeYears();

                });
            });
        },
        setMonths: function () {
            if (this.selectedYear == new Date(this.dataDates.LastDate).getFullYear()) {
                var lastMonth = new Date(this.dataDates.LastDate).getMonth();
                this.months = [...this.MonthNames.slice(0, lastMonth + 2)];
            }
            else if (this.selectedYear == new Date(this.dataDates.FirstDate).getFullYear()) {
                var firstMonth = new Date(this.dataDates.FirstDate).getMonth();
                this.months = [...this.MonthNames.slice(firstMonth, 13)];
            }
            else {
                this.months = [...this.MonthNames.slice(0, 13)];
            }
            var months = []
            for (i = 0; i < this.months.length; i++) {
                months.push({ Id: i, Name: this.months[i] })
            }
            this.months = months;
        },
        loadOverviewChart: function () {
            let expenditures = [];
            let incomes = [];

            this.selectedItems.map(x => {
                if (x.IsDeposit) {
                    incomes.push(x.Price);
                    expenditures.push(0);
                }
                else {
                    expenditures.push(x.Price);
                    incomes.push(0);
                }
            });
            let data = {
                labels: this.selectedItems.map(x => x.CategoryName),
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
        computeYears: function () {
            let startYear = new Date(this.dataDates.FirstDate).getFullYear();
            let endYear = new Date(this.dataDates.LastDate).getFullYear()
            let years = []
            for (let i = startYear; i <= endYear; i++) {
                years.push({ Id: i, Name: i });
            }
            return years;
        },
        filterChanged: function () {
            this.filter.CurrentPage = 1;
            this.getDetailItems();
        },
        filterChagnedDelayed: function () {
            clearTimeout(timeout);
            timeout = setTimeout(() => {
                this.filterChanged();
            }, 200);
        },
        orderBy: function (value) {
            this.payment.filter = orderByHandler.Handle(this.detailItemsfilter, value);
            this.filterChanged();
        }
    },
    created: function () {
        this.getList();
        this.getDates();
    },
    mounted: function () {
    }
})