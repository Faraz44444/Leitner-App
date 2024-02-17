var timeout = null;
var app = vueContext({
    el: "#app",
    data: function () {
        return {
            batchFilter: {
                Step: 5,
            },
            materialFilter: {
                BatchId: null
            },
            boxes: [
                {
                    Id: 1,
                    Name: "Box 1"
                },
                {
                    Id: 2,
                    Name: "Box 2"
                },
                {
                    Id: 3,
                    Name: "Box 3"
                },
                {
                    Id: 4,
                    Name: "Box 4"
                },
                {
                    Id: 5,
                    Name: "Box 5"
                }
            ],
            batches: [],
            materials: [],
            reviewingMaterials: [],
            items: [],
            isFlipped: false
        }

    },
    watch: {
        'batchFilter.Step': function () {
            this.getBatches();
        },
        'materialFilter.BatchId': function () {
            this.getMaterials();
        },
    },
    computed: {

    },
    methods: {
        getBatches: function () {
            return apiHandler.Get("material/lookup", this.batchFilter).then(response => {
                response = response.map(x => { x.showAnswer = false; return x; })
                this.materials = response.reduce((accumulator, item) => {
                    (accumulator[item.BatchId] = accumulator[item.BatchId] || []).push(item);
                    return accumulator;
                }, {})
                for (let key in this.materials) {
                    this.batches.push({ Id: key, Name: this.materials[key][0].BatchNo })
                }
            })
        },
        getMaterials: function () {
            this.reviewingMaterials = this.materials[this.materialFilter.BatchId]
        },
        increaseStep: function (material) {
            if (material.Loading) return;
            material.Loading = true
            return apiHandler.Put("material/increaseStep", material.MaterialId, material).then(() => {
                material.Loading = false
            });
        },
        decreaseStep: function (material) {
            if (material.Loading) return;
            material.Loading = true
            return apiHandler.Put("material/decreaseStep", material.MaterialId, material).then(() => {
                material.Loading = false
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
        },
        fetchPage: function (event) {
            this.filter.CurrentPage = event.page;
            this.getList();
        },
        updateModelDatepickerValue: function (event) {
            this.paymentDetails.Date = event
        },
        setOrderByDirection: function (event) {
            this.filter.OrderByDirection = event;
        },
        setOrderBy: function (event) {
            this.filter.OrderBy = event;
        }
    },
    created: function () {
    },
    mounted: function () {
    }
})
