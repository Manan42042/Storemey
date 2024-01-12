(function () {
    angular.module('app').controller('app.views.adminCurrancy', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeCurrencies',
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

            var GetadminCurrancyInputDto = {
                Id: 0
            };

            $scope.GetadminCurrancyOutputDto = {};

            function getadminCurrancy(Id) {
                GetadminCurrancyInputDto.Id = Id;
                _thisServices.getById(GetadminCurrancyInputDto)
                    .then(function (result) {
                        $scope.GetadminCurrancyOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getadminCurrancy($scope.Id);
            }
            else {
                getadminCurrancy($scope.Id);
            }



            $scope.saveAddEdit = function () {
                debugger;
                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                if ($scope.GetadminCurrancyOutputDto.id === undefined || $scope.GetadminCurrancyOutputDto.id === 0) {
                    _thisServices.create($scope.GetadminCurrancyOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('adminCurrancy', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetadminCurrancyOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('adminCurrancy', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('adminCurrancy');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('adminCurrancy', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();