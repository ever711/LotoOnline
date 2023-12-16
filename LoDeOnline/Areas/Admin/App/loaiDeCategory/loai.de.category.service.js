(function () {
    'use strict';
    angular
        .module('app')
        .factory('LoaiDeCategoryService', LoaiDeCategoryService);

    LoaiDeCategoryService.$inject = ['$resource'];

    function LoaiDeCategoryService($resource) {
        var odataUrl = "/odata/LoaiDeCategory";
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
