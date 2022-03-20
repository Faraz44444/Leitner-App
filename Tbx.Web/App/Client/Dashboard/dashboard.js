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
                    thisYearFilter: {
                        DateFrom: ThisYearPeriod.DateFrom,
                        DateTo: ThisYearPeriod.DateTo
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
                electricity: {
                    categoryFilter: {
                        CategoryName: "Electricity"
                    },
                    categoryDetails: {},
                    paymentFilter: {
                        CategoryId: 0,
                        DateFrom: ThisWeekPeriod.DateFrom,
                        DateTo: ThisWeekPeriod.DateTo
                    },
                    thisMonthSum: 0,
                },
                details: {},
                loadingItems: false,

                thisYearExpenditures: [
                    { FormattedSum: 0 },
                    { FormattedSum: 0 }
                ],
                thisMonthExpenditures: "",
                lastMonthExpenditures: "",

                thisYearSavings: [],
                thisMonthSavings: "",
                lastMonthSavings: "",

                thisMonthIncome: "",
                lastMonthIncome: "",
                thisYearIncomes: [
                    { FormattedSum: 0 },
                    { FormattedSum: 0 }
                ],

                groceriesPie: null,
                eatingOutPie: null,
                electricityPie: null
            },
            category: {
                withMonthlyLimitFilter: {
                    HasMonthlyLimit: true
                },
                categoriesWithMonthlyLimit: [],
                isLoading: false
            },
            overviewChart: null,
            thisMonth: today.getMonth(),
            lastMonth: today.getMonth() - 1,
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
                return Intl.NumberFormat('en-US').format(price);
            }
        },
        methods: {
            getMonthlyExpenditures: function () {
                this.payment.isLoading = true;
                apiService.GetList("payment/sums", this.payment.expenditures.total.thisYearFilter)
                    .then(data => {

                        this.payment.thisYearExpenditures = data.Sums;
                        this.payment.thisMonthExpenditures = this.payment.thisYearExpenditures[this.thisMonth].FormattedSum;
                        this.payment.lastMonthExpenditures = this.payment.thisYearExpenditures[this.lastMonth].FormattedSum;

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
                        this.payment.thisYearIncomes = data.Sums;
                        this.payment.thisMonthIncome = this.payment.thisYearIncomes[this.thisMonth].FormattedSum;
                        this.payment.lastMonthIncome = this.payment.thisYearIncomes[this.lastMonth].FormattedSum;
                        this.loadOverviewChart();

                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.payment.isLoading = false;
                    });
            },
            getCategoriesWithMonthlyLimit: function () {
                this.category.isLoading = true;
                apiService.GetList("category/lookup", this.category.withMonthlyLimitFilter).then(data => {
                    this.category.categoriesWithMonthlyLimit = data;
                }, function (error) {
                    feedback.DisplayError(error);
                }).always(function () {
                    app.category.isLoading = false;
                });
            },
            getSavings: function () {
                this.payment.isLoading = true;

                apiService.GetList("payment/savings", this.payment.savings.thisYearFilter)
                    .then(data => {
                        this.payment.thisYearSavings = data.Sums;
                        this.payment.thisMonthSavings = this.payment.thisYearSavings[this.thisMonth].FormattedSum;
                        this.payment.lastMonthSavings = this.payment.thisYearSavings[this.lastMonth].FormattedSum;

                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.payment.isLoading = false;
                    });
            },
            getGroceries: function () {
                apiService.GetList("category/lookup", this.payment.groceries.categoryFilter)
                    .then(data => {
                        this.payment.groceries.categoryDetails = data[0];
                        this.payment.groceries.paymentFilter.CategoryId = this.payment.groceries.categoryDetails.CategoryId
                        apiService.GetList("payment/sum", this.payment.groceries.paymentFilter)
                            .then(data2 => {
                                this.payment.groceries.thisWeekSum = data2.Sum;
                                this.loadGroceriesPie();

                            }, function (error) {
                                feedback.DisplayError(error);
                            }).always(function () {
                                app.payment.isLoading = false;
                            });
                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.payment.isLoading = false;
                    })
            },
            getEatingOuts: function () {
                apiService.GetList("category/lookup", this.payment.eatingOut.categoryFilter)
                    .then(data => {
                        this.payment.eatingOut.categoryDetails = data[0];
                        this.payment.eatingOut.paymentFilter.CategoryId = this.payment.eatingOut.categoryDetails.CategoryId
                        apiService.GetList("payment/sum", this.payment.eatingOut.paymentFilter)
                            .then(data2 => {
                                this.payment.eatingOut.thisWeekSum = data2.Sum;
                                this.loadEatingOutPie();

                            }, function (error) {
                                feedback.DisplayError(error);
                            }).always(function () {
                                app.payment.isLoading = false;
                            });
                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.payment.isLoading = false;
                    })
            },
            getElectricity: function () {
                apiService.GetList("category/lookup", this.payment.electricity.categoryFilter)
                    .then(data => {
                        this.payment.electricity.categoryDetails = data[0];
                        this.payment.electricity.paymentFilter.CategoryId = this.payment.electricity.categoryDetails.CategoryId
                        apiService.GetList("payment/sum", this.payment.electricity.paymentFilter)
                            .then(data2 => {
                                this.payment.electricity.thisMonthSum = data2.Sum;
                                this.loadElectricityPie();

                            }, function (error) {
                                feedback.DisplayError(error);
                            }).always(function () {
                                app.payment.isLoading = false;
                            });
                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.payment.isLoading = false;
                    })
            },
            loadOverviewChart: function () {
                let expenditures = this.payment.thisYearExpenditures.map(x => x.Sum);
                let incomes = this.payment.thisYearIncomes.map(x => x.Sum);
                let data = {
                    labels: Months,
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
                            label: 'Income',
                            data: incomes,
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
            loadElectricityPie: function () {
                let actualData = [this.payment.electricity.thisMonthSum, this.payment.electricity.categoryDetails.MonthlyLimit - this.payment.electricity.thisMonthSum]
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
                                text: 'Electricity'
                            }
                        }
                    },
                };

                var ctx = $("#electricityPie");
                if (this.electricityPie) electricityPie.destroy();
                this.electricityPie = new Chart(ctx, config);
            },
            loadEatingOutPie: function () {
                let actualData = [this.payment.eatingOut.thisWeekSum, this.payment.eatingOut.categoryDetails.WeeklyLimit - this.payment.eatingOut.thisWeekSum]
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
                let actualData = [this.payment.groceries.thisWeekSum, this.payment.groceries.categoryDetails.WeeklyLimit - this.payment.groceries.thisWeekSum]
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
            },
            MonthlyExpectedExpenditures: function () {
                return Intl.NumberFormat('en-US').format(this.category.categoriesWithMonthlyLimit.filter(x=>x.CategoryName != "Saving").map(x => x.MonthlyLimit).reduce((x, y) => x + y, 0));
            }
        },
        created: function () {
        },
        mounted: function () {
            eventHandler.CalculateTableHeights();

            this.getMonthlyExpenditures();
            this.getMonthlyIncomes();
            this.getSavings();
            this.getCategoriesWithMonthlyLimit();
            this.getGroceries();
            this.getEatingOuts();
            this.getElectricity();
        }
    })

}, false);
