<%@ Page Title="Payment List" Language="C#" MasterPageFile="~/TbxPortal.Master" AutoEventWireup="true" CodeBehind="MonthlyOverview.aspx.cs" Inherits="TbxPortal.Web.App.Client.Report.MonthlyOverview.MonthlyOverview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/jsPaymentList") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadCrumb" runat="server">
    <li>Payments</li>
    <li><a href="<%=Page.ResolveUrl("~/payment/list") %>">Payment List</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="server">
    <div id="app" class="row justify-content-center">
        <section is="content-section" icon="fas fa-list" title="Payment List" class="col-12 col-xl-11" um-anchor="#HowToArticles">
            <button class="btn btn-sm btn-primary" v-on:click="openModal()">New Payment</button>
            <div class="row">
                <div class="col-12 mb-3">
                    <ul class="nav nav-pills justify-content-center">
                        <li class="nav-item">
                            <a class="nav-link active" href="<%=Page.ResolveUrl("~/payment/list") %>">Payment List</a>
                        </li>
                        <%--                        <li class="nav-item">
                            <a class="nav-link" href="<%=Page.ResolveUrl("~/article/serie") %>">Serial List</a>
                        </li>--%>
                    </ul>
                    <div class="col-3 text-right">
                        <div class="custom-control custom-switch text-left">
                            <input id="chkShowCreatdByAndCreatedAt" type="checkbox" class="custom-control-input" v-model="payment.chkShowCreatdByAndCreatedAt" />
                            <label class="custom-control-label" for="chkShowCreatdByAndCreatedAt"><span v-show="payment.chkShowCreatdByAndCreatedAt">Show Created By And Created At</span><span v-show="!payment.chkShowCreatdByAndCreatedAt">Don't Show Created By And Created At</span></label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="table-responsive mt-3">
                <table id="datalist" class="table table-sm table-striped table-hover">
                    <thead>
                        <tr>
                            <th is="sortable-column" v-bind:value="1" v-bind:filter="payment.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Title              </th>
                            <th is="sortable-column" v-bind:value="10" v-bind:filter="payment.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Priority           </th>
                            <th is="sortable-column" v-bind:value="2" v-bind:filter="payment.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Business Name      </th>
                            <th is="sortable-column" v-bind:value="9" v-bind:filter="payment.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Is Deposit         </th>
                            <th is="sortable-column" v-bind:value="3" v-bind:filter="payment.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Is Paid to a Person</th>
                            <th is="sortable-column" v-bind:value="4" v-bind:filter="payment.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Category Name      </th>
                            <th is="sortable-column" v-bind:value="5" v-bind:filter="payment.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Price              </th>
                            <th is="sortable-column" v-bind:value="6" v-bind:filter="payment.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Date               </th>
                            <th is="sortable-column" v-if="payment.chkShowCreatdByAndCreatedAt" v-bind:value="7" v-bind:filter="payment.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Created By         </th>
                            <th is="sortable-column" v-if="payment.chkShowCreatdByAndCreatedAt" v-bind:value="8" v-bind:filter="payment.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Created At         </th>
                        </tr>
                        <tr>
                            <th>
                                <input type="text" class="form-control form-control-sm" placeholder="filter" v-model="payment.filter.Title" />
                            </th>
                            <th>
                                <input type="text" class="form-control form-control-sm" placeholder="filter" v-model="payment.filter.PaymentPriorityName" />
                            </th>
                            <th>
                                <input type="text" class="form-control form-control-sm" placeholder="filter" v-model="payment.filter.BusinessName" />
                            </th>
                            <th>
                                <select class="form-control form-control-sm" v-model="payment.filter.IsDeposit">
                                    <option value="null">All</option>
                                    <option value="false">No</option>
                                    <option value="true">Yes</option>
                                </select>
                            </th>
                            <th>
                                <select class="form-control form-control-sm" v-model="payment.filter.IsPaidToPerson">
                                    <option value="null">All</option>
                                    <option value="false">No</option>
                                    <option value="true">Yes</option>
                                </select>
                            </th>
                            <th>
                                <input type="text" class="form-control form-control-sm" placeholder="filter" v-model="payment.filter.CategoryName" />
                            </th>
                            <th>
                                <input type="text" class="form-control form-control-sm" placeholder="filter" v-model="payment.filter.Price" />
                            </th>
                            <th>
                                <date-picker v-model="payment.filter.Date"></date-picker>
                            </th>
                            <th>
                                <input v-if="payment.chkShowCreatdByAndCreatedAt" type="text" class="form-control form-control-sm" placeholder="filter" v-model="payment.filter.CreatedByFullName" /></th>
                            <th>
                                <input v-if="payment.chkShowCreatdByAndCreatedAt" type="text" class="form-control form-control-sm" placeholder="filter" v-model="payment.filter.CreatedAt" />
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-if="!payment.isLoading && payment.items.length == 0">
                            <td colspan="100%" class="text-center">Could not find any payments</td>
                        </tr>
                        <tr v-for="item in payment.items" class="cursor-pointer" v-on:click="openModal(item)">
                            <td>{{item.Title}}</td>
                            <td>{{item.PaymentPriorityName}}</td>
                            <td>{{item.BusinessName}}</td>
                            <td><span v-if="item.IsDeposit">Yes</span><span v-if="!item.IsDeposit">No</span></td>
                            <td><span v-if="item.IsPaidToPerson">Yes</span><span v-if="!item.IsPaidToPerson">No</span></td>
                            <td>{{item.CategoryName}}</td>
                            <td>{{item.FormattedPrice}}</td>
                            <td>{{item.Date}}</td>
                            <td v-if="payment.chkShowCreatdByAndCreatedAt">{{item.CreatedByFullName}}</td>
                            <td v-if="payment.chkShowCreatdByAndCreatedAt">{{item.CreatedAt}}</td>

                        </tr>
                    </tbody>
                </table>
            </div>
        </section>
        <!-- Modal -->
        <div id="modalNewPayment" class="modal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Create New Payment</h5>
                        <button type="button" class="btn btn-primary" data-dismiss="modal" data-toggle="modal" data-target="#modalNewCategory">New Category</button>
                        <button type="button" class="btn btn-sm btn-link" data-bs-dismiss="modal" aria-label="Close">
                            <i class="bi bi-x h5"></i>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row row-cols-1 g-2">
                            <div class=" col-8 mt-2">
                                <div is="custom-input-group" input-label="Title">
                                    <input class="form-control form-control-sm" type="text" v-model="payment.details.Title" required />
                                </div>
                            </div>
                            <div class=" col-4 mt-2">
                                <div is="custom-input-group" input-label="Category Name">
                                    <select class="form-control form-control-sm" v-model="payment.details.CategoryId" required>
                                        <option v-for="item in payment.categories" v-bind:value="item.CategoryId">{{item.CategoryName}}</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class="row row-cols-2 g-2">
                            <div class=" col-4 mt-2">
                                <div class="dropdown">
                                    <div is="custom-input-group" input-label="Business Name">
                                        <input class="form-control form-control-sm" required type="text" autocomplete="off" data-toggle="dropdown" id="searchBusinesses" v-model="payment.details.BusinessName" v-on:keydown.enter.prevent="scanBusiness()" autofocus />
                                        <div class="dropdown-menu" ref="searchBusinesses">
                                            <div class="dropdown-item" v-show="business.searchResultBusinesses.length < 1 && !business.loadingAvailableBusinesses"><span>No results.</span></div>
                                            <div class="dropdown-item" v-show="business.loadingAvailableBusinesses"><span>Searching...</span></div>
                                            <div class="dropdown-item cursor-pointer" v-for="item in business.searchResultBusinesses" v-bind:key="item.Id" v-on:click="selectBusinesss(item)"><span>{{item.BusinessName}}</span></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-4 mt-2">
                                <div is="custom-input-group" input-label="Is Deposit">
                                    <div class="custom-control custom-switch">
                                        <input id="chkDeposit" type="checkbox" class="custom-control-input" v-model="payment.details.IsDeposit" />
                                        <label class="custom-control-label" for="chkDeposit"><span v-show="payment.details.IsDeposit">Yes</span><span v-show="!payment.details.IsDeposit">No</span></label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-4 mt-2">
                                <div is="custom-input-group" input-label="Is Paid To a person">
                                    <div class="custom-control custom-switch">
                                        <input id="chkPaidToPerson" type="checkbox" class="custom-control-input" v-model="payment.details.IsPaidToPerson" required />
                                        <label class="custom-control-label" for="chkPaidToPerson"><span v-show="payment.details.IsPaidToPerson">Yes</span><span v-show="!payment.details.IsPaidToPerson">No</span></label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row row-cols-3 g-3">
                            <div class="fixed-ddmmyyyy-width mt-2">
                                <div is="custom-input-group" input-label="Date Of Payment">
                                    <date-picker v-model="payment.details.Date" required></date-picker>
                                </div>
                            </div>
                            <div class="col-4 mt-2">
                                <div is="custom-input-group" input-label="Price">
                                    <input class="form-control form-control-sm" type="number" v-model="payment.details.Price" required />
                                </div>
                            </div>
                           <div class="col-4 mt-2">
                                  <div is="custom-input-group" input-label="Payment Priority" class="text-left">
                                    <select class="form-control form-control-sm" v-model="payment.details.PaymentPriorityId" required>
                                        <option v-for="item in paymentPriority.items" v-bind:value="item.PaymentPriorityId">{{item.PaymentPriorityName}}</option>
                                    </select>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-primary" v-on:click="savePayment">Save changes</button>
                    </div>
                </div>
            </div>
        </div>
        <div id="modalNewCategory" class="modal fade" ref="recordModal" tabindex="-1">
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
                            <div class="col-5">
                                <div is="custom-input-group" input-label="Category Name">
                                    <input type="text" id="textCategoryName" class="form-control form-control-sm" v-model="category.details.CategoryName" required>
                                </div>
                            </div>
                            <div class="col-5">
                                <div is="custom-input-group" input-label="Category Priority">
                                    <select class="form-control form-control-sm" v-model="category.details.CategoryPriority">
                                        <option value="1">Critical</option>
                                        <option value="2">High Priority</option>
                                        <option value="3">Normal</option>
                                        <option value="4">Not Important</option>
                                        <option value="5">Extra</option>
                                        <option value="6">Unknown</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer mt-2">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal" data-toggle="modal" data-target="#modalNewPayment">Close</button>
                            <button type="button" class="btn btn-primary" data-dismiss="modal" data-toggle="modal" data-target="#modalNewPayment" v-on:click="saveCategory">Save changes</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
