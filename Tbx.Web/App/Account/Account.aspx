<%@ Page Title="My Account" Language="C#" MasterPageFile="~/TbxPortal.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="TbxPortal.Web.App.Account.Account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/jsAccount") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadCrumb" runat="server">
    <li><a href="<%=Page.ResolveUrl("~/account") %>">My Account</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="server">
    <div id="app" class="row justify-content-center">
        <section id="detailsSection" is="content-section" icon="fas fa-user" title="Account" class="col-12 col-xl-8">
            <div class="row">
                <div class="col-12 col-md-6 col-xl-6">
                    <div class="row">
                        <div class="col-12 col-md-6">
                            <div is="custom-input-group" input-label="Username">
                                <input class="form-control form-control-sm" type="text" v-model="details.Username" disabled="disabled" />
                            </div>
                        </div>
                        <div class="col-12 col-md-6 mt-2 mt-md-0">
                            <div is="custom-input-group" input-label="E-Mail">
                                <input class="form-control form-control-sm" type="email" v-model="details.Email" required />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 col-md-6 mt-2">
                            <div is="custom-input-group" input-label="FirstName">
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
                        <div class="col-12 mt-2">
                            <div class="custom-input-group">
                                <label>Font Size</label>
                                <div class="custom-control-range">
                                    <label for="rngFontSize" class="font-weight-bold">{{details.CssFontSize}}</label>
                                    <input class="form-control form-control-sm" type="range" id="rngFontSize" min="10" max="15" step=".1" v-model="details.CssFontSize" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <section is="content-section" title="Site Roles" class="col-12 col-md-6 col-xl-6 mt-3 mt-md-0" small v-if="details.UserType == 1">
                    <div class="table-responsive mt-0">
                        <table class="table table-sm">
                            <thead>
                                <tr>
                                    <th>Site</th>
                                    <th>Role</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="item in details.UserSiteAccessList">
                                    <td>{{item.SiteName}}<span class="text-gray-5" v-show="item.SiteId == currentSiteId">&nbsp;(current site)</span></td>
                                    <td>{{item.RoleName}}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </section>
                <section is="content-section" title="Supplier access list" class="col-12 col-md-6 col-xl-6 mt-3 mt-md-0" small v-if="details.UserType == 2">
                    <div class="table-responsive mt-0">
                        <table class="table table-sm">
                            <thead>
                                <tr>
                                    <th>Supplier</th>   
                                    <th>Role</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="item in details.UserSupplierAccessList">
                                    <td>{{item.SupplierName}}<span class="text-gray-5" v-show="item.SupplierId == currentSupplierId">&nbsp;(current supplier)</span></td>
                                    <td>{{item.RoleName}}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </section>
            </div>
            <div class="section-footer">
                <a href="<%=Page.ResolveUrl("~/account/changepassword") %>" class="btn btn-sm btn-warning">Change Password</a>
                <a href="javascript:history.back()" class="btn btn-sm btn-link">Return</a>
                <button class="btn btn-sm btn-primary" type="button" v-on:click="save">Save</button>
            </div>
        </section>
    </div>
</asp:Content>
