<%@ Page Title="Users" Language="C#" MasterPageFile="~/TbxPortal.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="TbxPortal.Web.App.Client.Admin.User.List.UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/jsUserList") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadCrumb" runat="server">
    <li>Admin</li>
    <li><a href="<%=Page.ResolveUrl("~/admin/users") %>">Users</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="server">
    <div id="app" class="row justify-content-center">
        <section is="content-section" icon="fas fa-users-cog" title="Users" class="col-12 col-md-10 col-lg-8 col-xl-6" um-anchor="#HowToUsersAndRoles">
            <a class="btn btn-sm btn-primary" href="/admin/users/new">New User</a>
            <div class="table-responsive" data-min-height="400">
                <table class="table table-sm table-hover" id="datalist">
                    <thead>
                        <tr>
                            <th is="sortable-column" v-bind:value="4" v-bind:filter="filter" v-on:order-by="orderBy" event-name="order-by">Name</th>
                            <th is="sortable-column" v-bind:value="3" v-bind:filter="filter" v-on:order-by="orderBy" event-name="order-by">E-Mail</th>
                            <th is="sortable-column" v-bind:value="2" v-bind:filter="filter" v-on:order-by="orderBy" event-name="order-by">Username</th>
                            <th is="sortable-column" v-bind:value="6" v-bind:filter="filter" v-on:order-by="orderBy" event-name="order-by">Status</th>
                            <th is="sortable-column" v-bind:value="7" v-bind:filter="filter" v-on:order-by="orderBy" event-name="order-by" class="text-center d-none d-md-table-cell">Last Logged In</th>
                        </tr>
                        <tr>
                            <th>
                                <input placeholder="filter" class="form-control form-control-sm" type="text" v-model="filter.FullName" /></th>
                            <th>
                                <input placeholder="filter" class="form-control form-control-sm" type="text" v-model="filter.Email" /></th>
                            <th>
                                <input placeholder="filter" class="form-control form-control-sm" type="text" v-model="filter.Username" /></th>
                            <th>
                                <select class="form-control form-control-sm" type="text" v-model="filter.Active">
                                    <option value="null">All</option>
                                    <option value="true">Active</option>
                                    <option value="false">Inactive</option>
                                </select>
                            </th>
                            <th class="d-none d-md-table-cell"></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in items">
                            <td>
                                <hyper-link v-bind:title="item.FullName" v-bind:link="'/admin/users/' + item.UserId" v-bind:new-window="false"></hyper-link>
                            </td>
                            <td>{{item.Email}}</td>
                            <td>{{item.Username}}</td>
                            <td>
                                <span v-show="item.Active"><i class="fas fa-fw fa-circle text-success mr-1"></i>Active</span>
                                <span v-show="!item.Active"><i class="fas fa-fw fa-circle text-danger mr-1"></i>Inacitve</span>
                            </td>
                            <td class="text-center d-none d-md-table-cell">{{item.LastLoginDate | moment}}</td>
                        </tr>
                    </tbody>
                    <tfoot></tfoot>
                </table>
            </div>            
        </section>
        <section is="content-section" icon="fas fa-glasses" title="Roles" class="col-12 col-md-6 col-lg-4 col-xl-2">
            <a class="btn btn-sm btn-primary" href="/admin/users/roles/new">New Role</a>
            <div class="table-responsive">
                <table class="table table-sm table-hover" id="rolelist">
                    <thead>
                        <tr>
                            <th>Role</th>
                            <th class="text-right">Permissions</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in roleList">
                            <td>
                                <hyper-link v-bind:title="item.Name" v-bind:link="'/admin/users/roles/' + item.RoleId" v-bind:new-window="false"></hyper-link>
                                <span v-show="item.IsLocked">&nbsp;<i class="fas fa-fw fa-lock text-danger"></i></span></td>
                            <td class="text-right">{{item.PermissionList.length}}</td>
                        </tr>
                    </tbody>
                    <tfoot></tfoot>
                </table>
            </div>
        </section>
    </div>
</asp:Content>
