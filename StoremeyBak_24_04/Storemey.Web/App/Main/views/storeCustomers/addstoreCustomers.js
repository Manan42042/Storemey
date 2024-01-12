(function () {
    angular.module('app').controller('app.views.storeCustomers', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeCustomers',
        function ($scope, $state, $stateParams, _thisServices) {

            $.validator.addMethod("regx", function (value, element, regexpr) {
                return regexpr.test(value);
            }, "Please enter a valid pasword.");

            $scope.validationOptions = {
                rules: {
                    firstName: {
                        required: true,
                    },
                    lastName: {
                        required: true,
                    },
                    gender: {
                        required: true,
                    },
                    dateofbirth: {
                        required: true,
                    },
                    companyName: {
                        required: true,
                    },
                    bill_Email: {
                        required: true,
                    },
                    bill_Phone: {
                        required: true,
                    },
                    bill_address: {
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

            var GetstoreCustomersInputDto = {
                Id: 0
            };

            $scope.GetstoreCustomersOutputDto = {};

            function getstoreCustomers(Id) {
                GetstoreCustomersInputDto.Id = Id;
                _thisServices.getById(GetstoreCustomersInputDto)
                    .then(function (result) {
                        $scope.GetstoreCustomersOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getstoreCustomers($scope.Id);
            }
            else {
                getstoreCustomers($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                debugger;
                if ($scope.GetstoreCustomersOutputDto.id === undefined || $scope.GetstoreCustomersOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreCustomersOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeCustomers', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreCustomersOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeCustomers', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeCustomers');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('storeCustomers', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();