const compInput = {
    template: `<div v-bind:class="extraClasses + ' c-input-input'">
                    <span v-if="isSelect" class="caret"></span>
                    <button v-if="showCopy" class='btn btn-link text-gray-5 copy-btn' type='button' title='Click to copy' v-on:click='copyToClipboard($event)'><i class='fas fa-copy'></i></button>
                    <div>                    
                        <label v-if="inputLabel" class="ml-2 mr-4">
                            {{inputLabel}}
                            <span v-show="required" class="ml-1 small text-danger">Required</span>
                        </label>
                    </div>
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