<%@ Page Title="Business List" Language="C#" MasterPageFile="~/TbxPortal.Master" AutoEventWireup="true" CodeBehind="BusinessList.aspx.cs" Inherits="TbxPortal.Web.App.Client.Business.BusinessList.List.BusinessList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/jsBusinessList") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadCrumb" runat="server">
    <li>Categories</li>
    <li><a href="<%=Page.ResolveUrl("~/business/list") %>">Business List</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="server">
    <div id="app" class="row justify-content-center">
        <section is="content-section" icon="fas fa-list" title="Business List" class="col-12 col-md-10 col-lg-8 col-xl-5" um-anchor="#HowToArticles">
            <div class="row row-cols-1 g-1">
                <div class="col-3">
                    <button class="btn btn-sm btn-primary" v-on:click="openModal()">New Business</button>
                </div>
                <div class="col-3 text-right">
                    <div class="custom-control custom-switch text-left">
                        <input id="chkActive" type="checkbox" class="custom-control-input" v-model="business.showCreatedAt" />
                        <label class="custom-control-label" for="chkActive"><span v-show="business.showCreatedAt">Show Created At</span><span v-show="!business.showCreatedAt">Don't Show Created At</span></label>
                    </div>
                </div>
            </div>
            <div class="table-responsive mt-3">
                <table id="datalist" class="table table-sm table-hover">
                    <thead>
                        <tr>
                            <th is="sortable-column" v-bind:value="1" v-bind:filter="business.filter" v-on:order-by-changed="orderBy" class="text-nowrap text-center">Name </th>
                            <th v-show="business.showCreatedAt" is="sortable-column" v-bind:value="3" v-bind:filter="business.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Created At </th>
                        </tr>
                        <tr>
                            <th>
                                <input type="text" class="form-control form-control-sm" placeholder="filter" v-model="business.filter.BusinessName" />
                            </th>
                            <th v-show="business.showCreatedAt">
                                <date-picker v-model="business.filter.createdAt"></date-picker>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-if="!business.isLoading && business.items.length == 0">
                            <td colspan="100%" class="text-center">Could not find any categories</td>
                        </tr>
                        <tr v-for="item in business.items" class="cursor-pointer" v-on:click="showBusinessDetails(item)">
                            <td class="text-center">{{item.BusinessName}}</td>
                            <td v-show="business.showCreatedAt">{{item.CreatedAt | moment}}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </section>
        <!-- Modal -->
        <div id="modalNewBusiness" class="modal fade" ref="recordModal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Create New Business</h5>
                        <button type="button" class="btn btn-sm btn-link" data-bs-dismiss="modal" aria-label="Close">
                            <i class="bi bi-x h5"></i>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row row-cols-1 g-2">
                            <div class="col-3">
                                <label for="txtOrderNo" class="form-label">Business Name</label>
                                <input type="text" id="textBusinessName" class="form-control form-control-sm" v-model="business.details.BusinessName" required>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-primary" v-if="!business.details.BusinessId > 0" v-on:click="saveBusiness">Save changes</button>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
