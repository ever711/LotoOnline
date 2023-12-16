(function () {
    'use strict';
    angular
        .module('app')
        .factory('AccountPaymentService', AccountPaymentService);

    AccountPaymentService.$inject = ['$resource'];

    function AccountPaymentService($resource) {
        var odataUrl = "/odata/AccountPayment";
        return $resource("", {}, {
            query: { method: 'GET', url: odataUrl },
            get: { method: 'GET', params: { key: '@key' }, url: odataUrl + '(:key)' },
            save: { method: 'POST', url: odataUrl },
            update: { method: 'PUT', params: { key: '@key' }, url: odataUrl + '(:key)' },
            remove: { method: 'DELETE', params: { key: '@key' }, url: odataUrl + '(:key)' },
            defaultGet: { method: 'POST', url: odataUrl + '/ODataService.DefaultGet' },
            actionPost: { method: 'POST', url: odataUrl + '/ODataService.ActionPost' },
            unlink: { method: 'POST', url: odataUrl + '/ODataService.Unlink' },
        });
    }
})();
