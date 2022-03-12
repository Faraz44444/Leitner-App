var apiService = function (baseApiUrl) {
    'use strict';

    function getById(url, id) {
        return $.getJSON(baseApiUrl + url + '/' + id);
    }

    function getList(url, data = null) {
        return $.getJSON(baseApiUrl + url, data);
    }

    function getLookup(url, data = null) {
        return $.getJSON(baseApiUrl + url + "/lookup", data);
    }

    function postData(url, data = null) {
        return $.post(baseApiUrl + url, data);
    }

    function postAttachmentListData(url, attachmentList = null, request = null, filesRequired = true) {
        // REQUIRED FIELDS IN THE DATA attachmentList: FILE

        if (filesRequired) {
            if (!Array.isArray(attachmentList))
                return;

            if (!attachmentList || attachmentList.length < 1)
                return;
        }
        var formData = new FormData();
        formData.append("Data", JSON.stringify(request));

        if (attachmentList && attachmentList.length > 0) {

            var files = attachmentList.map(x => x.File);

          
            files.forEach(function (file) { formData.append("file", file) })
        }

        var deferred = $.Deferred();

        $.ajax({
            url: baseApiUrl + url,
            type: "POST",
            data: formData,
            processData: false,
            contentType: false,
            success: deferred.resolve,
            error: deferred.reject
        })

        return deferred;
    }

    function postAttachmentData(url, attachment = null, request = null) {
        // REQUIRED FIELDS IN THE ATTACHMENT: FILE 

        if (Array.isArray(attachment))
            return;

        if (!attachment || !attachment.File)
            return;

        var file = attachment.File;

        var formData = new FormData();
        formData.append("Data", JSON.stringify(request));
        formData.append("file", file);

        var deferred = $.Deferred();

        $.ajax({
            url: baseApiUrl + url,
            type: "POST",
            data: formData,
            processData: false,
            contentType: false,
            success: deferred.resolve,
            error: deferred.reject
        })

        return deferred;
    }

    return {
        GetById: getById,
        GetList: getList,
        GetLookup: getLookup,
        PostRequest: postData,
        PostAttachmentList: postAttachmentListData,
        PostAttachment: postAttachmentData,
    }
};

supplierApiService = apiService("/supplierApi/");
apiService = apiService("/api/");