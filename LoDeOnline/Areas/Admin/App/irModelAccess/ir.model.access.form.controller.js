
(function () {
    'use strict';
    angular
        .module('app')
        .controller('IRModelAccessFormController', IRModelAccessFormController);

    IRModelAccessFormController.$inject = ['$scope', '$state', '$stateParams', 'IRModelAccessService', 'toastr'];

    function IRModelAccessFormController($scope, $state, $stateParams, IRModelAccessService, toastr) {
        var vm = this;
        vm.id = $stateParams.id;
        var list_state = "app.irmodelaccess.list";

        vm.resGroupDataSource = {
            type: "odata-v4",
            serverFiltering: true,
            serverPaging: true,
            pageSize: 10,
            transport: {
                read: {
                    url: "/odata/ResGroup",
                }
            }
        };

        vm.irModelDataSource = {
            type: "odata-v4",
            serverFiltering: true,
            serverPaging: true,
            pageSize: 10,
            transport: {
                read: {
                    url: "/odata/IRModel",
                }
            }
        };


        do_show();

        vm.on_button_save = on_button_save;
        vm.on_button_save_and_new = on_button_save_and_new;

        function do_show() {
            if (vm.id) {
                load_record();
            } else {
                IRModelAccessService.defaultGet({}, function (response) {
                    vm.model = response;
                });
            }
        }

        function load_record() {
            IRModelAccessService.get({ key: vm.id, $expand: 'Group,Model' }, function (response) {
                vm.model = response;
            });
        }

        function prepare_model() {
            vm.model.GroupId = vm.model.Group != null ? vm.model.Group.Id : null;
            vm.model.ModelId = vm.model.Model.Id;
        }

        function save() {
            prepare_model();
            if (vm.id) {
                return IRModelAccessService.update({ key: vm.id }, vm.model).$promise;
            } else {
                return IRModelAccessService.save(vm.model).$promise;
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
                    IRModelAccessService.defaultGet({}, function (response) {
                        vm.model = response;
                    });
                }
            });
        }

        function on_button_create() {
            $state.transitionTo($state.current.name, {}, { reload: true });
        }
    }
})();
