(function () {

    var app = angular.module("app");
    app.controller("app.views.uploadimages", ['$scope', '$uibModalStack', function ($scope, $uibModalStack) {


        $scope.closepopup = function () {
            $scope.$uibModalStack.close();
        };
    






        $scope.ok = function (e) {
            $scope.aside2.close();
            e.stopPropagation();
        };
        $scope.cancel = function (e) {
            $uibModalStack.dismissAll('closing');
            e.stopPropagation();
        };


    }]);

})();