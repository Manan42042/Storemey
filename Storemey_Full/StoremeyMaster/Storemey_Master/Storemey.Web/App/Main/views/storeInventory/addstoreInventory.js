(function () {
    angular.module('app').controller('app.views.storeInventory', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeInventory',
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

            var GetstoreInventoryInputDto = {
                Id: 0
            };

            $scope.GetstoreInventoryOutputDto = {};

            function getstoreInventory(Id) {
                GetstoreInventoryInputDto.Id = Id;
                _thisServices.getById(GetstoreInventoryInputDto)
                    .then(function (result) {
                        $scope.GetstoreInventoryOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getstoreInventory($scope.Id);
            }
            else {
                getstoreInventory($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                debugger;
                if ($scope.GetstoreInventoryOutputDto.id === undefined || $scope.GetstoreInventoryOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreInventoryOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeInventory', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreInventoryOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeInventory', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeInventory');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);

                $state.go('storeInventory', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();