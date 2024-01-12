(function () {
    angular.module('app').controller('app.views.adminUpdateAllDatabase', [
        '$scope', '$state', '$stateParams', 'abp.services.app.adminUpdateAllDatabase',
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

            var GetadminUpdateAllDatabaseInputDto = {
                Id: 0
            };

            $scope.GetadminUpdateAllDatabaseOutputDto = {};

            function getadminUpdateAllDatabase(Id) {
                GetadminUpdateAllDatabaseInputDto.Id = Id;
                _thisServices.getById(GetadminUpdateAllDatabaseInputDto)
                    .then(function (result) {
                        $scope.GetadminUpdateAllDatabaseOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getadminUpdateAllDatabase($scope.Id);
            }
            else {
                getadminUpdateAllDatabase($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                if ($scope.GetadminUpdateAllDatabaseOutputDto.id === undefined || $scope.GetadminUpdateAllDatabaseOutputDto.id === 0) {
                    _thisServices.create($scope.GetadminUpdateAllDatabaseOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('adminUpdateAllDatabase', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetadminUpdateAllDatabaseOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('adminUpdateAllDatabase', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('adminUpdateAllDatabase');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('adminUpdateAllDatabase', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();