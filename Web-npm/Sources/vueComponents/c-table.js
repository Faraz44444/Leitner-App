const compTable = {
    template: `
            <div v-bind:class="wClass" v-on:scroll="onScroll">
                <table class="table" v-bind:class="tClass">
                    <thead v-bind:class="theadClass">     
                        <slot name="thead"></slot>
                    </thead>        
                    <tbody v-bind:class="tbodyClass">
                        <slot name="tbody" v-if="useInfiniteScroll"></slot>
                        <tr v-show="filter.Loading || itemCount < 1">
                            <td colspan="50" class="text-center text-muted">
                                <i v-show="filter.Loading" class="fas fa-sync fa-spin"></i>
                                <span v-show="!filter.Loading && itemCount < 1">Ingen treff</span>
                            </td>
                        </tr>
                        <slot name="tbody" v-if="!useInfiniteScroll"></slot>
                    </tbody>
                    <tfoot v-if="useFoot" v-bind:class="tfootClass">
                        <slot name="tfoot"></slot>
                    </tfoot>
                </table>
                    <nav class="mt-5" v-if="showPager">
                        <ul class="flex justify-center">
                            <li  v-on:click="goToPage(1)" class="p-3 rounded-full transition bg-purple-2 hover:bg-purple-1 duration-500 mr-3" v-bind:class="{'disabled': filter.CurrentPage <= 1}">
                                <a class="page-link" href="#" tabindex="-1" aria-disabled="true">First</a>
                            </li>
                            <li class="p-3 rounded-full transition bg-purple-2 hover:bg-purple-1 duration-500 mr-3" v-bind:class="{'disabled': filter.CurrentPage <= 1}">
                                <a class="page-link" href="#" tabindex="-1" aria-disabled="true" v-on:click="goToPage(filter.CurrentPage - 1)">Previous</a>
                            </li>
                            <li class="p-3 rounded-full transition bg-purple-2 hover:bg-purple-1 duration-500 mr-3" v-bind:class="{'active border-2': startPage + (n - 1) == filter.CurrentPage}" v-for="n in numberOfPagesToShow" v-on:click="goToPage(startPage + (n - 1))">
                                <a class="page-link" v-bind:class="{'': startPage + (n - 1) != filter.CurrentPage}" href="#" >{{startPage + (n - 1)}}</a>
                            </li>                    
                            <li class="p-3 rounded-full transition bg-purple-2 hover:bg-purple-1 duration-500 mr-3" v-bind:class="{'disabled': filter.CurrentPage >= filter.TotalPages}">
                                <a class="page-link" href="#" v-on:click="goToPage(filter.CurrentPage + 1)">Next</a>
                            </li>
                            <li class="p-3 rounded-full transition bg-purple-2 hover:bg-purple-1 duration-500 mr-3" v-bind:class="{'disabled': filter.CurrentPage >= filter.TotalPages}">
                                <a class="page-link" href="#" v-on:click="goToPage(filter.TotalPages)">Last</a>
                            </li>
                        </ul>
                    </nav>
            </div>
`,
    emits: ['fetchPage'],
    data: function () {
        return {
            numberOfPagesToShow: 5,
            startPage: 1,
            endPage: 1
        }
    },
    props: {
        useInfiniteScroll: { Type: Boolean, default: false },
        filter: {
            type: Object,
            default: function () {
                return {
                    CurrentPage: 1,
                    TotalPages: 1,
                    Loading: false
                }
            }
        },
        useFoot: Boolean,
        marginTop: String,
        theadClass: String,
        tbodyClass: String,
        itemCount: Number,
        tableClass: String,
        isResponsive: { type: Boolean, default: true },
        striped: {
            type: Boolean,
            default: true
        },
    },
    methods: {
        onScroll: function (event) {
            if (!this.useInfiniteScroll) return;
            if (this.filter.TotalPages <= 1) return;
            if (this.filter.TotalPages <= this.filter.CurrentPage) return;
            if (this.filter.Loading) return;


            var tResponsiveElm = event.target.closest(".table-responsive");
            var tLastRowElm = Array.from(event.target.querySelectorAll("tr")).slice(-2)[0];

            if (tResponsiveElm.getBoundingClientRect().bottom < tLastRowElm.getBoundingClientRect().top) return;
            this.goToPage(this.filter.CurrentPage + 1);
        },
        goToPage: function (page) {
            if (page == this.filter.CurrentPage)
                return;
            this.$emit('fetchPage', { page });
        }
    },
    computed: {
        tClass: function () {
            let c = this.tableClass;
            if (this.striped)
                c += " table-striped";
            return c;
        },
        wClass: function () {

            return {
                "mt-2": this.marginTop == "",
                "table-responsive": this.isResponsive,
            };
        },
        showPager: function () {
            return this.filter.TotalPages > 1 && !this.useInfiniteScroll;
        },
    },
    watch: {

    },
    mounted: function () {
        document.addEventListener('scroll', this.onScroll)
    }
}