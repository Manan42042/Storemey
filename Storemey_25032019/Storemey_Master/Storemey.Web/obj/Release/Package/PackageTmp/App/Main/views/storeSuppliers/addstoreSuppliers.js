(function () {
    angular.module('app').controller('app.views.storeSuppliers', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeSuppliers',
        function ($scope, $state, $stateParams, _thisServices) {

            $.validator.addMethod("regx", function (value, element, regexpr) {
                return regexpr.test(value);
            }, "Please enter a valid pasword.");

            $scope.validationOptions = {
                rules: {
                    supplierFullName: {
                        required: true,
                    },
                    description: {
                        required: true,
                    },
                    firstName: {
                        required: true,
                    },
                    lastName: {
                        required: true,
                    },
                    companyName: {
                        required: true,
                    },
                    email: {
                        required: true,
                    },
                    phone: {
                        required: true,
                    },
                    mobile: {
                        required: true,
                    }
                }
            };

            var stateobj = $stateParams.SearchDto;
            if ($stateParams.SearchDto !== undefined && $stateParams.SearchDto !== null && $stateParams.SearchDto !== '') {
                $scope.Id = stateobj.tempID;
            }
            else {
                $stateParams.SearchDto = null;
            }

            var GetstoreSuppliersInputDto = {
                Id: 0
            };

            $scope.GetstoreSuppliersOutputDto = {};

            function getstoreSuppliers(Id) {
                GetstoreSuppliersInputDto.Id = Id;
                _thisServices.getById(GetstoreSuppliersInputDto)
                    .then(function (result) {
                        $scope.GetstoreSuppliersOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getstoreSuppliers($scope.Id);
            }
            else {
                getstoreSuppliers($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                debugger;
                if ($scope.GetstoreSuppliersOutputDto.id === undefined || $scope.GetstoreSuppliersOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreSuppliersOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeSuppliers', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreSuppliersOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeSuppliers', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeSuppliers');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('storeSuppliers', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();