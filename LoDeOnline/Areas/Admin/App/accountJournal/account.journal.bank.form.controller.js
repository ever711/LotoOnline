

(function () {
    'use strict';
    angular
        .module('app')
        .controller('AccountJournalBankFormController', AccountJournalBankFormController);

    AccountJournalBankFormController.$inject = ['$scope', '$state', '$stateParams', 'AccountJournalService', 'toastr', '$uibModal'];

    function AccountJournalBankFormController($scope, $state, $stateParams, AccountJournalService, toastr, $uibModal) {
        var vm = this;
        vm.id = $stateParams.id;
        var list_state = "app.accountjournalbank.list";
        vm.resBankNoDataTemplate = $("#noDataTemplate").html();

        vm.resBankDataSource = {
            type: "odata-v4",
            serverFiltering: true,
            serverPaging: true,
            pageSize: 10,
            transport: {
                read: {
                    url: "/odata/ResBank",
                }
            }
        };

        do_show();

        vm.on_button_save = on_button_save;
        vm.on_button_save_and_new = on_button_save_and_new;
        vm.addResBank = addResBank;
        vm.updateResBank = updateResBank;

        function do_show() {
            if (vm.id) {
                AccountJournalService.get({ key: vm.id, $expand: 'Bank' }, function (response) {
                    vm.model = response;
                });
            } else {
                AccountJournalService.defaultGet({ model: { Type: "bank" } }, function (response) {
                    vm.model = response;
                });
            }
        }

        function addResBank() {
            var modalInstance = $uibModal.open({
                animation: true,
                keyboard: false,
                backdrop: 'static',
                templateUrl: '/Admin/ResBank/FormModal',
                controller: 'ResBankFormModalController',
                controllerAs: 'vm',
                size: 'lg',
                scope: $scope,
                resolve: {
                    modalCtx: function () {
                        return {
                            title: 'Thêm ngân hàng',
                        };
                    },
                }
            });

            modalInstance.result.then(function (items) {

            }, function error() {
            });
        }

        function updateResBank() {
            var modalInstance = $uibModal.open({
                animation: true,
                keyboard: false,
                backdrop: 'static',
                templateUrl: '/Admin/ResBank/FormModal',
                controller: 'ResBankFormModalController',
                controllerAs: 'vm',
                size: 'lg',
                scope: $scope,
                resolve: {
                    modalCtx: function () {
                        return {
                            title: 'Cập nhật ngân hàng',
                            item: angular.copy(vm.model.Bank)
                        };
                    },
                }
            });

            modalInstance.result.then(function (items) {
            }, function error() {
            });
        }

        function prepare_model() {
            vm.model.BankId = vm.model.Bank != null ? vm.model.Bank.Id : null;
        }

        function save() {
            prepare_model();
            if (vm.id) {
                return AccountJournalService.update({ key: vm.id }, vm.model).$promise;
            } else {
                return AccountJournalService.save(vm.model).$promise;
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
    }
})();

