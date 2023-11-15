﻿'use strict';

var path = require('path');

module.exports = {
    watch: true,
    devtool: 'source-map',
    mode: 'development',
    resolve: {
        alias: {

            components: path.resolve(__dirname, "App/js/components/"),
            directives: path.resolve(__dirname, "App/js/directives/"),
            handlers: path.resolve(__dirname, "App/js/handlers/"),
            services: path.resolve(__dirname, "App/js/services/")
        }
    },
    entry: {
        site: './App/js/site.js',
        master: './App/js/master.js',

        login: './Pages/Account/Login/login.js',
        dashboard: './Pages/Dashboard/dashboard.js',

        // SETTINGS
        settingsDashboard: './Pages/Settings/Dashboard/settingsDashboard.js',
        settingsSoftware: './Pages/Settings/Software/settingsSoftware.js',
        settingsCustomer: './Pages/Settings/Customer/settingsCustomer.js',
        settingsRole: './Pages/Settings/Role/settingsRole.js',
        settingsUser: './Pages/Settings/User/settingsUser.js',

        // SMS
        smsDashboard: './Pages/SMS/Dashboard/smsDashboard.js',
        smsCustomerLink: './Pages/SMS/CustomerLink/smsCustomerLink.js',
        smsReportSummary: './Pages/SMS/Report/Summary/smsReportSummary.js',
        smsUsers: './Pages/SMS/Users/smsUsers.js',
        smsSecurity: './Pages/SMS/Security/smsSecurity.js',
        smsThresholdLimit: './Pages/SMS/ThresholdLimit/smsThresholdLimit.js',

        // System Message
        systemMessageDashboard: './Pages/SystemMessage/Dashboard/systemMessageDashboard.js',
        systemMessageMessages: './Pages/SystemMessage/Messages/systemMessageMessages.js'
    },
    output: {
        filename: '[name].bundle.js',
        path: path.resolve(__dirname, 'App/dist')
    },
    module: {
        rules: [{
            test: /\.(scss|sass|less|css)$/,
            use: [{ loader: 'style-loader' }, { loader: 'css-loader' }, {
                loader: 'postcss-loader',
                options: {
                    postcssOptions: {
                        plugins: [require('precss'), require('autoprefixer')]
                    }
                }
            }, { loader: 'sass-loader' }]
        }, {
            test: /\.woff(2)?(\?v=[0-9]\.[0-9]\.[0-9])?$/,
            use: {
                loader: 'file-loader',
                options: {
                    name: '[name].[ext]',
                    outputPath: './App/webfonts'
                }
            }
        }]
    }
};

