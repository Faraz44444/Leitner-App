const compTH = {
    template: `
                <th v-bind:colspan="colspan" v-on:click="emitEvents">{{title}}
                    <span v-show="orderBy == Value && orderByDirection == 1">
                        <i class="fa-solid fa-arrow-up-short-wide"></i>
                    </span>
                    <span v-show="orderBy == Value && orderByDirection == 2">
                        <i class="fa-solid fa-arrow-down-wide-short"></i>
                    </span>
                </th>
`,
    emits: ['update:modelValue', 'updateOrderByDirection', 'updateOrderBy'],
    props: {
        title: {
            Type: String,
        },
        value: {
            Type: String, Number,
            default: ""
        },
        orderBy: {
            Type: String, Number,
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
            default: null
        },
    },
    methods: {
        emitEvents: function () {
            if (this.value == this.orderBy)
                this.$emit('updateOrderByDirection', this.orderByDirection == 1 ? 2 : 1);
            else
                this.$emit('updateOrderBy', this.value);
        }
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