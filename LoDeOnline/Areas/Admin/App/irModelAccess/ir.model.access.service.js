(function () {
    'use strict';
    angular
        .module('app')
        .factory('IRModelAccessService', IRModelAccessService);

    IRModelAccessService.$inject = ['$resource'];

    function IRModelAccessService($resource) {
        var odataUrl = "/odata/IRModelAccess";
        return $resource("", {}, {
            query: { method: 'GET', url: odataUrl },
            get: { method: 'GET', params: { key: '@key' }, url: odataUrl + '(:key)' },
            save: { method: 'POST', url: odataUrl },
            update: { method: 'PUT', params: { key: '@key' }, url: odataUrl + '(:key)' },
            remove: { method: 'DELETE', params: { key: '@key' }, url: odataUrl + '(:key)' },
            defaultGet: { method: 'GET', url: odataUrl + '/ODataService.DefaultGet' },
        });
    }
})();
