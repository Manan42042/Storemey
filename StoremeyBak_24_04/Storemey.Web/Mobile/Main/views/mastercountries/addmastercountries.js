(function () {
    angular.module('app').controller('app.views.AddEditountrysCtrl', [
        '$scope', '$state', '$stateParams', 'abp.services.app.masterCountries',
        function ($scope, $state, $stateParams, _thisServices) {

            var stateobj = $stateParams.SearchDto;
            if ($stateParams.SearchDto !== undefined && $stateParams.SearchDto !== null && $stateParams.SearchDto !== '') {
                $scope.Id = stateobj.tempID;
            }
            else {
                $stateParams.SearchDto = null;
            }

            var GetMasterCountriesInputDto = {
                Id: 0
            };

            $scope.GetMasterCountriesOutputDto = {};

            function getmasterCountries(Id) {
                GetMasterCountriesInputDto.Id = Id;
                _thisServices.getById(GetMasterCountriesInputDto)
                    .then(function (result) {
                        $scope.GetMasterCountriesOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getmasterCountries($scope.Id);
            }
            else {
                getmasterCountries($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }

                if ($scope.GetMasterCountriesOutputDto.id === 0) {
                    _thisServices.create($scope.GetMasterCountriesOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('mastercountries', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetMasterCountriesOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('mastercountries', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('mastercountries');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('mastercountries', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();