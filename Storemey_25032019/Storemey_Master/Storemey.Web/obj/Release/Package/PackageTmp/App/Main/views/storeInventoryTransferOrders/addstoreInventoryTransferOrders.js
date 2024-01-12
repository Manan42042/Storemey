(function () {
    angular.module('app').controller('app.views.storeInventoryTransferOrders', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeInventoryTransferOrders',
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

            var GetstoreInventoryTransferOrdersInputDto = {
                Id: 0
            };

            $scope.GetstoreInventoryTransferOrdersOutputDto = {};

            function getstoreInventoryTransferOrders(Id) {
                GetstoreInventoryTransferOrdersInputDto.Id = Id;
                _thisServices.getById(GetstoreInventoryTransferOrdersInputDto)
                    .then(function (result) {
                        $scope.GetstoreInventoryTransferOrdersOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getstoreInventoryTransferOrders($scope.Id);
            }
            else {
                getstoreInventoryTransferOrders($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                debugger;
                if ($scope.GetstoreInventoryTransferOrdersOutputDto.id === undefined || $scope.GetstoreInventoryTransferOrdersOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreInventoryTransferOrdersOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeInventoryTransferOrders', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreInventoryTransferOrdersOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeInventoryTransferOrders', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeInventoryTransferOrders');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);

                $state.go('storeInventoryTransferOrders', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();