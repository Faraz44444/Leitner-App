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