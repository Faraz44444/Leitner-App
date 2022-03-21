window.addEventListener("load", function (event) {
    var filterTimeout = null;
    var localStorageKey = "filterArticleList";
    var app = new Vue({
        el: "#app",
        data: {
            totalPayment: {
                isLoading: false,
                filter: {
                    OrderBy: 3
                },
                items: [],
                selectedYearItems: [],
                isLoading: false,
                details: {
                },
                firstRecordDate: null,
                lastRecordDate: null,
                years: null,
                selectedYear: null
            },
            category: {
                details: {

                }
            },
            overalChart: null,
            yearlyChart: null,
        },
        methods: {
            fetchPayments: function () {
                this.totalPayment.isLoading = true;

                return apiService.GetList("paymenttotal/unpaged", this.totalPayment.filter)
                    .then(data => {
                        this.totalPayment.items = data;
                        this.loadOveralChart();
                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.totalPayment.isLoading = false;
                    });
            },
            loadOveralChart: function () {
                let expenditures = this.totalPayment.items.filter(x => !x.IsDeposit).map(x => x.Price)
                let incomes = this.totalPayment.items.filter(x => x.IsDeposit).map(x => x.Price)
                let data = {
                    labels: this.totalPayment.items.filter(x => x.IsDeposit).map(x => x.FormattedDate),
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
                let expenditures = this.totalPayment.selectedYearItems.filter(x => !x.IsDeposit).map(x => x.Price)
                let incomes = this.totalPayment.selectedYearItems.filter(x => x.IsDeposit).map(x => x.Price)
                let data = {
                    labels: this.totalPayment.selectedYearItems.filter(x => x.IsDeposit).map(x => x.FormattedDate),
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
                                text: ''
                            }
                        }
                    },
                };

                var ctx = $("#yearlyChart");
                if (this.yearlyChart) this.yearlyChart.destroy();
                this.yearlyChart = new Chart(ctx, config);
            },
            fetchYears: function () {
                this.totalPayment.isLoading = true;
                apiService.GetList("paymenttotal/firstrecorddate", null).then(data => {
                    this.totalPayment.firstRecordDate = data.Date;
                    apiService.GetList("paymenttotal/lastrecorddate", null).then(data2 => {
                        this.totalPayment.lastRecordDate = data2.Date;
                        this.totalPayment.years = this.computeYears();
                        this.totalPayment.selectedYear = this.totalPayment.years[this.totalPayment.years.length - 1];
                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.totalPayment.isLoading = false;
                    })
                }, function (error) {
                    feedback.DisplayError(error);
                }).always(function () {
                    app.totalPayment.isLoading = false;
                })
            },
            computeYears: function () {
                let startYear = new Date(this.totalPayment.firstRecordDate).getFullYear();
                let endYear = new Date(this.totalPayment.lastRecordDate).getFullYear()
                let years = []
                for (let i = startYear; i <= endYear; i++) {
                    years.push(i);
                }
                return years;
            },
            filterChanged: function () {
                this.payment.items = [];
                this.payment.filter.CurrentPage = 1;
                this.fetchPayments();
            },
            fetchPage: function () {
                this.filter.CurrentPage++;
                this.getList();
            },
            filterChangedTimeout: function () {
                clearTimeout(filterTimeout);
                filterTimeout = setTimeout(function () {
                    app.filterChanged();
                }, 500);
            },
            orderBy: function (value) {
                this.payment.filter = orderByHandler.Handle(this.payment.filter, value);
                this.filterChanged();
            },
        },
        computed: {

        },
        filters: {
            price: function (price) {
                return Intl.NumberFormat('en-US').format(price);
            },
            moment: function (date) {
                return moment(date).format('DD.MM.YY HH:mm');
            }

        },
        watch: {
            'totalPayment.selectedYear': function () {
                this.totalPayment.selectedYearItems = [];
                this.totalPayment.selectedYearItems = this.totalPayment.items.filter(x => {
                    let date = new Date(x.Date).getFullYear();
                    return date == this.totalPayment.selectedYear;
                })
                this.loadYearlyChart();
            },
            'totalPayment.isLoading': function (value) {
                if (this.loadingItems) {
                    loadHandler.AddTableLoader("#datalist");
                } else {
                    loadHandler.RemoveTableLoader("#datalist");
                }
            }
        },
        mounted: function () {
            this.fetchPayments();
            this.fetchYears();
        },
    });

}, false);
