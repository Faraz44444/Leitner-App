<%@ Page Title="Role Details" Language="C#" MasterPageFile="~/TbxPortal.Master" AutoEventWireup="true" CodeBehind="RoleDetails.aspx.cs" Inherits="TbxPortal.Web.App.Client.Admin.User.Details.RoleDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/jsRoleDetails") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadCrumb" runat="server">
    <li>Admin</li>
    <li><a href="<%=Page.ResolveUrl("~/admin/users") %>">Users</a></li>
    <li>Roles</li>
    <li><a href="<%=Page.ResolveUrl($"~/admin/users/roles/{EntityIdString}") %>"><%=EntityName %></a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="server">
    <div id="app" class="row justify-content-center" data-entity-id="<%=EntityIdString %>">
        <section is="content-section" id="detailsSection" icon="fas fa-glasses" v-bind:title="title" class="col-12 col-lg-9 col-xl-6" um-anchor="#HowToUsersAndRoles">
            <div class="row">
                <div class="col-12 col-sm-6 col-md-4 col-lg-4 col-xl-3">
                    <div is="custom-input-group" input-label="Role Name">
                        <input class="form-control form-control-sm" type="text" v-model="details.Name" required />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-12 mt-4">
                <h3>Role Permissions</h3>
                    <div class="row">
                        <div v-for="permissions, PermissionGroup in groupAvailablePermissions" class="col-12 col-sm-6 col-md-4">
                            <h5><strong class="cursor-pointer" v-on:click="selectPermissions(permissions)">{{PermissionGroup}}</strong></h5>
                            <ul class="custom-ul">
                                <li v-for="item in permissions">
                                    <div class="custom-control custom-checkbox">
                                        <input type="checkbox" class="custom-control-input" v-bind:id="item.PermissionType" v-model="item.selected" v-bind:disabled="item.isLocked" />
                                        <label class="custom-control-label" v-bind:class="{'text-gray': item.isLocked, 'text-primary': !item.isLocked}" v-bind:for="item.PermissionType"><strong>{{item.PermissionName}}</strong></label>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="section-footer">
                <a href="javascript:history.back()" class="btn btn-sm btn-link">Return</a>
                <button class="btn btn-sm btn-primary" type="button" v-on:click="save">Save</button>
            </div>
        </section>
    </div>
</asp:Content>
