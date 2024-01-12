(function () {
    angular.module('app').controller('app.views.storePaymentTypes', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storePaymentTypes',
        function ($scope, $state, $stateParams, _thisServices) {

            $.validator.addMethod("regx", function (value, element, regexpr) {
                return regexpr.test(value);
            }, "Please enter a valid pasword.");

            $scope.validationOptions = {
                rules: {
                    name: {
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

            var GetstorePaymentTypesInputDto = {
                Id: 0
            };

            $scope.GetstorePaymentTypesOutputDto = {};

            function getstorePaymentTypes(Id) {
                GetstorePaymentTypesInputDto.Id = Id;
                _thisServices.getById(GetstorePaymentTypesInputDto)
                    .then(function (result) {
                        $scope.GetstorePaymentTypesOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getstorePaymentTypes($scope.Id);
            }
            else {
                getstorePaymentTypes($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                debugger;
                if ($scope.GetstorePaymentTypesOutputDto.id === undefined || $scope.GetstorePaymentTypesOutputDto.id === 0) {
                    _thisServices.create($scope.GetstorePaymentTypesOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storePaymentTypes', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstorePaymentTypesOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storePaymentTypes', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storePaymentTypes');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('storePaymentTypes', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();