(function () {
    angular.module('app').controller('app.views.adminPlanPrice', [
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

            var GetadminPlanPriceInputDto = {
                Id: 0
            };

            $scope.GetadminPlanPriceOutputDto = {};

            function getadminPlanPrice(Id) {
                GetadminPlanPriceInputDto.Id = Id;
                _thisServices.getById(GetadminPlanPriceInputDto)
                    .then(function (result) {
                        $scope.GetadminPlanPriceOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getadminPlanPrice($scope.Id);
            }
            else {
                getadminPlanPrice($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                if ($scope.GetadminPlanPriceOutputDto.id === undefined || $scope.GetadminPlanPriceOutputDto.id === 0) {
                    _thisServices.create($scope.GetadminPlanPriceOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('adminPlanPrice', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetadminPlanPriceOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('adminPlanPrice', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('adminPlanPrice');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('adminPlanPrice', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();