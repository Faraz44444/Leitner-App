const dateStyle = "short";
const yearStyle = "2-digit";
const monthStyle = "2-digit";
const dayStyle = "2-digit";
const hourStyle = "2-digit";
const minuteStyle = "2-digit";
const timeStyle = "short";
const hour12 = false;
const hourCycle = "h24";
const secondStyle = "2-digit";

const utcSupport = false;
const weekStartsOn = 'Monday';

function GetLanguage() {
    if (utcSupport) {
        if (navigator && navigator.language)
            return navigator.language;
    }
    return "no";
}

function IsDate(d) {
    if (d instanceof Date)
        if (typeof d.getMonth === 'function' && !isNaN(d.getTime()))
            return true;
    return false;
}

function ToLocaleString(date) {
    if (!date)
        return "";

    var d = new Date(date);
    if (IsDate(d))
        return d.toLocaleString(GetLanguage(), { year: yearStyle, month: monthStyle, day: dayStyle, hour12: hour12, hourCycle: hourCycle, hour: hourStyle, minute: minuteStyle });
}

function ToLocaleStringWithSeconds(date) {
    if (!date)
        return "";

    var d = new Date(date);
    if (IsDate(d))
        return d.toLocaleString(GetLanguage(), { year: yearStyle, month: monthStyle, day: dayStyle, hour12: hour12, hourCycle: hourCycle, hour: hourStyle, minute: minuteStyle, second: secondStyle });
}

function ToLocaleDateString(date) {
    if (!date)
        return "";

    var d = new Date(date);
    if (IsDate(d))
        return d.toLocaleDateString(GetLanguage(), { dateStyle: dateStyle });
}

function FormatYYYYddMM(date) {
    let d = new Date(date);

    if (!IsDate(d))
        return "";

    let yyyy = d.getFullYear();
    let mm = d.getMonth();
    let dd = d.getDate();

    let mmFormatted = "";
    if ((mm + 1) > 9)
        mmFormatted = (mm + 1).toString();
    else
        mmFormatted = "0" + (mm + 1).toString();

    let ddFormatted = "";
    if (dd > 9)
        ddFormatted = dd.toString();
    else
        ddFormatted = "0" + dd.toString();

    return yyyy + "-" + mmFormatted + "-" + ddFormatted;
}

function FormatHHmm(date) {
    let d = new Date(date);

    if (!IsDate(d))
        return "";

    let hh = d.getHours();
    let mm = d.getMinutes();

    let hhFormatted = "";
    if (hh > 9)
        hhFormatted = hh.toString();
    else
        hhFormatted = "0" + hh.toString();

    let mmFormatted = "";
    if (mm > 9)
        mmFormatted = mm.toString();
    else
        mmFormatted = "0" + mm.toString();

    return hhFormatted + ":" + mmFormatted;
}

function FormatHHmmss(date) {
    let d = new Date(date);

    if (!IsDate(d))
        return "";

    let hh = d.getHours();
    let mm = d.getMinutes();
    let ss = d.getSeconds();

    let hhFormatted = "";
    if (hh > 9)
        hhFormatted = hh.toString();
    else
        hhFormatted = "0" + hh.toString();

    let mmFormatted = "";
    if (mm > 9)
        mmFormatted = mm.toString();
    else
        mmFormatted = "0" + mm.toString();

    let ssFormatted = "";
    if (ss > 9)
        ssFormatted = ss.toString();
    else
        ssFormatted = "0" + ss.toString();

    return hhFormatted + ":" + mmFormatted + ":" + ssFormatted;
}

function FormatYYYYddMMHHmm(date) {
    let dateFormatted = FormatYYYYddMM(date);
    let timeFormatted = FormatHHmm(date);

    return dateFormatted + "T" + timeFormatted;
}

