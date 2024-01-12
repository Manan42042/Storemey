(function () {
    angular.module('app').controller('app.views.adminPlans', [
        '$scope', '$state', '$stateParams', 'abp.services.app.masterPlans',
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

            var GetadminPlansInputDto = {
                Id: 0
            };

            $scope.GetadminPlansOutputDto = {};

            function getadminPlans(Id) {
                GetadminPlansInputDto.Id = Id;
                _thisServices.getById(GetadminPlansInputDto)
                    .then(function (result) {
                        $scope.GetadminPlansOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getadminPlans($scope.Id);
            }
            else {
                getadminPlans($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                if ($scope.GetadminPlansOutputDto.id === undefined || $scope.GetadminPlansOutputDto.id === 0) {
                    _thisServices.create($scope.GetadminPlansOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('adminPlans', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetadminPlansOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('adminPlans', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('adminPlans');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('adminPlans', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();