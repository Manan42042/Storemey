(function () {
    angular.module('app').controller('app.views.pos', [
        '$http', '$scope', '$uibModalStack', 'abp.services.app.storeProducts', 'abp.services.app.masterService','$uibModal',
        function ($http, $scope, $uibModalStack, storeProductservices, _masterService, $uibModal) {


            function getMasters() {
                if ($scope.Categories === undefined) {

                    _masterService.listAllCategories()
                        .then(function (result) {
                            $scope.Categories = result.data.items;
                        });
                }
            }
            getMasters();

            $scope.modalInstance = $uibModal.open({
                templateUrl: '/App/Main/views/popups/processbar/processbar.cshtml',
                controller: 'AngularCounterDemoCtrl as vm',
                backdrop: 'static',
                size: 'lg',
                scope: $scope,
                animation: true,
                keyboard: false,
                resolve: {
                }
            });

        }
    ]);



})();