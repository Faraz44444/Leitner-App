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
            overalItems: [],
            searchResultBusinesses: [],
            dataDates: { FirstDate: null, LastDate: null },
            years: [],
            overalChart: null,
            yearlyChart: null,
            selectedYear: null,
            selectedYearItems: []
        }

    },
    watch: {
        'selectedYear': function () {
            this.selectedYearItems = [];
            this.selectedYearItems = this.overalItems.filter(x => {
                let date = new Date(x.Date).getFullYear();
                return date == this.selectedYear;
            })
            this.loadYearlyChart();
        },
    },
    computed: {

    },
    methods: {
        getList: function () {

            if (this.filter.Loading) return;
            this.filter.Loading = true;
            return apiHandler.Get("reports/yearlytotaloverview", this.filter).then(response => {
                this.overalItems = response;
                this.loadOveralChart();
                this.filter.Loading = false;
                if (!this.selectedYear) {
                    this.selectedYear = new Date().getFullYear();
                }
            });
        },
        loadOveralChart: function () {
            let expenditures = this.overalItems.filter(x => !x.IsDeposit).map(x => x.Price)
            let incomes = this.overalItems.filter(x => x.IsDeposit).map(x => x.Price)
            let data = {
                labels: this.overalItems.filter(x => x.IsDeposit).map(x => x.FormattedDate),
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

            var ctx = $("#overalChart");
            if (this.overalChart) this.overalChart.destroy();
            this.overalChart = new Chart(ctx, config);
        },
        loadYearlyChart: function () {
            let expenditures = this.selectedYearItems.filter(x => !x.IsDeposit).map(x => x.Price)
            let incomes = this.selectedYearItems.filter(x => x.IsDeposit).map(x => x.Price)
            let data = {
                labels: this.selectedYearItems.filter(x => x.IsDeposit).map(x => x.FormattedDate),
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

            var ctx = $("#yearlyChart");
            if (this.yearlyChart) this.yearlyChart.destroy();
            this.yearlyChart = new Chart(ctx, config);
        },
        getDates: function () {
            if (this.datesFilter.Loading) return;
            this.datesFilter.Loading = true;
            apiHandler.Get("reports/yearlytotaloverview/lastDate", this.filter).then(response => {
                this.dataDates.LastDate = response;
                this.datesFilter.Loading = false;
            }).then(() => {
                apiHandler.Get("reports/yearlytotaloverview/firstDate", this.filter).then(response => {
                    this.dataDates.FirstDate = response;
                    this.datesFilter.Loading = false;
                    this.years = this.computeYears();

                });
            });
        },
        computeYears: function () {
            let startYear = new Date(this.dataDates.FirstDate).getFullYear();
            let endYear = new Date(this.dataDates.LastDate).getFullYear()
            let years = []
            for (let i = startYear; i <= endYear; i++) {
                years.push(i);
            }
            return years;
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
        this.getDates();
    },
    mounted: function () {
        $("#datepickere").datepicker();
    }
})

