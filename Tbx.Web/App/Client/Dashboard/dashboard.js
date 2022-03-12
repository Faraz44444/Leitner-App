window.addEventListener("load", function (event) {
    var filterTimeout = null;
    var today = new Date();
    var ThisMonthPeriod = {
        DateFrom: today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + 1,
        DateTo: today.getFullYear() + '-' + (today.getMonth() + 2) + '-' + 1,
    }
    var LastMonthPeriod = {
        DateFrom: today.getFullYear() + '-' + (today.getMonth()) + '-' + 1,
        DateTo: today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + 1,
    }
    var ThisYearPeriod = {
        DateFrom: today.getFullYear() + '-' + 1 + '-' + 1,
        DateTo: today.getFullYear() + '-' + 12 + '-' + 1,
    }
    var Months = ["January", "Februrary", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"]
    var app = new Vue({
        el: '#app',
        data: {
            payment: {
                expenditures: {
                    total: {
                        thisMonthFilter: {
                            IsDeposit: false,
                            DateFrom: ThisMonthPeriod.DateFrom,
                            DateTo: ThisMonthPeriod.DateTo
                        },
                        lastMonthFilter: {
                            IsDeposit: false,
                            DateFrom: LastMonthPeriod.DateFrom,
                            DateTo: LastMonthPeriod.DateTo
                        }
                    }
                },
                incomes: {
                    total: {
                        thisMonthFilter: {
                            IsDeposit: true,
                            DateFrom: ThisMonthPeriod.DateFrom,
                            DateTo: ThisMonthPeriod.DateTo
                        },
                        lastMonthFilter: {
                            IsDeposit: true,
                            DateFrom: LastMonthPeriod.DateFrom,
                            DateTo: LastMonthPeriod.DateTo
                        }
                    }
                },
                savings: {
                    thisMonthFilter: {
                        DateFrom: ThisMonthPeriod.DateFrom,
                        DateTo: ThisMonthPeriod.DateTo
                    },
                    lastMonthFilter: {
                        DateFrom: LastMonthPeriod.DateFrom,
                        DateTo: LastMonthPeriod.DateTo
                    }
                },

                details: {},
                loadingItems: false,

                thisMonthExpenditures: 0,
                thisYearExpenditures: [],
                lastMonthExoenditures: 0,

                thisMonthSavings: 0,
                lastMonthSavings: 0,
                thisYearSavings: 0,

                thisMonthIncome: 0,
                lastMonthIncome: 0,
                thisYearIncomes: 0,

                expendituresChartData: [],
                incomesChartData: [],
                overviewChart: null
            },
        },
        computed: {
            DatePeriods: function () {
                var now = () => moment(moment().utcOffset(0, false))

                return {
                    0: { Name: "All", FromDate: undefined, ToDate: undefined },
                    1: { Name: "Today", FromDate: now().startOf('day').toISOString(), ToDate: now().endOf('day').toISOString() },
                    2: { Name: "1 Week", FromDate: now().startOf('day').add(-1, 'week').toISOString(), ToDate: now().endOf('day').toISOString() },
                    3: { Name: "1 Month", FromDate: now().startOf('day').add(-1, 'month').toISOString(), ToDate: now().endOf('day').toISOString() },
                    4: { Name: "3 Months", FromDate: now().startOf('day').add(-3, "month").toISOString(), ToDate: now().endOf('day').toISOString() },
                    5: { Name: "1 Year", FromDate: now().startOf('day').add(-1, 'year').toISOString(), ToDate: now().endOf('day').toISOString() },
                }
            }
        },
        watch: {
            'payment.loadingItems': function () {
                if (this.loadingRequisitionItems) {
                    loadHandler.AddTableLoader("#datalist");
                } else {
                    loadHandler.RemoveTableLoader("#datalist");
                }
            }
        },
        filters: {
            price: function (price) {
                return date.format('##,###.##');
            }
        },
        methods: {
            getMonthlyExpenditures: function () {
                this.payment.isLoading = true;

                apiService.GetList("payment/sum", this.payment.expenditures.total.thisMonthFilter)
                    .then(data => {
                        this.payment.thisMonthExpenditures = data;

                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.payment.isLoading = false;
                    });

                apiService.GetList("payment/sum", this.payment.expenditures.total.lastMonthFilter)
                    .then(data => {
                        this.payment.lastMonthExpenditures= data;

                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.payment.isLoading = false;
                    });


                this.expendituresChartData = [];

                this.payment.expenditures.total.lastMonthFilter.DateFrom = ThisYearPeriod.DateFrom;
                this.payment.expenditures.total.lastMonthFilter.DateTo = ThisYearPeriod.DateTo;
                apiService.GetList("payment/sums", this.payment.expenditures.total.lastMonthFilter)
                    .then(data => {
                        this.payment.thisYearExpenditures = data;

                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.payment.isLoading = false;
                    });
            },
            getMonthlyIncomes: function () {
                this.payment.isLoading = true;

                apiService.GetList("payment/sum", this.payment.incomes.total.thisMonthFilter)
                    .then(data => {
                        this.payment.thisMonthIncome = data;

                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.payment.isLoading = false;
                    });

                apiService.GetList("payment/sum", this.payment.incomes.total.lastMonthFilter)
                    .then(data => {
                        this.payment.lastMonthIncome = data;

                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.payment.isLoading = false;
                    });

                this.payment.incomes.total.lastMonthFilter.DateFrom = ThisYearPeriod.DateFrom;
                this.payment.incomes.total.lastMonthFilter.DateTo = ThisYearPeriod.DateTo;
                apiService.GetList("payment/sums", this.payment.incomes.total.lastMonthFilter)
                    .then(data => {
                        this.payment.thisYearIncomes = data;
                        this.loadTurnoverChart();

                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.payment.isLoading = false;
                    });
            },
            getSavings: function () {
                this.payment.isLoading = true;

                apiService.GetList("payment/saving", this.payment.savings.thisMonthFilter)
                    .then(data => {
                        this.payment.thisMonthSavings = data;

                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.payment.isLoading = false;
                    });

                apiService.GetList("payment/saving", this.payment.savings.lastMonthFilter)
                    .then(data => {
                        this.payment.lastMonthSavings = data;

                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.payment.isLoading = false;
                    });
            },
            getRequisitionList: function () {


            },
            drawLoanSatisticsChartData: function () {

            },
            loadTurnoverChart: function () {

                let data = {
                    labels: Months,
                    datasets: [
                        {
                            label: 'Expenditure',
                            data: this.payment.thisYearExpenditures,
                            borderColor: 'rgba(255, 99, 132, 1)',
                            backgroundColor: 'rgba(255, 99, 132, 0.2)',
                            borderWidth: 2,
                            borderSkipped: false,
                        },
                        {
                            label: 'Income',
                            data: this.payment.thisYearIncomes,
                            borderColor: 'rgba(100, 200, 255, 1)',
                            backgroundColor: 'rgba(100, 200, 255, 0.2)',
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
                if (this.overviewChart) overviewChart.destroy();
                this.overviewChart = new Chart(ctx, config);
            },
        },
        created: function () {
        },
        mounted: function () {
            eventHandler.CalculateTableHeights();

            this.getMonthlyExpenditures();
            this.getMonthlyIncomes();
            this.getSavings();
        }
    })

}, false);
