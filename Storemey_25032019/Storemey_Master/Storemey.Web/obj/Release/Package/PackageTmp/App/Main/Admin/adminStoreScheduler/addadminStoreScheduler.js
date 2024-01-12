(function () {
    angular.module('app').controller('app.views.adminStoreScheduler', [
        '$scope', '$state', '$stateParams', 'abp.services.app.adminStoreScheduler',
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

            var GetadminStoreSchedulerInputDto = {
                Id: 0
            };

            $scope.GetadminStoreSchedulerOutputDto = {};

            function getadminStoreScheduler(Id) {
                GetadminStoreSchedulerInputDto.Id = Id;
                _thisServices.getById(GetadminStoreSchedulerInputDto)
                    .then(function (result) {
                        $scope.GetadminStoreSchedulerOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getadminStoreScheduler($scope.Id);
            }
            else {
                getadminStoreScheduler($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                if ($scope.GetadminStoreSchedulerOutputDto.id === undefined || $scope.GetadminStoreSchedulerOutputDto.id === 0) {
                    _thisServices.create($scope.GetadminStoreSchedulerOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('adminStoreScheduler', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetadminStoreSchedulerOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('adminStoreScheduler', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('adminStoreScheduler');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('adminStoreScheduler', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();