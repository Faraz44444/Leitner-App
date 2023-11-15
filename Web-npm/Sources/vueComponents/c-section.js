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
        <div v-if="!hideHeader" class="grid grid-flow-col bg-background-2 text-white rounded-full">
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