(function () {



    angular.module('app').controller('app.views.homepage', [
        '$scope', '$state', '$stateParams', 'abp.services.app.adminStores', 'abp.services.app.masterService', '$aside',
        function ($scope, $state, $stateParams, _thisServices, _masterService, $aside) {


            //$scope.aside = {
            //    "title": "Title",
            //    "content": "Hello Aside<br />This is a multiline message!",
            //    "duration": 100,
            //    "templateUrl": '/App/Main/views/popups/storeowner/NewStoreOwner.cshtml'
            //};

            //$scope.aside2 = $aside.open({
            //    templateUrl: '/App/Main/views/popups/storeowner/NewStoreOwner.cshtml',
            //    controller: 'app.views.storeOwner',
            //    placement: 'left',
            //    size: 'lg'
            //});



            //$scope.aside = function () {
            //    $aside.open({
            //        templateUrl: '/App/Main/views/popups/storeowner/NewStoreOwner.cshtml',
            //        controller: 'app.views.storeOwner',
            //        placement: 'right',
            //        size: 'lg',
            //        scope: $scope,
            //    });
            //};



            $scope.aside2 = function () {
                $aside.open({
                    templateUrl: '/App/Main/views/popups/imageupload/Uploadimages.cshtml',
                    controller: 'app.views.uploadimages',
                    placement: 'right',
                    size: '50perWidth',
                    disableClose: 'true',
                    scope: $scope,

                });
            };

        }
    ]);
})();