(function () {
    angular.module('app').controller('app.views.storeOutlets', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeOutlets', 'abp.services.app.masterService',
        function ($scope, $state, $stateParams, _thisServices, _masterService) {



            function getMasters() {
                if ($scope.Countries === undefined) {

                    _masterService.listAllCountries()
                        .then(function (result) {
                            $scope.Countries = result.data.items;


                            _masterService.listAllWarehouses()
                                .then(function (result2) {
                                    $scope.Warehouse = result2.data.items;


                                    _masterService.listAllTimeZones()
                                        .then(function (result3) {
                                            $scope.TimeZones = result3.data.items;

                                            if ($scope.Id !== 0) {
                                                getstoreOutlets($scope.Id);
                                            }
                                            else {
                                                getstoreOutlets($scope.Id);
                                            }

                                        });

                                });

                        });
                }
            }
            getMasters();




            $scope.$watch("GetstoreOutletsOutputDto.uicountryId", function (newValue, oldValue) {

                if (newValue !== undefined && newValue !== "") {

                    //$scope.GetstoreOutletsOutputDto.countryId = $scope.GetstoreOutletsOutputDto.con.id;
                    //$scope.GetstoreOutletsOutputDto.country = $scope.GetstoreOutletsOutputDto.con.countryName;

                    _masterService.listAllStates(newValue.id)
                        .then(function (result) {
                            $scope.States = result.data.items;

                            var result3 = $scope.States.filter(function (element) {
                                if (parseInt(element.id) == parseInt($scope.temp.stateId)) {
                                    return true;
                                } else {
                                    return false;
                                }
                            });
                            if (result3[0] !== undefined) {
                                console.log(result3[0]);
                                $scope.GetstoreOutletsOutputDto.uistateId = result3[0];
                            }
                        });
                }
            });

            $scope.$watch("GetstoreOutletsOutputDto.uistateId", function (newValue, oldValue) {

                if (newValue !== undefined && newValue !== "") {

                    //$scope.GetstoreOutletsOutputDto.stateId = $scope.GetstoreOutletsOutputDto.stat.id;
                    //$scope.GetstoreOutletsOutputDto.state = $scope.GetstoreOutletsOutputDto.stat.stateName;

                    _masterService.listAllCities(newValue.id)
                        .then(function (result) {
                            $scope.Cities = result.data.items;

                            var result3 = $scope.Cities.filter(function (element) {
                                if (parseInt(element.id) == parseInt($scope.temp.cityId)) {
                                    return true;
                                } else {
                                    return false;
                                }
                            });
                            if (result3[0] !== undefined) {
                                console.log(result3[0]);
                                $scope.GetstoreOutletsOutputDto.uicityId = result3[0];
                            }

                        });
                }
            });




            $scope.$watch("GetstoreOutletsOutputDto.uicityId", function (newValue, oldValue) {

                if (newValue !== undefined && newValue !== "") {

                    //$scope.GetstoreOutletsOutputDto.cityId = $scope.GetstoreOutletsOutputDto.cit.id;
                    //$scope.GetstoreOutletsOutputDto.city = $scope.GetstoreOutletsOutputDto.cit.cityName;

                }
            });



            var stateobj = $stateParams.SearchDto;


            if ($stateParams.SearchDto !== undefined && $stateParams.SearchDto !== null && $stateParams.SearchDto !== '') {
                $scope.Id = stateobj.tempID;
            }
            else {
                $stateParams.SearchDto = null;
            }

            var GetstoreOutletsInputDto = {
                Id: 0
            };

            $scope.GetstoreOutletsOutputDto = {};
            $scope.temp = {};

            function getstoreOutlets(Id) {
                GetstoreOutletsInputDto.Id = Id;
                _thisServices.getById(GetstoreOutletsInputDto)
                    .then(function (result) {
                        $scope.GetstoreOutletsOutputDto = result.data;

                        $scope.temp.stateId = $scope.GetstoreOutletsOutputDto.stateId;
                        $scope.temp.cityId =  $scope.GetstoreOutletsOutputDto.cityId;
                        $scope.temp.timeZone =  $scope.GetstoreOutletsOutputDto.timeZone;

                        var result2 = $scope.Countries.filter(function (element) {
                            if (parseInt(element.id) == parseInt($scope.GetstoreOutletsOutputDto.countryId)) {
                                return true;
                            } else {
                                return false;
                            }
                        });
                        if (result2[0] !== undefined) {
                            console.log(result2[0]);
                            $scope.GetstoreOutletsOutputDto.uicountryId = result2[0];
                        }

                        var result3 = $scope.Warehouse.filter(function (element) {
                            if (parseInt(element.id) == parseInt($scope.GetstoreOutletsOutputDto.warehouseId)) {
                                return true;
                            } else {
                                return false;
                            }
                        });
                        if (result3[0] !== undefined) {
                            console.log(result3[0]);
                            $scope.GetstoreOutletsOutputDto.uiwarehouseId = result3[0];
                        }
                     
                        var result4 = $scope.TimeZones.filter(function (element) {
                            
                            if (element.name == $scope.GetstoreOutletsOutputDto.timeZone) {
                                return true;
                            } else {
                                return false;
                            }
                        });
                        if (result4[0] !== undefined) {
                            console.log(result4[0]);
                            $scope.GetstoreOutletsOutputDto.uitimeZoneId = result4[0];
                        }
                    });
            }
            //if ($scope.Id !== 0) {
            //    getstoreOutlets($scope.Id);
            //}
            //else {
            //    getstoreOutlets($scope.Id);
            //}



            $scope.saveAddEdit = function () {

                $scope.GetstoreOutletsOutputDto.country = $scope.GetstoreOutletsOutputDto.uicountryId.countryName;
                $scope.GetstoreOutletsOutputDto.countryId = $scope.GetstoreOutletsOutputDto.uicountryId.id;
                $scope.GetstoreOutletsOutputDto.state = $scope.GetstoreOutletsOutputDto.uistateId.stateName;
                $scope.GetstoreOutletsOutputDto.stateId = $scope.GetstoreOutletsOutputDto.uistateId.id;
                $scope.GetstoreOutletsOutputDto.city = $scope.GetstoreOutletsOutputDto.uicityId.cityName;
                $scope.GetstoreOutletsOutputDto.cityId = $scope.GetstoreOutletsOutputDto.uicityId.id;
                $scope.GetstoreOutletsOutputDto.warehouseId = $scope.GetstoreOutletsOutputDto.uiwarehouseId.id;
                $scope.GetstoreOutletsOutputDto.timeZone = $scope.GetstoreOutletsOutputDto.uitimeZoneId.name;
                $scope.GetstoreOutletsOutputDto.timeZoneId = $scope.GetstoreOutletsOutputDto.uitimeZoneId.id;


                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }

                if ($scope.GetstoreOutletsOutputDto.id === undefined || $scope.GetstoreOutletsOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreOutletsOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeOutlets', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreOutletsOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeOutlets', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeOutlets');
            //};

            $scope.cancelAddEdit = function () {
                window.scrollTo(0, 0);
                $state.go('storeOutlets', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();