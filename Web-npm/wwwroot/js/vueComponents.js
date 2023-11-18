const compButton = {
    template: `
            <div class="flex" :class="extraClasses">
                <button 
                    type="button" 
                    class="p-3 rounded-2xl" 
                    :class="{'transition bg-purple-2 hover:bg-purple-1 duration-500': type == 'normal'}"
                    v-on:click="$emit('click')">
                <i class="mr-1" v-bind:class="icon"></i>
                {{title}}
                </button>
            </div>
`,
    emits: ['click'],
    props: {
        title: {
            Type: String,
            required: true
        },
        extraClasses: {
            Type: String,
            default: ""
        },
        type: {
            Type: String,
            default: "normal"
        },
        icon: {
            type: String,
            default: ""
        }
    },
    methods: {

    },
    computed: {

    },
    watch: {

    },
    mounted: function () {

    }
}
const compCheckbox = {
    emits: ['update:modelValue'],
    props: {
        id: String,
        label: String,
        modelValue: {
            type: [Boolean, String],
            //validator: val => [true, false].
            required: false
        },
        tabindex: {
            type: String,
            default: ""
        },
        readonly: {
            type: [Boolean, String],
            default: false
        },
        disabled: {
            type: [Boolean, String],
            default: false
        },
        useMuted: {
            type: [Boolean, String],
            default: false,
        },
        variant: {
            type: String,
            validator: val => ["normal", "green", "danger"].indexOf(val) > -1,
            default: 'normal',
        },
        useIndeterminate: {
            type: [Boolean, String],
            default: false,
        },
        formclass: {
            type: String,
            required: false
        },
        inputclass: {
            type: String,
            required: false
        }
    },
    methods: {
        handleIndeterminate: function () {
            if (this.$refs.input && this.useIndeterminate.toString() == "true")
                this.$refs.input.indeterminate = (this.modelValue == undefined);
        }
    },
    computed: {
        ElementId: function () {
            return this.id || this.$.uid;
        },
        Value: {
            get: function () {
                return (this.modelValue||"").toString() == "true";
            },
            set: function (value) { this.$emit("update:modelValue", value); }
        },

        UseMuted: function () {
            return this.useMuted.toString() == "true";
        },
    },
    watch: {
        'modelValue': function () {
            this.handleIndeterminate();
        }
    },
    mounted: function() {
        this.handleIndeterminate();
    },
    template: `
        <div class="flex align-center self-center" v-bind:class="formclass">
            <input
                v-bind:class="inputclass + ' mr-2'"
                ref="input"
                type="checkbox"
                v-model="Value"
                :id="'chk_'+ElementId"
                :tabindex="tabindex"
                :disabled="disabled"
                :readonly="readonly"
                :class="{'bg-green': variant == 'green', 'bg-red': variant == 'danger', }"
            >
            <label class="form-check-label" :for="'chk_'+ElementId"
                :class="{'text-muted': UseMuted && !Value }">
                {{label}}
            </label>
        </div>
    `
}
const compDatepicker = {
    template: `
            <div class="">
                <label class="mr-2">{{label}}</label>
                <input v-model="Value" class="bg-background-1 border-2 p-1 rounded-full"  type="date">     
            </div>
`,
    emits: ['update:modalValue'],
    data: function () {
        return {
            input: null
        }
    },
    props: {
        label: {
            type: String
        },
        modelValue: {}
    },
    methods: {
        updateValue: function (value) {
            this.$emit('input', value)
        }
    },
    computed: {
        Value: {
            get: function () {
                return this.modelValue ;
            },
            set: function (value) { this.$emit("update:modelValue", value); }
        },
    },
    watch: {
    },
    mounted: function () {

    }
}
let dropdownId = 0
const compDropdown = {
    template: `
            <div :class="extraClasses" v-on:click="toggle" class="flex">
                <div>
                    <label class="ml-2 mr-4">
                            {{inputLabel}}
                            <span v-show="required" class="ml-1 small text-danger">Required</span>
                    </label>
                </div>
                <div>
                    <slot name="body"></slot>
                    <div v-bind:id="DropdownId" class="absolute border-2 max-h-60 overflow-auto hidden">
                        <slot name="items"></slot>
                    </div>
                </div>
            </div>
`,
    emits: [''],
    props: {
        extraClasses: {
            Type: String,
            default: ""
        },
        inputLabel: {
            type: String,
            default: "",
        },
        itemsVisiblility: false,
        dorpdownItems: 0,
        required: {
            Type: Boolean,
            default: false
        }
    },
    data: function () {
        return {
            dropdownId: null,
        }
    },
    methods: {
        toggle: function () {
            if (document.getElementById(this.DropdownId)?.classList.contains("hidden"))
                this.showItems();
            else
                this.hideItems();
        },
        showItems: function () {
            if (document.getElementById(this.DropdownId)?.classList.contains("hidden"))
                document.getElementById(this.DropdownId)?.classList.remove("hidden");
        },
        hideItems: function () {
            document.getElementById(this.DropdownId)?.classList.add("hidden");
        }
    },
    computed: {
        DropdownId: function () {
            return 'Dropdown' + this.dropdownId;
        }
    },
    watch: {
        'itemsVisiblility': function () {
            if (document.getElementById(this.DropdownId)?.classList.contains("hidden"))
                document.getElementById(this.DropdownId)?.classList.remove("hidden");
            else
                document.getElementById(this.DropdownId)?.classList.add("hidden");
        }
    },
    mounted: function () {
        this.dropdownId = dropdownId;
        dropdownId += 1;
    }
}

