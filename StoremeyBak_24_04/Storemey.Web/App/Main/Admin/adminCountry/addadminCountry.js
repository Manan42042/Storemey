(function () {
    angular.module('app').controller('app.views.adminCountry', [
        '$scope', '$state', '$stateParams', 'abp.services.app.masterCountries',
        function ($scope, $state, $stateParams, _thisServices) {

            $.validator.addMethod("regx", function (value, element, regexpr) {
                return regexpr.test(value);
            }, "Please enter a valid pasword.");

            $scope.validationOptions = {
                rules: {
                    tagName: {
                        required: true,
                    },
                    description: {
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

            var GetadminCountryInputDto = {
                Id: 0
            };

            $scope.GetadminCountryOutputDto = {};

            function getadminCountry(Id) {
                GetadminCountryInputDto.Id = Id;
                _thisServices.getById(GetadminCountryInputDto)
                    .then(function (result) {
                        $scope.GetadminCountryOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getadminCountry($scope.Id);
            }
            else {
                getadminCountry($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                if ($scope.GetadminCountryOutputDto.id === undefined || $scope.GetadminCountryOutputDto.id === 0) {
                    _thisServices.create($scope.GetadminCountryOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('adminCountry', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetadminCountryOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('adminCountry', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('adminCountry');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('adminCountry', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();