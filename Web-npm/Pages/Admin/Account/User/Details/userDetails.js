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
            return apiHandler.GetById("user", this.details.UserId).then(response => {
                this.details = response;
            });
        },
        save: function () {
            if (this.details.UserId > 0) {
                return apiHandler.Put("user", this.details.UserId, this.details).then(response => {
                    if (response && response.Ok) {
                        return window.location = "/admin/account/user/userlist"
                    }
                });
            }
            else {
                return apiHandler.Post("user", this.details).then(response => {
                    if (response && response.Ok) {
                        return window.location = "/admin/account/user/userlist"
                    }
                });
            }
        }
    },
    created: function () {
        this.details.UserId = parseInt(document.getElementById("app").dataset.entityId);
        if (this.details.UserId < 1)
            return;
        this.getDetails();
    },
    mounted: function () {

    }
})

