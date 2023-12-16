(function () {
    'use strict';
    angular
        .module('app')
        .factory('DanhDeService', DanhDeService);

    DanhDeService.$inject = ['$resource'];

    function DanhDeService($resource) {
        var odataUrl = "/odata/DanhDe";
        return $resource("", {}, {
            query: { method: 'GET', url: odataUrl },
            get: { method: 'GET', params: { key: '@key' }, url: odataUrl + '(:key)' },
            save: { method: 'POST', url: odataUrl },
            update: { method: 'PUT', params: { key: '@key' }, url: odataUrl + '(:key)' },
            remove: { method: 'DELETE', params: { key: '@key' }, url: odataUrl + '(:key)' },
            defaultGet: { method: 'GET', url: odataUrl + '/ODataService.DefaultGet' },
            getLines: { method: 'GET', params: { key: '@key' }, url: odataUrl + '(:key)/Lines' },
            actionInvoiceOpen: { method: 'POST', url: odataUrl + '/ODataService.ActionInvoiceOpen' },
            doKetQua: { method: 'POST', url: odataUrl + '/ODataService.DoKetQua' },
            doKetQuaAll: { method: 'POST', url: odataUrl + '/ODataService.DoKetQuaAll' },
        });
    }
})();
