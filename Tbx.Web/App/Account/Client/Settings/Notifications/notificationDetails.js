window.addEventListener("load", function () {
    var app = new Vue({
        el: '#app',
        data: {
            daysInAMonth: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28],
            items: {},
            availableNotifications: [
                { NotificationSubscriberType: 2000, NotificationGroup: "General", Icon: "fas fa-newspaper", NotificationName: "Tbx News", SupportsSms: true, IsEmailSubscriber: false, IsSmsSubscriber: false, IsInterval: false, IntervalType: 1, IntervalDayOfWeek: 1, IntervalDayOfMonth: 1, IntervalDate: new moment().format() },


                //{ NotificationSubscriberType: 1003, NotificationGroup: "Reports", Icon: "fas fa-chart-line", NotificationName: "Article Turnover", SupportsSms: false, IsEmailSubscriber: false, IsSmsSubscriber: false, IsInterval: true, IntervalType: 1, IntervalDayOfWeek: 1, IntervalDayOfMonth: 1, IntervalDate: new moment().format() },
                //{ NotificationSubscriberType: 1008, NotificationGroup: "Reports", Icon: "fas fa-chart-line", NotificationName: "Supplier Article Turnover", SupportsSms: false, IsEmailSubscriber: false, IsSmsSubscriber: false, IsInterval: true, IntervalType: 1, IntervalDayOfWeek: 1, IntervalDayOfMonth: 1, IntervalDate: new moment().format() },
                //{ NotificationSubscriberType: 1004, NotificationGroup: "Reports", Icon: "fas fa-exchange-alt", NotificationName: "Price Changes", SupportsSms: false, IsEmailSubscriber: false, IsSmsSubscriber: false, IsInterval: true, IntervalType: 1, IntervalDayOfWeek: 1, IntervalDayOfMonth: 1, IntervalDate: new moment().format() },
                //{ NotificationSubscriberType: 1005, NotificationGroup: "Reports", Icon: "fas fa-file-alt", NotificationName: "Order Lines", SupportsSms: false, IsEmailSubscriber: false, IsSmsSubscriber: false, IsInterval: true, IntervalType: 1, IntervalDayOfWeek: 1, IntervalDayOfMonth: 1, IntervalDate: new moment().format() }

            ],
            selectedNotification: {}
        },
        computed: {
            groupAvailableNotifications() {
                return dataManipulation.GroupBy(this.availableNotifications, "NotificationGroup");
            }
        },
        watch: {

        },
        methods: {
            getList: function () {
                loadHandler.AddGlobalLoader();
                return apiService.GetList("notificationSubscriber").then((response) => {
                    this.items = response;

                    for (x = 0; x < this.availableNotifications.length; x++) {
                        for (i = 0; i < this.items.length; i++) {
                            if (this.availableNotifications[x].NotificationSubscriberType === this.items[i].NotificationSubscriberType) {
                                this.availableNotifications[x].IsEmailSubscriber = this.items[i].IsEmailSubscriber;
                                this.availableNotifications[x].IsSmsSubscriber = this.items[i].IsSmsSubscriber;
                                this.availableNotifications[x].IsInterval = this.items[i].IsInterval;
                                this.availableNotifications[x].IntervalType = this.items[i].IntervalType;
                                this.availableNotifications[x].IntervalDayOfWeek = this.items[i].IntervalDayOfWeek == 0 ? 1 : this.items[i].IntervalDayOfWeek;
                                this.availableNotifications[x].IntervalDayOfMonth = this.items[i].IntervalDayOfMonth == 0 ? 1 : this.items[i].IntervalDayOfMonth;
                                this.availableNotifications[x].IntervalDate = this.items[i].IntervalDate || new moment().format();
                                this.availableNotifications[x].SmsPrice = this.items[i].SmsPrice;
                                this.availableNotifications[x].IsSmsEnabled = this.items[i].IsSmsEnabled;
                                this.availableNotifications[x].SmsPaymentType = this.items[i].SmsPaymentType;
                            }
                        }
                    }
                }, function (error) {
                    feedback.DisplayError(error);
                }).always(function () {
                    loadHandler.RemoveGlobalLoader();
                });
            },
            showSmsDisclaimer: function (item) {
                if (item.IsSmsSubscriber && item.SmsPaymentType == 1) {
                    this.selectedNotification = item;
                    $("#modalSmsDisclaimer").modal("show");
                } else {
                    this.saveOrUpdate(item);
                }
            },
            saveOrUpdate: function (item) {
                return apiService.PostRequest("notificationSubscriber", item).then((response) => {
                    $("#modalSmsDisclaimer").modal("hide");
                    this.items = [];
                    this.getList();
                }, function (error) {
                    feedback.DisplayError(error);
                }).always(function () {
                    loadHandler.RemoveGlobalLoader();
                });

            },

        },
        created: function () {

        },
        mounted: function () {
            this.getList();
        }
    })

}, false);
