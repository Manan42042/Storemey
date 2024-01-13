(function () {
    angular.module('app').controller('app.views.storeGeneralSettings', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeGeneralSettings',
        function ($scope, $state, $stateParams, _thisServices) {

            var stateobj = $stateParams.SearchDto;
            if ($stateParams.SearchDto !== undefined && $stateParams.SearchDto !== null && $stateParams.SearchDto !== '') {
                $scope.Id = stateobj.tempID;
            }
            else {
                $stateParams.SearchDto = null;
            }

            var GetstoreGeneralSettingsInputDto = {
                id: 1
            };

            $scope.GetstoreGeneralSettingsOutputDto = {};

            function getstoreGeneralSettings(Id) {
                GetstoreGeneralSettingsInputDto.Id = Id;
                _thisServices.getById(GetstoreGeneralSettingsInputDto)
                    .then(function (result) {
                        $scope.GetstoreGeneralSettingsOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getstoreGeneralSettings($scope.Id);
            }
            else {
                getstoreGeneralSettings($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }

                if ($scope.GetstoreGeneralSettingsOutputDto.id !== undefined && $scope.GetstoreGeneralSettingsOutputDto.id !== 0) {
                    _thisServices.update($scope.GetstoreGeneralSettingsOutputDto)
                        .then(function () {
                            abp.notify.success("Updated Successfully.");
                            
                            $state.go('storeGeneralSettings', { SearchDto: $stateParams.SearchDto });
                        });
                }
                else {
                    _thisServices.create($scope.GetstoreGeneralSettingsOutputDto)
                        .then(function () {
                            $scope.GetstoreGeneralSettingsOutputDto.id = 1;
                            abp.notify.success("Updated Successfully.");
                            $state.go('storeGeneralSettings', { SearchDto: $stateParams.SearchDto });
                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeGeneralSettings');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('storeGeneralSettings', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();