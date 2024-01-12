(function () {

    var app = angular.module("app");
    app.controller("app.views.uploadimages", ['$scope', function ($scope) {


        $scope.closepopup = function () {
            $scope.modalInstance.close();
        };
        



    }]);

})();