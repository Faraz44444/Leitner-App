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