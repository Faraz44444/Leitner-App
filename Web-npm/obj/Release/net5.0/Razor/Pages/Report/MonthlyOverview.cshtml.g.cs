#pragma checksum "D:\Programs\Budgeting App\New Budgeting App\new-budgeting-app\Web-npm\Pages\Report\MonthlyOverview.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bad99da39658247e806db550cbf958d3a1d386d6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Web0.Pages.Report.Pages_Report_MonthlyOverview), @"mvc.1.0.razor-page", @"/Pages/Report/MonthlyOverview.cshtml")]
namespace Web0.Pages.Report
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
#line 1 "D:\Programs\Budgeting App\New Budgeting App\new-budgeting-app\Web-npm\Pages\_ViewImports.cshtml"
using Web0;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bad99da39658247e806db550cbf958d3a1d386d6", @"/Pages/Report/MonthlyOverview.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4f43449c610f6f012e7a6c7f832a39ac939c28c0", @"/Pages/_ViewImports.cshtml")]
    #nullable restore
    public class Pages_Report_MonthlyOverview : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
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
#line 3 "D:\Programs\Budgeting App\New Budgeting App\new-budgeting-app\Web-npm\Pages\Report\MonthlyOverview.cshtml"
  
    ViewData["Title"] = "Monthly Report";

