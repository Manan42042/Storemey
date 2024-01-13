(function () {
    angular.module('app').controller('app.views.modal-storeSeasons', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeSeasons', '$uibModalStack', 'cfpLoadingBar',
        function ($scope, $state, $stateParams, _thisServices, $uibModalStack, cfpLoadingBar) {

            $.validator.addMethod("regx", function (value, element, regexpr) {
                return regexpr.test(value);
            }, "Please enter a valid pasword.");

            $scope.validationOptions = {
                rules: {
                    SeasonName: {
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

            var GetstoreSeasonsInputDto = {
                Id: 0
            };

            $scope.GetstoreSeasonsOutputDto = {};

            function getstoreSeasons(Id) {
                GetstoreSeasonsInputDto.Id = Id;
                _thisServices.getById(GetstoreSeasonsInputDto)
                    .then(function (result) {
                        $scope.GetstoreSeasonsOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getstoreSeasons($scope.Id);
            }
            else {
                getstoreSeasons($scope.Id);
            }



            $scope.saveAddEdit = function () {

                if ($scope.GetstoreSeasonsOutputDto.id === undefined || $scope.GetstoreSeasonsOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreSeasonsOutputDto)
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
            //    $state.go('storeSeasons');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $uibModalStack.dismissAll('closing');
            };
        }
    ]);
})();