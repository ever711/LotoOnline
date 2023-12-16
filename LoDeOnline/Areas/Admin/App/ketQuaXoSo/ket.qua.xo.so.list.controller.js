
(function () {
    'use strict';
    angular
        .module('app')
        .controller('KetQuaXoSoListController', KetQuaXoSoListController);

    KetQuaXoSoListController.$inject = ['$scope', '$state', 'KetQuaXoSoService', '$uibModal'];

    function KetQuaXoSoListController($scope, $state, KetQuaXoSoService, $uibModal) {
        var vm = this;

        vm.gridOptions = {
            dataSource: {
                type: "odata-v4",
                transport: {
                    read: {
                        url: "/odata/KetQuaXoSo",
                    },
                },
                pageSize: 20,
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                sort: [
                    { field: "Ngay", dir: 'desc' },
                    { field: "Id", dir: 'desc' }
                ],
                schema: {
                    model: {
                        id: "Id",
                        fields: {
                            Ngay: {
                                type: "date",
                            },
                        }
                    }
                },
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
                    field: "Ngay",
                    title: "Ngày",
                    format: "{0:d}"
                },
                {
                    field: "DaiXSName",
                    title: "Tên đài",
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

        vm.edit = edit;
        vm.destroy = destroy;
        vm.layKQXS = layKQXS;

        function edit(id) {
            $state.go('app.kqxs.form', { id: id });
        }

        function layKQXS() {
            var modalInstance = $uibModal.open({
                animation: true,
                keyboard: false,
                backdrop: 'static',
                templateUrl: '/Admin/KetQuaXoSo/LayKetQua',
                controller: 'KetQuaXoSoLayKetQuaController',
                controllerAs: 'vm',
                size: 'md',
                scope: $scope,
            });

            modalInstance.result.then(function () {
                vm.grid.dataSource.read();
            }, function error() {
            });
        }

        function destroy(id) {
            if (!confirm('Bạn chắc chắn muốn xóa?'))
                return false;
            KetQuaXoSoService.remove({ key: id }, function (response) {
                vm.grid.dataSource.read();
            }, function (response) {
            });
        }
    }
})();
