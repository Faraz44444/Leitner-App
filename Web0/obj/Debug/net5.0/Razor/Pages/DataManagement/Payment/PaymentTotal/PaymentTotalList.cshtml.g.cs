#pragma checksum "D:\Programs\Budgeting App\New Budgeting App\new-budgeting-app\Web0\Pages\DataManagement\Payment\PaymentTotal\PaymentTotalList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "be72bd36e9c4c4e0dbf07f3b1223c68f4a1e21cd"
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
#line 1 "D:\Programs\Budgeting App\New Budgeting App\new-budgeting-app\Web0\Pages\_ViewImports.cshtml"
using Web0;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"be72bd36e9c4c4e0dbf07f3b1223c68f4a1e21cd", @"/Pages/DataManagement/Payment/PaymentTotal/PaymentTotalList.cshtml")]
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
#line 3 "D:\Programs\Budgeting App\New Budgeting App\new-budgeting-app\Web0\Pages\DataManagement\Payment\PaymentTotal\PaymentTotalList.cshtml"
  
    ViewData["Title"] = "Payment Total List";

#line default
#line hidden
#nullable disable
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("environment", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "be72bd36e9c4c4e0dbf07f3b1223c68f4a1e21cd4163", async() => {
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("environment", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "be72bd36e9c4c4e0dbf07f3b1223c68f4a1e21cd5480", async() => {
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
        <div class=""col"">
            <button type=""button"" class=""btn btn-primary mb-2"" v-on:click=""openPaymentDetailsModal()"">Create New</button>
            <div class=""col"">
                <table class=""table table-dark table-hover"">
                    <thead>
                        <tr>
                            <th>Title</th>
                            <th>Priority</th>
                            <th>Business</th>
                            <th>Is Deposit</th>
                            <th>Price</th>
                            <th>Date</th>
                        </tr>
                        <tr>
                            <th>
                                <input class=""form-control form-control-sm"" type=""text"" placeholder=""Name..."" v-model=""filter.Name"" />
                            </th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>");
            WriteLiteral(@"
                        <template v-for=""item in items"">
                            <tr v-on:click=""openPaymentDetailsModal(item)"">
                                <td>{{item.Title}}</td>
                                <td>{{item.PaymentPriorityName}}</td>
                                <td>{{item.BusinessName}}</td>
                                <td>{{item.IsDeposit}}</td>
                                <td>{{item.Price}}</td>
                                <td>{{item.Date}}</td>
                            </tr>
                        </template>
                    </tbody>
                </table>
            </div>
        </div>
        <div id=""PaymentTotalDetails"" class=""modal"" tabindex=""-1"">
            <div class=""modal-dialog"">
                <div class=""modal-content"">
                    <div class=""modal-header"">
                        <h5 class=""modal-title"">
                            <span v-if=""PaymentTotalDetails.PaymentId > 0"">{{paymentTotaldetails.Title}}</");
            WriteLiteral(@"span>
                            <span v-else>New Payment</span>
                        </h5>
                        <button type=""button"" class=""btn-close"" data-bs-dismiss=""modal"" aria-label=""Close""></button>
                    </div>
                    <div class=""modal-body"">
                        <div class=""row row-cols-1 g-2"">
                            <div class="" col-8 mt-2"">
                                <div is=""input-group"">
                                    <label>Title: </label>
                                    <input class=""form-control form-control-sm"" type=""text"" v-model=""PaymentTotalDetails.Title"" required />
                                </div>
                            </div>
                        </div>
                        <div class=""row row-cols-2 g-2 mt-2"">
                            <div class="" col-4 mt-2"">
                                <div class=""dropdown"">
                                    <div is=""input-group"">
                     ");
            WriteLiteral(@"                   <label>Business Name: </label>
                                        <input class=""form-control form-control-sm"" required type=""text"" autocomplete=""off"" data-toggle=""dropdown"" id=""searchBusinesses"" v-model=""PaymentTotalDetails.BusinessName"" v-on:keydown.enter.prevent=""scanBusiness()"" autofocus />
                                        <div class=""dropdown-menu"" ref=""searchBusinesses"">
                                            <div class=""dropdown-item"" v-show=""searchResultBusinesses.length < 1 && !loadingAvailableBusinesses""><span>No results.</span></div>
                                            <div class=""dropdown-item"" v-show=""loadingAvailableBusinesses""><span>Searching...</span></div>
                                            <div class=""dropdown-item cursor-pointer"" v-for=""item in searchResultBusinesses"" v-bind:key=""item.Id"" v-on:click=""selectBusinesss(item)""><span>{{item.BusinessName}}</span></div>
                                        </div>
                       ");
            WriteLiteral(@"             </div>
                                </div>
                            </div>
                            <div class=""col-4 mt-2"">
                                <div is=""input-group"">
                                    <label class=""container-test"">Is Deposit: </label>
                                    <div class=""custom-control custom-switch"">
                                        <input id=""chkDeposit"" type=""checkbox"" class=""checkbox"" v-model=""PaymentTotalDetails.IsDeposit"" />
                                        <label class=""container"" for=""chkDeposit""><span v-show=""PaymentTotalDetails.IsDeposit"">Yes</span><span v-show=""!PaymentTotalDetails.IsDeposit"">No</span></label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class=""row row-cols-3 g-3 mt-2"">
                            <div class=""col-4 mt-2"">
                                <div>");
            WriteLiteral(@"
                                    <label for=""example-datepicker"">Choose a date</label>
                                    <input class=""form-control form-control-sm"" type=""text"" id=""datepickere"" v-model=""PaymentTotalDetails.Date"" required />
                                </div>
                            </div>
                            <div class=""col-4 mt-2"">
                                <div is=""input-group"">
                                    <label>Price: </label>
                                    <input class=""form-control form-control-sm"" type=""number"" v-model=""PaymentTotalDetails.Price"" required />
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class=""modal-footer"">
                        <button type=""button"" class=""btn btn-secondary"" data-bs-dismiss=""modal"">Close</button>
                        <button type=""button"" class=""btn btn-primary"" v-on:click=""savePa");
            WriteLiteral("yment()\">Save changes</button>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>");
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
