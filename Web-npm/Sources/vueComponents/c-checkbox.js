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