
(function () {
    'use strict';
    angular
        .module('app')
        .controller('ResGroupModelAccessModalController', ResGroupModelAccessModalController);

    ResGroupModelAccessModalController.$inject = ['$scope', '$uibModalInstance', 'item', 'group'];

    function ResGroupModelAccessModalController($scope, $uibModalInstance, item, group) {
        var vm = this;
        vm.item = item;
        vm.items = [];
        vm.itemDefault = {
            PermRead: true,
            PermWrite: true,
            PermCreate: true,
            PermUnlink: true,
        };
        if (vm.item != null) {
            vm.model = item;
        }
        else {
            vm.model = angular.copy(vm.itemDefault);
        }

        vm.irModelDataSource = {
            type: "odata-v4",
            serverFiltering: true,
            transport: {
                read: {
                    url: "/odata/IRModel",
                },
            }
        };

        vm.resGroupDataSource = {
            type: "odata-v4",
            serverFiltering: true,
            transport: {
                read: {
                    url: "/odata/ResGroup",
                },
            }
        };

        vm.save = save;
        vm.saveAndCreate = saveAndCreate;
        vm.cancel = cancel;

        function prepareModel() {
            vm.model.ModelId = vm.model.Model.Id;
        }

        function save() {
            if (!vm.validator.validate())
                return false;
            prepareModel();
            vm.items.push(vm.model);
            $uibModalInstance.close(vm.items);
        }

        function saveAndCreate() {
            if (!vm.validator.validate())
                return false;
            prepareModel();
            vm.items.push(vm.model);
            vm.model = angular.copy(vm.itemDefault);
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
