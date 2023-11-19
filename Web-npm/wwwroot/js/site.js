function ApiError(message, stackTrace) {
    var instance = new Error(message);
    instance.name = 'ApiError';
    instance.StackTrace = stackTrace;

    Object.setPrototypeOf(instance, Object.getPrototypeOf(this));
    if (Error.captureStackTrace) {
        Error.captureStackTrace(instance, ApiError);
    }
    return instance;
}

ApiError.prototype = Object.create(Error.prototype, {
    constructor: {
        value: Error,
        enumerable: false,
        writable: true,
        configurable: true
    }
});

if (Object.setPrototypeOf) {
    Object.setPrototypeOf(ApiError, Error);
} else {
    ApiError.__proto__ = Error;
}


var apiHandler = (function () {
    const apiUrl = "/api/";

    async function getById(url = '', id, data = {}) {
        var response = await getData(url + "/" + id, data);
        return response;
    }

    async function getData(url = '', data = {}) {
        var response = await fetchData(url, data, 'GET');
        return response;
    }

    async function postData(url = '', data = {}) {
        var response = await fetchData(url, data, 'POST');
        return response;
    }

    async function postFormData(url = '', data = {}) {
        var response = await fetchFormData(url, data, 'POST');
        return response;
    }


    async function putData(url = '', id = 0, data = {}, urlExtra = '') {
        if (id > 0)
            url = url + "/" + id
        if (urlExtra != '')
            url = url + '/' + urlExtra;
        var response = await fetchData(url, data, 'PUT');
        return response;
    }

    async function deleteData(url = '', id, data = {}) {
        var response = await fetchData(url + "/" + id, data, 'DELETE');
        return response;
    }

    function downloadFile(url, data = {}) {
        if (Object.keys(data || {}).length > 0)
            url += "?" + convertToQueryString(data);

        window.open(apiUrl + url);
    }

    return {
        GetById: getById,
        Get: getData,
        Post: postData,
        PostFormData: postFormData,
        Put: putData,
        Delete: deleteData,
        DownloadFile: downloadFile,

        ConvertToQueryString: convertToQueryString,
    }

    async function parseResponse(response) {
        if (!response.ok) {
            let exception;

            try {
                exception = await response.clone().json()
            }
            catch (error) {
                throw new ApiError("En feil har oppstått.");
            }

            throw new ApiError(exception.ExceptionMessage, exception.ExceptionStackTrace);
        }
        else {
            return response.clone().json().catch(() => response.text());
        }
    }

    function formatResponse(dto) {
        if (typeof dto === 'object' && dto !== null) {
            if ('ExceptionMessage' in dto && !dto.IsComplexWarning) {
                throw new ApiError(dto.ExceptionMessage, dto.ExceptionStackTrace);
            }
        }
        return dto;
    }
    function handleException(error) {
        var message = error.message;

        if (error.StackTrace)
            message += `<pre class="mt-3 bg-gray-1 p-3 rounded">${error.StackTrace}</pre>`

        feedbackHandler.ShowError("", message);
    }

    async function fetchData(url = '', data = {}, verb = '') {
        // Default options are marked with *
        let request = {
            method: verb, // *GET, POST, PUT, DELETE, etc.
            mode: 'cors', // no-cors, *cors, same-origin
            cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
            credentials: 'same-origin', // include, *same-origin, omit
            headers: {
                'Content-Type': 'application/json'
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
            redirect: 'follow', // manual, *follow, error
            referrerPolicy: 'no-referrer' // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url            
        };


        if (data) {
            if (verb === 'GET')
                url += "?" + convertToQueryString(data);
            else
                request.body = JSON.stringify(data) // body data type must match "Content-Type" header
        }


        return await fetch(apiUrl + url, request)
            .then(parseResponse)
            .then(formatResponse)
            .catch(handleException);
    }

    async function fetchFormData(url = '', data = {}, verb = '') {
        // Default options are marked with *
        let request = {
            method: verb, // POST, PUT, DELETE, etc.
            mode: 'cors', // no-cors, *cors, same-origin
            cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
            credentials: 'same-origin', // include, *same-origin, omit
            redirect: 'follow', // manual, *follow, error
            referrerPolicy: 'no-referrer', // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url    
            body: convertToFormData(data)
        };


        return await fetch(apiUrl + url, request)
            .then(parseResponse)
            .then(formatResponse)
            .catch(handleException);
    }

    function convertToQueryString(json) {
        return convertTo(new URLSearchParams(), json).toString();
    }
    function convertToFormData(json) {
        return convertTo(new FormData(), json);
    }

    function convertTo(container, json) {
        if (typeof (json) != 'object') throw "the data must be an object";
        function parse(data, path = "") {
            for (var key in data) {
                var newPath = key;
                if (path && Array.isArray(data)) newPath = `${path}[${key}]`;
                else if (path) newPath = `${path}.${key}`;

                if (typeof (data[key]) == 'string') container.append(newPath, data[key]);
                else if (typeof (data[key]) == 'boolean') container.append(newPath, data[key]);
                else if (typeof (data[key]) == 'number') container.append(newPath, data[key]);
                else if (IsDate(data[key])) container.append(newPath, data[key].toJSON());
                else if ('File' in window && data[key] instanceof File) container.append(newPath, data[key]);
                else if (Array.isArray(data[key])) parse(data[key], newPath);
                else if (typeof (data[key]) === 'object') parse(data[key], newPath);
            }
        }

        parse(json);
        return container;
    }

})();
var feedbackHandler = (() => {

    const toastContainer = document.getElementById("toastContainer");

    function showError(title = '', message = '') {
        if (!title || title.trim() == '')
            title = 'Oops!'
        if (!message || message.trim() == '')
            message = 'Noe gikk galt!'

        let modal= document.getElementById('errorModal');
        let modalTitle = document.getElementById('errorModalTitle');
        let modalMessage = document.getElementById('errorModalMessage');
        modalTitle.innerHTML = title;
        modalMessage.innerHTML = message;
        modal.style.display = '';
    }

    function showMessage(title = '', message = '') {
        if (!title || title.trim() == '')
            title = 'Obs!'
        if (!message || message.trim() == '')
            return;

        let modalTitle = document.getElementById('messageModalTitle');
        let modalMessage = document.getElementById('messageModalMessage');
        modalTitle.innerHTML = title;
        modalMessage.innerHTML = message;

        messageModal.toggle();
    }

    const toastTemplate = `<div class="d-flex">
                                <div class="toast-body">
                                    <h5>{content}</h5>
                                </div>
                                <button type="button" class="btn-close me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
                            </div>`;

    function showToast(message = '') {
        let id = 'id' + (new Date()).getTime();

        let toast = document.createElement('div');
        toast.setAttribute("class", "toast align-items-center bg-white border-danger border-4 rounded-0 border-top-0 border-end-0 border-bottom-0");
        toast.setAttribute("role", "alert");
        toast.setAttribute("aria-live", "assertive");
        toast.setAttribute("aria-atomic", "true");
        toast.setAttribute("data-bs-delay", "2500");
        toast.setAttribute("id", id);

        let toastContent = toastTemplate.replace("{content}", message);

        toast.innerHTML = toastContent;
        toastContainer.appendChild(toast);

        var toastEl = new bootstrap.Toast(toast, {});
        toastEl.show();
    }

    return {
        ShowError: showError,
        ShowMessage: showMessage,
        ShowToast: showToast
    }
})();
var orderByHandler = (function () {
    'use strict';

    function handle(filter, key) {
        filter.CurrentPage = 1;
        if (filter.OrderBy === key) {
            if (filter.OrderByDirection === 1) {
                filter.OrderByDirection = 2;
            } else {
                filter.OrderByDirection = 1;
            }            
            return filter;
        }
        filter.OrderBy = key;
        filter.OrderByDirection = 1;
         
        return filter;
    }

    return {
        Handle: handle
    }
})();
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
var formvalidation = (function () {
    'use strict';

    function validate(formId) {
        var valid = true;

        $(getFormId(formId)).find("input, select, textarea").each(function (idx, el) {
            $(el).removeClass("input-error");

            if (!$(el)[0].checkValidity()) {
                $(el).addClass("input-error");
                valid = false;
            }
            else if ($(el).is("select[required]") && el.value == 0) {
                $(el).addClass("input-error");
                valid = false;
            }


        });

        return valid;
    }
    function removeInputErrorClass(formId) {
        $(getFormId(formId)).find("input, select, textarea").each(function (idx, el) {
            $(el).removeClass("input-error");
        });
    }

    function getFormId(formId) {
        if (!formId)
            return;
        if (formId.charAt(0) !== "#")
            formId = "#" + formId;

        return formId;
    }

    return {
        Validate: validate,
        removeInputErrorClass: removeInputErrorClass
    }
})();
const template = document.createElement('template');
template.innerHTML = `
    <style>
        h3 {
            color: coral;
        }   
    </style>
    <div class="info">
        <h4><slot name="name"/></h4>
    </div>
    <button type="button" id="toggle-info">Hide Info</button>
`
class MyUsername extends HTMLElement {
    constructor() {
        // Constructor of HTMLElement, the extending class
        super();

        this.showInfo = true;


        // makes a shodow dom which means whatever is inside the component is in a separate dom
        // the outside will not effect our component and our component won't effect the outside
        this.attachShadow({ mode: 'open' });
        // appending the defined element to the created shadow dom
        this.shadowRoot.appendChild(template.content.cloneNode(true));
        //this.shadowRoot.querySelector('h3').innerText = this.getAttribute('name');
    }
    toggleInfo() {
        this.showInfo = !this.showInfo;

        const info = this.shadowRoot.querySelector(".info");
        const toggleBtn = this.shadowRoot.querySelector("#toggle-info");

        if (this.showInfo) {
            info.style.display = 'block';
            toggleBtn.innerHTML = 'Hide Info';
        }
        else {
            info.style.display = 'none';
            toggleBtn.innerHTML = 'Show Info';
        }
    }
    // Called every time the element is inserted into the DOM
    connectedCallback() {
        this.shadowRoot.querySelector('#toggle-info').addEventListener('click', () => this.toggleInfo());
    }
    // Called every time the element is removed from the DOM
    disconnectedCallback() {
        this.shadowRoot.querySelector('#toggle-info').removeEventListener();
    }
}

window.customElements.define('my--username', MyUsername)
function vueContext(options) {
    if (!options.el) throw "[VueContext]::options.el is not set";

    var app = Vue.createApp(options);

    app.config.unwrapInjectedRef = true;
    app.config.compilerOptions.isCustomElement = (tag) => tag.includes('--')

    //Global Mixins
    //app.mixin(dateMixin);

    //Global Components
    app.component('c-checkbox', compCheckbox);
    app.component('c-section', compSection);
    app.component('c-input', compInput);
    app.component('c-select', compSelect);
    app.component('c-button', compButton)
    app.component('c-modal', compModal)
    app.component('c-datepicker', compDatepicker)
    app.component('c-table', compTable)
    app.component('c-dropdown', compDropdown)
    app.component('c-th', compTH)
    app.component('c-td', compTD)
    app.component('c-tr', compTR)

    return app.mount(options.el);
}