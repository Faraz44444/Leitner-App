
Vue.component('content-section', {
    template: `<section v-bind:id="id" v-bind:class="sectionClass">
                    <div class="section-header" v-bind:class="{'cursor-pointer': collapsable}" v-on:click="isCollapsed = !isCollapsed">                        
                        <h5 class='d-inline-block'>
                            <i v-if="hasIcon" v-bind:class="icon"></i>  
                            <slot name="title">{{title}}</slot>
                            <a v-if="hasExitIcon != ''" class='d-inline-block pr-0 float-right' v-bind:href="exitUrl">
                                <i class="fas fa-times mr-0"></i>
                            </a>
                            <a v-if="userManualLink != ''" class='d-inline-block pr-0 float-right' v-bind:href="userManualLink" target='_blank'>
                                <i class="fas fa-question-circle mr-0"></i>
                            </a>
                        </h5>
                    </div>
                    <div class="section-content">
                        <slot></slot>                        
                    </div>                    
                </section>`,
    props: {
        id: {
            type: String,
            default: "",
            required: false
        },
        title: {
            type: String,
            default: "",
            required: true
        },
        icon: {
            type: String,
            default: "",
            required: false
        },
        exitUrl: {
            type: String,
            default: "",
            required: false
        },
        small: {
            type: Boolean,
            default: false,
            required: false
        },
        collapsable: {
            type: Boolean,
            default: false,
            required: false
        },
        collapsed: {
            type: Boolean,
            default: false,
            required: false
        },
        largeHeaderFont: {
            type: Boolean,
            default: false,
            required: false
        },
        umAnchor: {
            type: String,
            default: "",
            required: false
        },

    },
    data: function () {
        return {
            isCollapsed: this.collapsed,
        }
    },
    watch: {
        'collapsed': function () {
            this.isCollapsed = this.collapsed;
        },
    },
    computed: {
        hasExitIcon: function () {
            return this.exitUrl.trim() != "";
        },
        hasIcon: function () {
            return this.icon.trim() != "";
        },
        userManualLink: function () {
            if (!this.umAnchor || this.umAnchor == '')
                return '';
            return '/usermanual' + this.umAnchor;
        },
        sectionClass: function () {
            var classList = [];

            if (!this.collapsable)
                this.isCollapsed = false;

            if (this.isCollapsed)
                classList.push("section-collapsed");

            if (this.small) {
                classList.push("section-small");
                if (this.title && this.title != '') {
                    classList.push("section-fieldset");
                }
            } else {
                classList.push("section-large");
            }

            if (this.largeHeaderFont)
                classList.push("large-header-font");

            return classList;
        }
    }
});