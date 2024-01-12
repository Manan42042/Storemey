(function () {
    angular.module('app').controller('app.views.storeTaxGroupMapping', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeTaxGroupMapping',
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

            var GetstoreTaxGroupMappingInputDto = {
                Id: 0
            };

            $scope.GetstoreTaxGroupMappingOutputDto = {};

            function getstoreTaxGroupMapping(Id) {
                GetstoreTaxGroupMappingInputDto.Id = Id;
                _thisServices.getById(GetstoreTaxGroupMappingInputDto)
                    .then(function (result) {
                        $scope.GetstoreTaxGroupMappingOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getstoreTaxGroupMapping($scope.Id);
            }
            else {
                getstoreTaxGroupMapping($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                debugger;
                if ($scope.GetstoreTaxGroupMappingOutputDto.id === undefined || $scope.GetstoreTaxGroupMappingOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreTaxGroupMappingOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeTaxGroupMapping', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreTaxGroupMappingOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeTaxGroupMapping', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeTaxGroupMapping');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('storeTaxGroupMapping', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();