
Vue.component('sortable-column', {
    template: `<th class="sortable-th" v-on:click="emitEvent()">
                <slot></slot>
                <span v-show="filter.OrderBy===value">
                    <i v-show="filter.OrderByDirection===1" class="fas fa-sort-amount-up-alt"></i>
                    <i v-show="filter.OrderByDirection===2" class="fas fa-sort-amount-down"></i>
                </span>
            </th>`,
    props: {
        value: {
            type: Number,
            default: null
        },        
        filter: {
            type: Object,
            default: function () {
                return {
                    OrderBy: 1,
                    OrderByDirection: 1
                }
            }
        },
        eventName: {
            type: String,
            default: "order-by-changed"
        }
    },
    methods: {
        emitEvent: function () {
            this.$emit(this.eventName, this.value);
        }
    }
});