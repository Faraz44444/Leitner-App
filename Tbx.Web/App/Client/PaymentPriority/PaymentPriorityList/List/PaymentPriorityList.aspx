<%@ Page Title="Payment Priority List" Language="C#" MasterPageFile="~/TbxPortal.Master" AutoEventWireup="true" CodeBehind="PaymentPriorityList.aspx.cs" Inherits="TbxPortal.Web.App.Client.PaymentPriority.PaymentPriorityList.List.PaymentPriorityList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/jsPaymentPriorityList") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadCrumb" runat="server">
    <li>Priorities</li>
    <li><a href="<%=Page.ResolveUrl("~/paymentpriority/list") %>">Payment Priority List</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="server">
    <div id="app" class="row justify-content-center">
        <section is="content-section" icon="fas fa-hashtag" title="Priority List" class="col-12 col-md-5 col-lg-8 col-xl-5" um-anchor="#HowToArticles">
            <div class="row row-cols-1 g-1">
                <div class="col-3">
                    <button class="btn btn-sm btn-primary" v-on:click="openModal()">New Payment Priority</button>
                </div>
                <div class="col-3 text-right">
                    <div class="custom-control custom-switch text-left">
                        <input id="chkActive" type="checkbox" class="custom-control-input" v-model="paymentPriority.showCreatedAt" />
                        <label class="custom-control-label" for="chkActive"><span v-show="paymentPriority.showCreatedAt">Show Created At</span><span v-show="!paymentPriority.showCreatedAt">Don't Show Created At</span></label>
                    </div>
                </div>
            </div>
            <%--            <div class="d-flex justify-content-between mt-2">
                <div class="d-flex align-items-center">
                    <button class="btn btn-sm btn-primary" v-on:click="openModal()">New Category</button>
                </div>
                <div class="custom-control custom-switch text-left">
                    <p class="text-right">
                        Show CreatedAt:

                    <input id="chkActive" type="checkbox" class="custom-control-input" v-model="paymentPriority.showCreatedAt" />
                        <label class="custom-control-label" for="chkActive"><span v-show="paymentPriority.showCreatedAt">Yes</span><span v-show="!paymentPriority.showCreatedAt">No</span></label>
                    </p>
                </div>
            </div>--%>
            <div class="table-responsive mt-3">
                <table id="datalist" class="table table-sm table-hover">
                    <thead>
                        <tr>
                            <th is="sortable-column" v-bind:value="1" v-bind:filter="paymentPriority.filter" v-on:order-by-changed="orderBy" class="text-nowrap text-center">Name </th>
                            <th v-show="paymentPriority.showCreatedAt" is="sortable-column" v-bind:value="3" v-bind:filter="paymentPriority.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Created At </th>
                        </tr>
                        <tr>
                            <th>
                                <input type="text" class="form-control form-control-sm" placeholder="filter" v-model="paymentPriority.filter.PaymentPriorityName" />
                            </th>
                            <th v-show="paymentPriority.showCreatedAt">
                                <date-picker v-model="paymentPriority.filter.createdAt"></date-picker>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-if="!paymentPriority.isLoading && paymentPriority.items.length == 0">
                            <td colspan="100%" class="text-center">Could not find any priorities</td>
                        </tr>
                        <tr v-for="item in paymentPriority.items" class="cursor-pointer" v-on:click="showCategoryDetails(item)">
                            <td class="text-center">{{item.PaymentPriorityName}}</td>
                            <td v-show="paymentPriority.showCreatedAt">{{item.CreatedAt | moment}}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </section>
        <!-- Modal -->
        <div id="modalNewPaymentPriority" class="modal fade" ref="recordModal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Create New Category</h5>
                        <button type="button" class="btn btn-sm btn-link" data-bs-dismiss="modal" aria-label="Close">
                            <i class="bi bi-x h5"></i>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row row-cols-1 g-2">
                            <div class="col-3">
                                <label for="txtOrderNo" class="form-label">Priority Name</label>
                                <input type="text" id="textCategoryName" class="form-control form-control-sm" v-model="paymentPriority.details.PaymentPriorityName" required>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-primary" v-if="!paymentPriority.details.paymentPriorityId > 0" v-on:click="savePaymentPriority">Save changes</button>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
