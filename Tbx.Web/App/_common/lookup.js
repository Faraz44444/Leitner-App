var lookup = (function () {
    'use strict';

    return {
        ArticleGroupList: articleGroupList,
        ArticlePrefixList: articlePrefixList,
        SupplierArticleGroupList: supplierArticleGroupList,
        DimensionList: dimensionList,
        UserList: userList,
        SiteList: siteList,
        RoleList: roleList,
        SupplierList: supplierList,
        SupplierRoleList: supplierRoleList,
        CountryList: countryList,
        UnitList: unitList,
        SupplierUnitList:  supplierUnitList,
        DeliveryTermList: deliveryTermList,
        PaymentTermList: paymentTermList,
        CurrencyList: currencyList,
        DestinationList: destinationList,
        InvoiceAddressList: invoiceAddressList,
        SupplierContactList: supplierContactList,
        SupplierList: supplierList,
        SupplierSiteInfoSummaryList: supplierSiteInfoSummaryList,
        WarehouseList: warehouseList,
        WarehouseLocationList: warehouseLocationList,
        AvailableClientListForSupplier: availableClientListForSupplier,
        SupplierUserList: supplierUserList,
        DepartmentList: departmentList
    };

    function getLookupList(url, request) {
        return apiService.GetList(url + "/lookup", request).then(function (d) {
            return d;
        }, function (error) {
            feedback.DisplayError(error);
        })
    }

    function getLookupListSupplierApi(url, request) {
        return supplierApiService.GetList(url + "/lookup", request).then(function (d) {
            return d;
        }, function (error) {
            feedback.DisplayError(error);
        })
    }

    function articleGroupList(ClientId, Name = "", HasParent = null) {
        return getLookupList("articlegroup", { ClientId, Name, HasParent});
    }
    function articlePrefixList() {
        return getLookupList("articleserie/prefix", {});
    }
    function userList(SiteId = 0, Active = null) {
        return getLookupList("user", { SiteId, Active });
    }
    function siteList(Name = "") {
        return getLookupList("site", { Name });
    }
    function roleList(Name = "", IsLocked = null) {
        return getLookupList("role", { Name, IsLocked });
    }
    function countryList(Name = "", CountryCode = "", HasPhoneCountryCode = false) {
        return getLookupList("country", { Name, CountryCode, HasPhoneCountryCode });
    }
    function unitList(UnitCode = "", Name = "", Description = "", Active = null) {
        return getLookupList("unit", { UnitCode, Name, Description, Active });
    }
    function deliveryTermList(DeliveryTermCode = "", Name = "", Description = "", Active = null) {
        return getLookupList("deliveryterm", { DeliveryTermCode, Name, Description, Active });
    }
    function dimensionList(DimensionName = "", DimensionOrder=0) {
        return getLookupList("dimension", { DimensionName, DimensionOrder });
    }
    function departmentList(Active = null) {
        return getLookupList("department", { Active });
    }
    function paymentTermList(PaymentTermCode = "", Name = "", Description = "", Active = null) {
        return getLookupList("paymentterm", { PaymentTermCode, Name, Description, Active });
    }
    function currencyList(CurrencyCode = "", Name = "") {
        return getLookupList("currency", { CurrencyCode, Name });
    }
    function destinationList(Name = "", Description = "", Address = "", Phone = "", Email = "", CountryName = "", Active = null) {
        return getLookupList("destination", { Name, Description, Address, Phone, Email, CountryName, Active });
    }
    function invoiceAddressList(Name = "", Description = "", Address = "", Phone = "", Email = "", CountryName = "", Active = null) {
        return getLookupList("invoiceaddress", { Name, Description, Address, Phone, Email, CountryName, Active });
    }
    function supplierContactList(SupplierId = 0, Active = null) {
        return getLookupList("suppliercontact", { SupplierId, Active });
    }
    function supplierList(Active = null) {
        return getLookupList("supplier", {  Active  });
    }
    function supplierSiteInfoSummaryList(IsActiveTbx = null, IncludeUnmappedRelations = null, Active = null) {
        return getLookupList("supplierSiteInfo", { IsActiveTbx, IncludeUnmappedRelations, Active,  });
    }
    function warehouseList(WarehouseType = null, SiteId = 0, Active = null, IsTbxWarehouse = null) {
        return getLookupList("warehouse", { WarehouseType, SiteId, Active, IsTbxWarehouse });
    }
    function warehouseLocationList(WarehouseLocationType = null, WarehouseId = 0, WarehouseIds = [], SiteId = 0, Active = null) {
        return getLookupList("warehouselocation", { WarehouseLocationType, WarehouseId, WarehouseIds, SiteId, Active });
    }

    //***** SUPPLIER API
    function availableClientListForSupplier() {
        return getLookupListSupplierApi("client", { });
    }   
    function supplierRoleList(Name = "", IsLocked = null) {
        return getLookupListSupplierApi("role", { Name, IsLocked });
    }
    function supplierArticleGroupList(SupplierArticleGroupId = 0, Active = null) {
        return getLookupListSupplierApi("supplierarticlegroup", { SupplierArticleGroupId, Active });
    }
    function supplierUnitList(SupplierUnitCode = "", Name = "", Description = "", Active = null) {
        return getLookupListSupplierApi("supplierunit", { SupplierUnitCode, Name, Description, Active });
    }
    function supplierUserList(Active = null) {
        return getLookupListSupplierApi("user", { Active });
    }
})();