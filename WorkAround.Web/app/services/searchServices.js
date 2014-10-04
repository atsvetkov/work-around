var searchServices = angular.module('searchServices', ['ngResource']);

searchServices.factory('Positions', ['$resource',
    function ($resource) {
        return {
            search: function (keywords, location) {
                return $resource('api/search', { k: keywords, l: location }, { query: { method: 'GET', isArray: true } });
            }
        };
    }
]);