
(function () {
    'use strict';
    angular
        .module('app')
        .controller('DaiXoSoFormController', DaiXoSoFormController);

    DaiXoSoFormController.$inject = ['$scope', '$state', '$stateParams', 'DaiXoSoService', 'toastr', '$uibModal'];

    function DaiXoSoFormController($scope, $state, $stateParams, DaiXoSoService, toastr, $uibModal) {
        var vm = this;
        vm.id = $stateParams.id;
        var list_state = "app.daixs.list";
        vm.rules = [];
        vm.thus = {
            'all': 'Tất cả các ngày',
            '0': 'Chủ nhật',
            '1': 'Thứ hai',
            '2': 'Thứ ba',
            '3': 'Thứ tư',
            '4': 'Thứ năm',
            '5': 'Thứ sáu',
            '6': 'Thứ bảy',
        };

        vm.loDeCategoryDataSource = {
            type: "odata-v4",
            serverFiltering: true,
            serverPaging: true,
            pageSize: 10,
            transport: {
                read: {
                    url: "/odata/LoDeCategory",
                }
            }
        };


        do_show();

        vm.on_button_save = on_button_save;
        vm.on_button_save_and_new = on_button_save_and_new;
        vm.add_rule = add_rule;
        vm.edit_rule = edit_rule;
        vm.remove_rule = remove_rule;

        function do_show() {
            if (vm.id) {
                load_record();
                load_rules();
            }
        }

        function load_record() {
            DaiXoSoService.get({ key: vm.id, $expand: 'Mien' }, function (response) {
                vm.model = response;
            });
        }

        function load_rules() {
            DaiXoSoService.getRules({ key: vm.id }, function (response) {
                vm.rules = response.value;
            });
        }

        function add_rule() {
            var modalInstance = $uibModal.open({
                animation: true,
                keyboard: false,
                backdrop: 'static',
                templateUrl: '/Admin/DaiXoSo/RuleModal',
                controller: 'DaiXoSoRuleModalController',
                controllerAs: 'vm',
                size: 'lg',
                scope: $scope,
                resolve: {
                    item: function () {
                        return null;
                    },
                }
            });

            modalInstance.result.then(function (items) {
                angular.forEach(items, function (value, index) {
                    this.push(value);
                }, vm.rules);
            }, function error() {
            });
        }

        function edit_rule(index) {
            var rule = vm.rules[index];
            var modalInstance = $uibModal.open({
                animation: true,
                keyboard: false,
                backdrop: 'static',
                templateUrl: '/Admin/DaiXoSo/RuleModal',
                controller: 'DaiXoSoRuleModalController',
                controllerAs: 'vm',
                size: 'lg',
                scope: $scope,
                resolve: {
                    item: function () {
                        return angular.copy(rule);
                    },
                }
            });

            modalInstance.result.then(function (items) {
                if (items.length) {
                    vm.rules[index] = items[0];
                }
            }, function error() {
            });
        }

        function remove_rule(index) {
            vm.rules.splice(index, 1);
        }

        function prepare_model() {
            vm.model.MienId = vm.model.Mien != null ? vm.model.Mien.Id : null;
            vm.model.Rules = vm.rules;
        }

        function save() {
            prepare_model();
            if (vm.id) {
                return DaiXoSoService.update({ key: vm.id }, vm.model).$promise;
            } else {
                return DaiXoSoService.save(vm.model).$promise;
            }
        }

        function on_button_save() {
            if (!vm.validator.validate())
                return false;

            save().then(function (response) {
                if (vm.id) {
                    toastr.success("Cập nhật thành công");
                } else {
                    $state.go(list_state);
                }
            });
        }

        function on_button_save_and_new() {
            if (!vm.validator.validate())
                return false;

            save().then(function (response) {
                if (vm.id) {
                    toastr.success("Cập nhật thành công");
                } else {
                    toastr.success("Thêm thành công");
                    vm.model = angular.copy({});
                }
            });
        }

        function on_button_create() {
            $state.transitionTo($state.current.name, {}, { reload: true });
        }
    }
})();
