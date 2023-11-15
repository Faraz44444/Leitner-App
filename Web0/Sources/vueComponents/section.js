const compSection = {
    components: {
        //'c-created-by': compCreatedBy,
    },
    data: function () {
        return {
            displayLog: false,
            collapseData: null,
            collapseLog: null
        }
    },
    props: {
        hideHeader: {
            type: Boolean,
            default: false
        },
        icon: String,
        title: String,
        howToLink: String,
        small: Boolean,
        returnIcon: Boolean,
        useFooter: Boolean,
        showLog: Boolean,
        entityType: String,
        entityTypes: Array,
        entityId: Number,
        altEntityId: Number,
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
        hasHowToLink: function () {
            return this.howToLink != null && this.howToLink.trim() != "";
        },
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
        returnIconClick: function () {
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
    mounted: function () {
        if (this.showLog) {
            this.collapseData = new bootstrap.Collapse(document.getElementById('collapseData'), { toggle: false });
            this.collapseLog = new bootstrap.Collapse(document.getElementById('collapseLog'), { toggle: false });
        }
    },
    template: `
    <div class="section" v-bind:class="additionalClass()">
        <div v-if="!hideHeader" class="section-header">
            <i v-bind:class="icon"></i>
            <slot name="title">
                <h5>
                    {{title}}
                    <span v-if="trash" class="badge bg-danger">Slettet</span>
                </h5>
            </slot>
            <span v-if="showLog">
                <button class="btn btn-sm btn-primary ms-3" type="button" v-on:click="toggleLog()">
                    <i class="fas fa-clipboard-list me-2"></i>
                    <span v-show="displayLog">Skjul logg</span>
                    <span v-show="!displayLog">Vis logg</span>
                </button>
            </span>
            <span v-if="hasHowToLink">
                <button class="btn btn-sm btn-primary ms-3" type="button">
                    <i class="fas fa-question-circle me-2"></i>Hjelp
                </button>
            </span>
            <span v-if="returnIcon">
                <button class="btn btn-sm btn-link ms-3 text-white" type="button" v-on:click="returnIconClick">
                    <i class="fas fa-times me-2"></i>
                </button>
            </span>
        </div>
        <div class="section-nav">
            <slot name="nav"></slot>
        </div>
        <div class="section-content" v-if="!showLog">
            <slot name="content"></slot>          
        </div>
        <div class="section-content" v-if="showLog">
            <div class="collapse show" id="collapseData" data-bs-toggle="false">
                <slot name="content"></slot>
            </div>                           
        </div>
       
        <div class="no-footer" v-if="!useFooter">           
        </div>        
    </div>
  `
}