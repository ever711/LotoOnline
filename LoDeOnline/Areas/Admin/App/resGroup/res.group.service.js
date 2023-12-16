(function () {
    'use strict';
    angular
        .module('app')
        .factory('ResGroupService', ResGroupService);

    ResGroupService.$inject = ['$resource'];

    function ResGroupService($resource) {
        var odataUrl = "/odata/ResGroup";
        return $resource("", {}, {
            query: { method: 'GET', url: odataUrl },
            get: { method: 'GET', params: { key: '@key' }, url: odataUrl + '(:key)' },
            save: { method: 'POST', url: odataUrl },
            update: { method: 'PUT', params: { key: '@key' }, url: odataUrl + '(:key)' },
            remove: { method: 'DELETE', params: { key: '@key' }, url: odataUrl + '(:key)' },
            defaultGet: { method: 'GET', url: odataUrl + '/ODataService.DefaultGet' },
            getModelAccesses: { method: 'GET', params: { key: '@key' }, url: odataUrl + '(:key)/ModelAccesses' },
            importAccess: { method: 'POST', url: odataUrl + '/ODataService.ImportAccess' },
        });
    }
})();
