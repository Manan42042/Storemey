(function () {
    angular.module('app').controller('app.views.storeStateMaster', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeStateMaster', 'abp.services.app.masterService',
        function ($scope, $state, $stateParams, _thisServices, _masterService) {




            $.validator.addMethod("regx", function (value, element, regexpr) {
                return regexpr.test(value);
            }, "Please enter a valid pasword.");

            $scope.validationOptions = {
                rules: {
                    stateName: {
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

            var GetstoreStateMasterInputDto = {
                Id: 0
            };




            function getMasters() {
                if ($scope.Countries === undefined) {

                    _masterService.listAllCountries()
                        .then(function (result) {
                            $scope.Countries = result.data.items;

                            if ($scope.Id !== 0) {
                                getstoreStateMaster($scope.Id);
                            }
                            else {
                                getstoreStateMaster($scope.Id);
                            }
                        });

                }
            }
            getMasters();




            $scope.GetstoreStateMasterOutputDto = {};

            function getstoreStateMaster(Id) {
                GetstoreStateMasterInputDto.Id = Id;
                _thisServices.getById(GetstoreStateMasterInputDto)
                    .then(function (result) {
                        $scope.GetstoreStateMasterOutputDto = result.data;
                        var result2 = $scope.Countries.filter(function (element) {

                            if (parseInt(element.id) == parseInt($scope.GetstoreStateMasterOutputDto.countryId)) {
                                return true;
                            } else {
                                return false;
                            }
                        });
                        if (result2[0] !== undefined) {
                            console.log(result2[0]);
                            $scope.GetstoreStateMasterOutputDto.countryId = result2[0];
                        }
                    });
            }




            $scope.saveAddEdit = function () {

                $scope.GetstoreStateMasterOutputDto.countryId = $scope.GetstoreStateMasterOutputDto.countryId.id;

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }

                if ($scope.GetstoreStateMasterOutputDto.id === undefined || $scope.GetstoreStateMasterOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreStateMasterOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeStateMaster', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreStateMasterOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeStateMaster', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeStateMaster');
            //};

            $scope.cancelAddEdit = function () {
                window.scrollTo(0, 0);
                $state.go('storeStateMaster', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();