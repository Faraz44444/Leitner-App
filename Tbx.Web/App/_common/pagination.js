var pagination = (function () {
    'use strict';

    var scrollEventListener = null;
    var elementsToSee = [];


    function infiniteScroll(tableId, pagedResponseDto, nextPageCallback, loading) {
        tableId = getId(tableId);
        let totalPages = pagedResponseDto.TotalPages;
        let currentPage = pagedResponseDto.CurrentPage;

        let tableFooter = $(getId(tableId)).find("tfoot")
        let elementInArray = elementsToSee.find(function (x) { x.element == tableFooter });
        if (currentPage >= totalPages) {
            if (elementsToSee != null && elementInArray != null)
                elementsToSee.splice(elementsToSee.indexOf(elementInArray), 1);
            return;
        }

        if (isScrolledIntoView(tableFooter) && !loading) {
            if (elementsToSee != null && elementInArray != null)
                elementsToSee.splice(elementsToSee.indexOf(elementInArray), 1);
            nextPageCallback();
            return;
        }

        if (elementInArray == null)
            elementsToSee.push({ element: tableFooter, callBack: nextPageCallback });

        if (scrollEventListener == null) {
            scrollEventListener = document.addEventListener('scroll', function (e) {
                for (var i = 0; i < elementsToSee.length; i++) {
                    if (isScrolledIntoView(elementsToSee[i].element)) {
                        let callback = elementsToSee[i].callBack;
                        elementsToSee.splice(elementsToSee.indexOf(elementsToSee[i]), 1);
                        callback();
                    }
                }
            }, true);
        }
    }


    function infiniteScrollBottomPage(nextPageCallBack) {
        document.addEventListener("scroll", function (event) {
            scrollBottom(nextPageCallBack);
        });
    }

    return {
        InfiniteScroll: infiniteScroll,
        InfiniteScrollBottomPage: infiniteScrollBottomPage
    }

    function isScrolledIntoView(element) {
        var docViewTop = $(window).scrollTop();
        var docViewBottom = docViewTop + $(window).height();

        var elemTop = $(element).offset().top;
        var elemBottom = elemTop + $(element).height();

        return ((elemBottom <= docViewBottom) && (elemTop >= docViewTop));
    }

    function getId(formId) {
        if (!formId)
            return;
        if (formId.charAt(0) !== "#")
            formId = "#" + formId;

        return formId;
    }

    function scrollBottom(nextPageCallBack) {

        if ($(window).scrollTop() >= $(document).height() - $(window).height()) {

            nextPageCallBack();
        }
    };

})();