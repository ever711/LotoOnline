
(function () {
    'use strict';
    angular
        .module('app')
        .controller('IRModelFormController', IRModelFormController);

    IRModelFormController.$inject = ['$scope', '$state', '$stateParams', 'IRModelService', '$q', '$uibModal'];

    function IRModelFormController($scope, $state, $stateParams, IRModelService, $q, $uibModal) {
        var vm = this;
        vm.id = $stateParams.id;
        vm.actual_mode = "view";

        do_show();

        vm.on_button_edit = on_button_edit;
        vm.on_button_create = on_button_create;
        vm.on_button_save = on_button_save;
        vm.on_button_cancel = on_button_cancel;

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
            return IRModelService.defaultGet({}, function (response) {
                delete response["@odata.context"];
                vm.model = response;
                _actualize_mode();
            });
        }

        function load_record() {
            return $q.all(loadRecordInfo());
        }

        function do_show() {
            if (vm.id) {
                load_record();
            }
            else {
                on_button_new();
            }
        }

        function loadRecordInfo() {
            return IRModelService.get({ key: vm.id }, function (response) {
                delete response["@odata.context"];
                vm.model = response;
            });
        }

        function prepareModel() {
        }

        function save() {
            prepareModel();

            if (vm.id) {
                return IRModelService.update({ key: vm.id }, vm.model).$promise;
            }
            else {
                return IRModelService.save(vm.model).$promise;
            }
        }
    }
})();
