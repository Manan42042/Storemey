(function () {
    angular.module('app').controller('app.views.adminBugTrackerComments', [
        '$scope', '$state', '$stateParams', 'abp.services.app.adminBugTrackerComments',
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

            var GetadminBugTrackerCommentsInputDto = {
                Id: 0
            };

            $scope.GetadminBugTrackerCommentsOutputDto = {};

            function getadminBugTrackerComments(Id) {
                GetadminBugTrackerCommentsInputDto.Id = Id;
                _thisServices.getById(GetadminBugTrackerCommentsInputDto)
                    .then(function (result) {
                        $scope.GetadminBugTrackerCommentsOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getadminBugTrackerComments($scope.Id);
            }
            else {
                getadminBugTrackerComments($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                if ($scope.GetadminBugTrackerCommentsOutputDto.id === undefined || $scope.GetadminBugTrackerCommentsOutputDto.id === 0) {
                    _thisServices.create($scope.GetadminBugTrackerCommentsOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('adminBugTrackerComments', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetadminBugTrackerCommentsOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('adminBugTrackerComments', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('adminBugTrackerComments');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('adminBugTrackerComments', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();