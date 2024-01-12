'use strict';
/**
 * controllers used for the dashboard
 */



angular.module('app').controller('AppCtrl', ['$rootScope', '$scope', '$state', '$swipe', '$translate', '$localStorage', '$window', '$document', '$timeout', 'cfpLoadingBar', 'Fullscreen', '$transitions',
    function ($rootScope, $scope, $state, $swipe, $translate, $localStorage, $window, $document, $timeout, cfpLoadingBar, Fullscreen, $transitions) {

        // Loading bar transition
        // -----------------------------------
        var $win = $($window), $body = $('body');

        $scope.horizontalNavbarCollapsed = true;
        $scope.menuInit = function (value) {
            $scope.horizontalNavbarCollapsed = value;
        };
        $scope.menuToggle = function (value) {
            $scope.horizontalNavbarCollapsed = !$scope.horizontalNavbarCollapsed;
        };

        $scope.slickConfig = {
            enabled: true,
            dots: true,
            arrows: false,
            autoplay: false,
            draggable: true,
            infinite: false,
            slidesToShow: 1,
            slidesToScroll: 1
        };

        $transitions.onStart({}, function (trans) {
            //start loading bar on stateChangeStart
            cfpLoadingBar.start();
            $scope.horizontalNavbarCollapsed = true;

            var stateTo = trans.$to();
            if (stateTo.name == "app.pagelayouts.boxedpage") {
                $body.addClass("app-boxed-page");
            } else {
                $body.removeClass("app-boxed-page");
            }
            if (typeof CKEDITOR !== 'undefined') {
                for (name in CKEDITOR.instances) {
                    CKEDITOR.instances[name].destroy();
                }
            }
        });

        $transitions.onSuccess({}, function (trans) {
            //stop loading bar on stateChangeSuccess
            $scope.$on('$viewContentLoaded', function (event) {
                cfpLoadingBar.complete();
            });

            // scroll top the page on change state
            $('#app .main-content').css({
                position: 'relative',
                top: 'auto'
            });

            $('footer').show();

            window.scrollTo(0, 0);

            if (angular.element('.email-reader').length) {
                angular.element('.email-reader').animate({
                    scrollTop: 0
                }, 0);
            }

            // Save the route title
            $rootScope.currTitle = $state.current.title;
        });

        $rootScope.pageTitle = function () {
            return $rootScope.app.name + ' - ' + ($rootScope.currTitle || $rootScope.app.description);
        };
     
        var defaultlayout = $scope.app.defaultLayout;
        if ($localStorage.lay != undefined) {
            $localStorage.lay.isBoxedPage = true;
            $localStorage.lay.isFooterFixed = true;
            $localStorage.lay.isNavbarFixed = true;
            $localStorage.lay.isSidebarFixed = true;
        }
        // save settings to local storage
        if (angular.isDefined($localStorage.lay)) {
            $scope.app.layout = angular.copy($localStorage.lay);

        }

        $scope.resetLayout = function () {
            $scope.loading_reset = true;
            // start loading
            $timeout(function () {
                delete $localStorage.lay;
                $scope.app.layout = angular.copy($rootScope.app.defaultLayout);
                $scope.loading_reset = false;
                // stop loading
            }, 500);

        };
        $scope.saveLayout = function () {
            $scope.loading_save = true;
            // start loading
            $timeout(function () {
                $localStorage.lay = angular.copy($scope.app.layout);
                $scope.loading_save = false;
                // stop loading
            }, 500);

        };
        $scope.setLayout = function () {

            $scope.app.layout.isNavbarFixed = true;
            $scope.app.layout.isSidebarClosed = true;
            $scope.app.layout.isSidebarFixed = true;
            $scope.app.layout.isFooterFixed = true;
            $scope.app.layout.isBoxedPage = true;

        };

        //global function to scroll page up
        $scope.toTheTop = function () {

            $document.scrollTopAnimated(0, 600);

        };

        // angular translate
        // ----------------------
        $scope.language = {
            // Handles language dropdown
            listIsOpen: false,
            // list of available languages
            available: {
                'en': 'English',
                'it_IT': 'Italiano',
                'de_DE': 'Deutsch'
            },
            // display always the current ui language
            init: function () {
                var proposedLanguage = $translate.proposedLanguage() || $translate.use();
                var preferredLanguage = $translate.preferredLanguage();
                // we know we have set a preferred one in app.config
                $scope.language.selected = $scope.language.available[(proposedLanguage || preferredLanguage)];
            },
            set: function (localeId, ev) {
                $translate.use(localeId);
                $scope.language.selected = $scope.language.available[localeId];
                $scope.language.listIsOpen = !$scope.language.listIsOpen;
            }
        };

        $scope.language.init();

        // Fullscreen
        $scope.isFullscreen = false;
        $scope.goFullscreen = function () {
            $scope.isFullscreen = !$scope.isFullscreen;
            if (Fullscreen.isEnabled()) {
                Fullscreen.cancel();
            } else {
                Fullscreen.all();
            }

            // Set Fullscreen to a specific element (bad practice)
            // Fullscreen.enable( document.getElementById('img') )

        };

        // Function that find the exact height and width of the viewport in a cross-browser way
        var viewport = function () {
            var e = window, a = 'inner';
            if (!('innerWidth' in window)) {
                a = 'client';
                e = document.documentElement || document.body;
            }
            return {
                width: e[a + 'Width'],
                height: e[a + 'Height']
            };
        };
        // function that adds information in a scope of the height and width of the page
        $scope.getWindowDimensions = function () {
            return {
                'h': viewport().height,
                'w': viewport().width
            };
        };
        // Detect when window is resized and set some variables
        $scope.$watch($scope.getWindowDimensions, function (newValue, oldValue) {
            $scope.windowHeight = newValue.h;
            $scope.windowWidth = newValue.w;

            if (newValue.w >= 992) {
                $scope.isLargeDevice = true;
            } else {
                $scope.isLargeDevice = false;
            }
            if (newValue.w < 992) {
                $scope.isSmallDevice = true;
            } else {
                $scope.isSmallDevice = false;
            }
            if (newValue.w <= 768) {
                $scope.isMobileDevice = true;
            } else {
                $scope.isMobileDevice = false;
            }
        }, true);
        // Apply on resize
        $win.on('resize', function () {

            $scope.$apply();
            if ($scope.isLargeDevice) {
                $('#app .main-content').css({
                    position: 'relative',
                    top: 'auto',
                    width: 'auto'
                });
                $('footer').show();
            }
        });
    }]);
