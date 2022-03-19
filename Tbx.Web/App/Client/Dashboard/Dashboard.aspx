<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/TbxPortal.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="TbxPortal.Web.App.Client.Dashboard.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/jsDashboard") %>
    <%: Scripts.Render("~/jsChartJs310") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadCrumb" runat="server">
    <li>Articles</li>
    <li><a href="<%=Page.ResolveUrl("~/dashboard") %>">Dashboard</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="server">
    <div id="app" class="row justify-content-center">
        <section is="content-section" icon="fas fa-tachometer-alt" title="Dashboard" class="col-12 col-md-12 col-lg-12 col-xl-12" um-anchor="#HowToArticles">
            <section is="content-section" title="Real Data" class="col-12 mt-4" small>
                <div class="row">
                    <section is="content-section" title="Expenditure" class="col-6 mt-4" small>
                        <div class="row mt-n3">
                            <div class="col-6 mt-2">
                                <div is="custom-input-group" input-label="This Month">
                                    <input class="form-control form-control-sm text-left" type="number" v-model="payment.thisYearExpenditures[thisMonth]" disabled />
                                </div>
                            </div>
                            <div class="col-6 mt-2">
                                <div is="custom-input-group" input-label="Last Month">
                                    <input class="form-control form-control-sm text-left" type="number" v-model="payment.thisYearExpenditures[lastMonth]" disabled />
                                </div>
                            </div>
                        </div>
                    </section>
                    <section is="content-section" title="Income" class="col-6 mt-4" small>
                        <div class="row mt-n3">
                            <div class="col-6 mt-2">
                                <div is="custom-input-group" input-label="This Month">
                                    <input class="form-control form-control-sm text-left" type="number" v-model="payment.thisYearIncomes[thisMonth]" disabled />
                                </div>
                            </div>
                            <div class="col-6 mt-2">
                                <div is="custom-input-group" input-label="Last Month">
                                    <input class="form-control form-control-sm text-left" type="number" v-model="payment.thisYearIncomes[lastMonth]" disabled />
                                </div>
                            </div>
                        </div>
                    </section>
                </div>
                <div class="row">
                    <section is="content-section" title="Saving" class="col-6 mt-4" small>
                        <div class="row mt-n3">
                            <div class="col-6 mt-2">
                                <div is="custom-input-group" input-label="This Month">
                                    <input class="form-control form-control-sm text-left" type="number" v-model="payment.thisMonthSavings" disabled />
                                </div>
                            </div>
                            <div class="col-6 mt-2">
                                <div is="custom-input-group" input-label="Last Month">
                                    <input class="form-control form-control-sm text-left" type="number" v-model="payment.lastMonthSavings" disabled />
                                </div>
                            </div>
                        </div>
                    </section>
                </div>
            </section>
            <section is="content-section" title="Expected Data" class="col-12 mt-4" small>
            </section>
            <section is="content-section" icon="fas fa-chart-bar" title="Statistics" class="col-12">
                <div class="row overflow-auto" style="min-height: 35em; max-height: 35em; height: 35em;">
                    <div class="col-12 d-flex justify-content-center">
                        <div style="position: relative; height: 50%; width: 50%;">
                            <canvas id="overviewChart" width="50" height="50"></canvas>
                        </div>
                    </div>
                </div>
                <div class="row overflow-auto" style="min-height: 35em; max-height: 35em; height: 35em;">
                    <div class="col-4 d-flex justify-content-center">
                        <div style="position: relative; height: 50%; width: 50%;">
                            <canvas id="groceriesPie" width="50" height="50"></canvas>
                        </div>
                    </div>
                    <div class="col-4 d-flex justify-content-center">
                        <div style="position: relative; height: 50%; width: 50%;">
                            <canvas id="eatingOutPie" width="50" height="50"></canvas>
                        </div>
                    </div>
                    <div class="col-4 d-flex justify-content-center">
                        <div style="position: relative; height: 50%; width: 50%;">
                            <canvas id="electricityPie" width="50" height="50"></canvas>
                        </div>
                    </div>
                </div>
            </section>
        </section>
        <%-- <section is="content-section" icon="fas fa-chart-pie" title="Equipment Statistics" class="col-12 col-md-5">
        </section>--%>
    </div>
</asp:Content>
