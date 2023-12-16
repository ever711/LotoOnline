
(function () {
    'use strict';
    angular
        .module('app')
        .controller('KetQuaXoSoFormController', KetQuaXoSoFormController);

    KetQuaXoSoFormController.$inject = ['$state', '$stateParams', 'KetQuaXoSoService', 'toastr'];

    function KetQuaXoSoFormController($state, $stateParams, KetQuaXoSoService, toastr) {
        var vm = this;
        vm.id = $stateParams.id;
        vm.lines = [];
        vm.giais = {
            '0': 'ĐB',
            '1': 'G1',
            '2': 'G2',
            '3': 'G3',
            '4': 'G4',
            '5': 'G5',
            '6': 'G6',
            '7': 'G7',
            '8': 'G8',
        };

        do_show();

        vm.so_trung = so_trung;

        function do_show() {
            if (vm.id) {
                loadRecord();
                loadLines();
            }
        }

        function so_trung(value) {
            var list = [];
            value.map(function (value, index) {
                list.push(value.SoTrung);
            });
            return list.join(' ');
        }

        function loadRecord() {
            KetQuaXoSoService.get({ key: vm.id, $expand: 'DaiXS' }, function (response) {
                vm.model = response;
            });
        }

        function loadLines() {
            KetQuaXoSoService.getLines({ key: vm.id }, function (response) {
                vm.lines = response.value;
            });
        }
    }
})();
