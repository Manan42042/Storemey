(function () {
    angular.module('app').controller('app.views.adminStores', [
        '$scope', '$state', '$stateParams', 'abp.services.app.adminStores',
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

            var GetadminStoresInputDto = {
                Id: 0
            };

            $scope.GetadminStoresOutputDto = {};

            function getadminStores(Id) {
                GetadminStoresInputDto.Id = Id;
                _thisServices.getById(GetadminStoresInputDto)
                    .then(function (result) {
                        $scope.GetadminStoresOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getadminStores($scope.Id);
            }
            else {
                getadminStores($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                if ($scope.GetadminStoresOutputDto.id === undefined || $scope.GetadminStoresOutputDto.id === 0) {
                    _thisServices.create($scope.GetadminStoresOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('adminStores', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetadminStoresOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('adminStores', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('adminStores');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('adminStores', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();