(function () {
    let days = ["Sunday", "Monday", "Thuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
    let months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    let weekName = "Week";

    // NORWEGIAN
    //days = ["Søndag", "Mandag", "Tirsdag", "Onsdag", "Torsdag", "Fredag", "Lørdag"];
    //months = ["Januar", "Februar", "Mars", "April", "Mai", "Juni", "July", "August", "September", "Oktober", "November", "Desember"];
    //weekName = "Uke";

    const dayOffset = days.indexOf(weekStartsOn);
    if (dayOffset == -1) throw "[date.js]::" + weekStartsOn + " is not a valid day";

    function getWeekNumber(date) {
        var date = new Date(date.getTime());
        date.setHours(0, 0, 0, 0);

        // Thursday in current week decides the year.
        date.setDate(date.getDate() + 3 - (date.getDay() + 6) % 7);

        // January 4 is always in week 1.
        var week1 = new Date(date.getFullYear(), 0, 4);

        // Adjust to Thursday in week 1 and count number of weeks from date to week1.
        return 1 + Math.round(((date.getTime() - week1.getTime()) / 864e5 - 3 + (week1.getDay() + 6) % 7) / 7);
    }

    function getDatesInWeek(date) {
        if (date.FirstDateOfWeek >= date.LastDateOfWeek) throw "[Date.js]::Internal error";

        var response = Array(7).fill(date.FirstDateOfWeek);
        return response.map((d, i) => new Date(new Date(d).addHours(24 * i)));
    }

    var dateDetails = {
        DateTime: { get: function () { return new Date(this.getTime() - (this.getTimezoneOffset() * 6e4)).toJSON().substr(0, 19); } },

        Year: { get: function () { return this.getFullYear(); } },

        Month: { get: function () { return this.getMonth(); } },
        MonthText: { get: function () { return months[this.getMonth()]; } },
        MonthShortText: { get: function () { return months[this.getMonth()].substr(0, 3); } },

        Week: { get: function () { return getWeekNumber(this); } },
        WeekText: { get: function () { return weekName + " " + getWeekNumber(this); } },

        Day: { get: function () { return this.getDate(); } },
        DayOfWeek: { get: function () { return days[this.getDay()] } },
        DayOfWeekText: { get: function () { return days[this.getDay()] } },
        DayOfWeekShortText: { get: function () { return days[this.getDay()].substr(0, 3) } },

        EndOfDay: { get: function () { return new Date(this.getFullYear(), this.getMonth(), this.getDate(), 23, 59, 59); } },
        StartOfDay: { get: function () { return new Date(this.getFullYear(), this.getMonth(), this.getDate(), 0, 0, 0); } },
        FirstDateOfWeek: { get: function () { return new Date(this.getFullYear(), this.getMonth(), this.getDate() - (this.getDay() < dayOffset ? 7 - dayOffset : this.getDay() - dayOffset)); } },
        LastDateOfWeek: { get: function () { return new Date(this.getFullYear(), this.getMonth(), this.getDate() + 6 - (this.getDay() < dayOffset ? 7 - dayOffset : this.getDay() - dayOffset)); } },

        Date: { get: function () { return FormatYYYYddMM(this) }, set: function (val) { this.setFullYear(val.split("-")[0]); this.setMonth(val.split("-")[1] - 1); this.setDate(val.split("-")[2]); } },
        Time: { get: function () { return FormatHHmm(this) }, set: function (val) { this.setHours(val.split(":")[0]); this.setMinutes(val.split(":")[1]); } },

        //HasTimeFrame : 
        //TimeFrameEnd :
        //TimeFrameStart :

        DatesInWeek: { get: function () { return getDatesInWeek(this); } },
    }

    Object.defineProperties(Date.prototype, dateDetails);

    Date.prototype.GetDetails = function () {
        return Object.keys(dateDetails).reduce((obj, key) => Object.assign(obj, { [key]: this[key] }), {});
    }

    Date.prototype.addHours = function (hours) {
        if (isNaN(hours * 1)) throw "[Date.addHours]::hours is not a number";
        if (hours == 0) return this;

        var _sign = hours / Math.abs(hours);
        var _hours = Math.floor(Math.abs(hours)) * _sign;
        var _minutes = Math.floor(Math.abs(hours) % 1 * 60) * _sign;
        var _seconds = Math.floor((Math.abs(hours) % 1 * 60) % 1 * 60) * _sign;
        var _milliseconds = Math.floor(((Math.abs(hours) % 1 * 60) % 1 * 60) % 1 * 60) * _sign;

        this.setHours(
            this.getHours() + _hours,
            this.getMinutes() + _minutes,
            this.getSeconds() + _seconds,
            this.getMilliseconds() + _milliseconds,
        );

        return this;
    }

    Date.prototype.addDays = function (days) {
        if (isNaN(days * 1)) throw "[Date.addDays]::hours is not a number";
        return new Date(this.setDate(this.getDate() + days));
    }

    Date.prototype.daysUntil = function (toDate) {
        if (!IsDate(toDate)) throw "[Date.daysUntil]::toDate is not a date"
        return Math.round((toDate - this) / (24 * 36e5))
    }
    Date.prototype.weeksUntil = function (toDate) {
        if (!IsDate(toDate)) throw "[Date.weeksUntil]::toDate is not a date"
        return Math.round((toDate - this) / (7 * 24 * 36e5))
    }

    Date.prototype.numberOfWeekInYear = function () {
        return Math.floor((new Date(this.getFullYear() + 1, 0, 0) - new Date(this.getFullYear(), 0, 1)) / (7 * 24 * 36e5));
    }

    Date.prototype.getStartOfWeek = function (week) {
        var simple = new Date(this.getFullYear(), 0, 1 + (week - 1) * 7);
        var dow = simple.getDay();
        var ISOweekStart = simple;
        if (dow <= 4)
            ISOweekStart.setDate(simple.getDate() - simple.getDay() + 1);
        else
            ISOweekStart.setDate(simple.getDate() + 8 - simple.getDay());
        return ISOweekStart;
    }

    Date.prototype.IsBetween = function (fromDate, toDate, strict = false) {
        fromDate = new Date(fromDate);
        toDate = new Date(toDate);

        return strict
            ? fromDate < this && this < toDate
            : fromDate <= this && this <= toDate
    }
})();



//function ToISOString(date) {
//    if (!date)
//        return null;

//    var d = new Date(date);
//    if (IsDate(d)) {
//        if (utcSupport)
//            return d.toISOString();
//        return ToLocaleIsoString(d);
//    }
//}

//function ToStartOfDayISOString(date) {
//    if (!date)
//        return null;

//    var d = new Date(date);
//    d.setHours(0);
//    d.setMinutes(0);
//    d.setSeconds(0);
//    if (IsDate(d)) {
//        if (utcSupport)
//            return d.toISOString();
//        return ToLocaleIsoString(d);
//    }
//}

//function ToEndOfDayISOString(date) {
//    if (!date)
//        return null;

//    var d = new Date(date);
//    d.setHours(23);
//    d.setMinutes(59);
//    d.setSeconds(59);
//    if (IsDate(d)) {
//        if (utcSupport)
//            return d.toISOString();
//        return ToLocaleIsoString(d);
//    }
//}

//function ToLocaleIsoString(date) {
//    var tzo = -date.getTimezoneOffset(),
//        dif = tzo >= 0 ? '+' : '-',
//        pad = function (num) {
//            var norm = Math.floor(Math.abs(num));
//            return (norm < 10 ? '0' : '') + norm;
//        };

//    return date.getFullYear() +
//        '-' + pad(date.getMonth() + 1) +
//        '-' + pad(date.getDate()) +
//        'T' + pad(date.getHours()) +
//        ':' + pad(date.getMinutes()) +
//        ':' + pad(date.getSeconds());
//}