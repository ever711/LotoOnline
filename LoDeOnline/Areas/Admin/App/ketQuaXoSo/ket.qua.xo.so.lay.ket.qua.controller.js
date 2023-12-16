
(function () {
    'use strict';
    angular
        .module('app')
        .controller('KetQuaXoSoLayKetQuaController', KetQuaXoSoLayKetQuaController);

    KetQuaXoSoLayKetQuaController.$inject = ['$scope', '$uibModalInstance', 'KetQuaXoSoService'];

    function KetQuaXoSoLayKetQuaController($scope, $uibModalInstance, KetQuaXoSoService) {
        var vm = this;

        vm.save = save;
        vm.cancel = cancel;

        function save() {
            if (!vm.validator.validate())
                return false;
            KetQuaXoSoService.layKetQua({ date: vm.date }, function (response) {
                $uibModalInstance.close();
            })
        }

        function cancel() {
            $uibModalInstance.dismiss('close');
        };
    }
})();
