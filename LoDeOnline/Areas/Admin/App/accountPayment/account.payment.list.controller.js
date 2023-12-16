
(function () {
    'use strict';
    angular
        .module('app')
        .controller('AccountPaymentListController', AccountPaymentListController);

    AccountPaymentListController.$inject = ['$scope', '$state', 'AccountPaymentService', 'toastr'];

    function AccountPaymentListController($scope, $state, AccountPaymentService, toastr) {
        var vm = this;

        vm.napTienGridOptions = {
            dataSource: {
                type: "odata-v4",
                transport: {
                    read: {
                        url: "/odata/AccountPayment",
                    },
                },
                pageSize: 20,
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                filter: [
                    { field: 'PartnerType', operator: 'eq', value: 'customer' },
                    { field: 'PaymentType', operator: 'eq', value: 'inbound' }
                ],
                sort: [
                    { field: 'PaymentDate', dir: 'desc' },
                    { field: 'Id', dir: 'desc' }
                ],
                schema: {
                    model: {
                        id: "Id",
                        fields: {
                            PaymentDate: {
                                type: "date",
                            },
                            Amount: {
                                type: "number",
                            },
                        }
                    }
                },
            },
            resizable: true,
            sortable: true,
            persistSelection: true,
            pageable: {
                refresh: true,
                pageSizes: [20, 50, 100]
            },
            filterable: true,
            columns: [
                {
                    selectable: true,
                    width: "30px",
                },
                {
                    field: "PaymentDate",
                    title: "Ngày",
                    format: "{0:d}",
                    width: "100px",
                },
                {
                    field: "Name",
                    title: "Tham chiếu",
                    width: "120px",
                },
                {
                    field: "JournalName",
                    title: "Tên Tài khoản",
                    width: "100px",
                },
                {
                    field: "BankAccNumber",
                    title: "Số tài khoản",
                    width: "100px",
                },
                {
                    field: "PartnerName",
                    title: "Khách hàng",
                    width: "150px",
                },
                {
                    field: "Amount",
                    title: "Số tiền",
                    format: "{0:n0}",
                    width: "120px",
                },
                {
                    field: "Communication",
                    title: "N.gửi/Mã g.dịch",
                    width: "120px",
                },
                {
                    field: "State",
                    template: "#:StateGet#",
                    title: "Trạng thái",
                    filterable: {
                        ui: stateFilter
                    },
                    width: "100px",
                }
            ]
        };

        vm.rutTienGridOptions = {
            dataSource: {
                type: "odata-v4",
                transport: {
                    read: {
                        url: "/odata/AccountPayment",
                    },
                },
                pageSize: 20,
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                filter: [
                    { field: 'PartnerType', operator: 'eq', value: 'customer' },
                    { field: 'PaymentType', operator: 'eq', value: 'outbound' }
                ],
                sort: [
                    { field: 'PaymentDate', dir: 'desc' },
                    { field: 'Id', dir: 'desc' }
                ],
                schema: {
                    model: {
                        id: "Id",
                        fields: {
                            PaymentDate: {
                                type: "date",
                            },
                            Amount: {
                                type: "number",
                            },
                        }
                    }
                },
            },
            resizable: true,
            sortable: true,
            persistSelection: true,
            pageable: {
                refresh: true,
                pageSizes: [20, 50, 100]
            },
            filterable: true,
            columns: [
                {
                    selectable: true,
                    width: "30px",
                },
                {
                    field: "PaymentDate",
                    title: "Ngày",
                    format: "{0:d}",
                    width: "100px",
                },
                {
                    field: "Name",
                    title: "Tham chiếu",
                    width: "120px",
                },
                {
                    field: "JournalName",
                    title: "Tên Tài khoản",
                    width: "100px",
                },
                {
                    field: "BankAccNumber",
                    title: "Số tài khoản",
                    width: "100px",
                },
                {
                    field: "PartnerName",
                    title: "Khách hàng",
                    width: "150px",
                },
                {
                    field: "Amount",
                    title: "Số tiền",
                    format: "{0:n0}",
                    width: "120px",
                },
                {
                    field: "Communication",
                    title: "N.nhận/số t.khoản",
                    width: "120px",
                },
                {
                    field: "State",
                    template: "#:StateGet#",
                    title: "Trạng thái",
                    filterable: {
                        ui: stateFilter
                    },
                    width: "100px",
                }
            ]
        };

        vm.actionPost = actionPost;
        vm.unlink = unlink;
        vm.getSelectIds = getSelectIds;

        function stateFilter(element) {
            element.kendoDropDownList({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    { text: "Chưa vào sổ", value: "draft" },
                    { text: "Đã vào sổ", value: "posted" },
                ],
            });
        }

        function getSelectIds() {
            var res = [];
            if (vm.gridNapTien) {
                angular.forEach(vm.gridNapTien.selectedKeyNames(), function (value, index) {
                    res.push(parseInt(value));
                });
            }

            if (vm.gridRutTien) {
                angular.forEach(vm.gridRutTien.selectedKeyNames(), function (value, index) {
                    res.push(parseInt(value));
                });
            }
            return res;
        }

        function actionPost() {
            var ids = getSelectIds();
            if (ids.length === 0) {
                toastr.error("Vui lòng chọn tối thiểu 1 dòng");
                return false;
            }

            AccountPaymentService.actionPost({ ids: ids }, function (response) {
                reloadGrid();
            });
        }

        function unlink() {
            var ids = getSelectIds();
            if (ids.length === 0) {
                toastr.error("Vui lòng chọn tối thiểu 1 dòng");
                return false;
            }

            if (!confirm("Bạn chắc chắn muốn xóa?")) {
                return false;
            }

            AccountPaymentService.unlink({ ids: ids }, function (response) {
                reloadGrid();
            });
        }

        function reloadGrid() {
            if (vm.gridNapTien) {
                vm.gridNapTien.dataSource.read();
            }

            if (vm.gridRutTien) {
                vm.gridRutTien.dataSource.read();
            }
        }
    }
})();
