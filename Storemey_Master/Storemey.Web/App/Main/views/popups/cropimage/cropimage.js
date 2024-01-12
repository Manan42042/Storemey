(function () {

    var app = angular.module("app");
    app.controller("app.views.cropimagenew", ['$scope', function ($scope) {

        $scope.myImage = $scope.data.src;
        $scope.data.result = $scope.data.src;

    }]);

})();