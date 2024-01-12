(function () {



    angular.module('app').controller('app.views.storeOwner', [
        '$scope', '$state', '$stateParams', 'abp.services.app.adminStores', 'abp.services.app.masterService',
        function ($scope, $state, $stateParams, _thisServices, _masterService) {
         
            function getMasters() {
                if ($scope.Countries === undefined) {

                    _masterService.listAllCountries()
                        .then(function (result) {
                            $scope.Countries = result.data.items;


                            _masterService.listAllCurrancies()
                                .then(function (result) {
                                    $scope.Currancies = result.data.items;


                                    _masterService.listAllTimeZones()
                                        .then(function (result) {
                                            $scope.TimeZones = result.data.items;

                                        });

                                });

                        });



                //$scope.GetadminStoresrOutputDto.country = SlectedCountry.countryName;
                //$scope.GetadminStoresrOutputDto.countryId = SlectedCountry.id;

                //$scope.GetadminStoresrOutputDto.state = SlectedCountry.stateName;
                //$scope.GetadminStoresrOutputDto.stateId = SlectedCountry.id;

                //$scope.GetadminStoresrOutputDto.city = SlectedCountry.cityName;
                //$scope.GetadminStoresrOutputDto.cityId = SlectedCountry.id;


                    $scope.$watch("GetadminStoresrOutputDto.con", function (newValue, oldValue) {

                        if (newValue !== undefined && newValue !== "") {

                            $scope.GetadminStoresrOutputDto.countryId = $scope.GetadminStoresrOutputDto.con.id;
                            $scope.GetadminStoresrOutputDto.country = $scope.GetadminStoresrOutputDto.con.countryName;

                            _masterService.listAllStates(newValue.id)
                                .then(function (result) {
                                    $scope.States = result.data.items;

                                });
                        }
                    });

                    $scope.$watch("GetadminStoresrOutputDto.stat", function (newValue, oldValue) {

                        if (newValue !== undefined && newValue !== "") {

                            $scope.GetadminStoresrOutputDto.stateId = $scope.GetadminStoresrOutputDto.stat.id;
                            $scope.GetadminStoresrOutputDto.state = $scope.GetadminStoresrOutputDto.stat.stateName;

                            _masterService.listAllCities(newValue.id)
                                .then(function (result) {
                                    $scope.Cities = result.data.items;
                                });
                        }
                    });




                    $scope.$watch("GetadminStoresrOutputDto.cit", function (newValue, oldValue) {

                        if (newValue !== undefined && newValue !== "") {

                            $scope.GetadminStoresrOutputDto.cityId = $scope.GetadminStoresrOutputDto.cit.id;
                            $scope.GetadminStoresrOutputDto.city = $scope.GetadminStoresrOutputDto.cit.cityName;

                        }
                    });


                }
            }
            getMasters();



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
                    email: {
                        required: true,
                    }
                    ,
                    mobile: {
                        required: true,
                    }
                    ,
                    address1: {
                        required: true,
                    }
                    ,
                    address2: {
                        required: true,
                    }
                    ,
                    country: {
                        required: true,
                    }
                    ,
                    state: {
                        required: true,
                    }
                    ,
                    city: {
                        required: true,
                    },
                    zip: {
                        required: true,
                    },
                    currancy: {
                        required: true,
                    },
                    timeZone: {
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

            $scope.GetadminStoresrOutputDto = {};

            function getadminStoresr() {
                _thisServices.getByStoreName($scope.storeName)
                    .then(function (result) {
                        $scope.GetadminStoresrOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getadminStoresr();
            }
            else {
                getadminStoresr();
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }

             
                //$scope.GetadminStoresrOutputDto.country = SlectedCountry.countryName;
                //$scope.GetadminStoresrOutputDto.countryId = SlectedCountry.id;

                //$scope.GetadminStoresrOutputDto.state = SlectedCountry.stateName;
                //$scope.GetadminStoresrOutputDto.stateId = SlectedCountry.id;

                //$scope.GetadminStoresrOutputDto.city = SlectedCountry.cityName;
                //$scope.GetadminStoresrOutputDto.cityId = SlectedCountry.id;


                $scope.GetadminStoresrOutputDto.currancy = $scope.GetadminStoresrOutputDto.cur.currency;
                $scope.GetadminStoresrOutputDto.timeZone = $scope.GetadminStoresrOutputDto.time.name;




                if ($scope.GetadminStoresrOutputDto.id === undefined || $scope.GetadminStoresrOutputDto.id === 0) {
                    _thisServices.create($scope.GetadminStoresrOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $scope.modalInstance.close();
                            $scope.modalInstance.dismiss('cancel');
                            $scope.storeOwnerModel.backgroundDismiss = true;
                        });
                }
                else {
                    _thisServices.update($scope.GetadminStoresrOutputDto)
                        .then(function () {
                            abp.notify.success("Saved Successfully.");
                            $scope.modalInstance.close();
                            $scope.modalInstance.dismiss('cancel');
                            $scope.storeOwnerModel.backgroundDismiss = true;

                        });
                }
            };


            //$scope.cancelAddEdit = function () {
            //    window.scrollTo(0, 0);
            //    $state.go('adminStoresr', { SearchDto: $stateParams.SearchDto });
            //};
        }
    ]);
})();