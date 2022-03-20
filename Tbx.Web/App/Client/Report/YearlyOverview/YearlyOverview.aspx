<%@ Page Title="Payment List" Language="C#" MasterPageFile="~/TbxPortal.Master" AutoEventWireup="true" CodeBehind="YearlyOverview.aspx.cs" Inherits="TbxPortal.Web.App.Client.Report.YearlyOverview.YearlyOverview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/jsYearlyOverview") %>
    <%: Scripts.Render("~/jsChartJs310") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadCrumb" runat="server">
    <li>Yearly Overview</li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="server">
    <div id="app" class="row justify-content-center">
        <section is="content-section" icon="fas fa-list" title="Overal Incomes and Expenditures" class="col-12 col-xl-11">
            <div class="row overflow-auto" style="min-height: 55em; max-height: 35em; height: 55em;">
                <div class="col-12 d-flex justify-content-center">
                    <div style="position: relative; height: 80%; width: 80%;">
                        <canvas id="overalChart" width="100" height="100"></canvas>
                    </div>
                </div>
            </div>
        </section>
        <section is="content-section" icon="fas fa-list" title="Yearly Based Incomes and Expenditures" class="col-12 col-xl-11">
        </section>
</asp:Content>