angular.module('app').controller('AcquisitionCtrl', ["$scope",
    function ($scope) {
        $scope.labels = ['January', 'February', 'March', 'April', 'May', 'June', 'July'];
        $scope.series = ['dataset'];
        $scope.data = [[65, 59, 80, 81, 56, 55, 40]];
        $scope.colors = [{
            fillColor: 'rgba(148,116,153,0.7)',
            strokeColor: 'rgba(148,116,153,0)',
            highlightFill: 'rgba(148,116,153,1)',
            highlightStroke: 'rgba(148,116,153,1)'
        }];
        // Chart.js Options - complete list at http://www.chartjs.org/docs/
        $scope.options = {
            maintainAspectRatio: false,
            showScale: false,
            barDatasetSpacing: 0,
            tooltipFontSize: 11,
            tooltipFontFamily: "'Helvetica', 'Arial', sans-serif",
            responsive: true,
            scaleBeginAtZero: true,
            scaleShowGridLines: false,
            scaleLineColor: "transparent",
            barShowStroke: false,
            barValueSpacing: 5,
            //barDatasetSpacing: 1
        };

    }]);
angular.module('app').controller('ConversionsCtrl', ["$scope",
    function ($scope) {
        $scope.labels = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
        $scope.series = ['Transactions', 'Unique Visitors'];
        $scope.data = [[65, 59, 80, 81, 56, 55, 40, 84, 64, 120, 132, 87], [172, 175, 193, 194, 161, 175, 153, 190, 175, 231, 234, 250]];
        $scope.colors = [{
            fillColor: 'rgba(91,155,209,0.5)',
            strokeColor: 'rgba(91,155,209,1)'
        }, {
            fillColor: 'rgba(91,155,209,0.5)',
            strokeColor: 'rgba(91,155,209,0.5)'
        }];

        // Chart.js Options - complete list at http://www.chartjs.org/docs/
        $scope.options = {
            maintainAspectRatio: false,
            showScale: false,
            scaleLineWidth: 0,
            responsive: true,
            scaleFontFamily: "'Helvetica', 'Arial', sans-serif",
            scaleFontSize: 11,
            scaleFontColor: "#aaa",
            scaleShowGridLines: true,
            tooltipFontSize: 11,
            tooltipFontFamily: "'Helvetica', 'Arial', sans-serif",
            tooltipTitleFontFamily: "'Helvetica', 'Arial', sans-serif",
            tooltipTitleFontSize: 12,
            scaleGridLineColor: 'rgba(0,0,0,.05)',
            scaleGridLineWidth: 1,
            bezierCurve: true,
            bezierCurveTension: 0.5,
            scaleLineColor: 'transparent',
            scaleShowVerticalLines: false,
            pointDot: false,
            pointDotRadius: 4,
            pointDotStrokeWidth: 1,
            pointHitDetectionRadius: 20,
            datasetStroke: true,
            datasetStrokeWidth: 2,
            datasetFill: true,
            animationEasing: "easeInOutExpo"
        };

    }]);
angular.module('app').controller('BarCtrl', ["$scope",
    function ($scope) {
        $scope.labels = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'a', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'i', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'];
        $scope.series = ['dataset'];
        $scope.data = [[65, 59, 80, 81, 56, 55, 40, 65, 59, 80, 81, 56, 55, 40, 65, 59, 80, 81, 56, 55, 40, 65, 59, 80, 81, 56, 80, 81]];
        $scope.colors = [{
            fillColor: 'rgba(255,255,244,0.3)',
            strokeColor: 'rgba(255,255,244,0.5)'
        }];
        // Chart.js Options - complete list at http://www.chartjs.org/docs/
        $scope.options = {
            maintainAspectRatio: false,
            showScale: false,
            barDatasetSpacing: 0,
            tooltipFontSize: 11,
            tooltipFontFamily: "'Helvetica', 'Arial', sans-serif",
            responsive: true,
            scaleBeginAtZero: true,
            scaleShowGridLines: false,
            scaleLineColor: 'transparent',
            barShowStroke: false,
            barValueSpacing: 5
        };

    }]);
angular.module('app').controller('BarCtrl2', ["$scope",
    function ($scope) {
        $scope.labels = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'a', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'i', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'];
        $scope.series = ['dataset'];
        $scope.data = [[65, 59, 80, 81, 56, 55, 40, 65, 59, 80, 81, 56, 55, 40, 65, 59, 80, 81, 56, 55, 40, 65, 59, 80, 81, 56, 80, 81]];
        $scope.colors = [{
            fillColor: 'rgba(154,137,181,0.6)',
            highlightFill: 'rgba(154,137,181,0.9)'
        }];
        // Chart.js Options - complete list at http://www.chartjs.org/docs/
        $scope.options = {
            maintainAspectRatio: false,
            showScale: false,
            barDatasetSpacing: 0,
            tooltipFontSize: 11,
            tooltipFontFamily: "'Helvetica', 'Arial', sans-serif",
            responsive: true,
            scaleBeginAtZero: true,
            scaleShowGridLines: false,
            scaleLineColor: 'transparent',
            barShowStroke: false,
            barValueSpacing: 5
        };

    }]);
