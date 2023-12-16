
(function () {
    'use strict';
    angular
        .module('app')
        .controller('DanhDeListController', DanhDeListController);

    DanhDeListController.$inject = ['$scope', '$state', 'DanhDeService'];

    function DanhDeListController($scope, $state, DanhDeService) {
        var vm = this;

        vm.gridOptions = {
            dataSource: {
                type: "odata-v4",
                transport: {
                    read: {
                        url: "/odata/DanhDe",
                    },
                },
                pageSize: 20,
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                sort: [
                    { field: 'Date', dir: 'desc' },
                    { field: 'Number', dir: 'desc' },
                    { field: 'Id', dir: 'desc' }
                ],
                schema: {
                    model: {
                        id: "Id",
                        fields: {
                            Date: {
                                type: "date",
                            },
                            AmountTotal: {
                                type: "number",
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
                    field: "PartnerName",
                    title: "Khách hàng",
                    width: "150px",
                },
                {
                    field: "Date",
                    title: "Ngày",
                    format: "{0:d}",
                    width: "100px",
                },
                {
                    field: "DaiName",
                    title: "Đài xổ",
                    format: "{0:d}",
                    width: "100px",
                },
                {
                    field: "Number",
                    title: "Mã",
                    width: "100px",
                },
                {
                    field: "AmountTotal",
                    title: "Tổng tiền",
                    format: "{0:n}",
                    attributes: { "class": "text-right" },
                    width: "100px",
                },
                {
                    field: "State",
                    title: "Trạng thái",
                    template: "#:ShowState#",
                    filterable: {
                        ui: stateFilter
                    },
                    width: "100px",
                },
                {
                    field: "Id",
                    title: " ",
                    filterable: false,
                    sortable: false,
                    width: "100px",
                    attributes: { "class": "text-center" },
                    template: "<a ng-click=\"vm.edit('#:Id#')\" title='Sửa' class='btn btn-success btn-sm'><span class='fa fa-pencil-square-o'></span></a> " +
                    "<a ng-click=\"vm.destroy('#:Id#')\" title='Xóa' class='btn btn-danger btn-sm'><span class='fa fa-trash-o'></span></a>"
                },
            ]
        };

        vm.create = create;
        vm.edit = edit;
        vm.destroy = destroy;
        vm.doKetQuaAll = doKetQuaAll;

        function stateFilter(element) {
            element.kendoDropDownList({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    { text: "Nháp", value: "draft" },
                    { text: "Đã xác nhận", value: "open" },
                    { text: "Đã dò kết quả", value: "done" },
                    { text: "Hủy bỏ", value: "cancel" },
                ],
            });
        }

        function doKetQuaAll() {
            DanhDeService.doKetQuaAll({}, function (response) {
                vm.grid.dataSource.read();
            });
        }

        function create() {
            $state.go('app.danhde.form');
        }

        function edit(id) {
            $state.go('app.danhde.form', { id: id });
        }

        function destroy(id) {
            if (!confirm('Bạn chắc chắn muốn xóa?'))
                return false;
            DanhDeService.remove({ key: id }, function (response) {
                vm.grid.dataSource.read();
            }, function (response) {
            });
        }
    }
})();
