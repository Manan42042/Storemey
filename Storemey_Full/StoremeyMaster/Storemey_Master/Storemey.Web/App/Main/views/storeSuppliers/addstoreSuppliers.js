(function () {
    angular.module('app').controller('app.views.storeSuppliers', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeSuppliers', 'abp.services.app.masterService',
        function ($scope, $state, $stateParams, _thisServices, _masterService) {




            function getMasters() {
                if ($scope.Countries === undefined) {

                    _masterService.listAllCountries()
                        .then(function (result) {
                            $scope.Countries = result.data.items;

                            if ($scope.Id !== 0) {
                                getstoreSuppliers($scope.Id);
                            }
                            else {
                                getstoreSuppliers($scope.Id);
                            }

                        });
                }
            }
            getMasters();







            $scope.$watch("GetstoreSuppliersOutputDto.uicountryId", function (newValue, oldValue) {

                if (newValue !== undefined && newValue !== "") {

                    //$scope.GetstoreOutletsOutputDto.countryId = $scope.GetstoreOutletsOutputDto.con.id;
                    //$scope.GetstoreOutletsOutputDto.country = $scope.GetstoreOutletsOutputDto.con.countryName;

                    _masterService.listAllStates(newValue.id)
                        .then(function (result) {
                            $scope.States = result.data.items;

                            var result3 = $scope.States.filter(function (element) {
                                if (parseInt(element.id) == parseInt($scope.GetstoreSuppliersOutputDto.stateId)) {
                                    return true;
                                } else {
                                    return false;
                                }
                            });
                            if (result3[0] !== undefined) {
                                console.log(result3[0]);
                                $scope.GetstoreSuppliersOutputDto.uistateId = result3[0];
                            }
                        });
                }
            });

            $scope.$watch("GetstoreSuppliersOutputDto.uistateId", function (newValue, oldValue) {

                if (newValue !== undefined && newValue !== "") {

                    _masterService.listAllCities(newValue.id)
                        .then(function (result) {
                            $scope.Cities = result.data.items;

                            var result3 = $scope.Cities.filter(function (element) {
                                if (parseInt(element.id) == parseInt($scope.GetstoreSuppliersOutputDto.cityId)) {
                                    return true;
                                } else {
                                    return false;
                                }
                            });
                            if (result3[0] !== undefined) {
                                console.log(result3[0]);
                                $scope.GetstoreSuppliersOutputDto.uicityId = result3[0];
                            }

                        });
                }
            });




          


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

                        var result2 = $scope.Countries.filter(function (element) {
                            if (parseInt(element.id) == parseInt($scope.GetstoreSuppliersOutputDto.countryId)) {
                                return true;
                            } else {
                                return false;
                            }
                        });
                        if (result2[0] !== undefined) {
                            console.log(result2[0]);
                            $scope.GetstoreSuppliersOutputDto.uicountryId = result2[0];
                        }


                    });
            }
            if ($scope.Id !== 0) {
                getstoreSuppliers($scope.Id);
            }
            else {
                getstoreSuppliers($scope.Id);
            }



            $scope.saveAddEdit = function () {


                $scope.GetstoreSuppliersOutputDto.country = $scope.GetstoreSuppliersOutputDto.uicountryId.countryName;
                $scope.GetstoreSuppliersOutputDto.countryId = $scope.GetstoreSuppliersOutputDto.uicountryId.id;
                $scope.GetstoreSuppliersOutputDto.state = $scope.GetstoreSuppliersOutputDto.uistateId.stateName;
                $scope.GetstoreSuppliersOutputDto.stateId = $scope.GetstoreSuppliersOutputDto.uistateId.id;
                $scope.GetstoreSuppliersOutputDto.city = $scope.GetstoreSuppliersOutputDto.uicityId.cityName;
                $scope.GetstoreSuppliersOutputDto.cityId = $scope.GetstoreSuppliersOutputDto.uicityId.id;

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