angular.module('app').controller('LineCtrl', ["$scope",
    function ($scope) {
        $scope.labels = ['a', 'b', 'c', 'd', 'e', 'f', 'g'];
        $scope.series = ['dataset'];
        $scope.data = [[65, 59, 80, 81, 56, 95, 100]];
        $scope.colors = [{
            fillColor: 'rgba(0,0,0,0)',
            strokeColor: 'rgba(0,0,0,0.2)'
        }];
        // Chart.js Options - complete list at http://www.chartjs.org/docs/
        $scope.options = {
            maintainAspectRatio: false,
            showScale: false,
            scaleLineWidth: 0,
            responsive: true,
            scaleFontFamily: "'Helvetica', 'Arial', sans-serif",
            scaleFontSize: 11,
            scaleFontColor: "#aaa",
            scaleShowGridLines: true,
            tooltipFontSize: 11,
            tooltipFontFamily: "'Helvetica', 'Arial', sans-serif",
            tooltipTitleFontFamily: "'Helvetica', 'Arial', sans-serif",
            tooltipTitleFontSize: 12,
            scaleGridLineColor: 'rgba(0,0,0,.05)',
            scaleGridLineWidth: 1,
            bezierCurve: false,
            bezierCurveTension: 0.2,
            scaleLineColor: 'transparent',
            scaleShowVerticalLines: false,
            pointDot: true,
            pointDotRadius: 4,
            pointDotStrokeWidth: 1,
            pointHitDetectionRadius: 20,
            datasetStroke: true,
            datasetStrokeWidth: 2,
            datasetFill: true,
            animationEasing: "easeInOutExpo"
        };

    }]);
angular.module('app').controller('RandomCtrl', function ($scope, $interval) {
    $scope.randomUsers = 0;
    var interval = 1500;

    $scope.realtime = function () {

        var random = $interval(function () {
            $scope.randomUsers = Math.floor((Math.random() * 6) + 100);
            interval = Math.floor((Math.random() * 5000) + 1000);
            $interval.cancel(random);
            $scope.realtime();
        }, interval);
    };
    $scope.realtime();
});
angular.module('app').controller('KnobCtrl1', function ($scope) {
    $scope.value = 65;
    $scope.options = {
        unit: "%",
        readOnly: true,
        size: 70,
        fontSize: '11px',
        textColor: '#fff',
        trackWidth: 5,
        barWidth: 10,
        trackColor: 'rgba(255,255,255,0.4)',
        barColor: '#8773A8'
    };
});
angular.module('app').controller('KnobCtrl2', function ($scope) {
    $scope.value = 330;
    $scope.options = {
        unit: "MB",
        readOnly: true,
        size: 70,
        fontSize: '11px',
        textColor: '#fff',
        trackWidth: 5,
        barWidth: 10,
        trackColor: 'rgba(255,255,255,0.4)',
        barColor: '#8773A8',
        max: 1024
    };
});
angular.module('app').controller('KnobCtrl3', function ($scope) {
    $scope.value = 65;
    $scope.options = {
        unit: "%",
        readOnly: true,
        size: 70,
        fontSize: '11px',
        textColor: 'rgb(154,137,181)',
        trackWidth: 5,
        barWidth: 10,
        trackColor: 'rgba(154,137,181,0.6)',
        barColor: 'rgba(154,137,181,0.9)'
    };
});
angular.module('app').controller('KnobCtrl4', function ($scope) {
    $scope.value = 330;
    $scope.options = {
        unit: "MB",
        readOnly: true,
        size: 70,
        fontSize: '11px',
        textColor: 'rgb(154,137,181)',
        trackWidth: 5,
        barWidth: 10,
        trackColor: 'rgba(154,137,181,0.6)',
        barColor: 'rgba(154,137,181,0.9)',
        max: 1024
    };
});
angular.module('app').controller('SocialCtrl1', ["$scope",
    function ($scope) {

        $scope.labels = ['Fb', 'YT', 'Tw'];
        $scope.data = [300, 50, 100];
        $scope.colors = ['#6F83B6', '#79BEF1', '#ED5952'];

        // Chart.js Options - complete list at http://www.chartjs.org/docs/
        $scope.options = {
            responsive: false,
            tooltipFontSize: 11,
            tooltipFontFamily: "'Helvetica', 'Arial', sans-serif",
            tooltipCornerRadius: 0,
            tooltipCaretSize: 2,
            segmentShowStroke: true,
            segmentStrokeColor: '#fff',
            segmentStrokeWidth: 2,
            percentageInnerCutout: 50,
            animationSteps: 100,
            animationEasing: 'easeOutBounce',
            animateRotate: true,
            animateScale: false

        };

    }]);
angular.module('app').controller('SocialCtrl2', ["$scope",
    function ($scope) {
        $scope.labels = ['Sc', 'Ad'];
        $scope.data = [200, 150];
        $scope.colors = ['#8BC33E', '#7F8C8D'];
        // Chart.js Options - complete list at http://www.chartjs.org/docs/
        $scope.options = {
            responsive: false,
            tooltipFontSize: 11,
            tooltipFontFamily: "'Helvetica', 'Arial', sans-serif",
            tooltipCornerRadius: 0,
            tooltipCaretSize: 2,
            segmentShowStroke: true,
            segmentStrokeColor: '#fff',
            segmentStrokeWidth: 2,
            percentageInnerCutout: 50,
            animationSteps: 100,
            animationEasing: 'easeOutBounce',
            animateRotate: true,
            animateScale: false

        };

    }]);
angular.module('app').controller('SocialCtrl3', ["$scope",
    function ($scope) {

        $scope.labels = ['Facebook', 'Twitter', 'YouTube', 'Spotify'];
        $scope.data = [300, 150, 100, 80];
        $scope.colors = ['#6F83B6', '#79BEF1', '#ED5952', '#8BC33E'];

        // Chart.js Options - complete list at http://www.chartjs.org/docs/
        $scope.options = {
            responsive: false,
            scaleShowLabelBackdrop: true,
            scaleBackdropColor: 'rgba(255,255,255,0.75)',
            scaleBeginAtZero: true,
            scaleBackdropPaddingY: 2,
            scaleBackdropPaddingX: 2,
            scaleShowLine: true,
            segmentShowStroke: true,
            segmentStrokeColor: '#fff',
            segmentStrokeWidth: 2,
            animationSteps: 100,
            animationEasing: 'easeOutBounce',
            animateRotate: true,
            animateScale: false
        };
    }]);
