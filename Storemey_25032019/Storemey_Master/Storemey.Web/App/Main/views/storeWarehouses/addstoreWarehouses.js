(function () {
    angular.module('app').controller('app.views.storeWarehouses', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeWarehouses', 'abp.services.app.masterService',
        function ($scope, $state, $stateParams, _thisServices, _masterService) {





            function getMasters() {
                if ($scope.Countries === undefined) {
                    //$scope.GetstoreWarehousesOutputDto.countryId = undefined;

                    _masterService.listAllCountries()
                        .then(function (result) {
                            $scope.Countries = result.data.items;

                            if ($scope.Id !== 0) {
                                getstoreWarehouses($scope.Id);
                            }
                            else {
                                getstoreWarehouses($scope.Id);
                            }
                        });

                }
            }
            getMasters();


            $scope.$watch("GetstoreWarehousesOutputDto.countryId", function (newValue, oldValue) {

                if (newValue !== undefined && newValue !== "" && newValue !== 0) {

                    _masterService.listAllStates(newValue.id)
                        .then(function (result) {
                            $scope.States = result.data.items;


                            var result3 = $scope.States.filter(function (element) {
                                if (parseInt(element.id) == parseInt($scope.GetstoreWarehousesOutputDto.stateId)) {
                                    return true;
                                } else {
                                    return false;
                                }
                            });
                            if (result3[0] !== undefined) {
                                console.log(result3[0]);
                                $scope.GetstoreWarehousesOutputDto.stateId = result3[0];
                            }

                        });
                }
            });


            $scope.$watch("GetstoreWarehousesOutputDto.stateId", function (newValue, oldValue) {

                if (newValue !== undefined && newValue !== "" && newValue !== 0) {

                    _masterService.listAllCities(newValue.id)
                        .then(function (result) {
                            $scope.Cities = result.data.items;


                            var result3 = $scope.Cities.filter(function (element) {
                                if (parseInt(element.id) == parseInt($scope.GetstoreWarehousesOutputDto.cityId)) {
                                    return true;
                                } else {
                                    return false;
                                }
                            });
                            if (result3[0] !== undefined) {
                                console.log(result3[0]);
                                $scope.GetstoreWarehousesOutputDto.cityId = result3[0];
                            }

                        });
                }
            });





            var stateobj = $stateParams.SearchDto;
            if ($stateParams.SearchDto !== undefined && $stateParams.SearchDto !== null && $stateParams.SearchDto !== '') {
                $scope.Id = stateobj.tempID;
            }
            else {
                $stateParams.SearchDto = null;
            }

            var GetstoreWarehousesInputDto = {
                Id: 0
            };

            $scope.GetstoreWarehousesOutputDto = {};

            function getstoreWarehouses(Id) {
                GetstoreWarehousesInputDto.Id = Id;
                _thisServices.getById(GetstoreWarehousesInputDto)
                    .then(function (result) {
                        $scope.GetstoreWarehousesOutputDto = result.data;
                        var result2 = $scope.Countries.filter(function (element) {
                            if (parseInt(element.id) == parseInt($scope.GetstoreWarehousesOutputDto.countryId)) {
                                return true;
                            } else {
                                return false;
                            }
                        });
                        if (result2[0] !== undefined) {
                            console.log(result2[0]);
                            $scope.GetstoreWarehousesOutputDto.countryId = result2[0];
                        }
                    });
            }


            //if ($scope.Id !== 0) {
            //    getstoreWarehouses($scope.Id);
            //}
            //else {
            //    getstoreWarehouses($scope.Id);
            //}



            $scope.saveAddEdit = function () {

                $scope.GetstoreWarehousesOutputDto.country = $scope.GetstoreWarehousesOutputDto.countryId.countryName;
                $scope.GetstoreWarehousesOutputDto.countryId = $scope.GetstoreWarehousesOutputDto.countryId.id;
                $scope.GetstoreWarehousesOutputDto.state = $scope.GetstoreWarehousesOutputDto.stateId.stateName;
                $scope.GetstoreWarehousesOutputDto.stateId = $scope.GetstoreWarehousesOutputDto.stateId.id;
                $scope.GetstoreWarehousesOutputDto.city = $scope.GetstoreWarehousesOutputDto.cityId.cityName;
                $scope.GetstoreWarehousesOutputDto.cityId = $scope.GetstoreWarehousesOutputDto.cityId.id;



                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }

                if ($scope.GetstoreWarehousesOutputDto.id === undefined || $scope.GetstoreWarehousesOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreWarehousesOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeWarehouses', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreWarehousesOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeWarehouses', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeWarehouses');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('storeWarehouses', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();