(function () {
    'use strict';
    angular
        .module('app')
        .factory('LoaiDeRuleService', LoaiDeRuleService);

    LoaiDeRuleService.$inject = ['$resource'];

    function LoaiDeRuleService($resource) {
        var odataUrl = "/odata/LoaiDeRule";
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
