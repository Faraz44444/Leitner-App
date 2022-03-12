var dataManipulation = (function () {
    'use strict';

    var numberFormatter = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD',
    });

    function groupBy(array, key) {
        const result = {}
        array.forEach(item => {
            if (!result[item[key]]) {
                result[item[key]] = []
            }
            result[item[key]].push(item)
        })
        return result
    }

    function convertToDictionary(list) {
        return Object.keys(list).map(function (key) {

            return {
                Key: key,
                Value: list[key]
            }
            // key: the name of the object key
         
        });
    }


    return {
        GroupBy: groupBy,
        ConvertToDictionary: convertToDictionary
    }

    
})();