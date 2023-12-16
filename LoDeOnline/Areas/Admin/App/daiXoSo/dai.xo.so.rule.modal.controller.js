
(function () {
    'use strict';
    angular
        .module('app')
        .controller('DaiXoSoRuleModalController', DaiXoSoRuleModalController);

    DaiXoSoRuleModalController.$inject = ['$scope', '$uibModalInstance', 'item'];

    function DaiXoSoRuleModalController($scope, $uibModalInstance, item) {
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
                vm.model = {};
            }
        }

        function prepare_model() {
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
            vm.model = angular.copy({});
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
