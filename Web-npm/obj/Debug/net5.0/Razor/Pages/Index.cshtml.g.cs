#pragma checksum "E:\Applications\new-budgeting-app\Web-npm\Pages\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f88965e7a2ccc08a7daadbac3758ae9e19cd5c3d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Web0.Pages.Pages_Index), @"mvc.1.0.razor-page", @"/Pages/Index.cshtml")]
namespace Web0.Pages
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f88965e7a2ccc08a7daadbac3758ae9e19cd5c3d", @"/Pages/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4f43449c610f6f012e7a6c7f832a39ac939c28c0", @"/Pages/_ViewImports.cshtml")]
    #nullable restore
    public class Pages_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
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
#line 3 "E:\Applications\new-budgeting-app\Web-npm\Pages\Index.cshtml"
  
    ViewData["Title"] = "Home page";

#line default
#line hidden
#nullable disable
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("environment", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f88965e7a2ccc08a7daadbac3758ae9e19cd5c3d3725", async() => {
                    WriteLiteral("\r\n        <script src=\"/js/index.js\"></script>\r\n    ");
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("environment", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f88965e7a2ccc08a7daadbac3758ae9e19cd5c3d5031", async() => {
                    WriteLiteral("\r\n        <script src=\"/js/index.min.js\"></script>\r\n    ");
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
            WriteLiteral(@"    <div class=""grid"" id=""app"">
        <c-section return-icon :title=""'Real Data'"">
            <template v-slot:content>
                <div class=""grid grid-flow-col gap-4"">
                    <c-section class=""ml-2"" return-icon :title=""'Expenditures'"" small>
                        <template v-slot:content>
                            <div class=""text-gray-300 grid grid-rows-2"">
                                <c-input :input-label=""'This Month: '"">
                                    <template v-slot:input>
                                        <input class=""text-center"" type=""text"" v-model=""sums.Expenditures.ThisMonthFormatted"" disabled />
                                    </template>
                                </c-input>
                                <c-input :input-label=""'Last Month: '"">
                                    <template v-slot:input>
                                        <input  type=""text"" v-model=""sums.Expenditures.LastMonthFormatted"" disabled />
         ");
            WriteLiteral(@"                           </template>
                                </c-input>
                            </div>
                        </template>
                    </c-section>
                    <c-section return-icon :title=""'Incomes'"" small>
                        <template v-slot:content>
                            <div class=""grid grid-rows-2 text-gray-300"">
                                <c-input :input-label=""'This Month: '"">
                                    <template v-slot:input>
                                        <input type=""text"" v-model=""sums.Incomes.ThisMonthFormatted"" disabled />
                                    </template>
                                </c-input>
                                <c-input :input-label=""'Last Month: '"">
                                    <template v-slot:input>
                                        <input type=""text"" v-model=""sums.Incomes.LastMonthFormatted"" disabled />
                                    </template>
");
            WriteLiteral(@"                                </c-input>
                            </div>
                        </template>
                    </c-section>
                    <c-section extra-classes=""mr-2"" return-icon :title=""'Savings'"" small>
                        <template v-slot:content>
                            <div class=""grid grid-rows-2 text-gray-300"">
                                <c-input :input-label=""'This Month: '"">
                                    <template v-slot:input>
                                        <input type=""text"" v-model=""sums.Savings.ThisMonthFormatted"" disabled />
                                    </template>
                                </c-input>
                                <c-input :input-label=""'Last Month: '"">
                                    <template v-slot:input>
                                        <input type=""text"" v-model=""sums.Savings.LastMonthFormatted"" disabled />
                                    </template>
                   ");
            WriteLiteral(@"             </c-input>
                            </div>
                        </template>
                    </c-section>
                </div>

            </template>
        </c-section>
        <c-section return-icon :title=""'Expected Data'"">
            <template v-slot:content>
                <c-input class=""mt-2 mb-2  text-gray-300"" :input-label=""'Monthly Expected Expenditures Without Savigs: '"">
                    <template v-slot:input>
                        <input type=""text"" v-model=""monthlyExpectedExpenditures"" disabled />
                    </template>
                </c-input>
                <div class=""grid grid-cols-3 grid-row text-gray-300"">
                    <template v-for=""category in categories"">
                        <c-input :input-labeL=""category.Name +': '"">
                            <template v-slot:input>
                                <input type=""text"" v-model=""category.MonthlyLimitFormatted"" disabled />
                            </templa");
            WriteLiteral(@"te>
                        </c-input>
                    </template>
                </div>
            </template>
        </c-section>
        <c-section return-icon :title=""'Statistics'"">
            <template v-slot:content>
                <div style=""min-height: 40em; max-height: 40em; height: 40em;"" >
                    <div class=""flex justify-center"" >
                        <div style=""position: relative; height: 50%; width: 50%;"">
                            <canvas id=""overviewChart"" width=""10"" height=""8""></canvas>
                        </div>
                    </div>
                </div>
            </template>
        </c-section>
    </div>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IndexModel> Html { get; private set; } = default!;
        #nullable disable
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel>)PageContext?.ViewData;
        public IndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591