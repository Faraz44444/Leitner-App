var timeout = null;
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
                OrderBy: 2,
                ItemsPerPage: 50,
                CurrentPage: 1,
                TotalPages: 150,
                Loading: false
            },
            items: []
        }
    },
    watch: {
        'filter.Name': function () {
            this.filterChagnedDelayed();
        }
    },
    computed: {

    },
    methods: {
        getList: function () {
            if (this.filter.Loading) return;
            this.filter.Loading = true;
            return apiHandler.Get("business", this.filter).then(response => {
                this.items = response.Items;
                this.filter.TotalPages = response.TotalPages;
                this.filter.Loading = false;
            });
        },
        goToDetails: function (id) {
            window.location = "/datamanagement/business/details/" + id;
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
    },
    mounted: function () {
    }
})

