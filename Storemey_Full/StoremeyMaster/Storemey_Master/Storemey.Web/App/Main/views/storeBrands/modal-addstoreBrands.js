(function () {
    angular.module('app').controller('app.views.modal-storeBrands', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeBrands',  '$uibModalStack', 'cfpLoadingBar',
        function ($scope, $state, $stateParams, _thisServices, $uibModalStack, cfpLoadingBar) {

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

            var GetstoreBrandsInputDto = {
                Id: 0
            };

            $scope.GetstoreBrandsOutputDto = {};

            function getstoreBrands(Id) {
                GetstoreBrandsInputDto.Id = Id;
                _thisServices.getById(GetstoreBrandsInputDto)
                    .then(function (result) {
                        $scope.GetstoreBrandsOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getstoreBrands($scope.Id);
            }
            else {
                getstoreBrands($scope.Id);
            }



            $scope.saveAddEdit = function () {

                //var $loginForm = $('#frmCountry');
                //if (!$loginForm.valid()) {
                //    return;
                //}
                //debugger;
                if ($scope.GetstoreBrandsOutputDto.id === undefined || $scope.GetstoreBrandsOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreBrandsOutputDto)
                        .then(function () {

                            cfpLoadingBar.start();
                            $scope.getMastersdata();

                            abp.notify.success("Saved Successfully.");
                            $uibModalStack.dismissAll('closing');
                            cfpLoadingBar.complete();

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreBrandsOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeBrands', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeBrands');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);

                $uibModalStack.dismissAll('closing');
            };
        }
    ]);
})();