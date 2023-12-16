
(function () {
    'use strict';
    angular
        .module('app')
        .controller('ResBankFormModalController', ResBankFormModalController);

    ResBankFormModalController.$inject = ['$scope', '$uibModalInstance', 'modalCtx', 'ResBankService'];

    function ResBankFormModalController($scope, $uibModalInstance, modalCtx, ResBankService) {
        var vm = this;
        vm.modalCtx = modalCtx;

        do_show();

        vm.save = save;
        vm.cancel = cancel;

        function do_show() {
            if (vm.modalCtx.item) {
                vm.model = vm.modalCtx.item;
            }
            else {
                ResBankService.defaultGet({}, function (response) {
                    vm.model = response;
                });
            }
        }

        function save() {
            if (!vm.validator.validate())
                return false;

            save_record().then(function (response) {
                $uibModalInstance.close(response);
            });
        }

        function save_record() {
            if (vm.model.Id) {
                return ResBankService.update({ key: vm.model.Id }, vm.model).$promise;
            } else {
                return ResBankService.save(vm.model).$promise;
            }
        }

        function cancel() {
            $uibModalInstance.dismiss('close');
        };
    }
})();
