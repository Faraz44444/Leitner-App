var loadHandler = (function () {
    'use strict';

    function tableLoader(tableId) {
        let id = getContentId(tableId);
        let markup = `<tr class="table-loader">
                            <td class="text-center" colspan="100%">
                                <h5 class="p-1 m-0 font-weight-bold">
                                    <i class="fas fa-fw fa-pulse fa-spinner"></i>
                                    Loading...
                                </h5>
                            </td>
                        </tr>`;
        let tbody = $(id).find("tbody").append(markup);
    }
    function removeTableLoader(tableId) {
        let id = getContentId(tableId);
        $(id).find("tbody").find(".table-loader").remove();        
    }

    function addGlobalLoader() {
        $("#globalLoader").show();
    }
    function removeGlobalLoader() {
        $("#globalLoader").hide();
    }
   
    return {
        AddTableLoader: tableLoader,
        RemoveTableLoader: removeTableLoader,
        AddGlobalLoader: addGlobalLoader,
        RemoveGlobalLoader: removeGlobalLoader,
    }

    function getContentId(contentId) {
        if (!contentId)
            return;
        if (contentId.charAt(0) !== "#")
            contentId = "#" + contentId;

        return contentId;
    }
})();