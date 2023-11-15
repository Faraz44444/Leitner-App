let dropdownId = 0
const compDropdown = {
    template: `
            <div :class="extraClasses" v-on:click="toggle" class="flex">
                <div>
                    <label class="ml-2 mr-4">
                            {{inputLabel}}
                            <span v-show="required" class="ml-1 small text-danger">Required</span>
                    </label>
                </div>
                <div>
                    <slot name="body"></slot>
                    <div v-bind:id="DropdownId" class="absolute border-2 max-h-60 overflow-auto hidden">
                        <slot name="items"></slot>
                    </div>
                </div>
            </div>
`,
    emits: [''],
    props: {
        extraClasses: {
            Type: String,
            default: ""
        },
        inputLabel: {
            type: String,
            default: "",
        },
        itemsVisiblility: false,
        dorpdownItems: 0,
        required: {
            Type: Boolean,
            default: false
        }
    },
    data: function () {
        return {
            dropdownId: null,
        }
    },
    methods: {
        toggle: function () {
            if (document.getElementById(this.DropdownId)?.classList.contains("hidden"))
                this.showItems();
            else
                this.hideItems();
        },
        showItems: function () {
            if (document.getElementById(this.DropdownId)?.classList.contains("hidden"))
                document.getElementById(this.DropdownId)?.classList.remove("hidden");
        },
        hideItems: function () {
            document.getElementById(this.DropdownId)?.classList.add("hidden");
        }
    },
    computed: {
        DropdownId: function () {
            return 'Dropdown' + this.dropdownId;
        }
    },
    watch: {
        'itemsVisiblility': function () {
            if (document.getElementById(this.DropdownId)?.classList.contains("hidden"))
                document.getElementById(this.DropdownId)?.classList.remove("hidden");
            else
                document.getElementById(this.DropdownId)?.classList.add("hidden");
        }
    },
    mounted: function () {
        this.dropdownId = dropdownId;
        dropdownId += 1;
    }
}