const compInput = {
    template: `<div v-bind:class="extraClasses + ' c-input-input flex justify-between'">
                    <span v-if="isSelect" class="caret"></span>
                    <button v-if="showCopy" class='btn btn-link text-gray-5 copy-btn' type='button' title='Click to copy' v-on:click='copyToClipboard($event)'><i class='fas fa-copy'></i></button>
                    <label v-if="inputLabel" class="ml-2 mr-4">
                        {{inputLabel}}
                        <span v-show="required" class="ml-1 small text-danger">Required</span>
                    </label>
                    <slot class="c-input-input" name="input">
                    </slot>
                </div>`,
    props: {
        inputLabel: {
            type: String,
            default: "",
        },
        isSelect: {
            type: Boolean,
            default: false,
            required: false
        },
        showCopy: {
            type: Boolean,
            default: false,
            required: false
        },
        extraClasses: {
            type: String
        }
    },
    computed: {
        required: function () {
            return false;
        }
    },
    methods: {
        copyToClipboard: function (el) {
        }
    }
}
const compModal = {
    template: `
                <div class="bg-blueGray-800 bg-opacity-80 fixed h-full left-0 top-0 transition w-full z-9998">
                    <div class=" transition duration-100 fixed z-9998 top-0  w-full h-full table max-w-lg inset-x-0 inset-y-1/4 m-auto">
                       <div class="table-cell align-middle ">
                           <div class="m-0 m-auto px-0.5 py-0.5 pb-10 bg-background-2 text-goldenText-1 opacity-1 scale-100 border-2 rounded-2xl px-3.5">
                                <div class="mt-3 flex justify-between">
                                    <div>
                                        <slot name="header">
                                        </slot>
                                    </div>
                                    <button type="button" class="mr-2" v-on:click="$emit('close')"><i class="fa-solid fa-xmark"></i></button>
                                </div>
                                <hr>
                                <div class="mt-3 p-10">
                                    <slot name="body">
                                    </slot>
                                </div>
                                <div class="mt-3">
                                    <hr>
                                    <slot name="footer">
                                        <div class="flex float-right">
                                            <c-button type="" class="mr-3" v-on:click="$emit('close')" title="Close"></c-button>
                                            <c-button type="" v-on:click="$emit('save')" title="Save"></c-button>
                                        </div>
                                    </slot>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
`,
    emits: ['close', 'save'],
    props: {
        title: {
            Type: String,
        },
        extraClasses: {
            Type: String,
            default: ""
        }

    },
    methods: {

    },
    computed: {

    },
    watch: {

    },
    mounted: function () {

    }
}
const compSection = {
    components: {
        //'c-created-by': compCreatedBy,
    },
    data: function () {
        return {
            displayLog: false,
            collapseData: null,
            collapseLog: null,
            display: true
        }
    },
    props: {
        hideHeader: {
            type: Boolean,
            default: false
        },
        icon: String,
        title: String,
        small: Boolean,
        displayIcon: {
            type: Boolean,
            default: true
        },
        useFooter: Boolean,
        showLog: Boolean,
        entityType: String,
        entityTypes: Array,
        entityId: Number,
        altEntityId: Number,
        bordered: {
            type: Boolean, 
            default:true
        },
        trash: {
            type: Boolean,
            default: false
        },
        createdAt: {
            type: String,
            default: ""
        },
        createdBy: {
            type: String,
            default: ""
        },
        mt: {
            type: String,
            default: "mt-4"
        },
    },
    computed: {
    },
    methods: {
        toggleLog: function () {
            if (this.displayLog) {
                this.collapseData.show();
                this.collapseLog.hide();
            } else {
                this.collapseData.hide();
                this.collapseLog.show();
            }
            this.displayLog = !this.displayLog;
        },
        displayClick: function () {
            return history.back();
        },
        additionalClass: function () {
            var c = this.mt;

            if (this.small)
                c += " section-small shadow-sm ";
            else
                c += " shadow ";

            return c;
        }
    },
    watch: {
        'display': function () {
            this.display ? document.getElementById("body"+this.title).classList.remove("hidden") :
                document.getElementById("body" + this.title).classList.add("hidden")
        }
    },
    mounted: function () {
        if (this.showLog) {
            this.collapseData = new bootstrap.Collapse(document.getElementById('collapseData'), { toggle: false });
            this.collapseLog = new bootstrap.Collapse(document.getElementById('collapseLog'), { toggle: false });
        }
    },
    template: `
    <div :class="{'justify-center p-3':true, ' border-2 border-coolGray-700 rounded-xl': bordered}" v-bind:class="additionalClass()">
        <div v-if="!hideHeader" class="grid grid-flow-col bg-background-2 text-purple-1 rounded-full">
            <span v-if="icon?.length">
                <i v-bind:class="icon"></i>
            </span>
            <span class="cursor-pointer ml-4 p-1" v-if="title?.length"  v-on:click.prevent="display = !display">
                <h5>
                    {{title}}
                    <span v-if="trash" class="">DELETED</span>
                </h5>
            </span>
            <span v-if="showLog">
                <button class="" type="button" v-on:click="toggleLog()">
                    <i class="fas fa-clipboard-list me-2"></i>
                    <span v-show="displayLog">Skjul logg</span>
                    <span v-show="!displayLog">Vis logg</span>
                </button>
            </span>
            <span v-if="displayIcon" class="mr-4">
                <button class="float-right" type="button" v-on:click.prevent="display = !display">
                    <i v-show="display" class="fas fa-chevron-up me-2"></i>
                    <i v-show="!display" class="fas fa-chevron-down me-2"></i>
                </button>
            </span>
        </div>
        <div class="">
            <slot name="nav"></slot>
        </div>
        <div :id="'body'+title" class="justify-center p-5" v-if="!showLog">
            <slot name="content"></slot>          
        </div>
        <div class="" v-if="showLog">
            <div class="collapse show" id="collapseData" data-bs-toggle="false">
                <slot name="content"></slot>
            </div>                           
        </div>
       
        <div class="" v-if="!useFooter">           
        </div>        
    </div>
  `
}
const compSelect = {
    template: `
                <select class="bg-background-2 border-2 rounded-full text-white"  v-model="Value" :placeholder=placeholder  :required=required>
                    <option :track-by=trackBy
                            v-for="item in options"
                            v-bind:value="item[trackBy]">{{item.Name}}</option>
                </select>`,
    data: function () {
        return {
            selectedItems: []
        }
    },
    props: {
        placeholder: {
            type: String,
            default: "",
        },
        required: {
            type: Boolean,
            default: false,
            required: false
        },
        options: {
            type: Array,
            default: false,
            required: false
        },
        trackBy: {
            type: String,
            default: "Id"
        },
        value: {}
    },
    computed: {
        Value: {
            get() { return this.value },
            set(value) { this.$emit('input', value) }
        }
    },
    methods: {
    }
}
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
const compTD= {
    template: `
            <td>{{Value}}</td>
    `,
    emits: [],
    props: {
        value: {
            Type: String, Number, Date,
            default: ""
        },
        orderByDirection: {
            Type: String, Number,
            default: ""
        },
        modelValue: {
        },
        colspan: {
            Type: Number,
            default: 1
        },
    },
    methods: {
    },
    computed: {
        Value: {
            get: function () {
                return this.value;
            },
            set: function (value) { this.$emit("update:modelValue", value); }
        },

    },
    watch: {

    },
    mounted: function () {

    }
}
const compTH = {
    template: `
                <th v-bind:colspan="colspan" v-on:click="emitEvents">{{title}}
                    <span v-show="orderBy == Value && orderByDirection == 1">
                        <i class="fa-solid fa-arrow-up-short-wide"></i>
                    </span>
                    <span v-show="orderBy == Value && orderByDirection == 2">
                        <i class="fa-solid fa-arrow-down-wide-short"></i>
                    </span>
                </th>
`,
    emits: ['update:modelValue', 'updateOrderByDirection', 'updateOrderBy'],
    props: {
        title: {
            Type: String,
        },
        value: {
            Type: String, Number,
            default: ""
        },
        orderBy: {
            Type: String, Number,
            default: ""
        },
        orderByDirection: {
            Type: String, Number,
            default: ""
        },
        modelValue: {
        },
        colspan: {
            Type: Number,
            default: null
        },
    },
    methods: {
        emitEvents: function () {
            if (this.value == this.orderBy)
                this.$emit('updateOrderByDirection', this.orderByDirection == 1 ? 2 : 1);
            else
                this.$emit('updateOrderBy', this.value);
        }
    },
    computed: {
        Value: {
            get: function () {
                return this.value;
            },
            set: function (value) { this.$emit("update:modelValue", value); }
        },

    },
    watch: {

    },
    mounted: function () {

    }
}
const compTR = {
    template: `
            <tr :class="{'bg-customPurple-10':index%2 ==0, 'hover:text-border-1 cursor-pointer': true}" v-on:click="$emit('click')">
                <slot name="content"></slot>
            </tr>
    `,
    emits: ['click'],
    props: {
        index: {
            Type: String, Number,
            default: 0
        }
    },
    methods: {
    },
    computed: {
       
    },
    watch: {

    },
    mounted: function () {

    }
}