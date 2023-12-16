
(function () {
    'use strict';
    angular
        .module('app')
        .controller('AccountJournalBankListController', AccountJournalBankListController);

    AccountJournalBankListController.$inject = ['$scope', '$state', 'AccountJournalService'];

    function AccountJournalBankListController($scope, $state, AccountJournalService) {
        var vm = this;

        vm.gridOptions = {
            dataSource: {
                type: "odata-v4",
                transport: {
                    read: {
                        url: "/odata/AccountJournal",
                    },
                },
                pageSize: 20,
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                filter: [
                    {field: 'Type', operator: 'eq', value: 'bank'}
                ]
            },
            resizable: true,
            sortable: true,
            pageable: {
                refresh: true,
                pageSizes: [20, 50, 100]
            },
            filterable: true,
            columns: [
                {
                    field: "Name",
                    title: "Tên tài khoản",
                },
                {
                    field: "BankAccNumber",
                    title: "Số tài khoản",
                },
                {
                    field: "BankName",
                    title: "Tên ngân hàng",
                },
                {
                    field: "Id",
                    title: " ",
                    filterable: false,
                    sortable: false,
                    width: "120px",
                    template: "<a ng-click=\"vm.edit('#:Id#')\" title='Sửa' class='btn btn-success btn-sm'><span class='fa fa-pencil-square-o'></span></a> " +
                    "<a ng-click=\"vm.destroy('#:Id#')\" title='Xóa' class='btn btn-danger btn-sm'><span class='fa fa-trash-o'></span></a>"
                },
            ]
        };

        vm.create = create;
        vm.edit = edit;
        vm.destroy = destroy;

        function create() {
            $state.go('app.accountjournalbank.form');
        }

        function edit(id) {
            $state.go('app.accountjournalbank.form', { id: id });
        }

        function destroy(id) {
            if (!confirm('Bạn chắc chắn muốn xóa?'))
                return false;
            AccountJournalService.remove({ key: id }, function (response) {
                vm.grid.dataSource.read();
            }, function (response) {
            });
        }
    }
})();
