Vue.filter("twoDecimals", function (number) {
    if (isNaN(number))
        number = 0;
    number = parseFloat(number);
    var parts = number.toFixed(2).split(".");
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, " ");
    var newNumber = parts.join(".");
    return newNumber;
});




