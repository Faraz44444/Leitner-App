Vue.filter("momentWithHour", function (date, nullableMessage = "") {
    if (date == undefined) {
        return nullableMessage;
    }

    var date = moment(date);
    if (date.year() < 1900)
        return nullableMessage

    return date.format('DD.MM.YYYY HH:mm');
});
Vue.filter("moment", function (date, nullableMessage = "") {
    if (date == undefined) {
        return nullableMessage;
    }
    var date = moment(date);
    if (date.year() < 1900)
        return nullableMessage

    return date.format('DD.MM.YYYY');
});
Vue.filter("shortMomentWithHour", function (date, nullableMessage = "") {
    if (date == undefined) {
        return nullableMessage;
    }
    var date = moment(date);
    if (date.year() < 1900)
        return nullableMessage

    return date.format('DD.MM.YY HH:mm');
});
Vue.filter("shortMoment", function (date, nullableMessage = "") {
    if (date == undefined) {
        return nullableMessage;
    }
    var date = moment(date);
    if (date.year() < 1900)
        return nullableMessage

    return date.format('DD.MM.YY');
});