const compTR = {
    template: `
            <tr :class="{'bg-customPurple-10':index%2 ==0, 'hover:text-border-1 cursor-pointer': true}" v-on:click="$emit('click')">
                <slot name="content"></slot>
            </tr>
    `,
    emits: ['click'],
    props: {
        index: {
            Type: String, Number,
            default: 0
        }
    },
    methods: {
    },
    computed: {
       
    },
    watch: {

    },
    mounted: function () {

    }
}