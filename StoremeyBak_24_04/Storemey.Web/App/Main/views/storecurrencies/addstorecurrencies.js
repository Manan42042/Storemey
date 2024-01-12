(function () {
    angular.module('app').controller('app.views.storecurrencies', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeCurrencies',
        function ($scope, $state, $stateParams, _thisServices) {

            var stateobj = $stateParams.SearchDto;
            if ($stateParams.SearchDto !== undefined && $stateParams.SearchDto !== null && $stateParams.SearchDto !== '') {
                $scope.Id = stateobj.tempID;
            }
            else {
                $stateParams.SearchDto = null;
            }

            var GetstorecurrenciesInputDto = {
                Id: 0
            };

            $scope.GetstorecurrenciesOutputDto = {};

            function getstorecurrencies(Id) {
                GetstorecurrenciesInputDto.Id = Id;
                _thisServices.getById(GetstorecurrenciesInputDto)
                    .then(function (result) {
                        $scope.GetstorecurrenciesOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getstorecurrencies($scope.Id);
            }
            else {
                getstorecurrencies($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }

                if ($scope.GetstorecurrenciesOutputDto.id === undefined || $scope.GetstorecurrenciesOutputDto.id === 0) {
                    _thisServices.create($scope.GetstorecurrenciesOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storecurrencies', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstorecurrenciesOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storecurrencies', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storecurrencies');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('storecurrencies', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();