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