console.log("Hello from JS");
const app = Vue.createApp({
    data: function () {
        return {
            message: "Hello From Vue",
            filter: {
                OrderByDirection: "Asc",
                OrderBy: 1,
                ItemsPerPage: 50,
                CurrentPage: 1,
                TotalPages: 150,
                Loading: false
            },
            items: []
        }
    },
    watch: {

    },
    computed: {

    },
    methods: {
        getList: function () {
            if (this.filter.Loading) return;
            this.filter.Loading = true;
            return apiHandler.Get("user", this.filter).then(response => {
                this.items = response;
                this.filter.TotalPages = response.TotalPages;
                this.filter.Loading = false;
            });
        }
    },
    created: function () {
        this.getList();
    },
    mounted: function () {
    }
})

app.mount("#app");