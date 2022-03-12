<%@ Page Title="Notification Settings" Language="C#" MasterPageFile="~/TbxPortal.Master" AutoEventWireup="true" CodeBehind="NotificationDetails.aspx.cs" Inherits="TbxPortal.Web.App.Account.Client.Settings.Notifications.NotificationDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/jsNotificationDetails") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="breadCrumb" runat="server">
    <li><a href="<%=Page.ResolveUrl("~/account") %>">My Account</a></li>
    <li><a href="<%=Page.ResolveUrl("~/account/settings/notifications") %>">Notification Settings</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="server">
    <div id="app" class="row justify-content-center">
        <section is="content-section" icon="fas fa-bell" title="Notification Settings" class="col-12 col-lg-9 col-xl-6" um-anchor="#HowToNotification">
            <p class="text-center mt-2 mb-3">
                Tbx may still send you important notifications about your account and content outside of your preferred notification settings.
                    Your notifications will also be based on your current site and permissions. This means that even if you turn a notification on you still may not get one.
            </p>
            <div class="row">
                <section v-for="notifications, NotificationGroup,index in groupAvailableNotifications" v-bind:key="index" is="content-section" v-bind:title="NotificationGroup" class="col-12 mb-3" small>

                    <div v-for="(item, index) in notifications" class="col-12">
                        <div class="row mt-2 d-flex align-items-center" v-bind:class="{'border-bottom' : (index != notifications.length - 1) }">
                            <div class="col-12 col-md-4">
                                <i class="mr-2 pl-2" v-bind:class="item.Icon"></i>
                                <h5 class="d-inline-block">{{item.NotificationName}}</h5>
                            </div>
                            <div class="col-12 col-md-4">
                                <div v-if="item.IsInterval && (item.IsEmailSubscriber || item.IsSmsSubscriber)" class="d-flex align-items-center mb-1">
                                    <div class="d-flex align-items-center mr-2">
                                        <label class="mr-3 mb-0">Interval:</label>
                                        <select class="form-control form-control-sm" type="text" v-model="item.IntervalType" v-on:change="saveOrUpdate(item)">
                                            <%--<option v-for="item in warehouseList" v-bind:value="item.Id" v-bind:key="item.WarehouseId">{{item.Name}}</option>--%>
                                            <option value="1">Daily</option>
                                            <option value="2">Weekly</option>
                                            <option value="3">Monthly</option>
                                            <option value="4">Yearly</option>
                                        </select>
                                    </div>
                                    <div v-if="item.IntervalType == 2" class="d-flex align-items-center mr-3 text-nowrap">
                                        <label class="mr-2 mb-0">Day:</label>
                                        <select class="form-control form-control-sm" type="text" v-model="item.IntervalDayOfWeek" v-on:change="saveOrUpdate(item)">
                                            <option value="1">Monday</option>
                                            <option value="2">Tuesday</option>
                                            <option value="3">Wednesday</option>
                                            <option value="4">Thursday</option>
                                            <option value="5">Friday</option>
                                            <option value="6">Saturday</option>
                                            <option value="0">Sunday</option>
                                        </select>
                                    </div>
                                    <div v-if="item.IntervalType == 3" class="d-flex align-items-center mr-3 text-nowrap">
                                        <label class="mr-2 mb-0">Day in month:</label>
                                        <select class="form-control form-control-sm" type="text" v-model="item.IntervalDayOfMonth" v-on:change="saveOrUpdate(item)">
                                            <option v-for="item in daysInAMonth" v-bind:value="item" v-bind:key="item">{{item}}</option>
                                        </select>
                                    </div>
                                    <div v-if="item.IntervalType == 4" class="d-flex align-items-center mr-3 text-nowrap">
                                        <label class="mr-2 mb-0">Yearly date:</label>
                                        <input is="date-picker" placeholder="Notification date" class="w-50" v-model="item.IntervalDate" v-bind:date-only="false" format="DD.MM" v-on:change="saveOrUpdate(item)" />
                                    </div>
                                </div>


                            </div>
                            <%--<div class="col-12 col-md-2 user-select-none  mt-3 mt-md-0">
                                <div v-if="item.SupportsSms && item.IsSmsEnabled" class="d-flex align-content-center justify-content-end text-nowrap ">
                                    <span class="mr-2"><i class="fas fa-mobile-alt mr-2"></i>Sms:</span>
                                    <div class="custom-control custom-switch mr-2">
                                        <input v-bind:id="'chkSms-' +item.NotificationSubscriberType" type="checkbox" class="custom-control-input" v-model="item.IsSmsSubscriber" v-on:change="showSmsDisclaimer(item)" />
                                        <label class="custom-control-label" v-bind:for="'chkSms-' +item.NotificationSubscriberType"><span v-show="item.IsSmsSubscriber">On</span><span v-show="!item.IsSmsSubscriber">Off</span></label>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="col-12 col-md-2 user-select-none  mt-3 mt-md-0">
                                <div class="d-flex align-content-center justify-content-end text-nowrap">
                                    <span class="mr-2"><i class="far fa-envelope mr-2"></i>Email:</span>
                                    <div class="custom-control custom-switch mr-2">
                                        <input v-bind:id="'chkEmail-'+item.NotificationSubscriberType" type="checkbox" class="custom-control-input" v-model="item.IsEmailSubscriber" v-on:change="saveOrUpdate(item)" />
                                        <label class="custom-control-label" v-bind:for="'chkEmail-'+item.NotificationSubscriberType"><span v-show="item.IsEmailSubscriber">On</span><span v-show="!item.IsEmailSubscriber">Off</span></label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
            <div class="section-footer">
                <a href="javascript:history.back()" class="btn btn-sm btn-link">Return</a>
            </div>
        </section>

        <div id="modalSmsDisclaimer" class="modal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title"><i class="far fa-comment-alt mr-2"></i>SMS Disclaimer</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-12 text-center">
                                <span>Receiving SMS costs {{selectedNotification.SmsPrice | twoDecimals}} kr for each message received. By clicking confirm you agree to the cost for receiving notification for: {{selectedNotification.NotificationName}}.</span>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary btn-sm" data-dismiss="modal" v-on:click="selectedNotification.IsSmsSubscriber=false">No</button>
                        <button type="button" class="btn btn-danger btn-sm" v-on:click="saveOrUpdate(selectedNotification)">Yes</button>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
