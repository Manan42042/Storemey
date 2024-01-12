(function () {
    angular.module('app').controller('app.views.adminBugTrackers', [
        '$scope', '$state', '$stateParams', 'abp.services.app.adminBugTrackers',
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

            var GetadminBugTrackersInputDto = {
                Id: 0
            };

            $scope.GetadminBugTrackersOutputDto = {};

            function getadminBugTrackers(Id) {
                GetadminBugTrackersInputDto.Id = Id;
                _thisServices.getById(GetadminBugTrackersInputDto)
                    .then(function (result) {
                        $scope.GetadminBugTrackersOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getadminBugTrackers($scope.Id);
            }
            else {
                getadminBugTrackers($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                if ($scope.GetadminBugTrackersOutputDto.id === undefined || $scope.GetadminBugTrackersOutputDto.id === 0) {
                    _thisServices.create($scope.GetadminBugTrackersOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('adminBugTrackers', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetadminBugTrackersOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('adminBugTrackers', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('adminBugTrackers');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('adminBugTrackers', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();