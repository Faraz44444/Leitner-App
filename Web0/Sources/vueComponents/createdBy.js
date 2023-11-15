const compCreatedBy = {
    name: "created-by",
    template: `
        <div>
            <div v-if="HasCreatedBy" class="me-auto text-muted">{{CreatedBy}}</div>
        </div>
    `,
    props: {
        createdBy: {
            type: Object,
            required: true,
        }
    },
    methods: {

    },
    computed: {
        HasCreatedBy: function () {
            return (this.createdBy.CreatedAt && this.createdBy.CreatedAt.trim() != "" || this.createdBy.CreatedByFullName && this.createdBy.CreatedByFullName.trim() != "");
        },
        CreatedBy: function () {

            let strs = ["Created"];

            if (this.createdBy.CreatedAt && this.createdBy.CreatedAt.trim() != "") strs.push("at", ToLocaleString(this.createdBy.CreatedAt));
            if (this.createdBy.CreatedByFullName && this.createdBy.CreatedByFullName.trim() != "") strs.push("by", this.createdBy.CreatedByFullName);

            return strs.join(" ");
        }
    }
}