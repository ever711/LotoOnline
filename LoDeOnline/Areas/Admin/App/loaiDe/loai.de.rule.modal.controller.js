
(function () {
    'use strict';
    angular
        .module('app')
        .controller('LoaiDeRuleModalController', LoaiDeRuleModalController);

    LoaiDeRuleModalController.$inject = ['$scope', '$uibModalInstance', 'item', 'LoaiDeRuleService'];

    function LoaiDeRuleModalController($scope, $uibModalInstance, item, LoaiDeRuleService) {
        var vm = this;
        vm.item = item;
        vm.items = [];

        do_show();

        vm.save = save;
        vm.saveAndCreate = saveAndCreate;
        vm.cancel = cancel;

        function do_show() {
            if (vm.item != null) {
                vm.model = item;
            }
            else {
                LoaiDeRuleService.defaultGet({}, function (response) {
                    delete response["@odata.context"];
                    vm.model = response;
                });
            }
        }

        function prepare_model() {
            vm.model.Name = get_rule_name();
        }

        function get_rule_name() {
            var giaiDanhs = {
                'all': 'tất cả các giải',
                '8': 'giải 8',
                '7': 'giải 7',
                '0': 'giải ĐB'
            };
            if (vm.model.ViTriDanh === 'chu_so_cuoi') {
                return "Đánh " + vm.model.SoLuongDanh + " chữ số cuối của " + giaiDanhs[vm.model.GiaiDanh];
            } else if (vm.model.ViTriDanh === 'hang_chuc') {
                return "Đánh " + vm.model.SoLuongDanh + " chữ số hàng chục của " + giaiDanhs[vm.model.GiaiDanh];
            } else {
                return "";
            }
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
            LoaiDeRuleService.defaultGet(itemDefault, function (response) {
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
