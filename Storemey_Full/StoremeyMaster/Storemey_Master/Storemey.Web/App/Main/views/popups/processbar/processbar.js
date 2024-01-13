

(function () {

    var app = angular.module("app");
    app.controller("AngularCounterDemoCtrl", ['$scope', '$timeout', function ($scope, $timeout){

        var ctrl = this;

        ctrl.myValue = 0;
        ctrl.myTarget = 0;
        ctrl.myDuration = 11000;
        ctrl.myEffect = 'swing';

       

        ctrl.counterstart = function () {
            setTimeout(function () {
                ctrl.myTarget = 100;
                ctrl.myDuration = 11000;
            }, 2000);
        };
        
        ctrl.effects = [
            'linear',
            'swing',
            'jswing',
            'easeInQuad',
            'easeOutQuad',
            'easeInOutQuad',
            'easeInCubic',
            'easeOutCubic',
            'easeInOutCubic',
            'easeInQuart',
            'easeOutQuart',
            'easeInOutQuart',
            'easeInQuint',
            'easeOutQuint',
            'easeInOutQuint',
            'easeInSine',
            'easeOutSine',
            'easeInOutSine',
            'easeInExpo',
            'easeOutExpo',
            'easeInOutExpo',
            'easeInCirc',
            'easeOutCirc',
            'easeInOutCirc',
            'easeInElastic',
            'easeOutElastic',
            'easeInOutElastic',
            'easeInBack',
            'easeOutBack',
            'easeInOutBack',
            'easeInBounce',
            'easeOutBounce',
            'easeInOutBounce'
        ];

        ctrl.finish = false;
        ctrl.counterFinish = function () {
            $scope.$apply(function () {
                ctrl.finish = true;

                $scope.modalInstance.close();
                $scope.modalInstance.dismiss('cancel');
            });
            $timeout(function () {
                ctrl.finish = false;
            }, 1000);
        };

        ctrl.toggleTarget = function () {
            ctrl.myTarget = ctrl.myTarget ? 0 : 100;
        };

        $scope.$watch('ctrl.myEffect', ctrl.toggleTarget);
    }]);

})();