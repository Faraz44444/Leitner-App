Vue.filter("length20", function (data) {
    if (!data || data == "")
        return data;
    if (data.length < 20)
        return data;
    return data.substring(0, 17) + "...";
});
Vue.filter("length30", function (data) {
    if (!data || data == "")
        return data;
    if (data.length < 30)
        return data;
    return data.substring(0, 27) + "...";
});
Vue.filter("length40", function (data) {
    if (!data || data == "")
        return data;
    if (data.length < 40)
        return data;
    return data.substring(0, 37) + "...";
});
Vue.filter("length50", function (data) {
    if (!data || data == "")
        return data;
    if (data.length < 50)
        return data;
    return data.substring(0, 47) + "...";
});