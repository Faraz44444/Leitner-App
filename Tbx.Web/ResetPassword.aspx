<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="TbxPortal.Web.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Tbx</title>
    <link rel="shortcut icon" href="/Media/Branding/favicon.ico" />
    <link href="/Content/site.min.css?v=1.1.1" rel="stylesheet" />
    <%: Scripts.Render("~/jsResetPassword") %>
</head>
<body>
    <div class="container">
        <header class="d-none d-sm-block mt-3 mb-3">
            <div class="d-flex justify-content-end">
                <a href="mailto:support@wise.no" class="ml-4"><i class="fas fa-envelope"></i>&nbsp;Support@wise.no</a>
                <a href="tel:+4741852000" class="ml-4"><i class="fas fa-phone"></i>&nbsp;+47 418 52 000 (mon - fri 08:00 - 16:00)</a>
            </div>
        </header>
        <section id="loginBannerContainer" class="d-flex justify-content-center mb-3">
            <img class="d-none d-sm-block" src="/Media/Login/login_img_large.png" />
            <img class="d-block d-sm-none" src="Media/Login/tbx_logo.png" />
        </section>
        <section>
            <form id="resetPasswordForm" runat="server">
                <div class="modal fade" id="resetPasswordModal" data-backdrop="static" data-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="staticBackdropLabel">Reset Password</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <p>Please fill in your new password and confirm it. <br /> Once password is reset you will be redirected to login page.</p>
                                <div class="row">
                                    <div class="col-12 col-sm-6 mt-2 mt-md-0">
                                        <div class="custom-input-group">
                                            <label>New Password</label>
                                            <input id="newPassword" runat="server" tabindex="1" class="form-control form-control-sm" type="password" required="required" />
                                        </div>
                                    </div>
                                    <div class="col-12 col-sm-6 mt-2 mt-md-0">
                                        <div class="custom-input-group">
                                            <label>Confirm Password</label>
                                            <input id="confirmedPassword" runat="server" tabindex="2" class="form-control form-control-sm" type="password" required="required" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 mt-2 text-center">
                                        <p id="returnMessage" runat="server" class="text-danger m-0"></p>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="col-12 d-flex flex-column flex-sm-row justify-content-end">
                                    <div>
                                        <asp:Button runat="server" ID="BtnResetPassword"  Text="Reset Password" CssClass="btn btn-primary btn-block" OnClick="BtnResetPassword_Click" TabIndex="3" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </section>
        <section class="mt-4 mb-3">
            <div class="d-flex justify-content-center">
                <h3 class="mb-3">Wise Consulting AS</h3>
            </div>
            <div class="d-flex flex-column flex-sm-row justify-content-center">
                <a class="text-center mb-2 mb-sm-0 mr-sm-2" href="https://wise.no"><i class="fas fa-lg fa-globe"></i>&nbsp;www.wise.no</a>
                <a class="text-center mb-2 mb-sm-0 ml-sm-2 mr-sm-2" href="tel:+4741852000"><i class="fas fa-lg fa-phone"></i>&nbsp;+47 418 52 000</a>
                <a class="text-center mb-2 mb-sm-0 ml-sm-2 mr-sm-2" href="mailto:support@wise.no?mybooking"><i class="fas fa-lg fa-envelope"></i>&nbsp;support@wise.no</a>
                <a class="text-center ml-sm-2" href="https://wise.no/Privacy/Policy"><i class="fas fa-lg fa-user-secret"></i>&nbsp;Privacy statement</a>
            </div>
        </section>
    </div>
</body>
<script type="text/javascript">    
    $(document).ready(function () {
        $("#resetPasswordModal").modal("show");
    });
</script>
</html>
