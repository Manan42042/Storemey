'use strict';
/**
 * controllers used for the dashboard
 */



angular.module('app').controller('topnavbar', ['$scope', 'Fullscreen', '$uibModal','$rootScope',
    function ($scope, Fullscreen, $uibModal, $rootScope) {


        $rootScope.localStorageoutletName = localStorage.getItem('outletName');
        $rootScope.localStorageregisterName = localStorage.getItem('registerName');

        // Fullscreen
        $scope.isFullscreen = false;
        $scope.goFullscreen = function () {

           

            $scope.isFullscreen = !$scope.isFullscreen;
            if (Fullscreen.isEnabled()) {
                Fullscreen.cancel();
            } else {
                document.documentElement.requestFullscreen();
                Fullscreen.all();
            }

            // Set Fullscreen to a specific element (bad practice)
            // Fullscreen.enable( document.getElementById('img') )


        };


        $scope.outletRegiter = function () {
            
            $scope.selectOutletRegister =  $uibModal.open({
                templateUrl: '/App/Main/views/popups/outletregister/outletregister.cshtml',
                controller: 'app.views.outletregister as vm',
                backdrop: 'static',
                size: '35perWidth',
                scope: $scope,
                animation: true,
                keyboard: true,
                resolve: {
                    //id: function () {
                    //    return user.id;
                    //}
                }
            });

        };


    }]);