angular.module('app').controller('SocialCtrl4', ["$scope",
    function ($scope) {

        $scope.labels = ['Facebook', 'Twitter', 'YouTube', 'Spotify'];
        $scope.data = [180, 210, 97, 60];
        $scope.colors = ['#6F83B6', '#79BEF1', '#ED5952', '#8BC33E'];
        // Chart.js Options - complete list at http://www.chartjs.org/docs/
        $scope.options = {
            responsive: false,
            scaleShowLabelBackdrop: true,
            scaleBackdropColor: 'rgba(255,255,255,0.75)',
            scaleBeginAtZero: true,
            scaleBackdropPaddingY: 2,
            scaleBackdropPaddingX: 2,
            scaleShowLine: true,
            segmentShowStroke: true,
            segmentStrokeColor: '#fff',
            segmentStrokeWidth: 2,
            animationSteps: 100,
            animationEasing: 'easeOutBounce',
            animateRotate: true,
            animateScale: false
        };
    }]);
angular.module('app').controller('PerformanceCtrl1', ["$scope",
    function ($scope) {
        $scope.value = 85;
        $scope.options = {
            size: 125,
            unit: "%",
            trackWidth: 10,
            barWidth: 10,
            step: 5,
            trackColor: 'rgba(52,152,219,.1)',
            barColor: 'rgba(69,204,206,.5)'
        };
    }]);
angular.module('app').controller('BudgetCtrl', ["$scope",
    function ($scope) {
        $scope.dailyValue = "25";
        $scope.totalValue = "750";

        $scope.dailyOptions = {
            from: 1,
            to: 100,
            step: 1,
            dimension: " $",
            className: "clip-slider",
            css: {
                background: {
                    "background-color": "silver"
                },
                before: {
                    "background-color": "#5A8770"
                }, // zone before default value
                after: {
                    "background-color": "#5A8770"
                },  // zone after default value
            }
        };
        $scope.totalOptions = {
            from: 100,
            to: 1000,
            step: 1,
            dimension: " $",
            className: "clip-slider",
            css: {
                background: {
                    "background-color": "silver"
                },
                before: {
                    "background-color": "#8773A8"
                }, // zone before default value
                after: {
                    "background-color": "#8773A8"
                },  // zone after default value
            }
        };

    }]);


angular.module('app').controller('ProductsCtrl', ["$scope",
    function ($scope) {
        $scope.labels = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
        $scope.series = ['Alpha', 'Omega', 'Kappa'];
        $scope.data = [[656, 594, 806, 817, 568, 557, 408, 843, 642, 1202, 1322, 847], [282, 484, 402, 194, 864, 275, 905, 1025, 123, 1455, 650, 1651], [768, 368, 253, 163, 437, 678, 1239, 1345, 1898, 1766, 1603, 2116]];
        $scope.colors = [{
            fillColor: 'rgba(90,135,112,0)',
            strokeColor: 'rgba(90,135,112,1)',
            pointColor: 'rgba(90,135,112,1)'
        }, {
            fillColor: 'rgba(127,140,141,0)',
            strokeColor: 'rgba(127,140,141,1)',
            pointColor: 'rgba(127,140,141,1)'
        }, {
            fillColor: 'rgba(148,116,153,0)',
            strokeColor: 'rgba(148,116,153,1)',
            pointColor: 'rgba(148,116,153,1)'
        }];
        // Chart.js Options - complete list at http://www.chartjs.org/docs/
        $scope.options = {
            maintainAspectRatio: false,
            responsive: true,
            scaleFontFamily: "'Helvetica', 'Arial', sans-serif",
            scaleFontSize: 11,
            scaleFontColor: "#aaa",
            scaleShowGridLines: true,
            tooltipFontSize: 11,
            tooltipFontFamily: "'Helvetica', 'Arial', sans-serif",
            tooltipTitleFontFamily: "'Helvetica', 'Arial', sans-serif",
            tooltipTitleFontSize: 12,
            scaleGridLineColor: 'rgba(0,0,0,.05)',
            scaleGridLineWidth: 1,
            bezierCurve: false,
            bezierCurveTension: 0.4,
            scaleLineColor: 'transparent',
            scaleShowVerticalLines: false,
            pointDot: false,
            pointDotRadius: 4,
            pointDotStrokeWidth: 1,
            pointHitDetectionRadius: 20,
            datasetStroke: true,
            tooltipXPadding: 20,
            datasetStrokeWidth: 2,
            datasetFill: true,
            animationEasing: "easeInOutExpo"
        };

    }]);
angular.module('app').controller('SalesCtrl', ["$scope",
    function ($scope) {
        $scope.labels = ['January', 'February', 'March', 'April', 'May', 'June', 'July'];
        $scope.series = ['First', 'Second'];
        $scope.data = [[65, 59, 80, 81, 56, 55, 40], [28, 48, 40, 19, 86, 27, 90]];
        $scope.colors = [{
            fillColor: 'rgba(148,116,153,0.7)',
            highlightFill: 'rgba(148,116,153,1)'
        }, {
            fillColor: 'rgba(127,140,141,0.7)',
            highlightFill: 'rgba(127,140,141,1)'
        }];
        // Chart.js Options - complete list at http://www.chartjs.org/docs/
        $scope.options = {
            maintainAspectRatio: false,
            responsive: true,
            scaleFontFamily: "'Helvetica', 'Arial', sans-serif",
            scaleFontSize: 11,
            scaleFontColor: "#aaa",
            scaleBeginAtZero: true,
            tooltipFontSize: 11,
            tooltipFontFamily: "'Helvetica', 'Arial', sans-serif",
            tooltipTitleFontFamily: "'Helvetica', 'Arial', sans-serif",
            tooltipTitleFontSize: 12,
            scaleShowGridLines: true,
            scaleLineColor: "transparent",
            scaleShowVerticalLines: false,
            scaleGridLineColor: "rgba(0,0,0,.05)",
            scaleGridLineWidth: 1,
            barShowStroke: false,
            barStrokeWidth: 2,
            barValueSpacing: 5,
            barDatasetSpacing: 1
        };

    }]);


