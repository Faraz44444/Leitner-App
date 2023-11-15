var app = vueContext({
    el: "#app",
    components: {
        'c-check-box': compCheckbox
    },
    data: function () {
        return {
            details: {
                BusinessId: 0,
                Name: null,
                Deleted: false,
                CreatedAt: null,
                CreatedByFullName: ""
            },
            items: []
        }
    },
    watch: {

    },
    computed: {

    },
    methods: {
        getDetails: function () {
            return apiHandler.GetById("business", this.details.BusinessId).then(response => {
                this.details = response;
            });
        },
        save: function () {
            if (this.details.BusinessId > 0) {
                return apiHandler.Put("business", this.details.BusinessId, this.details).then(response => {
                    if (response && response.Ok) {
                        return window.location = "/datamanagement/business/businesslist"
                    }
                });
            }
            else {
                return apiHandler.Post("business", this.details).then(response => {
                    if (response && response.Ok) {
                        return window.location = "/datamanagement/business/businesslist"
                    }
                });
            }
        }
    },
    created: function () {
        this.details.BusinessId = parseInt(document.getElementById("app").dataset.entityId);
        if (this.details.BusinessId < 1)
            return;
        this.getDetails();
    },
    mounted: function () {

    }
})

