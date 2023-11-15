const compButton = {
    template: `
            <div class="flex" :class="extraClasses">
                <button 
                    type="button" 
                    class="p-3 rounded-2xl" 
                    :class="{'transition bg-background-3 hover:bg-background-2 duration-500': type == 'normal'}"
                    v-on:click="$emit('click')">
                <i class="mr-1" v-bind:class="icon"></i>
                {{title}}
                </button>
            </div>
`,
    emits: ['click'],
    props: {
        title: {
            Type: String,
            required: true
        },
        extraClasses: {
            Type: String,
            default: ""
        },
        type: {
            Type: String,
            default: "normal"
        },
        icon: {
            type: String,
            default: ""
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