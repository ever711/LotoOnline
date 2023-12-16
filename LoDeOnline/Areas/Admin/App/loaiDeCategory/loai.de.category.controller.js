
(function () {
    'use strict';
    angular
        .module('app')
        .controller('LoaiDeCategoryListController', LoaiDeCategoryListController);

    LoaiDeCategoryListController.$inject = ['$scope', '$state', 'LoaiDeCategoryService'];

    function LoaiDeCategoryListController($scope, $state, LoaiDeCategoryService) {
        var vm = this;

        vm.gridOptions = {
            dataSource: {
                type: "odata-v4",
                transport: {
                    read: {
                        url: "/odata/LoaiDeCategory",
                    },
                },
                pageSize: 20,
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true
            },
            resizable: true,
            sortable: true,
            pageable: {
                refresh: true,
                pageSizes: [20, 50, 100]
            },
            filterable: true,
            columns: [
                {
                    field: "Name",
                    title: "Tên",
                },
                {
                    field: "Id",
                    title: " ",
                    filterable: false,
                    sortable: false,
                    width: "120px",
                    template: "<a ng-click=\"vm.edit('#:Id#')\" title='Sửa' class='btn btn-success btn-sm'><span class='fa fa-pencil-square-o'></span></a> " +
                    "<a ng-click=\"vm.destroy('#:Id#')\" title='Xóa' class='btn btn-danger btn-sm'><span class='fa fa-trash-o'></span></a>"
                },
            ]
        };

        vm.create = create;
        vm.edit = edit;
        vm.destroy = destroy;

        function create() {
            $state.go('app.loaidecategory.form');
        }

        function edit(id) {
            $state.go('app.loaidecategory.form', { id: id });
        }

        function destroy(id) {
            if (!confirm('Bạn chắc chắn muốn xóa?'))
                return false;
            LoaiDeCategoryService.remove({ key: id }, function (response) {
                vm.grid.dataSource.read();
            }, function (response) {
            });
        }
    }
})();


(function () {
    'use strict';
    angular
        .module('app')
        .controller('LoaiDeCategoryFormController', LoaiDeCategoryFormController);

    LoaiDeCategoryFormController.$inject = ['$state', '$stateParams', 'LoaiDeCategoryService', 'toastr'];

    function LoaiDeCategoryFormController($state, $stateParams, LoaiDeCategoryService, toastr) {
        var vm = this;
        vm.id = $stateParams.id;
        var list_state = "app.loaidecategory.list";

        vm.loDeCategoryOptions = {
            dataTextField: "Name",
            dataValueField: "Id",
            autoBind: false,
            dataSource: {
                type: "odata-v4",
                serverFiltering: true,
                serverPaging: true,
                pageSize: 10,
                transport: {
                    read: {
                        url: "/odata/LoDeCategory",
                    }
                }
            }
        };

        do_show();

        vm.on_button_save = on_button_save;
        vm.on_button_save_and_new = on_button_save_and_new;

        function do_show() {
            if (vm.id) {
                LoaiDeCategoryService.get({ key: vm.id, $expand: 'LoDeCategories' }, function (response) {
                    vm.model = response;
                });
            }
        }

        function save() {
            if (vm.id) {
                return LoaiDeCategoryService.update({ key: vm.id }, vm.model).$promise;
            } else {
                return LoaiDeCategoryService.save(vm.model).$promise;
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

