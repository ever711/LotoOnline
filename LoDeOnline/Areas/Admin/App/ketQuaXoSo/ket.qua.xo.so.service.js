(function () {
    'use strict';
    angular
        .module('app')
        .factory('KetQuaXoSoService', KetQuaXoSoService);

    KetQuaXoSoService.$inject = ['$resource'];

    function KetQuaXoSoService($resource) {
        var odataUrl = "/odata/KetQuaXoSo";
        return $resource("", {}, {
            query: { method: 'GET', url: odataUrl },
            get: { method: 'GET', params: { key: '@key' }, url: odataUrl + '(:key)' },
            save: { method: 'POST', url: odataUrl },
            update: { method: 'PUT', params: { key: '@key' }, url: odataUrl + '(:key)' },
            remove: { method: 'DELETE', params: { key: '@key' }, url: odataUrl + '(:key)' },
            getLines: { method: 'GET', params: { key: '@key' }, url: odataUrl + '(:key)/Lines' },
            layKetQua: { method: 'POST', url: odataUrl + '/ODataService.LayKetQua' },
        });
    }
})();
