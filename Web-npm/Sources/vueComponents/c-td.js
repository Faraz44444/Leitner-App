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