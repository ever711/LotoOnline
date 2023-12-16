
(function () {
    'use strict';
    angular
        .module('app')
        .controller('LoaiDeFormController', LoaiDeFormController);

    LoaiDeFormController.$inject = ['$scope', '$state', '$stateParams', 'LoaiDeService', 'toastr', '$uibModal'];

    function LoaiDeFormController($scope, $state, $stateParams, LoaiDeService, toastr, $uibModal) {
        var vm = this;
        vm.id = $stateParams.id;
        vm.rules = [];
        var list_state = "app.loaide.list";

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

        vm.loaiDeCategoryDataSource = {
            type: "odata-v4",
            serverFiltering: true,
            serverPaging: true,
            pageSize: 10,
            transport: {
                read: {
                    url: "/odata/LoaiDeCategory",
                }
            }
        };

        do_show();

        vm.on_button_save = on_button_save;
        vm.on_button_save_and_new = on_button_save_and_new;
        vm.add_rule = add_rule;
        vm.edit_rule = edit_rule;
        vm.remove_rule = remove_rule;

        function add_rule() {
            var modalInstance = $uibModal.open({
                animation: true,
                keyboard: false,
                backdrop: 'static',
                templateUrl: '/Admin/LoaiDe/RuleModal',
                controller: 'LoaiDeRuleModalController',
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
                templateUrl: '/Admin/LoaiDe/RuleModal',
                controller: 'LoaiDeRuleModalController',
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

        function do_show() {
            if (vm.id) {
                load_record();
                load_rules();
            } else {
                LoaiDeService.defaultGet({}, function (response) {
                    vm.model = response;
                });
            }
        }

        function load_record() {
            LoaiDeService.get({ key: vm.id, $expand: 'LoDeCateg,LoaiDeCateg' }, function (response) {
                vm.model = response;
            });
        }

        function load_rules() {
            LoaiDeService.getRules({ key: vm.id }, function (response) {
                vm.rules = response.value;
            });
        }

        function prepare_model() {
            vm.model.LoDeCategId = vm.model.LoDeCateg != null ? vm.model.LoDeCateg.Id : null;
            vm.model.LoaiDeCategId = vm.model.LoaiDeCateg != null ? vm.model.LoaiDeCateg.Id : null;
            vm.model.Rules = vm.rules;
        }

        function save() {
            prepare_model();
            if (vm.id) {
                return LoaiDeService.update({ key: vm.id }, vm.model).$promise;
            } else {
                return LoaiDeService.save(vm.model).$promise;
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
                    LoaiDeService.defaultGet({}, function (response) {
                        vm.model = response;
                    });
                    vm.rules = [];
                }
            });
        }

        function on_button_create() {
            $state.transitionTo($state.current.name, {}, { reload: true });
        }
    }
})();
