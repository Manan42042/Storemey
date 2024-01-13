(function () {
    angular.module('app').controller('app.views.storeTaxGroups', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeTaxGroups',
        function ($scope, $state, $stateParams, _thisServices) {

            $.validator.addMethod("regx", function (value, element, regexpr) {
                return regexpr.test(value);
            }, "Please enter a valid pasword.");

            $scope.validationOptions = {
                rules: {
                    taxGroupName: {
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

            var GetstoreTaxGroupsInputDto = {
                Id: 0
            };

            $scope.GetstoreTaxGroupsOutputDto = {};

            function getstoreTaxGroups(Id) {
                GetstoreTaxGroupsInputDto.Id = Id;
                _thisServices.getById(GetstoreTaxGroupsInputDto)
                    .then(function (result) {
                        $scope.GetstoreTaxGroupsOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getstoreTaxGroups($scope.Id);
            }
            else {
                getstoreTaxGroups($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                debugger;
                if ($scope.GetstoreTaxGroupsOutputDto.id === undefined || $scope.GetstoreTaxGroupsOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreTaxGroupsOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeTaxGroups', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreTaxGroupsOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeTaxGroups', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeTaxGroups');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('storeTaxGroups', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();