
(function () {
    'use strict';
    angular
        .module('app')
        .controller('DanhDeLineModalController', DanhDeLineModalController);

    DanhDeLineModalController.$inject = ['$scope', '$uibModalInstance', 'item', 'DanhDeLineService'];

    function DanhDeLineModalController($scope, $uibModalInstance, item, DanhDeLineService) {
        var vm = this;
        vm.item = item;
        vm.items = [];

        vm.loaiDeDataSource = {
            type: "odata-v4",
            serverFiltering: true,
            serverPaging: true,
            pageSize: 10,
            transport: {
                read: {
                    url: "/odata/LoaiDe",
                }
            }
        };

        do_show();

        vm.save = save;
        vm.saveAndCreate = saveAndCreate;
        vm.cancel = cancel;
        vm.onChangeLoaiDe = onChangeLoaiDe;
        vm.priceSubTotal = priceSubTotal;
        vm.add_xien_line = add_xien_line;
        vm.remove_xien_line = remove_xien_line;

        function do_show() {
            if (vm.item != null) {
                vm.model = item;
            }
            else {
                DanhDeLineService.defaultGet({}, function (response) {
                    delete response["@odata.context"];
                    vm.model = response;
                });
            }
        }

        function add_xien_line() {
            vm.model.XienNumbers = vm.model.XienNumbers || [];
            vm.model.XienNumbers.push({});
        }

        function remove_xien_line(index) {
            vm.model.XienNumbers.splice(index, 1);
        }

        function priceSubTotal() {
            if (vm.model) {
                return vm.model.Quantity * vm.model.PriceUnit;
            }
            return 0;
        }

        function onChangeLoaiDe() {
            DanhDeLineService.onChangeLoaiDe({ model: vm.model }, function (response) {
                vm.model.PriceUnit = response.PriceUnit;
                vm.model.IsXien = response.IsXien;
            });
        }

        function prepare_model() {
            vm.model.LoaiDeId = vm.model.LoaiDe.Id;
            vm.model.PriceSubtotal = vm.priceSubTotal();
            vm.model.SoDanh = computeSoDanh();
        }

        function computeSoDanh() {
            if (vm.model.IsXien) {
                var list = [];
                angular.forEach(vm.model.XienNumbers, function (value, index) {
                    list.push(value.SoXien);
                });
                return list.join(' - ');
            }
            return vm.model.SoDanh;
        }

        function save() {
            if (!vm.validator.validate())
                return false;
            prepare_model();

            vm.items.push(vm.model);
            $uibModalInstance.close(vm.items);
        }

        function saveAndCreate() {
            if (!vm.validator.validate())
                return false;
            prepare_model();
            vm.items.push(vm.model);
            DanhDeLineService.defaultGet({}, function (response) {
                delete response["@odata.context"];
                vm.model = response;
            });
        }

        function cancel() {
            if (vm.items.length) {
                $uibModalInstance.close(vm.items);
            }
            else {
                $uibModalInstance.dismiss('close');
            }
        };
    }
})();
