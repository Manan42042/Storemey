(function () {
    angular.module('app').controller('app.views.AddEditMasterPlan', [
        '$scope', '$state', 'abp.services.app.masterPlans',
        function ($scope, $state, _thisServices) {



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

                            abp.notify.info("Saved Successfully.");
                            $state.go('masterplans');

                        });
                }
                else {
                    _thisServices.update($scope.GetMasterCountriesOutputDto)
                        .then(function () {

                            abp.notify.info("Updated Successfully.");
                            $state.go('masterplans');

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('mastercountries');
            //};

            $scope.cancelAddEdit = function () {
                $state.go('masterplans');
            };
        }
    ]);
})();