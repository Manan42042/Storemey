﻿(function () {
    angular.module('app').controller('app.views.storeInventoryPurchaseOrders', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeInventoryPurchaseOrders',
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

            var GetstoreInventoryPurchaseOrdersInputDto = {
                Id: 0
            };

            $scope.GetstoreInventoryPurchaseOrdersOutputDto = {};

            function getstoreInventoryPurchaseOrders(Id) {
                GetstoreInventoryPurchaseOrdersInputDto.Id = Id;
                _thisServices.getById(GetstoreInventoryPurchaseOrdersInputDto)
                    .then(function (result) {
                        $scope.GetstoreInventoryPurchaseOrdersOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getstoreInventoryPurchaseOrders($scope.Id);
            }
            else {
                getstoreInventoryPurchaseOrders($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                debugger;
                if ($scope.GetstoreInventoryPurchaseOrdersOutputDto.id === undefined || $scope.GetstoreInventoryPurchaseOrdersOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreInventoryPurchaseOrdersOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeInventoryPurchaseOrders', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreInventoryPurchaseOrdersOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeInventoryPurchaseOrders', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeInventoryPurchaseOrders');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);

                $state.go('storeInventoryPurchaseOrders', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();