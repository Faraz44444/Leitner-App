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
    // TOGGLE
    //template: `
    //    <div class="form-check form-switch">
    //        <input class="form-check-input"
    //            type="checkbox"
    //            v-model="Value"
    //            :id="'chk_'+ElementId"
    //            :tabindex="tabindex"
    //            :disabled="disabled"
    //            :readonly="readonly"
    //        >
    //        <label class="form-check-label" :for="'chk_'+ElementId"
    //            :class="{'text-muted': UseMuted && !Value }">
    //            <strong>{{label}}</strong>
    //        </label>
    //    </div>
    //`
    template: `
        <div class="form-check" v-bind:class="formclass">
            <input class="form-check-input"
                v-bind:class="inputclass"
                ref="input"
                type="checkbox"
                v-model="Value"
                :id="'chk_'+ElementId"
                :tabindex="tabindex"
                :disabled="disabled"
                :readonly="readonly"
                :class="{'form-check-input-green bg-green': variant == 'green', 'form-check-input-danger': variant == 'danger', }"
            >
            <label class="form-check-label" :for="'chk_'+ElementId"
                :class="{'text-muted': UseMuted && !Value }">
                <strong>{{label}}</strong>
            </label>
        </div>
    `
}
const compCreatedBy = {
    name: "created-by",
    template: `
        <div>
            <div v-if="HasCreatedBy" class="me-auto text-muted">{{CreatedBy}}</div>
        </div>
    `,
    props: {
        createdBy: {
            type: Object,
            required: true,
        }
    },
    methods: {

    },
    computed: {
        HasCreatedBy: function () {
            return (this.createdBy.CreatedAt && this.createdBy.CreatedAt.trim() != "" || this.createdBy.CreatedByFullName && this.createdBy.CreatedByFullName.trim() != "");
        },
        CreatedBy: function () {

            let strs = ["Created"];

            if (this.createdBy.CreatedAt && this.createdBy.CreatedAt.trim() != "") strs.push("at", ToLocaleString(this.createdBy.CreatedAt));
            if (this.createdBy.CreatedByFullName && this.createdBy.CreatedByFullName.trim() != "") strs.push("by", this.createdBy.CreatedByFullName);

            return strs.join(" ");
        }
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