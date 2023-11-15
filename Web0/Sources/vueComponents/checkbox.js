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