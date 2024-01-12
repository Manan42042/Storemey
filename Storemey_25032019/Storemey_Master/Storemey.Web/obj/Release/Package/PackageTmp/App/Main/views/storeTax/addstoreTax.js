(function () {
    angular.module('app').controller('app.views.storeTax', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeTax',
        function ($scope, $state, $stateParams, _thisServices) {

            $.validator.addMethod("regx", function (value, element, regexpr) {
                return regexpr.test(value);
            }, "Please enter a valid pasword.");

            $scope.validationOptions = {
                rules: {
                    taxName: {
                        required: true,
                    },
                    rate: {
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

            var GetstoreTaxInputDto = {
                Id: 0
            };

            $scope.GetstoreTaxOutputDto = {};

            function getstoreTax(Id) {
                GetstoreTaxInputDto.Id = Id;
                _thisServices.getById(GetstoreTaxInputDto)
                    .then(function (result) {
                        $scope.GetstoreTaxOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getstoreTax($scope.Id);
            }
            else {
                getstoreTax($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }

                if ($scope.GetstoreTaxOutputDto.id === undefined || $scope.GetstoreTaxOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreTaxOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeTax', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreTaxOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeTax', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeTax');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('storeTax', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();