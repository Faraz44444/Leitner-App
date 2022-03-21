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
        <section is="content-section" icon="fas fa-list" title="Overal Incomes and Expenditures" class="col-12 col-xl-5">
            <div class="row overflow-auto" style="min-height: 40em; max-height: 35em; height: 40em;">
                <div class="col-12 d-flex justify-content-center">
                    <div style="position: relative; height: 50%; width: 50%;">
                        <canvas id="overalChart" width="10" height="10"></canvas>
                    </div>
                </div>
            </div>
        </section>
        <section is="content-section" icon="fas fa-list" title="Yearly Based Incomes and Expenditures" class="col-12 col-xl-11">
            <div class="row">
                <div class="col-2  text-right">
                    <div is="custom-input-group" input-label="Select a year" class="text-left">
                        <select class="form-control form-control-sm" v-model="totalPayment.selectedYear">
                            <option v-for="item in totalPayment.years" v-bind:value="item">{{item}}</option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="row overflow-auto" style="min-height: 55em; max-height: 35em; height: 55em;">
                <div class="col-12 d-flex justify-content-center">
                    <div style="position: relative; height: 80%; width: 50%;">
                        <canvas id="yearlyChart" width="80" height="50"></canvas>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
