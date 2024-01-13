(function () {



    angular.module('app').controller('app.views.outletregister', [
        '$scope', '$state', '$stateParams', 'abp.services.app.adminStores', 'abp.services.app.masterService', 'SweetAlert', 'abp.services.app.masterService', '$rootScope',
        function ($scope, $state, $stateParams, _thisServices, _masterService, SweetAlert, _masterService, $rootScope) {

            //$scope.tabs = [
            //    { title: 'Outlet1', content: 'Dynamic content 1' },
            //    { title: 'Outlet2', content: 'Dynamic content 2'}
            //];

            $scope.alertMe = function () {
                setTimeout(function () {
                    SweetAlert.swal({
                        title: 'You\'ve selected the alert tab!',
                        confirmButtonColor: '#007AFF'
                    });
                });
            };



            function getMasters() {
                if ($scope.Countries === undefined) {

                    _masterService.listAlloutlets()
                        .then(function (result) {
                            $scope.Outlets = result.data.items;
                            $scope.tabs = $scope.Outlets;
                            _masterService.listAllregister()
                                .then(function (result) {
                                    $scope.Registers = result.data.items;
                                    debugger;
                                    $scope.active = 1;

                                });

                        });


                }
            }
            getMasters();


            $scope.saveAddEdit = function (outletName, selected) {
                debugger;
                localStorage.setItem('outletName', outletName);
                localStorage.setItem('registerName', selected.registerName);

                $rootScope.localStorageoutletName = outletName;
                $rootScope.localStorageregisterName = selected.registerName;

                $scope.selectOutletRegister.close();
                $scope.selectOutletRegister.backgroundDismiss = true;


            };


        }
    ]);
})();