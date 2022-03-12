<%@ Page Title="Article Details" Language="C#" MasterPageFile="~/TbxPortal.Master" AutoEventWireup="True" CodeBehind="PaymentDetails.aspx.cs" Inherits="TbxPortal.Web.App.Client.Payment.PaymentList.Details.PaymentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/jsArticleDetails") %>
    <%: Styles.Render("~/cssjqueryUi") %>
    <%: Scripts.Render("~/jsjqueryUi") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadCrumb" runat="server">
    <li>Article</li>
    <li><a href="<%=Page.ResolveUrl("~/payment/list") %>">Article List</a></li>
    <li><a href="<%=Page.ResolveUrl($"~/payment/list/{EntityIdString}") %>"><%=EntityName %></a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="server">
    <div id="app" class="row justify-content-center" data-entity-id="<%=EntityIdString %>">
        <section is="content-section" icon="fas fa-tools" v-bind:title="title" class="col-12" um-anchor="#HowToArticles">
            <div class="row">
                <div class="col-12 col-lg-6">
                    <section is="content-section" title="Details" class="col-12" small id="maintanceDetails">
                        <div class="row">
                            <div class="col-12">
                                <div id="detailsSection">
                                    <div class="row">
                                        <div class="col-12 col-md-6">
                                            <div is="custom-input-group" input-label="Article No." show-copy>
                                                <input class="form-control form-control-sm" type="text" v-model="details.ArticleNo" disabled />
                                            </div>
                                        </div>
                                        <div class="col-12 col-md-6 mt-2 mt-md-0">
                                            <div is="custom-input-group" input-label="Article Group" is-select>
                                                <select class="form-control form-control-sm" v-model="details.ArticleGroupId" required>
                                                    <option v-for="item in articleGroupList" v-bind:value="item.Id">{{item.Name}}</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-md-6 mt-2">
                                            <div is="custom-input-group" input-label="Industry No. Type" is-select>
                                                <select class="form-control form-control-sm" v-model="details.IndustryNoType">
                                                    <option value="0">None</option>
                                                    <option value="1">ISO</option>
                                                    <option value="2">DIN</option>
                                                    <option value="3">EAN</option>
                                                    <option value="4">NRF</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-12 col-md-6 mt-2">
                                            <div is="custom-input-group" input-label="Industry No.">
                                                <input id="industryNo" class="form-control form-control-sm" type="text" v-model="details.IndustryNo" v-bind:disabled="details.IndustryNoType < 1" v-bind:required="details.IndustryNoType > 0" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12  mt-2">
                                            <div is="custom-input-group" input-label="Description">
                                                <input class="form-control form-control-sm" type="text" v-model="details.Description" required />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 mt-2 mb-2">
                                            <div is="custom-input-group" input-label="Technical Description">
                                                <input class="form-control form-control-sm" type="text" v-model="details.TechnicalDescription" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-md-4 mt-2">
                                            <div is="custom-input-group" input-label="Unit" is-select>
                                                <select id="unitDropdown" class="form-control form-control-sm" v-model="details.UnitId" required>
                                                    <option v-for="item in unitList" v-bind:value="item.Id">{{item.Name}}</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-12 col-md-4 mt-2 mb-2">
                                            <div is="custom-input-group" input-label="Price" required>
                                                <input class="form-control form-control-sm" min="0" step="0.01" type="number" v-model="details.Price" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-12 col-md-4 mt-2">
                                            <div is="custom-input-group" input-label="Serial Equipment">
                                                <div class="custom-control custom-switch">
                                                    <input id="chkIsSerial" type="checkbox" class="custom-control-input" v-model="details.IsSerial" v-bind:disabled="id > 0" />
                                                    <label class="custom-control-label" for="chkIsSerial"><span v-show="details.IsSerial">Yes</span><span v-show="!details.IsSerial">No</span></label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-md-4 mt-2">
                                            <div is="custom-input-group" input-label="Only for Sale">
                                                <div class="custom-control custom-switch">
                                                    <input id="chkIsSale" type="checkbox" class="custom-control-input" v-model="details.IsSale" />
                                                    <label class="custom-control-label" for="chkIsSale"><span v-show="details.IsSale">Yes</span><span v-show="!details.IsSale">No</span></label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-md-4 mt-2">
                                            <div is="custom-input-group" input-label="Active">
                                                <div class="custom-control custom-switch">
                                                    <input id="chkActive" type="checkbox" class="custom-control-input" v-model="details.Active" />
                                                    <label class="custom-control-label" for="chkActive"><span v-show="details.Active">Yes</span><span v-show="!details.Active">No</span></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </section>
                </div>
                <div class="col-12 col-lg-6">
                    <section is="content-section" title="Maintenance" class="col-12" small id="maintanceDetails">
                        <div class="row">
                            <div class="col-12 col-md-6 mt-2">
                                <div is="custom-input-group" input-label="Maintentance Article">
                                    <div class="custom-control custom-switch" v-bind:disabled="!details.IsSerial">
                                        <input id="chkIsMaintenanceArticle" type="checkbox" class="custom-control-input" v-model="details.IsMaintenanceArticle" v-bind:disabled="!details.IsSerial" />
                                        <label class="custom-control-label" for="chkIsMaintenanceArticle"><span v-show="details.IsMaintenanceArticle">Yes</span><span v-show="!details.IsMaintenanceArticle">No</span></label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 col-md-6 mt-2">
                                <div is="custom-input-group" input-label="Maintentance After Loan">
                                    <div class="custom-control custom-switch" v-bind:disabled="!details.IsSerial">
                                        <input id="chkHasMaintenanceAfterLoan" type="checkbox" class="custom-control-input" v-model="details.HasMaintenanceAfterLoan" v-bind:disabled="!details.IsSerial" />
                                        <label class="custom-control-label" for="chkHasMaintenanceAfterLoan"><span v-show="details.HasMaintenanceAfterLoan">Yes</span><span v-show="!details.HasMaintenanceAfterLoan">No</span></label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 col-md-6 mt-2 ">
                                <div is="custom-input-group" input-label="Maintenance Officer Name" v-bind:disabled="!details.IsMaintenanceArticle">
                                    <select class="form-control form-control-sm" type="text" v-bind:disabled="!details.IsMaintenanceArticle" v-model="details.MaintenanceOfficerUserId">
                                        <option value="0">None</option>
                                        <option v-for="item in userList" v-bind:value="item.Id">{{item.Name}}</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-12 col-md-6 mt-2 ">
                                <div is="custom-input-group" input-label="Maintenance Internal Months">
                                    <input class="form-control form-control-sm" type="number" min="1" v-model="details.MaintenanceIntervalMonths" v-bind:disabled="!details.IsMaintenanceArticle" required />
                                </div>
                            </div>
                            <div class="col-12 col-md-6 mt-2  mt-2">
                                <div is="custom-input-group" input-label="Days Before First Notification ">
                                    <input class="form-control form-control-sm" type="number" min="1" v-model="details.DaysBeforeNotification1" v-bind:disabled="!details.IsMaintenanceArticle" required />
                                </div>
                            </div>
                            <div class="col-12 col-md-6 mt-2  mt-2 ">
                                <div is="custom-input-group" input-label="Days Before Second Notification">
                                    <input class="form-control form-control-sm" type="number" min="1" v-model="details.DaysBeforeNotification2" v-bind:disabled="!details.IsMaintenanceArticle" required />
                                </div>
                            </div>
                        </div>
                    </section>
                </div>
                <div class="col-12 mt-4">
                    <div class="row">
                        <section v-show=" id < 1" is="content-section" title="stock / serie administration" class="col-12" small>
                            <div class="text-center">
                                Create the article to start administrating stock/series. 
                            </div>
                        </section>
                        <section v-show="!details.IsSerial && id > 0" is="content-section" title="Stock administration" class="col-6" small>
                            <div class="table-responsive" data-min-height="350">
                                <table class="table table-sm" id="stocklist">
                                    <thead>
                                        <tr>
                                            <th is="sortable-column" v-bind:value="1" v-bind:filter="stockFilter" v-on:order-by="orderBy" event-name="order-by">Warehouse Name</th>
                                            <th is="sortable-column" v-bind:value="2" v-bind:filter="stockFilter" v-on:order-by="orderBy" event-name="order-by" class="text-right">Current Stock</th>
                                            <th is="sortable-column" v-bind:value="3" v-bind:filter="stockFilter" v-on:order-by="orderBy" event-name="order-by" class="text-right">Default Stock</th>
                                            <th></th>

                                        </tr>
                                        <tr>
                                            <th>
                                                <select class="form-control form-control-sm" v-model="stockFilter.WarehouseId">
                                                    <option value="0">All</option>
                                                    <option v-for="item in warehouseList" v-bind:value="item.Id">{{item.Name}}</option>
                                                </select>
                                            </th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%-- <tr v-if="articleStockList.length < 1 && !loadingItems" class="table-no-hover">
                                            <td class="text-center" colspan="3">No stock found.</td>
                                        </tr>
                                        <tr v-for="item in articleStockList">
                                            <td>{{item.WarehouseName}}</td>
                                            <td>{{item.CurrentStock}}</td>
                                            <td>{{item.DefaultStock}}</td>
                                        </tr>--%>
                                        <template v-for="(item, index) in articleStockList">
                                            <tr v-show="item.editing">
                                                <td>{{item.WarehouseName}}</td>
                                                <td>
                                                    <input class="form-control form-control-sm h-auto text-center" type="text" v-model="item.edit.CurrentStock" />
                                                </td>
                                                <td>
                                                    <input class="form-control form-control-sm h-auto text-center" type="text" v-model="item.edit.DefaultStock" />

                                                </td>
                                                <td class="text-right">
                                                    <button type="button" title="Save" class="btn btn-primary btn-sm mr-1" v-on:click="saveEditedStock(item)"><i class="fas fa-save"></i></button>
                                                    <button type="button" title="Cancel" class="btn btn-light btn-sm" v-on:click="item.editing=false; isEditing=false;"><i class="fas fa-times"></i></button>
                                                </td>
                                            </tr>
                                            <tr v-show="!item.editing">
                                                <td>{{item.WarehouseName}}</td>
                                                <td class="text-right">{{item.CurrentStock}}</td>
                                                <td class="text-right">{{item.DefaultStock}}</td>
                                                <td class="text-right">
                                                    <button v-show="true" title="Show log" type="button" class="btn btn-primary btn-sm" v-on:click="openStockLog(item)"><i class="fas fa-clipboard-list"></i></button>
                                                    <button v-show="true" title="Edit" type="button" class="btn btn-primary btn-sm" v-on:click="enterEditingStock(item)"><i class="fas fa-pencil-alt"></i></button>
                                                </td>
                                            </tr>
                                        </template>
                                    </tbody>
                                    <tfoot></tfoot>
                                </table>
                            </div>
                        </section>
                        <section v-show="details.IsSerial && id > 0" is="content-section" title="Serie administration" class="col-12" small>
                            <div v-show="id > 0" class="table-responsive" data-min-height="350">
                                <button type="button" class="btn btn-primary btn-sm mb-2" v-on:click="getSerial()">New Serial</button>
                            </div>
                            <div class="table-responsive" data-min-height="400">
                                <table class="table table-sm table-hover table-pointer" id="seriallist">
                                    <thead>
                                        <tr>
                                            <th is="sortable-column" v-bind:value="1" v-bind:filter="serieFilter" v-on:order-by="orderBy" event-name="order-by">Serial Number </th>
                                            <th is="sortable-column" v-bind:value="14" v-bind:filter="serieFilter" v-on:order-by="orderBy" event-name="order-by">Owner Warehouse</th>
                                            <th is="sortable-column" v-bind:value="6" v-bind:filter="serieFilter" v-on:order-by="orderBy" event-name="order-by">Current Warehouse</th>
                                            <th is="sortable-column" v-bind:value="11" v-bind:filter="serieFilter" v-on:order-by="orderBy" event-name="order-by">Expert Controller Name</th>
                                            <th is="sortable-column" v-bind:value="7" v-bind:filter="serieFilter" v-on:order-by="orderBy" event-name="order-by">Active</th>
                                            <th is="sortable-column" v-bind:value="14" v-bind:filter="serieFilter" v-on:order-by="orderBy" event-name="order-by">In Maintenance</th>
                                            <th is="sortable-column" v-bind:value="12" v-bind:filter="serieFilter" v-on:order-by="orderBy" event-name="order-by">Sold</th>
                                            <th></th>
                                        </tr>
                                        <tr>

                                            <th>
                                                <input placeholder="filter" class="form-control form-control-sm" type="text" v-model="serieFilter.SerialNumber" /></th>
                                            <th>
                                                <input placeholder="filter" class="form-control form-control-sm" type="text" v-model="serieFilter.OwnerWarehouseName" /></th>
                                            <th>
                                                <input placeholder="filter" class="form-control form-control-sm" type="text" v-model="serieFilter.CurrentWarehouseName" /></th>
                                            <th></th>
                                            <th>
                                                <select class="form-control form-control-sm" type="text" v-model="serieFilter.Active">
                                                    <option value="null">All</option>
                                                    <option value="true">Active</option>
                                                    <option value="false">Inactive</option>
                                                </select>
                                            </th>
                                            <th>
                                                <select class="form-control form-control-sm" type="text" v-model="serieFilter.IsOnMaintenance">
                                                    <option value="null">All</option>
                                                    <option value="true">Yes</option>
                                                    <option value="false">No</option>
                                                </select>
                                            </th>
                                            <th>
                                                <select class="form-control form-control-sm" type="text" v-model="serieFilter.Sold">
                                                    <option value="null">All</option>
                                                    <option value="true">Yes</option>
                                                    <option value="false">No</option>
                                                </select>
                                            </th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-if="articleSerieList.length < 1 && !loadingItems" class="table-no-hover">
                                            <td class="text-center" colspan="5">No series found.</td>
                                        </tr>
                                        <tr v-for="item in articleSerieList" v-on:click="getSerial(item)">
                                            <td>{{item.SerialNumber}}</td>
                                            <td>{{item.OwnerWarehouseName}}</td>
                                            <td>{{item.CurrentWarehouseName}}</td>
                                            <td>{{item.ExpertControllerFullName}}</td>
                                            <td>
                                                <span v-show="item.Active"><i class="fas fa-fw fa-circle text-success mr-1"></i>Active</span>
                                                <span v-show="!item.Active"><i class="fas fa-fw fa-circle text-danger mr-1"></i>Inactive</span>
                                            </td>
                                            <td>
                                                <span v-show="item.IsOnMaintenance"><i class="fas fa-fw fa-circle text-warning mr-1"></i>Yes</span>
                                                <span v-show="!item.IsOnMaintenance"><i class="fas fa-fw fa-circle text-success mr-1"></i>No</span>
                                            </td>
                                            <td>
                                                <span v-show="item.Sold"><i class="fas fa-fw fa-circle text-danger mr-1"></i>Yes</span>
                                                <span v-show="!item.Sold"><i class="fas fa-fw fa-circle text-success mr-1"></i>No</span>
                                            </td>
                                            <td class="text-right">
                                                <button v-show="true" title="Show log" type="button" class="btn btn-primary btn-sm" v-on:click.stop="openStockLog(item)"><i class="fas fa-clipboard-list"></i></button>
                                            </td>
                                        </tr>

                                    </tbody>
                                    <tfoot></tfoot>
                                </table>
                            </div>
                        </section>
                    </div>
                </div>
            </div>
            <div class="section-footer">
                <a href="/article/list" class="btn btn-sm btn-link">Return</a>
                <button class="btn btn-sm btn-secondary mr-2" type="button" v-on:click="openGenerateLabelModal">Generate label</button>
                <button class="btn btn-sm btn-primary" type="button" v-on:click="save">Save</button>
            </div>
        </section>
        <div id="modalSerial" class="modal" tabindex="-1">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title"><span v-show="serialDetails.ArticleSerieId < 1"><i class="fas fa-tools mr-2"></i>New Serie</span><span v-show="serialDetails.ArticleSerieId > 0">{{serialDetails.SerialNumber}}</span></h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <h5 v-show="serialDetails.Sold" class="text-danger text-center">SOLD</h5>
                        <div class="row">
                            <div class="col-12 mb-2" is="custom-input-group" input-label="Prefix" v-show="serialDetails.ArticleSerieId < 1">
                                <select class="form-control form-control-sm" v-model="serialDetails.ArticleSeriePrefixId">
                                    <option value="0">None</option>
                                    <option v-for="item in articlePrefixList" v-bind:value="item.ArticleSeriePrefixId">{{item.Prefix}}</option>
                                </select>
                            </div>
                            <div class="col-12">
                                <div is="custom-input-group" input-label="Serial Number">
                                    <input class="form-control form-control-sm" type="text" v-model="serialDetails.SerialNumber" required v-bind:disabled="serialDetails.ArticleSerieId > 0 || serialDetails.ArticleSeriePrefixId > 0 || serialDetails.Sold" />
                                </div>
                            </div>
                            <div class="col-12 mt-2">
                                <div is="custom-input-group" input-label="Owner Warehouse">
                                    <select class="form-control form-control-sm" v-model="serialDetails.OwnerWarehouseId" v-bind:disabled="serialDetails.ArticleSerieId > 0 || serialDetails.Sold" required>
                                        <option v-for="item in warehouseList" v-bind:value="item.Id">{{item.Name}}</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-12 mt-2" v-show="!serialDetails.Sold">
                                <div is="custom-input-group" input-label="Current Warehouse" v-show="serialDetails.ArticleSerieId > 0">
                                    <select class="form-control form-control-sm" v-model="serialDetails.CurrentWarehouseId" v-bind:required="serialDetails.ArticleSerieId > 0" v-bind:disabled="serialDetails.Sold">
                                        <option v-for="item in warehouseList" v-bind:value="item.Id">{{item.Name}}</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-12 mt-2" v-show="!serialDetails.Sold">
                                <div is="custom-input-group" input-label="Expert Controller Name" is-select>
                                    <select class="form-control form-control-sm" type="text" v-model="serialDetails.ExpertControllerUserId" v-bind:disabled="!details.IsMaintenanceArticle || serialDetails.Sold">
                                        <option value="0">None</option>
                                        <option v-for="item in userList" v-bind:value="item.Id">{{item.Name}}</option>
                                    </select>
                                </div>
                            </div>
                             <div class="col-12 mt-2" v-show="!serialDetails.Sold">
                                <div is="custom-input-group" input-label="In Maintenance">
                                    <div class="custom-control custom-switch" v-bind:disabled="!details.IsMaintenanceArticle || serialDetails.Sold">
                                        <input id="chkMaintenance" type="checkbox" class="custom-control-input" v-model="serialDetails.IsOnMaintenance" v-bind:disabled="!details.IsMaintenanceArticle || serialDetails.Sold"/>
                                        <label class="custom-control-label" for="chkMaintenance"><span v-show="serialDetails.IsOnMaintenance">Yes</span><span v-show="!serialDetails.IsOnMaintenance">No</span></label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 mt-2" v-show="!serialDetails.Sold">
                                <div is="custom-input-group" input-label="Active">
                                    <div class="custom-control custom-switch" v-bind:disabled="serialDetails.Sold">
                                        <input id="chkCardActive" type="checkbox" class="custom-control-input" v-model="serialDetails.Active" v-bind:disabled="serialDetails.Sold"/>
                                        <label class="custom-control-label" for="chkCardActive"><span v-show="serialDetails.Active">Yes</span><span v-show="!serialDetails.Active">No</span></label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-link btn-sm" data-dismiss="modal">Close</button>
                        <button v-show="!serialDetails.Sold" type="button" class="btn btn-primary btn-sm" v-on:click="saveAndUpdateSerial()">Save</button>
                    </div>
                </div>
            </div>
        </div>

        <div id="modalGenerateLabels" class="modal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title"><i class="fas fa-barcode mr-2"></i>Generate label</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-12 text-center">
                                <span>Select warehouse to print label for:</span>
                                <select class="form-control form-control-sm" type="text" v-model="selectedWarehouseId">
                                    <option v-for="item in warehouseList" v-bind:value="item.Id" v-bind:key="item.WarehouseId">{{item.Name}}</option>
                                </select>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-link btn-sm" data-dismiss="modal">Close</button>
                        <a class="btn btn-sm btn-primary" v-bind:href="'/api/article/getLabels/'+selectedWarehouseId+'?articleIds=' + id" target="_blank">Generate label</a>
                    </div>
                </div>
            </div>
        </div>
        <div id="modalLog" class="modal" tabindex="-1">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title"><span>Stock Log</span></h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="table-responsive" data-min-height="400">
                                <table class="table table-sm table-hover table-pointer" id="stockLogList">
                                    <thead>
                                        <tr class="sort-options">
                                            <th is="sortable-column" v-bind:value="2" v-bind:filter="stockLogFilter" v-on:order-by="stockLogOrderBy" event-name="order-by">Log Event</th>
                                            <th is="sortable-column" v-bind:value="1" v-bind:filter="stockLogFilter" v-on:order-by="stockLogOrderBy" event-name="order-by" class="text-center" colspan="2">Log Date</th>
                                            <th is="sortable-column" v-bind:value="10" v-bind:filter="stockLogFilter" v-on:order-by="stockLogOrderBy" event-name="order-by" v-show="!stockLogFilter.ArticleSerieId < 1">Warehouse Name</th>
                                            <th is="sortable-column" v-bind:value="7" v-bind:filter="stockLogFilter" v-on:order-by="stockLogOrderBy" event-name="order-by">User Name</th>
                                            <th></th>
                                        </tr>
                                        <tr>

                                            <th>
                                                <input placeholder="filter" class="form-control form-control-sm" type="text" v-model="stockLogFilter.LogEvent" />
                                            </th>
                                            <th class="text-center fixed-ddmmyyyy-width">
                                                <input is="date-picker" placeholder="from" class="d-inline-block" v-model="stockLogFilter.LogDateFrom" date-only="true" /></th>
                                            <th class="text-center fixed-ddmmyyyy-width">
                                                <input is="date-picker" placeholder="to" class="d-inline-block" v-model="stockLogFilter.LogDateTo" date-only="true" /></th>
                                            <th v-show="!stockLogFilter.ArticleSerieId < 1">
                                                <select class="form-control form-control-sm" v-model="stockLogFilter.WarehouseId">
                                                    <option value="0">All</option>
                                                    <option v-for="item in warehouseList" v-bind:value="item.Id">{{item.Name}}</option>
                                                </select>
                                            </th>
                                            <th>
                                                <input placeholder="filter" class="form-control form-control-sm" type="text" v-model="stockLogFilter.UserFullName" />
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-if="articleStockLogList.length < 1" class="table-no-hover">
                                            <td class="text-center" colspan="5">No stock log found.</td>
                                        </tr>
                                        <tr v-for="(item, index) in articleStockLogList">
                                            <td>{{item.LogEvent}}</td>
                                            <td colspan="2" class="text-center">{{item.LogDate | momentWithHour}}</td>
                                            <td v-show="!stockLogFilter.ArticleSerieId < 1">{{item.WarehouseName}}</td>
                                            <td>{{item.UserFullName}}</td>
                                        </tr>
                                    </tbody>
                                    <tfoot>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
