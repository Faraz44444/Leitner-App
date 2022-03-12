var eventHandler = (function () {
    'use strict';

    var tableHeightEvent = null;

    function calculateTableHeights() {
        if (tableHeightEvent == null)
            tableHeightEvent = document.createEvent("Event");
        tableHeightEvent.initEvent("calcTableMaxHeight", false, true);
        document.dispatchEvent(tableHeightEvent);
    }

    return {
        CalculateTableHeights: calculateTableHeights
    }
})();