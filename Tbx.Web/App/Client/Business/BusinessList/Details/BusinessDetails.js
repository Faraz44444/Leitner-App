window.addEventListener("load", function () {
    var filterTimeout = null;
    var defaultIntervalMonths = 12;
    var defaultNotification1 = 14;
    var defaultNotification2 = 3;
    var app = new Vue({
        el: '#app',
        data: {
            title: "Business Details",
            id: 0,

            details: {
                Active: true,
                IndustryNoType: 0,
                SupplierArticleLinkList: [],
                SupplierArticlePriceList: [],
                ArticleSupplierPriorityList: [],
                ArticleWarehouseList: [],
                Dimensions: [],
                IsSale: false,
                IsSerial: false,
                IsMaintenanceArticle: false,
                HasMaintenanceAfterLoan: false,
                MaintenanceOfficerUserId: 0,
                MaintenanceIntervalMonths: defaultIntervalMonths,
                DaysBeforeNotification1: defaultNotification1,
                DaysBeforeNotification2: defaultNotification2,
                Price: 0
            },
            serieFilter: {
                ArticleId: 0,
                SerialNumber: "",
                ExpertControllerUserId: 0,
                Active: null,
                Sold: false,
                IsOnMaintenance: null,
                CurrentWarehouseId: "",
                OwnerWarehouseId: "",
                OrderBy: 1,
                OrderByDirection: 1,
                ItemsPerPage: 50,
                CurrentPage: 1,
            },
            serialDetails: {
                Active: true,
                SerialNumber: "",
                OwnerWarehouseId: currentUser.CurrentWarehouseId,
                CurrentWarehouseId: 0,
                ArticleId: 0,
                ArticleSerieId: 0,
                ArticleSeriePrefixId: 0,
                ExpertControllerUserId: 0,
                ExpertControllerFullName: "",
            },
            stockFilter: {
                ArticleId: 0,
                WarehouseName: "",
                WarehouseId: 0,
                IsTbxWarehouse: true,
                OrderBy: 1,
                OrderByDirection: 1,
                ItemsPerPage: 50,
                CurrentPage: 1,
            },
            stockDetails: {
                ArticleId: 0,
                CurrentStock: 0,
                DefaultStock: 0,
            },
            stockLogDetails: {
                ArticleStockLogId: 0,
                LogDate: "",
                LogEvent: "",
                StockChangeTypeId: 0,
                ArticleId: 0,
                ArticleSerieId: 0,
                UserId: 0,
                WarehouseId: 0,
                UserFirstName: "",
                UserLastName: "",
                UserEmail: "",
            },
            stockLogFilter: {
                LogDateFrom: null,
                LogDateTo: null,
                LogEvent: "",
                ArticleId: 0,
                SerialNumber: "",
                StockChangeTypeId: 0,
                UserId: 0,
                UserFullName: "",
                UserEmail: "",
                WarehouseId: 0,
                WarehouseName: "",
                OrderBy: 1,
                OrderByDirection: 1,
                ItemsPerPage: 50,
                CurrentPage: 1
            },
            prefixFilter: {
                Prefix: "",
                StartingNumber: 1,
                OrderBy: 1,
                OrderByDirection: 1,
                ItemsPerPage: 50,
                CurrentPage: 1
            },
            selectedWarehouseId: 0,
            articleGroupList: [],
            unitList: [],
            userList: [],
            articleSerieList: [],
            articleStockList: [],
            articlePrefixList: [],
            articleStockLogList: [],
            warehouseList: [],
            loadingItems: false,
            unitWarningShowed: false,
            resettingFilters: false,
        },
        components: {
            draggable: window.vuedraggable
        },
        computed: {

        },
        watch: {
            'details.IndustryNoType': function () {
                $("#industryNo").removeClass("input-error");
                if (this.details.IndustryNoType == 0) {
                    this.details.IndustryNo = undefined;
                }

            }, 'loadingArticleWarehouseItems': function () {
                if (this.loadingArticleWarehouseItems) {
                    loadHandler.AddTableLoader("#warehouseList");
                } else {
                    loadHandler.RemoveTableLoader("#warehouseList");
                }
            },
            'details.IsSerial': function () {
                if (!this.details.IsSerial) {
                    this.details.IsMaintenanceArticle = false;
                    this.details.HasMaintenanceAfterLoan = false;
                }
            },
            'loadingItems': function () {
                if (this.loadingItems) {
                    loadHandler.AddTableLoader("#supplierArticleList");
                } else {
                    loadHandler.RemoveTableLoader("#supplierArticleList");
                }
            },
            'serieFilter.SerialNumber': function () {
                this.filterChangedTimeout();
            },
            'serieFilter.CurrentWarehouseName': function () {
                this.filterChangedTimeout();
            },
            'serieFilter.OwnerWarehouseName': function () {
                this.filterChangedTimeout();
            },
            'serieFilter.Active': function () {
                this.filterChanged();
            },
            'serieFilter.IsOnMaintenance': function () {
                this.filterChanged();
            },
            'serieFilter.Sold': function () {
                this.filterChanged();
            },
            'stockFilter.WarehouseId': function () {
                this.filterChanged();
            },
            'stockLogFilter.LogEvent': function () {
                if (!this.resettingFilters)
                    this.filterStockLogChanged();
            },
            'stockLogFilter.WarehouseName': function () {
                if (!this.resettingFilters)
                    this.filterStockLogChanged();
            },
            'stockLogFilter.UserFullName': function () {
                if (!this.resettingFilters)
                    this.filterStockLogChanged();
            },
            'stockLogFilter.WarehouseId': function () {
                if (!this.resettingFilters)
                    this.filterStockLogChanged();
            },
            'stockLogFilter.LogDateFrom': function () {
                if (!this.resettingFilters)
                    this.filterStockLogChanged();
            },
            'stockLogFilter.LogDateTo': function () {
                if (!this.resettingFilters)
                    this.filterStockLogChanged();
            },
            'serialDetails.ArticleSeriePrefixId': function () {
                if (this.serialDetails.ArticleSeriePrefixId > 0) {
                    return apiService.GetList("articleserie/prefix/" + this.serialDetails.ArticleSeriePrefixId + "/firstavailable").then((response) => {
                        this.serialDetails.SerialNumber = response;
                    });
                } else {
                    this.serialDetails.SerialNumber = "";
                }
            },
        },
        filters: {

        },
        methods: {
            getSerieList: function () {
                this.articleSerieList = [];
                this.loadingItems = true;
                return apiService.GetList("articleserie", this.serieFilter).then((response) => {
                    this.articleSerieList = this.articleSerieList.concat(response.Items.map(function (x) {
                        x.selected = false;
                        return x;
                    }));
                    pagination.InfiniteScroll("seriallist", response, this.fetchPage, this.loadingItems);
                }, function (error) {
                    feedback.DisplayError(error);
                }).always(function () {
                    this.loadingItems = false;
                });
            },
            getStockList: function () {
                this.loadingItems = true;
                return apiService.GetList("articlestock", this.stockFilter).then((response) => {
                    this.articleStockList = this.articleStockList.concat(response.Items.map(function (x) {
                        x.editing = false;
                        return x;
                    }));
                    pagination.InfiniteScroll("stocklist", response, this.fetchPage, this.loadingItems);
                    response.Items.forEach((item) => {
                        item.edit = {
                            CurrentStock: item.CurrentStock,
                            DefaultStock: item.DefaultStock,
                        }
                    });
                }, (error) => {
                    feedback.DisplayError(error);
                }).always(() => {
                    this.loadingItems = false;
                });
            },
            getStockLogList: function (item) {
                this.loadingItems = true;
                if (item != null) {
                    this.stockLogFilter.ArticleId = item.ArticleId;
                    if (item.ArticleSerieId > 0) {
                        this.stockLogFilter.ArticleSerieId = item.ArticleSerieId;
                    }
                    else {
                        this.stockLogFilter.WarehouseId = item.WarehouseId;
                    }
                }
                loadHandler.AddGlobalLoader();
                return apiService.GetList("articlestock/getlog", this.stockLogFilter).then((response) => {
                    this.articleStockLogList = this.articleStockLogList.concat(response.Items);
                }, function (error) {
                    feedback.DisplayError(error);
                }).always(function () {
                    loadHandler.RemoveGlobalLoader();
                });
            },
            getDetails: function () {
                if (this.id < 0)
                    return;
                loadHandler.AddGlobalLoader();
                return apiService.GetById("article", this.id).then((response) => {
                    this.details = response;
                    //    this.details.MaintenanceIntervalMonths = this.details.MaintenanceIntervalMonths < 1 ? this.defaultIntervalMonths : this.details.MaintenanceIntervalMonths;
                    //    this.details.DayKUsBeforeNotification1 = this.details.DaysBeforeNotification1 < 1 ? this.defaultNotification1 : this.details.DaysBeforeNotification1;
                    //    this.details.DaysBeforeNotification2 = this.details.DaysBeforeNotification2 < 1 ? this.defaultNotification2 : this.details.DaysBeforeNotification2;
                }, function (error) {
                    feedback.DisplayError(error);
                }).always(function () {
                    loadHandler.RemoveGlobalLoader();
                });
            },
            save: function () {
                var legal = true;
                if (this.details.IsMaintenanceArticle) {
                    if (!formvalidation.Validate("maintanceDetails"))
                        legal = false;
                }
                if (!formvalidation.Validate("detailsSection"))
                    legal = false;

                if (!legal) return;

                loadHandler.AddGlobalLoader();

                app.SaveArticleDetails().then(function (response) {
                    if (response && response.Success) {
                        if (response.Id && response.Id > 0) {
                            window.location.replace("/article/list/" + response.Id);
                        } else {
                            window.location.replace("/article/list");
                        }
                    }
                }).always(() => {
                    loadHandler.RemoveGlobalLoader();
                });

            },
            SaveArticleDetails: function () {
                var request = $.extend({}, this.details);

                if (this.id == 0) {
                    return apiService.PostRequest("article", request).then((response) => {
                        return response;
                    }, function (error) {
                        feedback.DisplayError(error);
                    });
                } else {
                    return apiService.PostRequest("article/" + this.id, request).then((response) => {

                        return response;
                    }, function (error) {
                        feedback.DisplayError(error);
                    });
                }
            },
            openGenerateLabelModal: function () {
                $("#modalGenerateLabels").modal("show");
            },
            openStockLog(item) {
                this.resettingFilters = true;
                this.articleStockLogList = [];
                this.stockLogFilter.CurrentPage = 1;
                this.stockLogFilter.LogEvent = "";
                this.stockLogFilter.LogDateFrom = null;
                this.stockLogFilter.LogDateTo = null;
                this.stockLogFilter.WarehouseId = 0;
                this.stockLogFilter.UserFullName = "";
                this.stockLogFilter.OrderBy = 1;
                this.stockLogFilter.OrderByDirection = 1;

                this.getStockLogList(item).then(() => {
                    $("#modalLog").modal("show");
                    this.resettingFilters = false;
                });
            },
            fetchPage: function () {
                this.filter.CurrentPage++;
                if (this.details.IsSerial) {
                    this.getSerieList();

                } else {
                    this.getStockList()
                }
            },
            fetchStockLogPage: function () {
                this.stockLogFilter.CurrentPage++;
                this.getStockLogList();

            },
            filterChanged: function () {
                this.articleSerieList = [];
                this.articleStockList = [];
                this.serieFilter.CurrentPage = 1
                this.stockFilter.CurrentPage = 1
                if (this.details.IsSerial) {
                    this.getSerieList();

                } else {
                    this.getStockList();
                }
            },
            filterStockLogChanged: function () {
                this.articleStockLogList = [];
                this.stockLogFilter.CurrentPage = 1
                this.getStockLogList();
            },
            filterChangedTimeout: function () {
                clearTimeout(filterTimeout);
                filterTimeout = setTimeout(() => {
                    this.filterChanged();
                }, 500);
            },
            filterStockLogChangedTimeout: function () {
                clearTimeout(filterTimeout);
                filterTimeout = setTimeout(() => {
                    this.filterStockLogChanged();
                }, 500);
            },
            orderBy: function (value) {
                this.serieFilter = orderByHandler.Handle(this.serieFilter, value);
                this.stockFilter = orderByHandler.Handle(this.stockFilter, value);
                this.filterChanged();
            },
            stockLogOrderBy: function (value) {
                this.stockLogFilter = orderByHandler.Handle(this.stockLogFilter, value);
                this.filterStockLogChanged();
            },
            getSerial: function (item = null) {
                $("#modalSerial").find(".input-error").removeClass("input-error");
                $("#modalSerial").modal("show");
                if (item == null) {
                    this.serialDetails.ArticleSeriePrefixId = 0;
                    this.serialDetails.SerialNumber = null;
                    this.serialDetails.Active = true;
                    this.serialDetails.ArticleSerieId = 0;
                    this.CurrentWarehouseId = currentUser.CurrentWarehouseId;
                    this.serialDetails.ExpertControllerUserId = 0;
                    this.serialDetails.Sold = false;
                } else {
                    this.serialDetails.SerialNumber = item.SerialNumber;
                    this.serialDetails.CurrentWarehouseId = item.CurrentWarehouseId;
                    this.serialDetails.Active = item.Active;
                    this.serialDetails.ArticleSerieId = item.ArticleSerieId;
                    this.serialDetails.ExpertControllerUserId = item.ExpertControllerUserId;
                    this.serialDetails.Sold = item.Sold;
                }
            },
            saveAndUpdateSerial: function () {
                if (!formvalidation.Validate("modalSerial"))
                    return;
                $("#modalSerial").modal("hide");
                loadHandler.AddGlobalLoader();
                this.serialDetails.ArticleId = this.id;
                var request = $.extend({}, this.serialDetails);
                if (this.serialDetails.ArticleSerieId == 0) {
                    apiService.PostRequest("articleserie/", request).then((response) => {
                        if (response && response.Success) {
                            this.filterChanged();
                        }
                    }, (error) => {
                        feedback.DisplayError(error);
                    }).always(() => {
                        loadHandler.RemoveGlobalLoader();
                    });
                } else {
                    apiService.PostRequest("articleserie/" + app.serialDetails.ArticleSerieId, request).then((response) => {
                        if (response && response.Success) {
                            this.getSerieList();
                        }
                    }, (error) => {
                        feedback.DisplayError(error);
                    }).always(() => {
                        loadHandler.RemoveGlobalLoader();
                    });
                }
            },
            enterEditingStock(item) {
                item.editing = true;
                var edit = {
                    CurrentStock: item.CurrentStock,
                    DefaultStock: item.DefaultStock,
                }
                this.$set(item, 'edit', edit);
                this.$set(item, 'edit', edit);
            },
            saveEditedStock: function (item) {
                item.CurrentStock = item.edit.CurrentStock;
                item.DefaultStock = item.edit.DefaultStock;
                loadHandler.AddGlobalLoader();
                var request = $.extend({}, item);
                apiService.PostRequest("articlestock/" + request.ArticleId + "/" + request.WarehouseId, request).then((response) => {
                    item.editing = false;
                }, function (error) {
                    feedback.DisplayError(error);
                }).always(function () {
                    loadHandler.RemoveGlobalLoader();
                });
            },
        },
        created: function () {
            this.id = parseInt($("#app").data("entityId"));

            lookup.ArticleGroupList().then((response) => {
                this.articleGroupList = response;
            });
            lookup.UnitList().then((response) => {
                this.unitList = response;
            });
            lookup.UserList().then((response) => {
                this.userList = response;
            });
            lookup.WarehouseList(null, currentUser.CurrentSiteId, true, true).then((response) => {
                this.warehouseList = response;
                this.selectedWarehouseId = this.warehouseList[0].Id;
            });


            lookup.ArticlePrefixList().then((response) => {
                this.articlePrefixList = response;
            });

            if (isNaN(this.id)) {
                this.id = 0;
                this.title = "New Article"
                return;
            }
            this.serieFilter.ArticleId = this.id;
            this.stockFilter.ArticleId = this.id;
            this.getDetails();
            this.getSerieList();
            if (this.details.IsSerial) {
                this.getSerieList();
            } else {
                this.getStockList()
            }
        },
        mounted: function () {
            eventHandler.CalculateTableHeights();
            $("#unitDropdown").on("change", () => {
                if (this.id == 0)
                    return;
                if (!this.unitWarningShowed) {
                    this.unitWarningShowed = true;
                    $("#modalUnitWarning").modal("show");
                }
            });
        }
    })

}, false);
