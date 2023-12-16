
(function () {
    'use strict';
    angular
        .module('app')
        .controller('IRRuleListController', IRRuleListController);

    IRRuleListController.$inject = ['$scope', '$state', 'IRRuleService'];

    function IRRuleListController($scope, $state, IRRuleService) {
        var vm = this;
        vm.create = create;
        vm.edit = edit;
        vm.destroy = destroy;

        vm.gridOptions = {
            dataSource: {
                type: "odata-v4",
                transport: {
                    read: {
                        url: "/odata/IRRule",
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
                    title: "Tên quy tắc",
                },
                {
                    field: "ModelName",
                    title: "Đối tượng",
                },
                {
                    field: "Global",
                    title: "Toàn bộ",
                    template: '<input type="checkbox" #= Global ? \'checked="checked"\' : "" # disabled />',
                },
                {
                    field: "Active",
                    title: "Hiệu lực",
                    template: '<input type="checkbox" #= Active ? \'checked="checked"\' : "" # disabled />',
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
            $state.go('app.irrule.form');
        }

        function edit(id) {
            $state.go('app.irrule.form', { id: id });
        }

        function destroy(id) {
            if (!confirm('Bạn chắc chắn muốn xóa?'))
                return false;
            IRRuleService.remove({ key: id }, function (response) {
                vm.grid.dataSource.read();
            }, function (response) {
            });
        }
    }
})();
