
(function () {
    'use strict';
    angular
        .module('app')
        .controller('IRModelListController', IRModelListController);

    IRModelListController.$inject = ['$scope', '$state', 'IRModelService'];

    function IRModelListController($scope, $state, IRModelService) {
        var vm = this;
        vm.create = create;
        vm.edit = edit;
        vm.destroy = destroy;

        vm.gridOptions = {
            dataSource: {
                type: "odata-v4",
                transport: {
                    read: {
                        url: "/odata/IRModel",
                    },
                },
                pageSize: 20,
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true
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
                    title: "Tên đối tượng",
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

        function create() {
            $state.go('app.irmodel.form');
        }

        function edit(id) {
            $state.go('app.irmodel.form', { id: id });
        }

        function destroy(id) {
            if (!confirm('Bạn chắc chắn muốn xóa?'))
                return false;
            IRModelService.remove({ key: id }, function (response) {
                vm.grid.dataSource.read();
            }, function (response) {
            });
        }
    }
})();
