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