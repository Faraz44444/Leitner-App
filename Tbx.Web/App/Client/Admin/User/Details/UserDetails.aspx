<%@ Page Title="User Details" Language="C#" MasterPageFile="~/TbxPortal.Master" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="TbxPortal.Web.App.Client.Admin.User.Details.UserDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/jsUserDetails") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadCrumb" runat="server">
    <li>Admin</li>
    <li><a href="<%=Page.ResolveUrl("~/admin/users") %>">Users</a></li>
    <li><a href="<%=Page.ResolveUrl($"~/admin/users/{EntityIdString}") %>"><%=EntityName %></a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="server">
    <div id="app" class="row justify-content-center" data-entity-id="<%=EntityIdString %>">
        <section is="content-section" id="detailsSection" icon="fas fa-user" v-bind:title="title" class="col-12 col-md-7 col-lg-9 col-xl-7" um-anchor="#HowToUsersAndRoles">
            <div class="row">
                <div class="col-12 col-lg-7">
                    <div class="row">
                        <div class="col-12 col-md-6">
                            <div is="custom-input-group" input-label="Username">
                                <input class="form-control form-control-sm" type="text" v-model="details.Username" required v-bind:disabled="id > 0" />
                            </div>
                        </div>
                        <div class="col-12 col-md-6">
                            <div is="custom-input-group" input-label="E-Mail">
                                <input class="form-control form-control-sm" type="email" v-model="details.Email" required />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 col-md-6 mt-2">
                            <div is="custom-input-group" input-label="First Name">
                                <input class="form-control form-control-sm" type="text" v-model="details.FirstName" required />
                            </div>
                        </div>
                        <div class="col-12 col-md-6 mt-2">
                            <div is="custom-input-group" input-label="Last Name">
                                <input class="form-control form-control-sm" type="text" v-model="details.LastName" required />
                            </div>
                        </div>
                    </div>
                    <div class="row">                        
                        <div class="col-12 col-md-6 mt-2">                            
                            <div is="custom-input-group" input-label="Active">
                                <div class="custom-control custom-switch">
                                    <input id="chkActive" type="checkbox" class="custom-control-input" v-model="details.Active"/>
                                    <label class="custom-control-label" for="chkActive"><span v-show="details.Active">Yes</span><span v-show="!details.Active">No</span></label>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 col-md-6 mt-2">
                            <div is="custom-input-group" input-label="External ID" title="External ID is typically user ID in your ERP system">
                                <input class="form-control form-control-sm" type="text" v-model="details.ExternalId" />
                            </div>
                        </div>
                    </div>
                </div>
                <section is="content-section" title="User Site Roles" class="col-12 col-lg-5 mt-3 mt-lg-0" small>
                    <div class="table-responsive">
                        <table class="table table-sm table-hover">
                            <thead>
                                <tr>
                                    <th>Site</th>
                                    <th>Role</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="item in userRoles">
                                    <td>{{item.SiteName}}</td>
                                    <td>
                                        <select class="form-control form-control-sm" v-bind:class="{'text-danger': item.RoleId == 0}" v-model="item.RoleId">
                                            <option class="text-danger" value="0">No Access</option>
                                            <option class="text-body" v-for="role in roleList" v-bind:value="role.Id">{{role.Name}}</option>
                                        </select>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </section>
            </div>
            <div class="section-footer">
                <a href="javascript:history.back()" class="btn btn-sm btn-link">Return</a>
                <button class="btn btn-sm btn-primary" type="button" v-on:click="save">Save</button>
            </div>
        </section>
    </div>
</asp:Content>
