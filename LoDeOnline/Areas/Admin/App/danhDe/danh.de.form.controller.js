
(function () {
    'use strict';
    angular
        .module('app')
        .controller('DanhDeFormController', DanhDeFormController);

    DanhDeFormController.$inject = ['$scope', '$state', '$stateParams', 'DanhDeService', 'toastr', '$uibModal', '$q'];

    function DanhDeFormController($scope, $state, $stateParams, DanhDeService, toastr, $uibModal, $q) {
        var vm = this;
        vm.id = $stateParams.id;
        var list_state = "app.daixs.list";
        vm.lines = [];
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

        vm.partnerCustomerDataSource = {
            type: "odata-v4",
            serverFiltering: true,
            serverPaging: true,
            pageSize: 10,
            transport: {
                read: {
                    url: "/odata/Partner",
                }
            },
            filter: [
                { field: 'Customer', operator: 'eq', value: true }
            ]
        };

        vm.daixsDataSource = {
            type: "odata-v4",
            serverFiltering: true,
            serverPaging: true,
            pageSize: 10,
            transport: {
                read: {
                    url: "/odata/DaiXoSo",
                }
            }
        };

        do_show();

        vm.on_button_save = on_button_save;
        vm.on_button_save_and_new = on_button_save_and_new;
        vm.on_button_create = on_button_create;
        vm.add_line = add_line;
        vm.edit_line = edit_line;
        vm.remove_line = remove_line;
        vm.invoiceOpen = invoiceOpen;
        vm.doKetQua = doKetQua;

        function do_show() {
            if (vm.id) {
                load_record();
                load_lines();
            } else {
                DanhDeService.defaultGet({}, function (response) {
                    vm.model = response;
                });
            }
        }

        function load_record() {
            DanhDeService.get({ key: vm.id, $expand: 'Partner,Dai' }, function (response) {
                vm.model = response;
            });
        }

        function load_lines() {
            DanhDeService.getLines({ key: vm.id, $expand: 'LoaiDe,XienNumbers' }, function (response) {
                vm.lines = response.value;
            });
        }

        function reloadData() {
            load_record();
            load_lines();
        }

        function doKetQua() {
            DanhDeService.doKetQua({ ids: [parseInt(vm.id)] }, function () {
                handle_action();
            });
        }

        function invoiceOpen() {
            on_button_save2().then(function (response) {
                DanhDeService.actionInvoiceOpen({ ids: [response.Id] }).$promise.finally(function () {
                    handle_action(response);
                });
            });
        }

        function on_button_create() {
            $state.transitionTo($state.current.name, {}, { reload: true });
        }

        function handle_action(data) {
            if (vm.id) {
                toastr.success("Lưu thành công");
                reloadData();
            } else {
                $state.go($state.current.name, { id: data.Id });
            }
        }

        function add_line() {
            var modalInstance = $uibModal.open({
                animation: true,
                keyboard: false,
                backdrop: 'static',
                templateUrl: '/Admin/DanhDe/LineModal',
                controller: 'DanhDeLineModalController',
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
                }, vm.lines);
                load_summary();
            }, function error() {
            });
        }

        function load_summary() {
            vm.model.AmountTotal = computeAmountTotal();
        }

        function computeAmountTotal() {
            var total = 0;
            angular.forEach(vm.lines, function (value, index) {
                total += value.PriceSubtotal;
            });
            return total;
        }

        function edit_line(index) {
            var line = vm.lines[index];
            var modalInstance = $uibModal.open({
                animation: true,
                keyboard: false,
                backdrop: 'static',
                templateUrl: '/Admin/DanhDe/LineModal',
                controller: 'DanhDeLineModalController',
                controllerAs: 'vm',
                size: 'lg',
                scope: $scope,
                resolve: {
                    item: function () {
                        return angular.copy(line);
                    },
                }
            });

            modalInstance.result.then(function (items) {
                if (items.length) {
                    vm.lines[index] = items[0];
                }
                load_summary();
            }, function error() {
            });
        }

        function remove_line(index) {
            vm.lines.splice(index, 1);
            load_summary();
        }

        function prepare_model() {
            vm.model.DaiId = vm.model.Dai.Id;
            vm.model.PartnerId = vm.model.Partner.Id;
            vm.model.Lines = vm.lines;
        }

        function save() {
            prepare_model();
            if (vm.id) {
                return DanhDeService.update({ key: vm.id }, vm.model).$promise;
            } else {
                return DanhDeService.save(vm.model).$promise;
            }
        }

        function on_button_save() {
            if (!vm.validator.validate())
                return false;

            return save().then(function (response) {
                handle_action(response);
            });
        }

        function on_button_save2() {
            var defer = $q.defer();
            if (!vm.validator.validate()) {
                defer.reject();
            }

            save().then(function (response) {
                defer.resolve(response);
            });

            return defer.promise;
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
