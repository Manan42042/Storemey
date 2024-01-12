(function () {
    angular.module('app').controller('app.views.storeProducts', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeProducts',
        function ($scope, $state, $stateParams, _thisServices) {

            $.validator.addMethod("regx", function (value, element, regexpr) {
                return regexpr.test(value);
            }, "Please enter a valid pasword.");

            $scope.validationOptions = {
                rules: {
                    ProductName: {
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

            var GetstoreProductsInputDto = {
                Id: 0
            };

            $scope.GetstoreProductsOutputDto = {};

            function getstoreProducts(Id) {
                GetstoreProductsInputDto.Id = Id;
                _thisServices.getById(GetstoreProductsInputDto)
                    .then(function (result) {
                        $scope.GetstoreProductsOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getstoreProducts($scope.Id);
            }
            else {
                getstoreProducts($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                debugger;
                if ($scope.GetstoreProductsOutputDto.id === undefined || $scope.GetstoreProductsOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreProductsOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeProducts', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreProductsOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeProducts', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeProducts');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);

                $state.go('storeProducts', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();