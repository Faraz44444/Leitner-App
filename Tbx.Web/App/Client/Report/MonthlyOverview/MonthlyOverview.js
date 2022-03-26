
window.addEventListener("load", function (event) {
    var Today = new Date();
    var Month = Today.getMonth() + 1;
    var Year = Today.getFullYear();
    var filterTimeout = null;
    var localStorageKey = "filterArticleList";
    var app = new Vue({
        el: "#app",
        data: {
            payment: {
                isLoading: false,
                Overviewfilter: {
                },
                filter: {
                    CurrentPage: 1,
                    ItemsPerPage: 50,
                    OrderBy: 6,
                    OrderByDirection: 2
                },
                items: [],
                selectedItems: [],
                firstRecordDate: null,
                lastRecordDate: null,
                dateLimits: {},
                detailItems: [],
                detailsExpendituresSum: "",
                detailsIncomesSum: ""
            },
            category: {
                filter: {},
                items: [],
                isLoading: false,
                details: {

                },

            },
            overviewChart: {},
            selectedYear: null,
            selectedMonth: null,
            test: null,
            MonthNames: ["", "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"]
        },
        methods: {
            fetchOverview: function () {
                this.payment.isLoading = true;

                return apiService.GetList("report/monthlyoverview", this.payment.Overviewfilter)
                    .then(data => {
                        //data.map(x => {
                        //    let key = "#overviewChart" + x.CategoryId + x.Year + x.Month;
                        //    this.overviewChart[key] = null
                        //});
                        this.payment.items = data;
                        this.filterItemsForSelectedDate();
                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.payment.isLoading = false;
                    });
            },
            fetchPayments: function () {
                this.payment.isLoading = true;

                let lastDayOfSelectedMonth = new Date(this.selectedYear, this.selectedMonth, 0).getDate();
                this.payment.filter.DateFrom = this.selectedYear + "-" + this.selectedMonth + "-" + 1
                this.payment.filter.DateTo = this.selectedYear + "-" + this.selectedMonth + "-" + lastDayOfSelectedMonth

                return apiService.GetList("payment", this.payment.filter).then(response => {
                    this.payment.detailsExpendituresSum = Intl.NumberFormat('en-US').format(response.Items.filter(x=> x.IsDeposit == false).map(x => x.Price).reduce((x, y) => x + y , 0));
                    this.payment.detailsIncomesSum = Intl.NumberFormat('en-US').format(response.Items.filter(x=> x.IsDeposit == true).map(x => x.Price).reduce((x, y) => x + y , 0));
                    this.payment.detailItems = response.Items;
                    pagination.InfiniteScroll("datalist", response, app.fetchPage);
                }, function (error) {
                    feedback.DisplayError(error);
                }
                ).always(function () {
                    app.payment.isLoading = false;
                })
            },
            fetchDateLimits: function () {
                this.payment.isLoading = true;
                apiService.GetList("payment/firstrecorddate", null).then(data => {
                    this.payment.firstRecordDate = data.Date;
                    apiService.GetList("payment/lastrecorddate", null).then(data2 => {
                        this.payment.lastRecordDate = data2.Date;
                        this.computeDatelimits();
                    }, function (error) {
                        feedback.DisplayError(error);
                    }).always(function () {
                        app.payment.isLoading = false;
                    })
                }, function (error) {
                    feedback.DisplayError(error);
                }).always(function () {
                    app.payment.isLoading = false;
                })
            },
            computeDatelimits: function () {
                let dateLimits = {};
                let firstRecordDate = new Date(Object.assign(this.payment.firstRecordDate));
                let lastRecordDate = new Date(Object.assign(this.payment.lastRecordDate));
                while (firstRecordDate.getFullYear() <= lastRecordDate.getFullYear()) {
                    dateLimits.hasOwnProperty(firstRecordDate.getFullYear().toString()) ?
                        dateLimits[firstRecordDate.getFullYear().toString()].push(firstRecordDate.getMonth() + 1) :
                        dateLimits[firstRecordDate.getFullYear().toString()] = [firstRecordDate.getMonth() + 1]
                    if (firstRecordDate.getFullYear() == lastRecordDate.getFullYear() &&
                        firstRecordDate.getMonth() == lastRecordDate.getMonth()) {
                        break;
                    }
                    firstRecordDate.setMonth(firstRecordDate.getMonth() + 1);
                }

                this.payment.dateLimits = dateLimits
            },
            loadOverviewPies: function () {
                this.payment.items.forEach(x => {
                    let actualData = [x.Price, 0]
                    let data = {
                        labels: ["Spent"],
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
                                    display: false
                                }
                            }
                        },
                    };
                    var ctx = el.getContext(temp);
                    var ctx = $(temp);
                    if (this.overviewChart[temp])
                        this.overviewChart[temp].destroy();
                    this.overviewChart[temp] = new Chart(ctx, config);

                })

            },
            filterItemsForSelectedDate: function () {
                if (this.payment.items.length > 0)
                    this.payment.selectedItems = this.payment.items.filter(x => x.Month == this.selectedMonth && x.Year == parseInt(this.selectedYear));
            },
            filterChanged: function () {
                this.payment.items = [];
                this.payment.filter.CurrentPage = 1;
                this.fetchOverview();
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
            }
        },
        computed: {
            DatesYears: function () {
                return Object.keys(this.payment.dateLimits)
            },
            SelectedDateExpendituresSum: function () {
                let price = this.payment.selectedItems.filter(x => x.IsDeposit == false).map(x => x.Price).reduce((x, y) => x + y, 0);
                return Intl.NumberFormat('en-US').format(price);
            },
            SelectedDateIncomeSum: function () {
                let price = this.payment.selectedItems.filter(x => x.IsDeposit == true).map(x => x.Price).reduce((x, y) => x + y, 0);
                return Intl.NumberFormat('en-US').format(price);
            },
            SelectedDateSaving: function () {
                let price = this.payment.selectedItems.filter(x => x.IsDeposit == true).map(x => x.Price).reduce((x, y) => x + y, 0) -
                    this.payment.selectedItems.filter(x => x.IsDeposit == false).map(x => x.Price).reduce((x, y) => x + y, 0);
                return Intl.NumberFormat('en-US').format(price);

            }
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
            'selectedMonth': function () {
                this.filterItemsForSelectedDate();
                this.fetchPayments();

            },
            'payment.filter.Title': function () {
                this.payment.filter.CurrentPage = 1;
                this.fetchPayments();
            },
            'payment.filter.PaymentPriorityName': function () {
                this.payment.filter.CurrentPage = 1;
                this.fetchPayments();
            },
            'payment.filter.BusinessName': function () {
                this.payment.filter.CurrentPage = 1;
                this.fetchPayments();
            },

            'payment.filter.CategoryName': function () {
                this.payment.filter.CurrentPage = 1;
                this.fetchPayments();
            },

            'payment.filter.IsDeposit': function () {
                this.payment.filter.CurrentPage = 1;
                this.fetchPayments();
            },
            'payment.filter.IsPaidToPerson': function () {
                this.payment.filter.CurrentPage = 1;
                this.fetchPayments();
            },
            'payment.filter.Price': function () {
                this.payment.filter.CurrentPage = 1;
                this.fetchPayments();
            },
            'payment.filter.Date': function () {
                this.payment.filter.CurrentPage = 1;
                this.fetchPayments();
            },
            'payment.isLoading': function (value) {
                if (this.loadingItems) {
                    loadHandler.AddTableLoader("#datalist");
                } else {
                    loadHandler.RemoveTableLoader("#datalist");
                }
            }
        },
        mounted: function () {
            this.selectedYear = Year;
            this.selectedMonth = Month;
            this.fetchDateLimits();
            this.fetchOverview();
            this.fetchPayments();
        },
    });

}, false);
