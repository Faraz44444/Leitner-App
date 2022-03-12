/*
    FILE SIZE FILER
    - COULD EITEHR BE STRICT TO A UNIT SIZE OR AUTOMATICALLY CHOOSE THE SMALLEST 
*/
Vue.filter('fileSize', function (value, toSizeUnit) {
    if (!value) return ''

    var sizeUnits = ["B", "KB", "MB", "GB", "TB"];
    var unitMultiplier;
    if (toSizeUnit) unitMultiplier = sizeUnits.indexOf(toSizeUnit);
    else unitMultiplier = Math.floor(Math.log(value) / Math.log(2) / 10) * 10

    if (unitMultiplier/10 > sizeUnits.length - 1) unitMultiplier = (sizeUnits.length - 1)*10;

    var size = Math.round(value / Math.pow(2, unitMultiplier) * 100) / 100 
    return size + " " + sizeUnits[unitMultiplier / 10]
});