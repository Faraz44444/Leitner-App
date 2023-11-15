const path = require('path');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

const { VueLoaderPlugin } = require('vue-loader/dist/index');

module.exports = {
    resolve: {
        alias: {
            //vue: 'vue/dist/vue.esm-bundler.js',

            //js: path.resolve(__dirname, "App/js/"),
            //components: path.resolve(__dirname, "App/js/components/"),
            //directives: path.resolve(__dirname, "App/js/directives/"),
            //handlers: path.resolve(__dirname, "App/js/handlers/"),
            //services: path.resolve(__dirname, "App/js/services/"),
        },
    },
    entry: {
        master: './Sources/js/master.js',


        //DASHBOARD
        dashboard: './Pages/index.js'

        // DEVICE
        //device: './Pages/Device/device.js',

    },
    output: {
        filename: '[name].bundle.js',
        path: path.resolve(__dirname, 'wwwroot/dist'),
    },
    plugins: [
        new VueLoaderPlugin({
            VUE_OPTIONS_API: true,
            VUE_PROD_DEVTOOLS: false,
        }),
        new MiniCssExtractPlugin()
    ],
    module: {
        rules: [
            {
                test: /\.vue$/,
                use: ['vue-loader']
            },
            {
                test: /\.(scss|sass|less|css)$/,
                use: [
                    { loader: MiniCssExtractPlugin.loader },
                    //{ loader: 'style-loader' },
                    { loader: 'css-loader' },
                    {
                        loader: 'postcss-loader',
                        options: {
                            postcssOptions: {
                                plugins: [
                                    require('precss'),
                                    require('autoprefixer')
                                ]
                            }
                        }
                    },
                    { loader: 'sass-loader' }
                ]
            },
            {
                test: /\.(woff2|woff|eot|ttf|svg)$/,
                loader: 'file-loader',
                options: {
                    name: '[name].[ext]',
                    outputPath: '../webfonts',
                },
            },
        ]
    },
};