<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/TbxPortal.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="TbxPortal.Web.App.Account.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/jsChangePassword") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadCrumb" runat="server">
    <li><a href="<%=Page.ResolveUrl("~/account") %>">Account</a></li>
    <li><a href="<%=Page.ResolveUrl("~/account/changepassword") %>">Change Password</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="server">
    <div id="app" class="row justify-content-center">
        <section id="detailsSection" is="content-section" icon="fas fa-user-lock" title="Change Password" class="col-12 col-md-8 col-lg-6 col-xl-5">
            <div class="row">
                <div class="col-12 col-md-4">
                    <div is="custom-input-group" input-label="Old Password">
                        <input id="oldPassword" class="form-control form-control-sm" type="password" v-model="oldPassword" required />
                    </div>
                </div>
                <div class="col-12 col-md-4 mt-2 mt-md-0">
                    <div is="custom-input-group" input-label="New Password">
                        <input id="newPassword" class="form-control form-control-sm" type="password" v-model="newPassword" required />
                    </div>
                </div>
                <div class="col-12 col-md-4 mt-2 mt-md-0">                    
                    <div is="custom-input-group" input-label="Confirm Password">
                        <input id="confirmedPassword" class="form-control form-control-sm" type="password" v-model="confirmedPassword" required />
                    </div>
                </div>
            </div>
            <div class="section-footer">                
                <button class="btn btn-sm btn-primary" type="button" v-on:click="save">Save</button>
            </div>
        </section>
    </div>
</asp:Content>
