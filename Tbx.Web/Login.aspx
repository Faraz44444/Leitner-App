<%@ Page Title="Data Management" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TbxPortal.Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>TagIt</title>
    <link rel="shortcut icon" href="/Media/Branding/payment-icon.jpg" />
    <link href="/Content/site.min.css?v=1.0" rel="stylesheet" />
    <%: Scripts.Render("~/jsLogin") %>
</head>
<body>
    <div class="container">
        <header class="d-none d-sm-block mt-3 mb-3">
        </header>
        <section id="loginBannerContainer" class="d-flex justify-content-center mb-3">
            <img class="d-none d-sm-block" src="/Media/Login/login_img_large.png" />
        </section>
        <section>
            <form id="loginForm" runat="server">
                <div class="d-flex flex-column flex-sm-row justify-content-end">
                    <div class="flex-md-grow-1 d-none d-sm-block">
                        <%--<img alt="Logo" width="150" src="" />--%>
                    </div>
                    <div class="mb-2 mb-sm-0 mr-sm-2">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text bg-white text-primary"><i class="fas fa-user"></i></span>
                            </div>
                            <input runat="server" id="txtUsername" type="text" class="form-control" placeholder="Username" aria-label="Username" />
                        </div>
                    </div>
                    <div class="mb-2 mb-sm-0 mr-sm-2">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text bg-white text-primary"><i class="fas fa-key"></i></span>
                            </div>
                            <input runat="server" id="txtPassword" type="password" class="form-control" placeholder="Password" aria-label="Username" />
                        </div>
                        <div class="text-right">
                            <p runat="server" id="returnMessage" class="text-danger font-weight-bold m-0"></p>
                        </div>
                        <div class="text-right">
                            <button id="btnForgottenPassword" class="btn btn-link pl-0 pr-0" type="button">Forgotten password?</button>
                        </div>
                    </div>
                    <div>
                        <asp:Button runat="server" ID="BtnLogin" Text="Sign In" CssClass="btn btn-primary btn-block" OnClick="BtnLogin_Click" />
                    </div>
                </div>
                <div id="resetPasswordModal" class="modal" tabindex="-1" role="dialog">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Reset password</h5>
                                <button type="button" class="close bg-transparent" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <p>Please fill in your username and we will send further information to the e-mail address registered to your account.</p>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text bg-white text-primary"><i class="fas fa-user"></i></span>
                                    </div>
                                    <input runat="server" id="resetPasswordUsername" type="text" class="form-control" placeholder="Username" aria-label="Username" />
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                <asp:Button runat="server" ID="BtnResetPassword" Text="Send" CssClass="btn btn-primary" OnClick="BtnResetPassword_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </section>
      
        <section class="mt-5">
            <div class="d-flex justify-content-center">
                <h3 class="mb-3">Faraz Safarpour</h3>
            </div>
            <div class="d-flex flex-column flex-sm-row justify-content-center">
                <a class="text-center mb-2 mb-sm-0 ml-sm-2 mr-sm-2" href="tel:+47000000"><i class="fas fa-lg fa-phone"></i>&nbsp;+47000000</a>
            </div>
        </section>
    </div>
</body>
</html>
