var currentUser = {
    CurrentSiteId: 0,
    CurrentSupplierId: 0,
    CurrentWarehouseId: 0,
    CurrentUserFullName: "",
    CurrentUserId: 0,
    CurrentUsername: "",
    CurrentSiteDestinationId: 0,
    CurrentUserFirstName: "",
    CurrentUserLastName: ""
};

window.addEventListener("load", function () {
    var siteId = $("#spanCurrentSiteName").data("currentSiteId");
    var supplierId = $("#spanCurrentSiteName").data("currentSupplierId");
    var warehouseId = $("#spanCurrentWarehouseName").data("currentWarehouseId");
    if ((!siteId || !supplierId) && (siteId == 0 || supplierId ==0)) {
        return feedback.DisplayMessage("Error", "Something went wrong, site/supplier ID cannot be found. Please contact system provider.");
    }
   
    currentUser.CurrentSiteId = siteId;
    currentUser.CurrentSupplierId = supplierId;
    currentUser.CurrentWarehouseId = warehouseId;
    currentUser.CurrentUserId = $("#spanCurrentSiteName").data("currentUserId");
    currentUser.CurrentUsername = $("#spanCurrentSiteName").data("currentUsername");
    currentUser.CurrentUserFullName = $("#spanCurrentSiteName").data("currentUserFullName");
    currentUser.CurrentUserFirstName = $("#spanCurrentSiteName").data("currentUserFirstName");
    currentUser.CurrentUserLastName = $("#spanCurrentSiteName").data("currentUserLastName");
    currentUser.CurrentSiteDestinationId = $("#spanCurrentSiteName").data("currentSiteDestinationId");
    currentUser.CurrentSiteName = $("#spanCurrentSiteName").data("currentSiteName");
    currentUser.CurrentSupplierName = $("#spanCurrentSiteName").data("currentSupplierName");
    currentUser.CurrentWarehouseName = $("#spanCurrentWarehouseName").data("currentWarehouseName");
}, false);