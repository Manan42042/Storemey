(function () {
    angular.module('app').controller('app.views.storeTaxGroupLinks', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeTaxGroupLinks',
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

            var GetstoreTaxGroupLinksInputDto = {
                Id: 0
            };

            $scope.GetstoreTaxGroupLinksOutputDto = {};

            function getstoreTaxGroupLinks(Id) {
                GetstoreTaxGroupLinksInputDto.Id = Id;
                _thisServices.getById(GetstoreTaxGroupLinksInputDto)
                    .then(function (result) {
                        $scope.GetstoreTaxGroupLinksOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getstoreTaxGroupLinks($scope.Id);
            }
            else {
                getstoreTaxGroupLinks($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                debugger;
                if ($scope.GetstoreTaxGroupLinksOutputDto.id === undefined || $scope.GetstoreTaxGroupLinksOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreTaxGroupLinksOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeTaxGroupLinks', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreTaxGroupLinksOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeTaxGroupLinks', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeTaxGroupLinks');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('storeTaxGroupLinks', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();