angular.module('app').controller('InboxCtrl', ["$scope", "$state", "$interval",
    function ($scope, $state, $interval) {
        $scope.noAvatarImg = "/assets/images/default-user.png";
        var date = new Date();
        var d = date.getDate();
        var m = date.getMonth();
        var y = date.getFullYear();
        $scope.messages = [{
            "from": "John Stark",
            "date": new Date(y, m, d - 1, 19, 0),
            "subject": "Reference Request - Nicole Bell",
            "email": "stark@example.com",
            "avatar": "/assets/images/avatar-6.jpg",
            "starred": false,
            "sent": false,
            "spam": false,
            "read": false,
            "content": "<p>Hi Peter, <br>Thanks for the e-mail. It is always nice to hear from people, especially from you, Scott.</p> <p>I have not got any reply, a positive or negative one, from Seibido yet.<br>Let's wait and hope that it will make a BOOK.</p> <p>Have you finished your paperwork for Kaken and writing academic articles?<br>If you have some free time in the near future, I want to meet you and explain to you our next project.</p> <p>Why not drink out in Hiroshima if we are accepted?<br>We need to celebrate ourselves, don't we?<br>Let's have a small end-of-the-year party!</p> <p>Sincerely, K. Nakagawa</p>",
            "id": 50223456
        }, {
            "from": "James Patterson",
            "date": new Date(y, m, d - 1, 18, 43),
            "subject": "Position requirements",
            "email": "patterson@example.com",
            "avatar": "/assets/images/avatar-9.jpg",
            "starred": true,
            "sent": false,
            "read": true,
            "spam": false,
            "content": "<p>Dear Mr. Clarks</p> <p>I am interested in the Coordinator position advertised on XYZ. My resume is enclosed for your review. Given my related experience and excellent capabilities I would appreciate your consideration for this job opening. My skills are an ideal match for this position.</p> <p> <strong>Your Requirements:</strong> </p> <ul> <li>Responsible for evening operations in Student Center and other facilities, including managing registration, solving customer problems, dealing with risk management and emergencies, enforcement of department policies.</li> <li>Assists with hiring, training, and management of staff. Coordinate statistics and inventory.</li> <li>Experience in the supervision of student staff and strong interpersonal skills are also preferred.</li> <li>Valid Minnesota driver's license with good driving record. Ability to travel to different sites required.</li> <li>Experience in collegiate programming and management.</li> </ul> <p> <strong>My Qualifications:</strong> </p> <ul> <li>Register students for courses, design and manage program software, solve customer problems, enforce department policies, and serve as a contact for students, faculty, and staff.</li> <li>Hiring, training, scheduling and management of staff, managing supply inventory, and ordering.</li> <li>Minnesota driver's license with NTSA defensive driving certification.</li> <li>Extensive experience in collegiate programming and management.</li> <li>Excellent interpersonal and communication skills.</li> </ul> <p>I appreciate your taking the time to review my credentials and experience. Again, thank you for your consideration.</p> <p>Sincerely,<br> <br> James</p>",
            "id": 50223457
        }, {
            "from": "Mary Ferguson",
            "date": new Date(y, m, d - 1, 17, 51),
            "subject": "Employer's job requirements",
            "email": "mary@example.com",
            "avatar": "/assets/images/avatar-8.jpg",
            "starred": false,
            "sent": false,
            "read": true,
            "spam": false,
            "content": "<p>Dear Mr. Clarks</p> <p>In response to your advertisement in the<em> Milliken Valley Sentinel </em> for Vice President, Operations, please consider the following:</p> <p> <strong>Develop and implement strategic operational plans.</strong> <br> 15+ years aggressive food company production management experience. Planned, implemented, coordinated, and revised all production operations in plant of 250+ employees.</p> <p> <strong>Manage people, resources and processes.</strong> <br> Developed and published weekly processing and packaging schedules to meet annual corporate sales demands of up to $50 million. Met all production requirements and minimized inventory costs.</p> <p> <strong>Coach and develop direct reports.</strong> <br> Designed and presented training programs for corporate, divisional and plant management personnel. Created employee involvement program resulting in $100,000+ savings annually.</p> <p> <strong>Ensure operational service groups meet needs of external and internal customers.</strong> <br> Chaired cross-functional committee of 16 associates that developed and implemented processes, systems and procedures plant-wide. Achieved year end results of 12% increase in production, 6% reduction in direct operational costs and increased customer satisfaction rating from 85% to 93.5%.</p> <p>I welcome the opportunity to visit with you about this position. My resume has been uploaded, per your instructions. I may be reached at the number above. Thanks again for your consideration.</p> <p>Sincerely,<br> <br> Mary Ferguson</p>",
            "id": 50223458
        }, {
            "from": "Jane Fieldstone",
            "date": new Date(y, m, d - 1, 17, 38),
            "subject": "Job Offer",
            "email": "fieldstone@example.com",
            "starred": false,
            "sent": false,
            "read": true,
            "spam": false,
            "content": "<p>Dear Mr. Clarks,</p> <p>As we discussed on the phone, I am very pleased to accept the position of Marketing Manager with Smithfield Pottery. Thank you for the opportunity. I am eager to make a positive contribution to the company and to work with everyone on the Smithfield team.</p> <p>As we discussed, my starting salary will be $38,000 and health and life insurance benefits will be provided after 90 days of employment.</p> <p>I look forward to starting employment on July 1, 20XX. If there is any additional information or paperwork you need prior to then, please let me know.</p> <p>Again, thank you.</p> <p> <br> Jane Fieldstone</p>",
            "id": 50223459
        }, {
            "from": "Steven Thompson",
            "date": new Date(y, m, d - 1, 12, 2),
            "subject": "Personal invitation",
            "email": "thompson@example.com",
            "avatar": "/assets/images/avatar-3.jpg",
            "starred": false,
            "sent": false,
            "spam": false,
            "content": "<p>Dear Peter,</p> <p>Good Day!</p> <p>We would like to invite you to the coming birthday party of our son Francis John Waltz Jr. as he is celebrating his first birthday. The said party will be on November 27, 2010 at Toy Kingdom just along Almond road. All kids are expected to wear their beautiful fancy dress.</p> <p>We will be delighted with your presence in this party together with your family. We will be arranging transportation for all the guests for your convenience in going to the venue of the said party promptly.</p> <p>It is a great honor for us to see you at the party and please do confirm your attendance before the party in the given number so that we will arrange the service accordingly.</p> <p>Best regards,</p> <p>Mr. and Mrs. Thompson</p>",
            "id": 50223460
        }, {
            "from": "Michael A. Faust",
            "date": new Date(y, m, d - 1, 11, 22),
            "subject": "Re: Group Meeting",
            "email": "faust@example.com",
            "starred": true,
            "sent": false,
            "read": true,
            "spam": false,
            "content": "<p>Dear Sir</p><p>It was my pleasure to be introduced to you by Mr. Elliot Carson last Tuesday. I am delighted to make your acquaintance. Mr. Carson has highly recommended you as an esteemed businessman with integrity and good reputation.</p><p>Hence, it would be my pleasure to extend an invitation to you to join our Texas Businessmen Fellowship every last Saturday of the month from 6pm to 9pm at Texas Holiday Inn. This fellowship was set up by Texan businessmen who are sincere in assisting one another in honest business dealings and to look out for one another as a brother for another.</p><p>Attendance and membership are by invitation only. We share about the business trends and strategies as well as pitfalls to avoid so that it would make our members sharper in our business acumen. Every member is free to share his business knowledge, skills and tips. We want all members to succeed as a businessman.</p><p>As you are highly recommended by Mr. Carson, one of our founders, we shall be pleased to have you join us this month. Dress code: Smart casual. There would be a dinner at the fellowship so that members can be in a relaxed environment to mingle with one another.</p><p>We look forward to your confirmation to this invitation so that the necessary preparations can be done.</p><p>Respectfully yours,</p><p>Michael A. Faust</p>",
            "id": 50223461
        }, {
            "from": "Nicole Bell",
            "date": new Date(y, m, d - 1, 10, 31),
            "subject": "Congratulations ",
            "email": "nicole@example.com",
            "avatar": "/assets/images/avatar-2.jpg",
            "starred": false,
            "sent": false,
            "read": true,
            "spam": false,
            "content": "<p>Dear friend Peter,</p><p>I am feeling very happy today to congratulate you as you got promotion. I got the news two days before that you are promoted from the post of junior manager to the post of senior manager. You really deserved that promotion. You were a employee of that company since 10 years. In these 10 years you have served the company a lot. With your skills, hard work, intelligence you have contributed to the companies success. Due to all these reasons you had to get promotion.</p><p>Personally I am very happy to see you getting successful in your life. This time also it was very delightful to hear about your success. I hope god bless you and give you pink of health. I will always ask god to give you everything that you need in your life. He may bless you with lot of happiness in your future. </p><p>Give my love to your children and regards to your parents.</p><p>Your’s affectionately,</p><p>Nicole Bell.</p>",
            "id": 50223462
        }, {
            "from": "Google Geoff",
            "date": new Date(y, m, d - 1, 9, 38),
            "subject": "JobSearch information letter",
            "email": "mutating@example.com",
            "starred": false,
            "sent": false,
            "spam": true,
            "content": "<p>Dear recipient,</p><p>Avangar Technologies announces the beginning of a new unprecendented global employment campaign. reviser yeller winers butchery twenties Due to company's exploding growth Avangar is expanding business to the European region. During last employment campaign over 1500 people worldwide took part in Avangar's business and more than half of them are currently employed by the company. And now we are offering you one more opportunity to earn extra money working with Avangar Technologies. druggists blame classy gentry Aladdi</p><p>We are looking for honest, responsible, hard-working people that can dedicate 2-4 hours of their time per day and earn extra Â£300-500 weekly. All offered positions are currently part-time and give you a chance to work mainly from home.</p><p>lovelies hockey Malton meager reordered</p><p>Please visit Avangar's corporate web site (http://www.avangar.com/sta/home/0077.htm) for more details regarding these vacancies.</p>",
            "id": 50223463
        }, {
            "from": "Shane Michaels",
            "date": new Date(y, m, d - 2, 20, 32),
            "subject": "Marketing agreement between two companies",
            "email": "shane@example.com",
            "starred": false,
            "sent": false,
            "read": true,
            "spam": false,
            "content": "<p>This letter is with regards to the advertisement given in the yesterdays newspaper &amp; we feel proud to introduce ourselves as M/s ABC advertising agency. We are ready to take up your proposal of doing marketing work for your company. We will charge $10,000 for a week for this work of marketing. This price includes print material like posters, handbills, radio announcements, advertisements in local newspaper as well as on television channels &amp; also street-to-street mike announcements. Your company will give the wordings of the announcement &amp; the payment can be made after the work gets complete. Mode of payment will be through cheques &amp; payment should be made in three installments, first on agreement, second at the time when work commences &amp; lastly when the work is completed.</p><p>Yours sincerely,</p><p>Shane Michaels</p>",
            "id": 50223464
        }, {
            "from": "Kenneth Ross",
            "date": new Date(y, m, d - 2, 19, 59),
            "subject": "Sincere request to keep in touch.",
            "email": "kenneth@example.com",
            "avatar": "/assets/images/avatar-5.jpg",
            "starred": false,
            "sent": false,
            "read": true,
            "spam": false,
            "content": "<p>Dear Mr. Clarks,</p><p>I was shocked to see my letter after having just left and  part away from college just a couple of weeks ago. Well it’s my style to bring back together and  hold on to our college group who seems to get separated and  simply go along their own ways. Just giving it a sincere try, who wish to live life just like those college days, to share and  support for every minute crust and  fragments happening in the life.</p><p>So without any compulsion and  without any special invitation this is a one time offer cum request cum order to keep in touch and  also to live forever as best buddies. Hoping to see you at Café da Villa on this Sunday evening to celebrate our new beginning in a grand way.</p><p>Thanking you,</p>",
            "id": 50223465
        }];


        var incomingMessages = [
            {
                "from": "Nicole Bell",
                "date": new Date(),
                "subject": "New Project",
                "email": "nicole@example.com",
                "avatar": "/assets/images/avatar-2.jpg",
                "starred": false,
                "sent": false,
                "read": false,
                "spam": false,
                "content": "Hi there! Are you available around 2pm today? I’d like to talk to you about a new project",
                "id": 50223466
            },
            {
                "from": "Steven Thompson",
                "date": new Date(),
                "subject": "Apology",
                "email": "thompson@example.com",
                "avatar": "/assets/images/avatar-3.jpg",
                "starred": false,
                "sent": false,
                "read": false,
                "spam": false,
                "content": "<p>Hi Peter,</p> <p>I am very sorry for my behavior in the staff meeting this morning.</p> <p>I cut you off in the middle of your presentation, and criticized your performance in front of the staff. This was not only unprofessional, but also simply disrespectful. I let my stress about a personal matter impact my management of the office.</p>",
                "id": 50223467
            },
            {
                "from": "Mary Ferguson",
                "date": new Date(),
                "subject": "Congratulations ",
                "email": "mary@example.com",
                "avatar": "/assets/images/avatar-8.jpg",
                "starred": false,
                "sent": false,
                "read": false,
                "spam": false,
                "content": "<p>Dear Ms. Clarks,</p> I am a friend of Emily Little and she encouraged me to forward my resume to you. I know Emily through a local children's theater, for which I was a lighting assistant this semester. I also see her at college music performances, as I am in the orchestra.</p>",
                "id": 50223468
            }
        ];


        $scope.scopeVariable = 1;
        var loadMessage = function () {
            $scope.messages.push(incomingMessages[$scope.scopeVariable - 1]);
            $scope.scopeVariable++;
        };

        //Put in interval, first trigger after 10 seconds
        var add = $interval(function () {
            if ($scope.scopeVariable < 4) {
                loadMessage();
            }
        }, 10000);

        $scope.stopAdd = function () {
            if (angular.isDefined(add)) {
                $interval.cancel(add);
                add = undefined;
            }
        };
    }]);
