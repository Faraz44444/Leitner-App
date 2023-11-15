var app = vueContext({
    el: "#app",
    components: {
        'c-check-box': compCheckbox
    }, data: function () {
        return {
            details: {
                RoleId: 0,
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
            return apiHandler.GetById("role", this.details.RoleId).then(response => {
                this.details = response;
            });
        },
        save: function () {
            if (this.details.RoleId > 0) {
                return apiHandler.Put("role", this.details.RoleId, this.details).then(response => {
                    if (response && response.Ok) {
                        return window.location = "/admin/account/role/rolelist"
                    }
                });
            }
            else {
                return apiHandler.Post("role", this.details).then(response => {
                    if (response && response.Ok) {
                        return window.location = "/admin/account/role/rolelist"
                    }
                });
            }
        }
    },
    created: function () {
        this.details.RoleId = parseInt(document.getElementById("app").dataset.entityId);
        if (this.details.RoleId < 1)
            return;
        this.getDetails();
    },
    mounted: function () {

    }
})

