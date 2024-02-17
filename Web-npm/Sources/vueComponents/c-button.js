const compButton = {
    template: `
            <div class="" :class="extraClasses">
                <button 
                    type="button" 
                    class="p-3 rounded-2xl min-w-1/10 flex justify-center" 
                    :class="{'transition bg-purple-2 hover:bg-purple-1 border-2 border-blueGray-600 duration-500': type == 'normal'}"
                    v-on:click="$emit('click')">
                    <i v-bind:class="icon"></i>
                    <div v-if="title" v-bind:class="{'ml-2': title}">{{title}}</div>
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