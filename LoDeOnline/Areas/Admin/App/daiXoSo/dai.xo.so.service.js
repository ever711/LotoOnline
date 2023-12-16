(function () {
    'use strict';
    angular
        .module('app')
        .factory('DaiXoSoService', DaiXoSoService);

    DaiXoSoService.$inject = ['$resource'];

    function DaiXoSoService($resource) {
        var odataUrl = "/odata/DaiXoSo";
        return $resource("", {}, {
            query: { method: 'GET', url: odataUrl },
            get: { method: 'GET', params: { key: '@key' }, url: odataUrl + '(:key)' },
            save: { method: 'POST', url: odataUrl },
            update: { method: 'PUT', params: { key: '@key' }, url: odataUrl + '(:key)' },
            remove: { method: 'DELETE', params: { key: '@key' }, url: odataUrl + '(:key)' },
            defaultGet: { method: 'GET', url: odataUrl + '/ODataService.DefaultGet' },
            getRules: { method: 'GET', params: { key: '@key' }, url: odataUrl + '(:key)/Rules' },
        });
    }
})();
