<%@ Page Title="Category List" Language="C#" MasterPageFile="~/TbxPortal.Master" AutoEventWireup="true" CodeBehind="CategoryList.aspx.cs" Inherits="TbxPortal.Web.App.Client.Category.CategoryList.List.CategoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/jsCategoryList") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadCrumb" runat="server">
    <li>Categories</li>
    <li><a href="<%=Page.ResolveUrl("~/category/list") %>">Category List</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="server">
    <div id="app" class="row justify-content-center">
        <section is="content-section" icon="fas fa-list" title="Category List" class="col-12 col-md-10 col-lg-8 col-xl-5" um-anchor="#HowToArticles">
            <div class="row row-cols-1 g-1">
                <div class="col-3">
                    <button class="btn btn-sm btn-primary" v-on:click="openModal()">New Category</button>
                </div>
                <div class="col-3 text-right">
                    <div class="custom-control custom-switch text-left">
                        <input id="chkActive" type="checkbox" class="custom-control-input" v-model="category.showCreatedAt" />
                        <label class="custom-control-label" for="chkActive"><span v-show="category.showCreatedAt">Show Created At</span><span v-show="!category.showCreatedAt">Don't Show Created At</span></label>
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

                    <input id="chkActive" type="checkbox" class="custom-control-input" v-model="category.showCreatedAt" />
                        <label class="custom-control-label" for="chkActive"><span v-show="category.showCreatedAt">Yes</span><span v-show="!category.showCreatedAt">No</span></label>
                    </p>
                </div>
            </div>--%>
            <div class="table-responsive mt-3">
                <table id="datalist" class="table table-sm table-hover">
                    <thead>
                        <tr>
                            <th is="sortable-column" v-bind:value="1" v-bind:filter="category.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Name </th>
                            <th is="sortable-column" v-bind:value="2" v-bind:filter="category.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Priority </th>
                            <th v-show="category.showCreatedAt" is="sortable-column" v-bind:value="3" v-bind:filter="category.filter" v-on:order-by-changed="orderBy" class="text-nowrap">Created At </th>
                        </tr>
                        <tr>
                            <th>
                                <input type="text" class="form-control form-control-sm" placeholder="filter" v-model="category.filter.CategoryName" />
                            </th>
                            <th>
                                <select class="form-control form-control-sm" placeholder="filter" v-model="category.filter.CategoryPriority">
                                    <option value="null">All</option>
                                    <option value="1">Critical</option>
                                    <option value="2">High Priority</option>
                                    <option value="3">Normal</option>
                                    <option value="4">Not Important</option>
                                    <option value="5">Extra</option>
                                    <option value="6">Unknown</option>
                                </select>
                            </th>
                            <th v-show="category.showCreatedAt">
                                <date-picker v-model="category.filter.createdAt"></date-picker>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-if="!category.isLoading && category.items.length == 0">
                            <td colspan="100%" class="text-center">Could not find any categories</td>
                        </tr>
                        <tr v-for="item in category.items" class="cursor-pointer" v-on:click="showCategoryDetails(item)">
                            <td>{{item.CategoryName}}</td>
                            <td>
                                <span v-if="item.CategoryPriority == 1">Critical</span>
                                <span v-if="item.CategoryPriority == 2">High Priority</span>
                                <span v-if="item.CategoryPriority == 3">Normal</span>
                                <span v-if="item.CategoryPriority == 4">Not Important</span>
                                <span v-if="item.CategoryPriority == 5">Extra</span>
                                <span v-if="item.CategoryPriority == 6">Unknown</span>
                            </td>
                            <td v-show="category.showCreatedAt">{{item.CreatedAt | moment}}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </section>
        <!-- Modal -->
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
                            <div class="col-3">
                                <label for="txtOrderNo" class="form-label">Category Name</label>
                                <input type="text" id="textCategoryName" class="form-control form-control-sm" v-model="category.details.CategoryName" required>
                            </div>
                        </div>
                        <div class="row row-cols-2 g-2">
                            <div class=" col-3">
                                <label class="form-label">Priority</label>
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
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-primary" v-if="!category.details.categoryId > 0" v-on:click="saveCategory">Save changes</button>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
