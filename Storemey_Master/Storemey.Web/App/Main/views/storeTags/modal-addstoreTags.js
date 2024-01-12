(function () {
    angular.module('app').controller('app.views.modal-storeTags', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeTags', '$ngConfirm', '$uibModalStack', 'cfpLoadingBar',
        function ($scope, $state, $stateParams, _thisServices, $ngConfirm, $uibModalStack, cfpLoadingBar) {

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

            var GetstoreTagsInputDto = {
                Id: 0
            };

            $scope.GetstoreTagsOutputDto = {};

            function getstoreTags(Id) {
                GetstoreTagsInputDto.Id = Id;
                _thisServices.getById(GetstoreTagsInputDto)
                    .then(function (result) {
                        $scope.GetstoreTagsOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getstoreTags($scope.Id);
            }
            else {
                getstoreTags($scope.Id);
            }



            $scope.saveAddEdit = function () {

                if ($scope.GetstoreTagsOutputDto.id === undefined || $scope.GetstoreTagsOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreTagsOutputDto)
                        .then(function () {

                            cfpLoadingBar.start();
                            $scope.getMastersdata();

                            abp.notify.success("Saved Successfully.");
                            //$state.go('storeCategories', { SearchDto: $stateParams.SearchDto });
                            $uibModalStack.dismissAll('closing');
                            cfpLoadingBar.complete();

                        });
                }
               
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeTags');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $uibModalStack.dismissAll('closing');
            };
        }
    ]);
})();