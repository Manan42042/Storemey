(function () {
    angular.module('app').controller('app.views.storeProductVariantValues', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeProductVariantValues', 'abp.services.app.masterService',
        function ($scope, $state, $stateParams, _thisServices, _masterService) {

            $.validator.addMethod("regx", function (value, element, regexpr) {
                return regexpr.test(value);
            }, "Please enter a valid pasword.");

            $scope.validationOptions = {
                rules: {
                    brandName: {
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

            var GetstoreProductVariantValuesInputDto = {
                Id: 0
            };

            $scope.GetstoreProductVariantValuesOutputDto = {};

            function getstoreProductVariantValues(Id) {
                GetstoreProductVariantValuesInputDto.Id = Id;
                _thisServices.getById(GetstoreProductVariantValuesInputDto)
                    .then(function (result) {
                        $scope.GetstoreProductVariantValuesOutputDto = result.data;

                        var result2 = $scope.Variants.filter(function (element) {

                            if (parseInt(element.id) == parseInt($scope.GetstoreProductVariantValuesOutputDto.variantId)) {
                                return true;
                            } else {
                                return false;
                            }
                        });
                        if (result2[0] !== undefined) {
                            console.log(result2[0]);
                            $scope.GetstoreProductVariantValuesOutputDto.uivariantId = result2[0];
                        }
                    });
            }
            //if ($scope.Id !== 0) {
            //    getstoreProductVariantValues($scope.Id);
            //}
            //else {
            //    getstoreProductVariantValues($scope.Id);
            //}



            function getMasters() {
                if ($scope.Countries === undefined) {

                    _masterService.listAllVariants()
                        .then(function (result) {
                            $scope.Variants = result.data.items;

                            if ($scope.Id !== 0) {
                                getstoreProductVariantValues($scope.Id);
                            }
                            else {
                                getstoreProductVariantValues($scope.Id);
                            }
                        });

                }
            }
            getMasters();


            $scope.saveAddEdit = function () {

                $scope.GetstoreProductVariantValuesOutputDto.variantId = $scope.GetstoreProductVariantValuesOutputDto.uivariantId.id;

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                debugger;
                if ($scope.GetstoreProductVariantValuesOutputDto.id === undefined || $scope.GetstoreProductVariantValuesOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreProductVariantValuesOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeProductVariantValues', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreProductVariantValuesOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeProductVariantValues', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeProductVariantValues');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);

                $state.go('storeProductVariantValues', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();