(function () {
    'use strict';
    angular
        .module('app')
        .factory('AccountJournalService', AccountJournalService);

    AccountJournalService.$inject = ['$resource'];

    function AccountJournalService($resource) {
        var odataUrl = "/odata/AccountJournal";
        return $resource("", {}, {
            query: { method: 'GET', url: odataUrl },
            get: { method: 'GET', params: { key: '@key' }, url: odataUrl + '(:key)' },
            save: { method: 'POST', url: odataUrl },
            update: { method: 'PUT', params: { key: '@key' }, url: odataUrl + '(:key)' },
            remove: { method: 'DELETE', params: { key: '@key' }, url: odataUrl + '(:key)' },
            defaultGet: { method: 'POST', url: odataUrl + '/ODataService.DefaultGet' },
        });
    }
})();
