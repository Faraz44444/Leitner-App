window.addEventListener("load", function (event) {
    var filterTimeout = null;
    var today = new Date();

    var firstDayOfWeek = today.getDate() - today.getDay(); // First day is the day of the month - the day of the week
    var lastDayOfWeek = firstDayOfWeek + 6; // last day is the first day + 6
    var ThisWeekPeriod = {
        DateFrom: today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + firstDayOfWeek,
        DateTo: today.getFullYear() + '-' + (today.getMonth() + 2) + '-' + lastDayOfWeek,
    }
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
                        thisYearFilter: {
                            IsDeposit: false,
                            DateFrom: ThisYearPeriod.DateFrom,
                            DateTo: ThisYearPeriod.DateTo
                        },
                    }
                },
                incomes: {
                    total: {
                        thisYearFilter: {
                            IsDeposit: true,
                            DateFrom: ThisYearPeriod.DateFrom,
                            DateTo: ThisYearPeriod.DateTo
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
                groceries: {
                    categoryFilter: {
                        CategoryName: "Groceries"
                    },
                    categoryDetails: {},
                    paymentFilter: {
                        CategoryId: 0,
                        DateFrom: ThisWeekPeriod.DateFrom,
                        DateTo: ThisWeekPeriod.DateTo
                    },
                    thisWeekSum: 0,
                },
                eatingOut: {
                    categoryFilter: {
                        CategoryName: "Eating Out"
                    },
                    categoryDetails: {},
                    paymentFilter: {
                        CategoryId: 0,
                        DateFrom: ThisWeekPeriod.DateFrom,
                        DateTo: ThisWeekPeriod.DateTo
                    },
                    thisWeekSum: 0,
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

                overviewChart: null,
                groceriesPie: null,
                eatingOutPie: null
            },
            thisMonth: today.getMonth(),
            lastMonth: today.getMonth() - 1,
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
                apiService.GetList("payment/sums", this.payment.expenditures.total.thisYearFilter)
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
                apiService.GetList("payment/sums", this.payment.incomes.total.thisYearFilter)
                    .then(data => {
                        this.payment.thisYearIncomes = data;
                        this.loadOverviewChart();

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
            getGroceries: function () {
                apiService.GetList("category/lookup", this.payment.groceries.categoryFilter)
                    .then(data => {
                        this.payment.groceries.categoryDetails = data;

                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.payment.isLoading = false;
                    }).then((data2) => {
                        this.payment.groceries.paymentFilter.CategoryId = this.payment.groceries.categoryDetails[0].CategoryId
                        apiService.GetList("payment/sum", this.payment.groceries.paymentFilter)
                            .then(data => {
                                this.payment.groceries.thisWeekSum = data;

                            }, function (error) {
                                feedback.DisplayError(error);
                            }).always(function () {
                                app.payment.isLoading = false;
                            });
                    }).then((data3) => {
                        this.loadGroceriesPie();
                    })
            },
            getEatingOuts: function () {
                apiService.GetList("category/lookup", this.payment.eatingOut.categoryFilter)
                    .then(data => {
                        this.payment.eatingOut.categoryDetails = data;

                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.payment.isLoading = false;
                    }).then((data2) => {
                        this.payment.eatingOut.paymentFilter.CategoryId = this.payment.eatingOut.categoryDetails[0].CategoryId
                        apiService.GetList("payment/sum", this.payment.eatingOut.paymentFilter)
                            .then(data => {
                                this.payment.eatingOut.thisWeekSum = data;

                            }, function (error) {
                                feedback.DisplayError(error);
                            }).always(function () {
                                app.payment.isLoading = false;
                            });
                    }).then((data3) => {
                        this.loadEatingOutPie();
                    })
            },
            loadOverviewChart: function () {

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
            loadEatingOutPie: function () {
                let actualData = [this.payment.eatingOut.thisWeekSum, this.payment.eatingOut.categoryDetails[0].WeeklyLimit - this.payment.eatingOut.thisWeekSum]
                let data = {
                    labels: ["Spent", "Could be spent"],
                    datasets: [
                        {
                            label: 'spent',
                            data: actualData,
                            borderColor: colorsTransparent,
                            backgroundColor: colorsTransparentHigh,
                            borderWidth: 2,
                            borderSkipped: false,
                        }],
                };

                let config = {
                    type: 'pie',
                    data: data,
                    options: {
                        responsive: true,
                        plugins: {
                            legend: {
                                position: 'top',
                            },
                            title: {
                                display: true,
                                text: 'Eating Out'
                            }
                        }
                    },
                };

                var ctx = $("#eatingOutPie");
                if (this.eatingOutPie) eatingOutPie.destroy();
                this.eatingOutPie = new Chart(ctx, config);
            },
            loadGroceriesPie: function () {
                let actualData = [this.payment.groceries.thisWeekSum, this.payment.groceries.categoryDetails[0].WeeklyLimit - this.payment.groceries.thisWeekSum]
                let data = {
                    labels: ["Spent", "Could be spent"],
                    datasets: [
                        {
                            label: 'spent',
                            data: actualData,
                            borderColor: colorsTransparent,
                            backgroundColor: colorsTransparentHigh,
                            borderWidth: 2,
                            borderSkipped: false,
                        }],
                };

                let config = {
                    type: 'pie',
                    data: data,
                    options: {
                        responsive: true,
                        plugins: {
                            legend: {
                                position: 'top',
                            },
                            title: {
                                display: true,
                                text: 'Groceries'
                            }
                        }
                    },
                };

                var ctx = $("#groceriesPie");
                if (this.groceriesPie) groceriesPie.destroy();
                this.groceriesPie = new Chart(ctx, config);
            },
        },
        created: function () {
        },
        mounted: function () {
            eventHandler.CalculateTableHeights();

            this.getMonthlyExpenditures();
            this.getMonthlyIncomes();
            this.getSavings();
            this.getGroceries();
            this.getEatingOuts();
        }
    })

}, false);
