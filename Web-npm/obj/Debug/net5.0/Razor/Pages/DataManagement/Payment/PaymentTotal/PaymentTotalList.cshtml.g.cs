#pragma checksum "E:\Applications\new-budgeting-app\Web-npm\Pages\DataManagement\Payment\PaymentTotal\PaymentTotalList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a9e2fcc452afd972ef008f9be59ebff5e4596ecd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Web0.Pages.DataManagement.Payment.PaymentTotal.Pages_DataManagement_Payment_PaymentTotal_PaymentTotalList), @"mvc.1.0.razor-page", @"/Pages/DataManagement/Payment/PaymentTotal/PaymentTotalList.cshtml")]
namespace Web0.Pages.DataManagement.Payment.PaymentTotal
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\Applications\new-budgeting-app\Web-npm\Pages\_ViewImports.cshtml"
using Web0;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a9e2fcc452afd972ef008f9be59ebff5e4596ecd", @"/Pages/DataManagement/Payment/PaymentTotal/PaymentTotalList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4f43449c610f6f012e7a6c7f832a39ac939c28c0", @"/Pages/_ViewImports.cshtml")]
    #nullable restore
    public class Pages_DataManagement_Payment_PaymentTotal_PaymentTotalList : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("include", "Development", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("exclude", "Development", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.EnvironmentTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "E:\Applications\new-budgeting-app\Web-npm\Pages\DataManagement\Payment\PaymentTotal\PaymentTotalList.cshtml"
  
    ViewData["Title"] = "Payment Total List";

#line default
#line hidden
#nullable disable
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("environment", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a9e2fcc452afd972ef008f9be59ebff5e4596ecd4088", async() => {
                    WriteLiteral("\r\n        <script src=\"/js/paymentTotallist.js\"></script>\r\n    ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.EnvironmentTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper.Include = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("environment", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a9e2fcc452afd972ef008f9be59ebff5e4596ecd5405", async() => {
                    WriteLiteral("\r\n        <script src=\"/js/paymentTotallist.min.js\"></script>\r\n    ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.EnvironmentTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper.Exclude = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
            WriteLiteral(@"    <div class=""row justify-content-center"" id=""app"">
        <c-section title=""Payment Total List"">
            <template v-slot:content>
                <div>
                    <c-button v-bind:extra-classes=""'mt-3 transition bg-background-3 hover:bg-background-2 duration-500'""
                          v-bind:title=""'Create New'"" v-on:click=""showDetailsModal = true""></c-button>
                    <div class=""col"">
                        <table class=""table table-dark table-hover"">
                            <thead>
                                <tr>
                                    <th>Title</th>
                                    <th>Business</th>
                                    <th>Is Deposit</th>
                                    <th colspan=""2"">Price</th>
                                    <th colspan=""2"">Date</th>
                                </tr>
                                <tr>
                                    <th>
                                      ");
            WriteLiteral(@"  <c-input>
                                            <template v-slot:input>
                                                <input class=""bg-blue-900 border-2 rounded-full"" type=""text"" v-model=""filter.Title"" />
                                            </template>
                                        </c-input>
                                    </th>
                                    <th>
                                        <c-select :options=""businesses"" :track-by=""'BusinessId'"" v-model=""filter.BusinessId""></c-select>
                                    </th>
                                    <th>
                                        <c-select :options=""[{Id:null, Name:'All'},{Id:false, Name:'No'}, {Id:true, Name:'Yes'}]"" v-model=""filter.IsDeposit""></c-select>
                                    </th>
                                    <th>
                                        <c-input>
                                            <template v-slot:input>
              ");
            WriteLiteral(@"                                  <input class=""bg-blue-900 border-2 rounded-full""
                                                   type=""number"" v-model=""filter.PriceFrom"" />
                                            </template>
                                        </c-input>
                                    </th>
                                    <th>
                                        <c-input>
                                            <template v-slot:input>
                                                <input class=""bg-blue-900 border-2 rounded-full""
                                                   type=""number"" v-model=""filter.PriceTo"" />
                                            </template>
                                        </c-input>
                                    </th>
                                    <th>
                                        <c-datepicker v-model=""filter.DateFrom""></c-datepicker>
                                    </th>
    ");
            WriteLiteral(@"                                <th>
                                        <c-datepicker v-model=""filter.DateTo""></c-datepicker>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <template v-for=""(item, index) in items"">
                                    <tr v-on:click=""openPaymentDetailsModal(item)"" :class=""{'bg-customPurple-10':index%2 ==0}"">
                                        <td>{{item.Title}}</td>
                                        <td>{{item.BusinessName}}</td>
                                        <td>{{item.IsDeposit}}</td>
                                        <td class=""text-center"" colspan=""2"">{{item.PriceFormatted}}</td>
                                        <td class=""text-center"" colspan=""2"">{{item.Date}}</td>
                                    </tr>
                                </template>
                            </tbod");
            WriteLiteral(@"y>
                        </table>

                    </div>
                </div>
            </template>
        </c-section>
        <c-modal v-on:save=""savePayment"" v-on:keyup.esc=""closeModal()"" v-on:close=""closeModal()"" v-show=""showDetailsModal"">
            <template v-slot:header>
                <h4>{{PaymentTotalDetails.PaymentTotalId > 0 ? PaymentTotalDetails.Title : 'New Payment Total'}}</h4>
                <button type=""button"" class=""btn-close"" data-bs-dismiss=""modal"" aria-label=""Close""></button>
            </template>
            <template v-slot:body>
                <div class=""flex g-2"">
                    <c-input class=""mt-2"" input-label=""Title"">
                        <template v-slot:input>
                            <input class=""bg-blue-900 border-2 rounded-full"" type=""text"" v-model=""PaymentTotalDetails.Title"" />
                        </template>
                    </c-input>
                    <c-input class=""mt-2"" input-label=""Price: "">
               ");
            WriteLiteral(@"         <template v-slot:input>
                            <input type=""number"" class=""bg-blue-900 border-2 rounded-full"" v-model=""PaymentTotalDetails.Price"" required />
                        </template>
                    </c-input>
                </div>
                <div class=""flex justify-between mt-2"">
                    <c-checkbox v-model=""PaymentTotalDetails.IsDeposit"" label=""Is Deposit""></c-checkbox>
                    <c-dropdown v-bind:dorpdown-items=""searchResultBusinesses.length"" v-bind:show-items=""showItems"">
                        <template v-slot:body>
                            <c-input input-label=""Business"">
                                <template v-slot:input>
                                    <input class=""bg-blue-900 border-2 rounded-full"" id=""searchBusinesses""
                                       v-model=""PaymentTotalDetails.BusinessName"" v-on:keydown.enter.prevent=""scanBusiness()"" autofocus />
                                </template>
                ");
            WriteLiteral(@"            </c-input>
                        </template>
                        <template v-slot:items>
                            <div v-show=""searchResultBusinesses.length < 1 && !loadingAvailableBusinesses""><span>No results.</span></div>
                            <div v-show=""loadingAvailableBusinesses""><span>Searching...</span></div>
                            <div :class=""{'bg-customPurple-10':index%2 ==0,'bg-background-2':index%2 ==1, 'hover:text-border-1 cursor-pointer w-full': true}"" v-for=""(item, index) in searchResultBusinesses"" v-bind:key=""item.Id"" v-on:click=""selectBusiness(item)"">
                                <span>{{item.Name}}</span>
                            </div>
                        </template>
                    </c-dropdown>
                </div>
                <div class=""flex justify-between mt-2"">
                    <c-datepicker label=""Date:"" v-model=""PaymentTotalDetails.Date"" required />
                </div>
            </template>
        </c-moda");
            WriteLiteral("l>\r\n    </div>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Web.Pages.DataManagement.Payment.PaymentTotal.PaymentTotalListModel> Html { get; private set; } = default!;
        #nullable disable
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Web.Pages.DataManagement.Payment.PaymentTotal.PaymentTotalListModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Web.Pages.DataManagement.Payment.PaymentTotal.PaymentTotalListModel>)PageContext?.ViewData;
        public Web.Pages.DataManagement.Payment.PaymentTotal.PaymentTotalListModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
