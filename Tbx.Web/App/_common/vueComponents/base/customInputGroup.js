
Vue.component('custom-input-group', {
    template: `<div v-bind:class="customInputGroupClass">
                    <label>{{inputLabel}}<span v-show="required" class="ml-1 small text-danger">Required</span></label>
                    <span v-if="isSelect" class="caret"></span>
                    <button v-if="showCopy" class='btn btn-link text-gray-5 copy-btn' type='button' title='Click to copy' v-on:click='copyToClipboard($event)'><i class='fas fa-copy'></i></button>
                    <slot></slot>
                </div>`,
    props: {
        inputLabel: {
            type: String,
            default: "",
            required: true
        },
        isSelect: {
            type: Boolean,
            default: false,
            required: false
        },
        isTextarea: {
            type: Boolean,
            default: false,
            required: false
        },
        showCopy: {
            type: Boolean,
            default: false,
            required: false
        }
    },
    computed: {
        customInputGroupClass: function () {
            if (this.isTextarea)
                return "custom-input-group auto-height";
            return "custom-input-group";
        },
        required: function () {
            let slot = this.$slots.default[0];
            if (slot.tag == "select" || slot.tag == "input") {
                if (!slot.data)
                    return false;
                if (!slot.data.attrs)
                    return false;
                if (!slot.data.attrs.hasOwnProperty('required'))
                    return false;
                if (slot.data.attrs.required === undefined)
                    return false;
                if (slot.data.attrs.required === true)
                    return true;
                if (slot.data.attrs.required === false)
                    return false;
                if (slot.data.attrs.required.trim().toLowerCase() == "false")
                    return false;
                return true;
            }
            return false;
        }
    },
    methods: {
        copyToClipboard: function (el) {
            var parent = $(el.target).closest(".custom-input-group");
            var text = parent.find("input").val();
            if (this.isTextarea)
                text = parent.find("text-area").val();
            if (this.isSelect)
                text = parent.find("select").find(":selected").text();            
            if (text)
                clipboard.CopyToClipboard(text);
        }
    },
});