angular.module('app').controller('ViewMessageCrtl', ['$scope', '$stateParams',
    function ($scope, $stateParams) {
        function getById(arr, id) {
            for (var d = 0, len = arr.length; d < len; d += 1) {
                if (arr[d].id == id) {

                    return arr[d];
                }
            }
        }


        $scope.message = getById($scope.messages, $stateParams.inboxID);

    }]);


angular.module('app').controller('ChatCtrl', ["$scope", function ($scope) {

    $scope.selfIdUser = 50223456;
    $scope.otherIdUser = 50223457;
    $scope.setOtherId = function (value) {

        $scope.otherIdUser = value;
    };
    var exampleDate = new Date().setTime(new Date().getTime() - 240000 * 60);

    $scope.chat = [{
        "user": "Peter Clark",
        "avatar": "/assets/images/avatar-1.jpg",
        "to": "Nicole Bell",
        "date": exampleDate,
        "content": "Hi, Nicole",
        "idUser": 50223456,
        "idOther": 50223457
    }, {
        "user": "Peter Clark",
        "avatar": "/assets/images/avatar-1.jpg",
        "to": "Nicole Bell",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 1000 * 60),
        "content": "How are you?",
        "idUser": 50223456,
        "idOther": 50223457
    }, {
        "user": "Nicole Bell",
        "avatar": "/assets/images/avatar-2.jpg",
        "to": "Peter Clark",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 1000 * 60),
        "content": "Hi, i am good",
        "idUser": 50223457,
        "idOther": 50223456
    }, {
        "user": "Peter Clark",
        "avatar": "/assets/images/avatar-1.jpg",
        "to": "Nicole Bell",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 1000 * 60),
        "content": "Glad to see you ;)",
        "idUser": 50223456,
        "idOther": 50223457
    }, {
        "user": "Nicole Bell",
        "avatar": "/assets/images/avatar-2.jpg",
        "to": "Peter Clark",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 65000 * 60),
        "content": "What do you think about my new Dashboard?",
        "idUser": 50223457,
        "idOther": 50223456
    }, {
        "user": "Nicole Bell",
        "avatar": "/assets/images/avatar-2.jpg",
        "to": "Peter Clark",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 128000 * 60),
        "content": "Alo...",
        "idUser": 50223457,
        "idOther": 50223456
    }, {
        "user": "Nicole Bell",
        "avatar": "/assets/images/avatar-2.jpg",
        "to": "Peter Clark",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 128000 * 60),
        "content": "Are you there?",
        "idUser": 50223457,
        "idOther": 50223456
    }, {
        "user": "Peter Clark",
        "avatar": "/assets/images/avatar-1.jpg",
        "to": "Nicole Bell",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 1000 * 60),
        "content": "Hi, i am here",
        "idUser": 50223456,
        "idOther": 50223457
    }, {
        "user": "Peter Clark",
        "avatar": "/assets/images/avatar-1.jpg",
        "to": "Nicole Bell",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 1000 * 60),
        "content": "Your Dashboard is great",
        "idUser": 50223456,
        "idOther": 50223457
    }, {
        "user": "Nicole Bell",
        "avatar": "/assets/images/avatar-2.jpg",
        "to": "Peter Clark",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 230000 * 60),
        "content": "How does the binding and digesting work in AngularJS?, Peter? ",
        "idUser": 50223457,
        "idOther": 50223456
    }, {
        "user": "Peter Clark",
        "avatar": "/assets/images/avatar-1.jpg",
        "to": "Nicole Bell",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 238000 * 60),
        "content": "oh that's your question?",
        "idUser": 50223456,
        "idOther": 50223457
    }, {
        "user": "Peter Clark",
        "avatar": "/assets/images/avatar-1.jpg",
        "to": "Nicole Bell",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 238000 * 60),
        "content": "little reduntant, no?",
        "idUser": 50223456,
        "idOther": 50223457
    }, {
        "user": "Peter Clark",
        "avatar": "/assets/images/avatar-1.jpg",
        "to": "Nicole Bell",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 238000 * 60),
        "content": "literally we get the question daily",
        "idUser": 50223456,
        "idOther": 50223457
    }, {
        "user": "Nicole Bell",
        "avatar": "/assets/images/avatar-2.jpg",
        "to": "Peter Clark",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 238000 * 60),
        "content": "I know. I, however, am not a nerd, and want to know",
        "idUser": 50223457,
        "idOther": 50223456
    }, {
        "user": "Peter Clark",
        "avatar": "/assets/images/avatar-1.jpg",
        "to": "Nicole Bell",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 238000 * 60),
        "content": "for this type of question, wouldn't it be better to try Google?",
        "idUser": 50223456,
        "idOther": 50223457
    }, {
        "user": "Nicole Bell",
        "avatar": "/assets/images/avatar-2.jpg",
        "to": "Peter Clark",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 238000 * 60),
        "content": "Lucky for us :)",
        "idUser": 50223457,
        "idOther": 50223456
    }, {
        "user": "Steven Thompson",
        "avatar": "/assets/images/avatar-3.jpg",
        "to": "Peter Clark",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 1000 * 60),
        "content": "Hi, Peter. I'd like to start using AngularJS.",
        "idUser": 50223458,
        "idOther": 50223456
    }, {
        "user": "Steven Thompson",
        "avatar": "/assets/images/avatar-3.jpg",
        "to": "Peter Clark",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 1000 * 60),
        "content": "There are many differences from jquery?",
        "idUser": 50223458,
        "idOther": 50223456
    }, {
        "user": "Peter Clark",
        "avatar": "/assets/images/avatar-1.jpg",
        "to": "Steven Thompson",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 5000 * 60),
        "content": "Enough!",
        "idUser": 50223456,
        "idOther": 50223458
    }, {
        "user": "Peter Clark",
        "avatar": "/assets/images/avatar-1.jpg",
        "to": "Steven Thompson",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 5000 * 60),
        "content": "In jQuery, you design a page, and then you make it dynamic...",
        "idUser": 50223456,
        "idOther": 50223458
    }, {
        "user": "Peter Clark",
        "avatar": "/assets/images/avatar-1.jpg",
        "to": "Steven Thompson",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 5000 * 60),
        "content": "but in AngularJS, you must start from the ground up with your architecture in mind",
        "idUser": 50223456,
        "idOther": 50223458
    }, {
        "user": "Steven Thompson",
        "avatar": "/assets/images/avatar-3.jpg",
        "to": "Peter Clark",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 7000 * 60),
        "content": "ok!",
        "idUser": 50223458,
        "idOther": 50223456
    }, {
        "user": "Steven Thompson",
        "avatar": "/assets/images/avatar-3.jpg",
        "to": "Peter Clark",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 7000 * 60),
        "content": "could you give me some lessons?",
        "idUser": 50223458,
        "idOther": 50223456
    }, {
        "user": "Peter Clark",
        "avatar": "/assets/images/avatar-1.jpg",
        "to": "Steven Thompson",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 7000 * 60),
        "content": "sure!",
        "idUser": 50223456,
        "idOther": 50223458
    }, {
        "user": "Steven Thompson",
        "avatar": "/assets/images/avatar-3.jpg",
        "to": "Peter Clark",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 7000 * 60),
        "content": "Thanks a lot!",
        "idUser": 50223458,
        "idOther": 50223456
    }, {
        "user": "Ella Patterson",
        "avatar": "/assets/images/avatar-4.jpg",
        "to": "Peter Clark",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 16700 * 60),
        "content": "Peter what can you tell me about the new marketing project?",
        "idUser": 50223459,
        "idOther": 50223456
    }, {
        "user": "Peter Clark",
        "avatar": "/assets/images/avatar-1.jpg",
        "to": "Steven Thompson",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 18000 * 60),
        "content": "Well, there is a lot to say. Are you free tomorrow?",
        "idUser": 50223456,
        "idOther": 50223459
    }, {
        "user": "Ella Patterson",
        "avatar": "/assets/images/avatar-4.jpg",
        "to": "Peter Clark",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 19700 * 60),
        "content": "Yes",
        "idUser": 50223459,
        "idOther": 50223456
    }, {
        "user": "Peter Clark",
        "avatar": "/assets/images/avatar-1.jpg",
        "to": "Steven Thompson",
        "date": new Date(exampleDate).setTime(new Date(exampleDate).getTime() + 19700 * 60),
        "content": "OK, we will have a meeting tomorrow afternoon",
        "idUser": 50223456,
        "idOther": 50223459
    }, {
        "user": "Kenneth Ross",
        "avatar": "/assets/images/avatar-5.jpg",
        "to": "Peter Clark",
        "date": new Date(exampleDate).setTime(new Date(exampleDate)),
        "content": "Mr. Clark, congratulations for your new project",
        "idUser": 50223460,
        "idOther": 50223456
    }, {
        "user": "Peter Clark",
        "avatar": "/assets/images/avatar-1.jpg",
        "to": "Kenneth Ross",
        "date": new Date(exampleDate).setTime(new Date(exampleDate)),
        "content": "Thank You very much Mr. Ross",
        "idUser": 50223456,
        "idOther": 50223460
    }];

    $scope.sendMessage = function () {
        var newMessage = {
            "user": "Peter Clark",
            "avatar": "/assets/images/avatar-1.jpg",
            "date": new Date(),
            "content": $scope.chatMessage,
            "idUser": $scope.selfIdUser,
            "idOther": $scope.otherIdUser
        };
        $scope.chat.push(newMessage);
        $scope.chatMessage = '';

    };
}]);