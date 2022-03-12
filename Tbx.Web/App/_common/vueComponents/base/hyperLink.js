
Vue.component('hyper-link', {
    template: `<a class="text-underline" v-bind:href="link" v-bind:target="targetValue">
                    {{title}}
                    <i v-show="newWindow" class="fas fa-fw fa-sm fa-external-link-alt"></i>                     
                </a>`,
    props: {
        title: {
            type: String,
            default: ""
        },  
        link: {
            type: String,
            default: ""
        },       
        newWindow: {
            type: Boolean,
            default: false
        }             
    },
    computed: {
        targetValue: function () {
            if (this.newWindow)
                return "_blank";
            return "_self";
        }
    }
});