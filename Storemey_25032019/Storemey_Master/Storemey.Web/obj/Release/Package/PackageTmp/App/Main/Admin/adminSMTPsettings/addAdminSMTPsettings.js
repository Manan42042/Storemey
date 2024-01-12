(function () {
    angular.module('app').controller('app.views.adminSMTPsettings', [
        '$scope', '$state', '$stateParams', 'abp.services.app.adminSMTPsettings',
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

            var GetadminSMTPsettingsInputDto = {
                Id: 0
            };

            $scope.GetadminSMTPsettingsOutputDto = {};

            function getadminSMTPsettings(Id) {
                GetadminSMTPsettingsInputDto.Id = Id;
                _thisServices.getById(GetadminSMTPsettingsInputDto)
                    .then(function (result) {
                        $scope.GetadminSMTPsettingsOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getadminSMTPsettings($scope.Id);
            }
            else {
                getadminSMTPsettings($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                if ($scope.GetadminSMTPsettingsOutputDto.id === undefined || $scope.GetadminSMTPsettingsOutputDto.id === 0) {
                    _thisServices.create($scope.GetadminSMTPsettingsOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('adminSMTPsettings', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetadminSMTPsettingsOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('adminSMTPsettings', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('adminSMTPsettings');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('adminSMTPsettings', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();