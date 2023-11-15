var app = vueContext({
    el: "#app",
    components: {
        'c-check-box': compCheckbox
    },
    data: function () {
        return {
            details: {
                CategoryId: 0,
                Name: null,
                Deleted: false,
                CreatedAt: null,
                CreatedByFullName: ""
            },
            items: [],
            priorities: []
        }
    },
    watch: {

    },
    computed: {

    },
    methods: {
        getDetails: function () {
            return apiHandler.GetById("category", this.details.CategoryId).then(response => {
                this.details = response;
            });
        },
        getCategoryPriorities: function () {
            return apiHandler.Get("category/priorities", this.details).then(response => {
                this.priorities = response;
            });
        },
        save: function () {
            if (this.details.CategoryId > 0) {
                return apiHandler.Put("category", this.details.CategoryId, this.details).then(response => {
                    if (response && response.Ok) {
                        return window.location = "/datamanagement/category/categorylist"
                    }
                });
            }
            else {
                return apiHandler.Post("category", this.details).then(response => {
                    if (response && response.Ok) {
                        return window.location = "/datamanagement/category/categorylist"
                    }
                });
            }
        }
    },
    created: function () {
        this.details.CategoryId = parseInt(document.getElementById("app").dataset.entityId);
        if (this.details.CategoryId < 1)
            return;
        this.getDetails();
        this.getCategoryPriorities();
    },
    mounted: function () {
        this.getCategoryPriorities();

    }
})

