
(function () {
    'use strict';
    angular
        .module('app')
        .controller('ResGroupFormController', ResGroupFormController);

    ResGroupFormController.$inject = ['$state', '$stateParams', 'ResGroupService', 'toastr', '$uibModal', '$q'];

    function ResGroupFormController($state, $stateParams, ResGroupService, toastr, $uibModal, $q) {
        var vm = this;
        vm.id = $stateParams.id;
        vm.actual_mode = "view";
        vm.modelAccesses = [];

        vm.userDataSource = {
            type: "odata-v4",
            serverFiltering: true,
            serverPaging: true,
            transport: {
                read: {
                    url: '/odata/ApplicationUser',
                }
            }
        };

        vm.irRuleDataSource = {
            type: "odata-v4",
            serverFiltering: true,
            serverPaging: true,
            transport: {
                read: {
                    url: '/odata/IRRule',
                }
            }
        };

        vm.groupDataSource = new kendo.data.DataSource({
            type: "odata-v4",
            serverFiltering: true,
            serverPaging: true,
            transport: {
                read: {
                    url: '/odata/ResGroup',
                },
            }
        });

        do_show();

        vm.on_button_edit = on_button_edit;
        vm.on_button_create = on_button_create;
        vm.on_button_save = on_button_save;
        vm.on_button_cancel = on_button_cancel;

        vm.addModelAccess = addModelAccess;
        vm.editModelAccess = editModelAccess;
        vm.removeModelAccess = removeModelAccess;
        vm.onChangeCategory = onChangeCategory;

        function _actualize_mode(switch_to) {
            var mode = switch_to || vm.actual_mode;
            if (!$stateParams.id) {
                mode = "create";
            } else if (mode === "create") {
                mode = "edit";
            }

            vm.actual_mode = mode;
        }

        function to_view_mode() {
            _actualize_mode("view");
        }

        function on_button_cancel() {
            if (vm.actual_mode == "create") {
                window.history.back();
            } else {
                load_record().then(function () {
                    to_view_mode();
                });
            }
        }

        function on_button_save() {
            if (!vm.validator.validate())
                return false;

            return save().then(function (result) {
                var params = $state.params || {};
                if (!params.id) {
                    params.id = result.Id;
                    $state.go($state.current.name, params);
                    return false;
                }

                toastr.success("Cập nhật thành công");

                load_record().then(function () {
                    to_view_mode();
                }).finally(function () {
                });
            }).catch(function () {
            });
        }

        function on_button_create() {
            $state.transitionTo($state.current.name, {}, { reload: true });
        }

        function on_button_edit() {
            _actualize_mode("edit");
        }

        function on_button_new() {
            return ResGroupService.defaultGet({}, function (response) {
                delete response["@odata.context"];
                vm.model = response;
                _actualize_mode();
            });
        }

        function load_record() {
            return $q.all(loadResGroupInfo(), loadModelAccessList());
        }

        function do_show() {
            if (vm.id) {
                load_record();
            }
            else {
                on_button_new();
            }
        }

        function onChangeCategory() {
            if (vm.model.Category != null) {
                vm.groupDataSource.filter({ field: 'CategoryId', operator: 'eq', value: vm.model.Category.Id })
            }
        }

        function importCSV() {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/ResGroup/ImportAccessModal',
                controller: 'ResGroupImportAccessModalController',
                controllerAs: 'vm',
                size: 'lg',
            });

            modalInstance.result.then(function (items) {
                loadModelAccessList();
            }, function () {
            });
        }

        function loadModelAccessList() {
            return ResGroupService.getModelAccesses({ key: vm.id, $expand: 'Model' }, function (response) {
                vm.modelAccesses = response.value;
            });
        }

        function addModelAccess() {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/Admin/ResGroup/ModelAccessModal',
                controller: 'ResGroupModelAccessModalController',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    item: function () {
                        return null;
                    },
                    group: function () {
                        var g = angular.copy(vm.model);
                        delete g["@odata.context"];
                        return g;
                    }
                }
            });

            modalInstance.result.then(function (items) {
                angular.forEach(items, function (value, index) {
                    this.push(value);
                },vm.modelAccesses)
            }, function () {
            });
        }

        function editModelAccess(index) {
            var item = vm.modelAccesses[index];
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/Admin/ResGroup/ModelAccessModal',
                controller: 'ResGroupModelAccessModalController',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    item: function () {
                        return angular.copy(item);
                    },
                    group: function () {
                        var g = angular.copy(vm.model);
                        delete g["@odata.context"];
                        return g;
                    }
                }
            });

            modalInstance.result.then(function (items) {
                if (items.length) {
                    vm.modelAccesses[index] = items[0];
                }
            }, function () {
            });
        }

        function removeModelAccess(index) {
            vm.modelAccesses.splice(index, 1);
        }

        function loadResGroupInfo() {
            return ResGroupService.get({ key: vm.id, $expand: 'Implieds,Rules,Users' }, function (response) {
                vm.model = response;
            });
        }

        function prepareModel() {
            vm.model.ModelAccesses = vm.modelAccesses;
        }

        function save() {
            prepareModel();

            if (vm.id) {
                return ResGroupService.update({ key: vm.id }, vm.model).$promise;
            }
            else {
                return ResGroupService.save(vm.model).$promise;
            }
        }
    }
})();
