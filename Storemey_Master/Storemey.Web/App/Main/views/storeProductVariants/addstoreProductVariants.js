(function () {
    angular.module('app').controller('app.views.storeProductVariants', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeProductVariants',
        function ($scope, $state, $stateParams, _thisServices) {

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

            var GetstoreProductVariantsInputDto = {
                Id: 0
            };

            $scope.GetstoreProductVariantsOutputDto = {};

            function getstoreProductVariants(Id) {
                GetstoreProductVariantsInputDto.Id = Id;
                _thisServices.getById(GetstoreProductVariantsInputDto)
                    .then(function (result) {
                        $scope.GetstoreProductVariantsOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getstoreProductVariants($scope.Id);
            }
            else {
                getstoreProductVariants($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                debugger;
                if ($scope.GetstoreProductVariantsOutputDto.id === undefined || $scope.GetstoreProductVariantsOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreProductVariantsOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeProductVariants', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreProductVariantsOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeProductVariants', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeProductVariants');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);

                $state.go('storeProductVariants', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();