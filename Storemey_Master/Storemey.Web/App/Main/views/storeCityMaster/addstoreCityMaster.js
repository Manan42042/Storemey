(function () {
    angular.module('app').controller('app.views.storeCityMaster', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeCityMaster', 'abp.services.app.masterService',
        function ($scope, $state, $stateParams, _thisServices, _masterService) {


            function getMasters() {
                if ($scope.Countries === undefined) {

                    _masterService.listAllCountries()
                        .then(function (result) {
                            $scope.Countries = result.data.items;

                            if ($scope.Id !== 0) {
                                getstoreCityMaster($scope.Id);
                            }
                            else {
                                getstoreCityMaster($scope.Id);
                            }
                        });

                }
            }
            getMasters();


            $scope.$watch("GetstoreCityMasterOutputDto.uicountryId", function (newValue, oldValue) {

                if (newValue !== undefined && newValue !== "" && newValue !== 0) {
                    //$scope.GetstoreCityMasterOutputDto.countryId = $scope.GetstoreCityMasterOutputDto.con.id;
                    //$scope.GetstoreCityMasterOutputDto.country = $scope.GetstoreCityMasterOutputDto.con.countryName;

                    _masterService.listAllStates(newValue.id)
                        .then(function (result) {
                            $scope.States = result.data.items;


                            var result3 = $scope.States.filter(function (element) {
                                if (parseInt(element.id) == parseInt($scope.GetstoreCityMasterOutputDto.stateId)) {
                                    return true;
                                } else {
                                    return false;
                                }
                            });
                            if (result3[0] !== undefined) {
                                console.log(result3[0]);
                                $scope.GetstoreCityMasterOutputDto.uistateId = result3[0];
                            }

                        });
                }
            });


            $.validator.addMethod("regx", function (value, element, regexpr) {
                return regexpr.test(value);
            }, "Please enter a valid pasword.");

            $scope.validationOptions = {
                rules: {
                    cityName: {
                        required: true,
                    },
                    zipcode: {
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

            var GetstoreCityMasterInputDto = {
                Id: 0
            };

            $scope.GetstoreCityMasterOutputDto = {};

            function getstoreCityMaster(Id) {
                GetstoreCityMasterInputDto.Id = Id;
                _thisServices.getById(GetstoreCityMasterInputDto)
                    .then(function (result) {
                        $scope.GetstoreCityMasterOutputDto = result.data;
                        var result2 = $scope.Countries.filter(function (element) {
                            if (parseInt(element.id) == parseInt($scope.GetstoreCityMasterOutputDto.countryId)) {
                                return true;
                            } else {
                                return false;
                            }
                        });
                        if (result2[0] !== undefined) {
                            console.log(result2[0]);
                            $scope.GetstoreCityMasterOutputDto.uicountryId = result2[0];
                        }
                    });
            }
            //if ($scope.Id !== 0) {
            //    getstoreCityMaster($scope.Id);
            //}
            //else {
            //    getstoreCityMaster($scope.Id);
            //}



            $scope.saveAddEdit = function () {

                $scope.GetstoreCityMasterOutputDto.countryId = $scope.GetstoreCityMasterOutputDto.uicountryId.id;
                $scope.GetstoreCityMasterOutputDto.stateId = $scope.GetstoreCityMasterOutputDto.uistateId.id;



                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                debugger;
                if ($scope.GetstoreCityMasterOutputDto.id === undefined || $scope.GetstoreCityMasterOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreCityMasterOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeCityMaster', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreCityMasterOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeCityMaster', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeCityMaster');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('storeCityMaster', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();