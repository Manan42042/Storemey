(function () {
    angular.module('app').controller('app.views.storeTimeZones', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeTimeZones',
        function ($scope, $state, $stateParams, _thisServices) {

            var stateobj = $stateParams.SearchDto;
            if ($stateParams.SearchDto !== undefined && $stateParams.SearchDto !== null && $stateParams.SearchDto !== '') {
                $scope.Id = stateobj.tempID;
            }
            else {
                $stateParams.SearchDto = null;
            }

            var GetstoreTimeZonesInputDto = {
                Id: 0
            };

            $scope.GetstoreTimeZonesOutputDto = {};

            function getstoreTimeZones(Id) {
                GetstoreTimeZonesInputDto.Id = Id;
                _thisServices.getById(GetstoreTimeZonesInputDto)
                    .then(function (result) {
                        $scope.GetstoreTimeZonesOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getstoreTimeZones($scope.Id);
            }
            else {
                getstoreTimeZones($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }

                if ($scope.GetstoreTimeZonesOutputDto.id === undefined || $scope.GetstoreTimeZonesOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreTimeZonesOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeTimeZones', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreTimeZonesOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeTimeZones', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeTimeZones');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('storeTimeZones', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();