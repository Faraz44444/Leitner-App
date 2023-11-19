const compModal = {
    template: `
                <div class="bg-blueGray-800 bg-opacity-80 fixed h-full left-0 top-0 transition w-full z-9998">
                    <div class=" transition duration-100 fixed z-9998 top-0  w-full h-full table max-w-lg inset-x-0 inset-y-1/4 m-auto">
                       <div class="table-cell align-middle ">
                           <div class="m-0 m-auto px-0.5 py-0.5 pb-10 bg-background-2 text-goldenText-1 opacity-1 scale-100 border-2 rounded-2xl px-3.5">
                                <div class="mt-3 flex justify-between">
                                    <div>
                                        <slot name="header">
                                            <span id="modalHeader"> </span>
                                        </slot>
                                    </div>
                                    <button type="button" class="mr-2" v-on:click="$emit('close')"><i class="fa-solid fa-xmark"></i></button>
                                </div>
                                <hr>
                                <div class="mt-3 p-10">
                                    <slot name="body">
                                    </slot>
                                </div>
                                <div class="mt-3">
                                    <hr>
                                    <slot name="footer">
                                        <div class="flex float-right">
                                            <c-button type="" class="mr-3" v-on:click="$emit('close')" title="Close"></c-button>
                                            <c-button type="" v-on:click="$emit('save')" title="Save"></c-button>
                                        </div>
                                    </slot>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
`,
    emits: ['close', 'save'],
    props: {
        title: {
            Type: String,
        },
        extraClasses: {
            Type: String,
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