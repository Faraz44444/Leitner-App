//<i class="fas fa-calendar-alt"></i>
Vue.component('date-picker', {
    template: `<input type='text' class="form-control form-control-sm" ref="input" :placeholder="Placeholder"/>`,
    props: {
        value: {
            type: [String, moment, Date],
            validator: val => new Date(val) != "Invalid Date",
            default: null
        },  
        timeOnly: {
            type: [String, Boolean],
            validator: val => ['true', 'false', ''].indexOf((val || "").toString()) > -1,
            default: ()=> false
        },
        dateOnly: {
            type: [String, Boolean],
            validator: val => ['true', 'false', ''].indexOf((val || "").toString()) > -1,
            default: ()=> false
        },
        max: {
            type: [String, moment, Date],
            validator: val => new Date(val) !== "Invalid Date",
        },
        min: {
            type: [String, moment, Date],
            validator: val => new Date(val) !== "Invalid Date",
        },
        disabledDates: {
            type: Array,
            validator: arr => arr.every(val => new Date(val) !== "Invalid Date"),
        },
        showWeekNumber: {
            type: [String, Boolean],
            validator: val => ['true', 'false', ''].indexOf((val || "").toString()) > -1,
            default: ()=> false
        },
        format: {
            type: String, 
            default: () => "DD.MM.YYYY HH:mm",
        },
        modelFormat: {
            type: String,
            default: () => "YYYY-MM-DDTHH:mm:ssZ"
        },
        placeholder: {
            type: String,
        },
        showClear: {
            type: [String, Boolean],
            validator: val => ['true', 'false', ''].indexOf((val || "").toString()) > -1,
            default: ()=> true
        }
    },

    methods: {
        onHide: function () {
            var date = $(this.$refs.input).datetimepicker("date");
            if (date) date = date.format(this.modelFormat);
            this.$emit("input", date);
        },
        repositionModal: function () {
            var modalElm = $(this.$refs.input).siblings(".bootstrap-datetimepicker-widget");
            if (modalElm === undefined) return;

            var offset = modalElm.offset();
            if (offset === undefined) return;

            $("body").append(modalElm);
            modalElm.css("inset", "unset");
            modalElm.css("top", offset.top);
            modalElm.css("left", offset.left);
            modalElm.css("z-index", 9999);
        },
        onClick: function (event) {
            $(this.$refs.input).datetimepicker("show");
            this.repositionModal(event); // reposition the element to the body
        }
    },
    computed: {
        Format: function() {
            if (this.timeOnly && this.dateOnly)
                throw "Both timeOnly and dateOnly cannot be both used at the same time";

            if (this.timeOnly) return "HH:MM";
            else if (this.dateOnly) return "DD.MM.YYYY";
            else return this.format;
        },
        Placeholder: function(){
            return this.placeholder || this.Format;
        },
    },
    watch: {
        'value': {
            deep: true,
            initial: true,
            handler: function () {
                $(this.$refs.input).datetimepicker("date", moment(this.value));
            }
        }
    },
    mounted(){
        $(this.$refs.input).datetimepicker({
            date: this.value,
            format: this.Format,
            showClear: this.showClear,
            calendarWeeks: this.showWeekNumber.toString() == "true",
            disabledDates: this.disabledDates,
            minDate: this.min,
            maxDate: this.max,
            icons: {
                time: 'fas fa-clock',
                date: 'fas fa-calendar-alt',
                up: 'fas fa-chevron-up',
                down: 'fas fa-chevron-down',
                previous: 'fas fa-chevron-left',
                next: 'fas fa-chevron-right',
                today: 'fas fa-calendar-day',
                clear: 'fas fa-trash',
                close: 'fas fa-times'
            },
            //keepOpen: true 
        }).on("dp.hide", this.onHide)
            .on("dp.show", this.repositionModal)
            .on("click", this.onClick);

        
    }
});
