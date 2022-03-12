<%@ Page Title="PaymentTotal List" Language="C#" MasterPageFile="~/TbxPortal.Master" AutoEventWireup="true" CodeBehind="PaymentTotalList.aspx.cs" Inherits="TbxPortal.Web.App.Client.PaymentTotal.List.PaymentTotalList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/jsPaymentTotalList") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadCrumb" runat="server">
    <li>Payment Total</li>
    <li><a href="<%=Page.ResolveUrl("~/ptotal/list") %>">Payment Total List</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="server">
    <div id="app" class="row justify-content-center">
        <section is="content-section" icon="fas fa-list" title="Payment Total List" class="col-12 col-md-10 col-lg-8 col-xl-5" um-anchor="#HowToArticles">
            <div class="row row-cols-1 g-1">
                <div class="col-3">
                    <button class="btn btn-sm btn-primary" v-on:click="openModal()">New Payment Total</button>
                </div>
                <div class="col-3 text-right">
                    <div class="custom-control custom-switch text-left">
                        <input id="chkActive" type="checkbox" class="custom-control-input" v-model="paymentTotal.showCreatedAt" />
                        <label class="custom-control-label" for="chkActive"><span v-show="paymentTotal.showCreatedAt">Show Created At</span><span v-show="!paymentTotal.showCreatedAt">Don't Show Created At</span></label>
                    </div>
                </div>
            </div>
            <div class="table-responsive mt-3">
                <table id="datalist" class="table table-sm table-hover">
                    <thead>
                        <tr>
                            <th is="sortable-column" v-bind:value="1" v-bind:filter="paymentTotal.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Title </th>
                            <th is="sortable-column" v-bind:value="2" v-bind:filter="paymentTotal.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Price </th>
                            <th is="sortable-column" v-bind:value="3" v-bind:filter="paymentTotal.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Date </th>
                            <th is="sortable-column" v-bind:value="4" v-bind:filter="paymentTotal.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Is Deposit </th>
                            <th v-show="paymentTotal.showCreatedAt" is="sortable-column" v-bind:value="5" v-bind:filter="paymentTotal.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Created By </th>
                            <th v-show="paymentTotal.showCreatedAt" is="sortable-column" v-bind:value="6" v-bind:filter="paymentTotal.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Created At </th>
                        </tr>
                        <tr>
                            <th>
                                <input type="text" class="form-control form-control-sm" placeholder="filter" v-model="paymentTotal.filter.Title" />
                            </th>
                            <th>
                                <input type="text" class="form-control form-control-sm" placeholder="filter" v-model="paymentTotal.filter.Price" />
                            </th>
                            <th>
                                <date-picker v-model="paymentTotal.filter.Date"></date-picker>
                            </th>
                            <th>
                                <select class="form-control form-control-sm" v-model="paymentTotal.filter.IsDeposit">
                                    <option value="null">All</option>
                                    <option value="false">No</option>
                                    <option value="true">Yes</option>
                                </select>
                            </th>
                            <th v-show="paymentTotal.showCreatedAt">
                                <input type="text" class="form-control form-control-sm" placeholder="filter" v-model="paymentTotal.filter.CreatedByFullName" />
                            </th>
                            <th v-show="paymentTotal.showCreatedAt">
                                <date-picker v-model="paymentTotal.filter.createdAt"></date-picker>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-if="!paymentTotal.isLoading && paymentTotal.items.length == 0">
                            <td colspan="100%" class="text-center">Could not find any payments</td>
                        </tr>
                        <tr v-for="item in paymentTotal.items" class="cursor-pointer" v-on:click="showPaymentTotalDetails(item)">
                            <td>{{item.Title}}</td>
                            <td>{{item.Price}}</td>
                            <td>{{item.Date}}</td>
                            <td><span v-if="item.IsDeposit">Yes</span><span v-if="!item.IsDeposit">No</span></td>
                            <td v-if="paymentTotal.chkShowCreatdByAndCreatedAt">{{item.CreatedByFullName}}</td>
                            <td v-if="paymentTotal.chkShowCreatdByAndCreatedAt">{{item.CreatedAt}}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </section>
        <!-- Modal -->
        <div id="modalNewPaymentTotal" class="modal fade" ref="recordModal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Create New Payment Total</h5>
                        <button type="button" class="btn btn-sm btn-link" data-bs-dismiss="modal" aria-label="Close">
                            <i class="bi bi-x h5"></i>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row row-cols-1 g-2">
                            <div class=" col-8 mt-2">
                                <div is="custom-input-group" input-label="Title">
                                    <input class="form-control form-control-sm" type="text" v-model="paymentTotal.details.Title" required />
                                </div>
                            </div>
                            <div class="col-4 mt-2">
                                <div is="custom-input-group" input-label="Is Deposit">
                                    <div class="custom-control custom-switch">
                                        <input id="chkDeposit" type="checkbox" class="custom-control-input" v-model="paymentTotal.details.IsDeposit" />
                                        <label class="custom-control-label" for="chkDeposit"><span v-show="paymentTotal.details.IsDeposit">Yes</span><span v-show="!paymentTotal.details.IsDeposit">No</span></label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row row-cols-2 g-2">
                        </div>
                        <div class="row row-cols-3 g-3">
                            <div class="fixed-ddmmyyyy-width mt-2">
                                <div is="custom-input-group" input-label="Date">
                                    <date-picker v-model="paymentTotal.details.Date" required></date-picker>
                                </div>
                            </div>
                            <div class="col-4 mt-2">
                                <div is="custom-input-group" input-label="Price">
                                    <input class="form-control form-control-sm" type="number" v-model="paymentTotal.details.Price" required />
                                </div>
                            </div>
                            <div class="col-4 mt-2">
                                <div v-show="paymentTotal.details.IsDeposit" class="dropdown">
                                    <div is="custom-input-group" input-label="Business Name">
                                        <input class="form-control form-control-sm" required type="text" autocomplete="off" data-toggle="dropdown" id="searchBusinesses" v-model="paymentTotal.details.BusinessName" v-on:keydown.enter.prevent="scanBusiness()" autofocus />
                                        <div class="dropdown-menu" ref="searchBusinesses">
                                            <div class="dropdown-item" v-show="business.searchResultBusinesses.length < 1 && !business.loadingAvailableBusinesses"><span>No results.</span></div>
                                            <div class="dropdown-item" v-show="business.loadingAvailableBusinesses"><span>Searching...</span></div>
                                            <div class="dropdown-item cursor-pointer" v-for="item in business.searchResultBusinesses" v-bind:key="item.Id" v-on:click="selectBusinesss(item)"><span>{{item.BusinessName}}</span></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-primary" v-if="!paymentTotal.details.categoryId > 0" v-on:click="savePaymentTotal">Save changes</button>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
