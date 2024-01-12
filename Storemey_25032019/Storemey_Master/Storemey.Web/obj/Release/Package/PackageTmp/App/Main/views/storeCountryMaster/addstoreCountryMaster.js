(function () {
    angular.module('app').controller('app.views.storeCountryMaster', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeCountryMaster',
        function ($scope, $state, $stateParams, _thisServices) {

            $.validator.addMethod("regx", function (value, element, regexpr) {
                return regexpr.test(value);
            }, "Please enter a valid pasword.");

            $scope.validationOptions = {
                rules: {
                    taxGroupName: {
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

            var GetstoreCountryMasterInputDto = {
                Id: 0
            };

            $scope.GetstoreCountryMasterOutputDto = {};

            function getstoreCountryMaster(Id) {
                GetstoreCountryMasterInputDto.Id = Id;
                _thisServices.getById(GetstoreCountryMasterInputDto)
                    .then(function (result) {
                        $scope.GetstoreCountryMasterOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getstoreCountryMaster($scope.Id);
            }
            else {
                getstoreCountryMaster($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                debugger;
                if ($scope.GetstoreCountryMasterOutputDto.id === undefined || $scope.GetstoreCountryMasterOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreCountryMasterOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeCountryMaster', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreCountryMasterOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeCountryMaster', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeCountryMaster');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('storeCountryMaster', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();