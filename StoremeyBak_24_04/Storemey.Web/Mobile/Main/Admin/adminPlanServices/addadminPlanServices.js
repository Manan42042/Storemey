(function () {
    angular.module('app').controller('app.views.adminPlanServices', [
        '$scope', '$state', '$stateParams', 'abp.services.app.masterPlanServices',
        function ($scope, $state, $stateParams, _thisServices) {

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

            var GetadminPlanServicesInputDto = {
                Id: 0
            };

            $scope.GetadminPlanServicesOutputDto = {};

            function getadminPlanServices(Id) {
                GetadminPlanServicesInputDto.Id = Id;
                _thisServices.getById(GetadminPlanServicesInputDto)
                    .then(function (result) {
                        $scope.GetadminPlanServicesOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getadminPlanServices($scope.Id);
            }
            else {
                getadminPlanServices($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                if ($scope.GetadminPlanServicesOutputDto.id === undefined || $scope.GetadminPlanServicesOutputDto.id === 0) {
                    _thisServices.create($scope.GetadminPlanServicesOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('adminPlanServices', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetadminPlanServicesOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('adminPlanServices', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('adminPlanServices');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('adminPlanServices', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();