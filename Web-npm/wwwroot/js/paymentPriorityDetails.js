var app = vueContext({
    el: "#app",
    components: {
        'c-check-box': compCheckbox
    },
    data: function () {
        return {
            details: {
                PaymentPriorityId: 0,
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
            return apiHandler.GetById("paymentpriority", this.details.PaymentPriorityId).then(response => {
                this.details = response;
            });
        },
        save: function () {
            if (this.details.PaymentPriorityId > 0) {
                return apiHandler.Put("paymentpriority", this.details.PaymentPriorityId, this.details).then(response => {
                    if (response && response.Ok) {
                        return window.location = "/datamanagement/payment/paymentPriority/paymentPrioritylist"
                    }
                });
            }
            else {
                return apiHandler.Post("paymentpriority", this.details).then(response => {
                    if (response && response.Ok) {
                        return window.location = "/datamanagement/payment/paymentPriority/paymentPrioritylist"
                    }
                });
            }
        }
    },
    created: function () {
        this.details.PaymentPriorityId = parseInt(document.getElementById("app").dataset.entityId);
        if (this.details.PaymentPriorityId < 1)
            return;
        this.getDetails();
    },
})