#line default
#line hidden
#nullable disable
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("environment", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bad99da39658247e806db550cbf958d3a1d386d63930", async() => {
                    WriteLiteral("\r\n        <script src=\"/js/monthlyoverview.js\"></script>\r\n    ");
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("environment", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bad99da39658247e806db550cbf958d3a1d386d65246", async() => {
                    WriteLiteral("\r\n        <script src=\"/js/monthlyoverview.min.js\"></script>\r\n    ");
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
            WriteLiteral(@"    <div class=""row justify-content-left"" id=""app"">
        <c-section icon=""fas fa-list"" title=""Yearly"">
            <template v-slot:content>
                <div class=""flex"">
                    <c-input input-label=""Select A Year: "">
                        <template v-slot:input>
                            <c-select class=""text-left"" :options=""years"" v-model=""selectedYear""></c-select>
                        </template>
                    </c-input>
                    <c-input input-label=""Month : "">
                        <template v-slot:input>
                            <c-select class=""text-left"" :options=""months"" ");
            WriteLiteral(@" v-model=""selectedMonth""></c-select>
                        </template>
                    </c-input>
                </div>
                <div class=""row overflow-auto"" style=""min-height: 20em; max-height: 35em; height: 20em;"">
                    <div class=""col-12 d-flex justify-content-center"">
                        <div style=""position: relative; height: 100%; width: 100%;"">
                            <canvas id=""yearlyChart"" width=""10"" height=""10""></canvas>
                        </div>
                    </div>
                </div>
            </template>
        </c-section>
        <c-section icon=""fas fa-list"" title=""Numbers"">
            <template v-slot:content>
                <div class=""row mb-4 w-1/3"">
                    <c-input input-label=""Expenditures Sum: "">
                        <template v-slot:input>
                            <input class=""text-center"" type=""text"" v-model=""SelectedDateExpendituresSum"" disabled />
                        </template>
 ");
            WriteLiteral(@"                   </c-input>
                    <c-input input-label=""Incomes Sum: "">
                        <template v-slot:input>
                            <input class=""text-center"" type=""text"" v-model=""SelectedDateIncomeSum"" disabled />
                        </template>
                    </c-input>
                    <c-input input-label=""Saving Sum: "">
                        <template v-slot:input>
                            <input class=""text-center"" type=""text"" v-model=""SelectedDateSaving"" disabled />
                        </template>
                    </c-input>
                </div>
                <div class=""row"">
                    <template v-for=""item in selectedItems"">
                        <div class=""col-3 mt-2"">
                            <div is=""custom-input-group"">
                                <label>{{item.CategoryName}}</label>
                                <input class=""form-control form-control-sm text-left"" type=""text"" v-model=""item.Formatt");
            WriteLiteral(@"edPrice"" disabled />
                            </div>
                        </div>
                    </template>
                </div>
            </template>
        </c-section>


        <c-section icon=""fas fa-list"" title=""Details"">
            <template v-slot:content>
                <div class=""row mb-4 mt-2 w-1/3"">
                    <c-input input-label=""Total Expenditures: "">
                        <template v-slot:input>
                            <input class=""text-center"" type=""text"" v-model=""detailsExpendituresSum"" disabled />
                        </template>
                    </c-input>
                    <c-input input-label=""Total Income: "">
                        <template v-slot:input>
                            <input class=""text-center"" type=""text"" v-model=""detailsIncomesSum"" disabled />
                        </template>
                    </c-input>
                </div>
                <div class=""table-responsive mt-3"">
                    ");
            WriteLiteral(@"<c-table id=""datalist"" class=""table table-sm table-striped table-hover"">
                        <template v-slot:thead>
                            <tr>
                                <th is=""sortable-column""  v-bind:filter=""detailItemsFilter.filter"" v-on:order-by-changed=""orderBy"" >Title              </th>
                                <th is=""sortable-column""  v-bind:filter=""detailItemsFilter.filter"" v-on:order-by-changed=""orderBy"">Priority            </th>
                                <th is=""sortable-column""  v-bind:filter=""detailItemsFilter.filter"" v-on:order-by-changed=""orderBy"" >Business Name      </th>
                                <th is=""sortable-column""  v-bind:filter=""detailItemsFilter.filter"" v-on:order-by-changed=""orderBy"" >Price              </th>
                                <th is=""sortable-column""  v-bind:filter=""detailItemsFilter.filter"" v-on:order-by-changed=""orderBy"" >Date               </th>
                                <th is=""sortable-column""  v-bind:filter=""deta");
            WriteLiteral(@"ilItemsFilter.filter"" v-on:order-by-changed=""orderBy"" >Is Deposit         </th>
                                <th is=""sortable-column""  v-bind:filter=""detailItemsFilter.filter"" v-on:order-by-changed=""orderBy"" >Is Paid to a Person</th>
                                <th is=""sortable-column""  v-bind:filter=""detailItemsFilter.filter"" v-on:order-by-changed=""orderBy"" >Category Name      </th>
                                <th is=""sortable-column"" v-if=""chkShowCreatdByAndCreatedAt"" v-bind:filter=""filter""                 >Created By         </th>
                                <th is=""sortable-column"" v-if=""chkShowCreatdByAndCreatedAt"" v-bind:filter=""filter""                 >Created At         </th>
                            </tr>
                            <tr>
                                <th>
                                    <c-input>
                                        <template v-slot:input>
                                            <input class=""bg-blue-900 border-2 rounded-full""");
            WriteLiteral(@" type=""text"" v-model=""detailItemsFilter.Title"" />
                                        </template>
                                    </c-input>
                                </th>
                                <th>
                                    <c-input>
                                        <template v-slot:input>
                                            <input class=""bg-blue-900 border-2 rounded-full"" type=""text"" v-model=""detailItemsFilter.PaymentPriorityName"" />
                                        </template>
                                    </c-input>
                                </th>
                                <th>
                                    <c-input>
                                        <template v-slot:input>
                                            <input class=""bg-blue-900 border-2 rounded-full"" type=""text"" v-model=""detailItemsFilter.BusinessName"" />
                                        </template>
                                 ");
            WriteLiteral(@"   </c-input>
                                </th>
                                <th>
                                    <c-input>
                                        <template v-slot:input>
                                            <input class=""bg-blue-900 border-2 rounded-full"" type=""text"" v-model=""detailItemsFilter.Price"" />
                                        </template>
                                    </c-input>
                                </th>
                                <th>
");
            WriteLiteral(@"                                </th>
                                <th>
                                    <c-select :options=""[{Id:null, Name:'All'},{Id:false, Name:'No'}, {Id:true, Name:'Yes'}]"" v-model=""filter.IsDeposit""></c-select>
                                </th>
                                <th>
                                    <c-select :options=""[{Id:null, Name:'All'},{Id:false, Name:'No'}, {Id:true, Name:'Yes'}]"" v-model=""filter.IsPaidToPerson""></c-select>
                                </th>
                                <th>
                                    <c-input>
                                        <template v-slot:input>
                                            <input class=""bg-blue-900 border-2 rounded-full"" type=""text"" v-model=""detailItemsFilter.CategoryName"" />
                                        </template>
                                    </c-input>
                                </th>
                                <th></th>
           ");
            WriteLiteral(@"                     <th>
                                    <input v-if=""chkShowCreatdByAndCreatedAt"" type=""text"" class=""form-control form-control-sm"" placeholder=""filter"" v-model=""detailItemsFilter.CreatedAt"" />
                                </th>
                            </tr>
                        </template>
                        <template v-slot:tbody>
                            <tr v-if=""!detailItemsFilter.Loading && detailItems.length == 0"">
                                <td class=""text-center"">Could not find any payments</td>
                            </tr>
                            <tr v-for=""(item, index) in detailItems""  :class=""{'bg-customPurple-10':index%2 ==0}"">
                                <td>{{item.Title}}</td>
                                <td>{{item.PaymentPriorityName}}</td>
                                <td>{{item.BusinessName}}</td>
                                <td>{{item.FormattedPrice}}</td>
                                <td>{{item.Date}}</td");
            WriteLiteral(@">
                                <td><span v-if=""item.IsDeposit"">Yes</span><span v-if=""!item.IsDeposit"">No</span></td>
                                <td><span v-if=""item.IsPaidToPerson"">Yes</span><span v-if=""!item.IsPaidToPerson"">No</span></td>
                                <td>{{item.CategoryName}}</td>
                                <td v-if=""chkShowCreatdByAndCreatedAt"">{{item.CreatedByFullName}}</td>
                                <td v-if=""chkShowCreatdByAndCreatedAt"">{{item.CreatedAt}}</td>
                            </tr>
                        </template>
                    </c-table>
                </div>
            </template>
        </c-section>
    </div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Web.Pages.Report.MonthlyOverview.MonthlyOverviewModel> Html { get; private set; } = default!;
        #nullable disable
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Web.Pages.Report.MonthlyOverview.MonthlyOverviewModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Web.Pages.Report.MonthlyOverview.MonthlyOverviewModel>)PageContext?.ViewData;
        public Web.Pages.Report.MonthlyOverview.MonthlyOverviewModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
