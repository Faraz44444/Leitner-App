var app = vueContext({
    el: "#app",
    components: {
        'c-check-box': compCheckbox
    },
    data: function () {
        return {
            filter: {
                Name: "",
                OrderByDirection: "Asc",
                OrderBy: "Name",
                ItemsPerPage: 50,
                CurrentPage: 1,
                TotalPages: 150,
                Loading: false
            },
            items: [],
            priorities: []
        }
    },
    watch: {
        'filter.Name': function () {
            this.filterChagnedDelayed();
        },
        'filter.Priority': function () {
            this.filterChanged();
        }
    },
    computed: {

    },
    methods: {
        getList: function () {
            if (this.filter.Loading) return;
            this.filter.Loading = true;
            return apiHandler.Get("category", this.filter).then(response => {
                this.items = response.Items;
                this.filter.TotalPages = response.TotalPages;
                this.filter.Loading = false;
            });
        },
        goToDetails: function (id) {
            window.location = "/datamanagement/category/details/" + id;
        },
        getCategoryPriorities: function () {
            return apiHandler.Get("category/priorities", this.details).then(response => {
                this.priorities = response;
            });
        },
        filterChanged: function () {
            this.filter.CurrentPage = 1;
            this.getList();
        },
        filterChagnedDelayed: function () {
            clearTimeout(timeout);
            timeout = setTimeout(() => {
                this.filterChanged();
            }, 200);
        }
    },
    created: function () {
        this.getList();
        this.getCategoryPriorities();
    },
    mounted: function () {
    }
})

