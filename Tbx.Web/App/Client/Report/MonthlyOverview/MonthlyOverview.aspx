<%@ Page Title="Payment List" Language="C#" MasterPageFile="~/TbxPortal.Master" AutoEventWireup="true" CodeBehind="MonthlyOverview.aspx.cs" Inherits="TbxPortal.Web.App.Client.Report.MonthlyOverview.MonthlyOverview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/jsMonthlyOverview") %>
    <%: Scripts.Render("~/jsChartJs310") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadCrumb" runat="server">
    <li>Reports</li>
    <li><a href="<%=Page.ResolveUrl("~/report/monthlyoverview") %>">Monthly Overview</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="server">
    <div id="app" class="row justify-content-center">
        <section is="content-section" icon="fas fa-list" title="?" class="col-12 col-lg-10">
            <div class="row">
                <div class="col-2">
                    <div is="custom-input-group" input-label="Select a year" class="text-left">
                        <select class="form-control form-control-sm" v-model="selectedYear">
                            <option v-for="item in DatesYears">{{item}}</option>
                        </select>
                    </div>
                </div>
                <div class="col-2">
                    <div is="custom-input-group" input-label="Select a month" class="text-left">
                        <select class="form-control form-control-sm" v-model="selectedMonth">
                            <option v-for="item in payment.dateLimits[selectedYear]" v-bind:value="MonthNames.indexOf(MonthNames[item])">{{MonthNames[item]}}</option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <div class="col-12 d-flex justify-content-center">
                        <div style="position: relative; height: 100%; width: 100%">
                            <canvas v-bind:id="'overviewChart'" width="70" height="70"></canvas>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <div class="row justify-content-center">
            <section is="content-section" icon="fas fa-list" title="Numbers" class="col-12 col-lg-10 ">
                <div class="row mb-4">
                    <div class="col-3 ">
                        <div is="custom-input-group" input-label="Expenditures Sum">
                            <input class="form-control form-control-sm text-left table-success" type="text" v-model="SelectedDateExpendituresSum" disabled />
                        </div>
                    </div>
                    <div class="col-3">
                        <div is="custom-input-group" input-label="Incomes Sum">
                            <input class="form-control form-control-sm text-left table-success" type="text" v-model="SelectedDateIncomeSum" disabled />
                        </div>
                    </div>
                    <div class="col-3">
                        <div is="custom-input-group" input-label="Saving Sum">
                            <input class="form-control form-control-sm text-left table-success" type="text" v-model="SelectedDateSaving" disabled />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <template v-for="item in payment.selectedItems">
                        <div class="col-3 mt-2">
                            <div is="custom-input-group" v-bind:input-label="item.CategoryName">
                                <input class="form-control form-control-sm text-left" type="text" v-model="item.FormattedPrice" disabled />
                            </div>
                        </div>
                    </template>
                </div>
            </section>
            <section is="content-section" icon="fas fa-list" title="Details" class="col-12 col-lg-10">
                <div class="row">
                    <div class="col-3">
                        <div is="custom-input-group" input-label="Total Expenditures">
                            <input class="form-control form-control-sm text-left" type="text" v-model="payment.detailsExpendituresSum" disabled />
                        </div>
                    </div>
                    <div class="col-3">
                        <div is="custom-input-group" input-label="Total Income">
                            <input class="form-control form-control-sm text-left" type="text" v-model="payment.detailsIncomesSum" disabled />
                        </div>
                    </div>
                </div>
                <div class="table-responsive mt-3">
                    <table id="datalist" class="table table-sm table-striped table-hover">
                        <thead>
                            <tr>
                                <th is="sortable-column" v-bind:value="1" v-bind:filter="payment.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Title              </th>
                                <th is="sortable-column" v-bind:value="10" v-bind:filter="payment.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Priority          </th>
                                <th is="sortable-column" v-bind:value="2" v-bind:filter="payment.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Business Name      </th>
                                <th is="sortable-column" v-bind:value="5" v-bind:filter="payment.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Price              </th>
                                <th is="sortable-column" v-bind:value="6" v-bind:filter="payment.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Date               </th>
                                <th is="sortable-column" v-bind:value="9" v-bind:filter="payment.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Is Deposit         </th>
                                <th is="sortable-column" v-bind:value="3" v-bind:filter="payment.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Is Paid to a Person</th>
                                <th is="sortable-column" v-bind:value="4" v-bind:filter="payment.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Category Name      </th>
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
                                    <input type="text" class="form-control form-control-sm" placeholder="filter" v-model="payment.filter.Price" />
                                </th>
                                <th>
                                    <date-picker v-model="payment.filter.Date"></date-picker>
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
                                    <input v-if="payment.chkShowCreatdByAndCreatedAt" type="text" class="form-control form-control-sm" placeholder="filter" v-model="payment.filter.CreatedByFullName" /></th>
                                <th>
                                    <input v-if="payment.chkShowCreatdByAndCreatedAt" type="text" class="form-control form-control-sm" placeholder="filter" v-model="payment.filter.CreatedAt" />
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-if="!payment.isLoading && payment.detailItems.length == 0">
                                <td colspan="100%" class="text-center">Could not find any payments</td>
                            </tr>
                            <tr v-for="item in payment.detailItems" class="cursor-pointer">
                                <td>{{item.Title}}</td>
                                <td>{{item.PaymentPriorityName}}</td>
                                <td>{{item.BusinessName}}</td>
                                <td>{{item.FormattedPrice}}</td>
                                <td>{{item.Date}}</td>
                                <td><span v-if="item.IsDeposit">Yes</span><span v-if="!item.IsDeposit">No</span></td>
                                <td><span v-if="item.IsPaidToPerson">Yes</span><span v-if="!item.IsPaidToPerson">No</span></td>
                                <td>{{item.CategoryName}}</td>
                                <td v-if="payment.chkShowCreatdByAndCreatedAt">{{item.CreatedByFullName}}</td>
                                <td v-if="payment.chkShowCreatdByAndCreatedAt">{{item.CreatedAt}}</td>

                            </tr>
                        </tbody>
                    </table>
                </div>

            </section>
        </div>

    </div>
</asp:Content>
