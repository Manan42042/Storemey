'use strict';

/**
 * Config constant
 */
angular.module('app').constant('APP_MEDIAQUERY', {
    'desktopXL': 1200,
    'desktop': 992,
    'tablet': 768,
    'mobile': 480
});
angular.module('app').constant('JS_REQUIRES', {
    //*** Scripts
    scripts: {
        //*** Javascript Plugins
        'd3': '/Content/StoremeyTheme/ANGULARJS/bower_components/d3/d3.min.js',

        //*** jQuery Plugins
        'chartjs': '/Content/StoremeyTheme/ANGULARJS/bower_components/Chart.js/Chart.min.js',
        'ckeditor-plugin': '/Content/StoremeyTheme/ANGULARJS/bower_components/ckeditor/ckeditor.js',
        'jquery-nestable-plugin': ['/Content/StoremeyTheme/ANGULARJS/bower_components/jquery-nestable/jquery.nestable.js'],
        'touchspin-plugin': ['/Content/StoremeyTheme/ANGULARJS/bower_components/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.min.js', '/Content/StoremeyTheme/ANGULARJS/bower_components/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.min.css'],
        'jquery-appear-plugin': ['/Content/StoremeyTheme/ANGULARJS/bower_components/jquery-appear/build/jquery.appear.min.js'],
        'spectrum-plugin': ['/Content/StoremeyTheme/ANGULARJS/bower_components/spectrum/spectrum.js', '/Content/StoremeyTheme/ANGULARJS/bower_components/spectrum/spectrum.css'],
		'jcrop-plugin': ['/Content/StoremeyTheme/ANGULARJS/bower_components/Jcrop/js/Jcrop.min.js', '/Content/StoremeyTheme/ANGULARJS/bower_components/Jcrop/css/Jcrop.min.css'],
		
        //*** Controllers
        'dashboardCtrl': '/assets/js/controllers/dashboardCtrl.js',
        'iconsCtrl': '/assets/js/controllers/iconsCtrl.js',
        'vAccordionCtrl': '/assets/js/controllers/vAccordionCtrl.js',
        'ckeditorCtrl': '/assets/js/controllers/ckeditorCtrl.js',
        'laddaCtrl': '/assets/js/controllers/laddaCtrl.js',
        'ngTableCtrl': '/assets/js/controllers/ngTableCtrl.js',
        'cropCtrl': '/assets/js/controllers/cropCtrl.js',
        'asideCtrl': '/assets/js/controllers/asideCtrl.js',
        'toasterCtrl': '/assets/js/controllers/toasterCtrl.js',
        'sweetAlertCtrl': '/assets/js/controllers/sweetAlertCtrl.js',
        'mapsCtrl': '/assets/js/controllers/mapsCtrl.js',
        'chartsCtrl': '/assets/js/controllers/chartsCtrl.js',
        'calendarCtrl': '/assets/js/controllers/calendarCtrl.js',
        'nestableCtrl': '/assets/js/controllers/nestableCtrl.js',
        'validationCtrl': ['/assets/js/controllers/validationCtrl.js'],
        'userCtrl': ['/assets/js/controllers/userCtrl.js'],
        'selectCtrl': '/assets/js/controllers/selectCtrl.js',
        'wizardCtrl': '/assets/js/controllers/wizardCtrl.js',
        'uploadCtrl': '/assets/js/controllers/uploadCtrl.js',
        'treeCtrl': '/assets/js/controllers/treeCtrl.js',
        'inboxCtrl': '/assets/js/controllers/inboxCtrl.js',
        'xeditableCtrl': '/assets/js/controllers/xeditableCtrl.js',
        'chatCtrl': '/assets/js/controllers/chatCtrl.js',
        'dynamicTableCtrl': '/assets/js/controllers/dynamicTableCtrl.js',
        'notificationIconsCtrl': '/assets/js/controllers/notificationIconsCtrl.js',
        'dateRangeCtrl': '/assets/js/controllers/daterangeCtrl.js',
        'notifyCtrl': '/assets/js/controllers/notifyCtrl.js',
        'sliderCtrl': '/assets/js/controllers/sliderCtrl.js',
        'knobCtrl': '/assets/js/controllers/knobCtrl.js',
        'crop2Ctrl': '/assets/js/controllers/crop2Ctrl.js',
    },
    //*** angularJS Modules
    modules: [{
        name: 'toaster',
        files: ['/Content/StoremeyTheme/ANGULARJS/bower_components/AngularJS-Toaster/toaster.js', '/Content/StoremeyTheme/ANGULARJS/bower_components/AngularJS-Toaster/toaster.css']
    }, {
        name: 'angularBootstrapNavTree',
        files: ['/Content/StoremeyTheme/ANGULARJS/bower_components/angular-bootstrap-nav-tree/dist/abn_tree_directive.js', '/Content/StoremeyTheme/ANGULARJS/bower_components/angular-bootstrap-nav-tree/dist/abn_tree.css']
    }, {
        name: 'ngTable',
        files: ['/Content/StoremeyTheme/ANGULARJS/bower_components/ng-table/dist/ng-table.min.js', '/Content/StoremeyTheme/ANGULARJS/bower_components/ng-table/dist/ng-table.min.css']
    }, {
        name: 'ui.mask',
        files: ['/Content/StoremeyTheme/ANGULARJS/bower_components/angular-ui-mask/dist/mask.min.js']
    }, {
        name: 'ngImgCrop',
        files: ['/Content/StoremeyTheme/ANGULARJS/bower_components/ng-img-crop/compile/minified/ng-img-crop.js', '/Content/StoremeyTheme/ANGULARJS/bower_components/ng-img-crop/compile/minified/ng-img-crop.css']
    }, {
        name: 'angularFileUpload',
        files: ['/Content/StoremeyTheme/ANGULARJS/bower_components/angular-file-upload/dist/angular-file-upload.min.js']
    }, {
        name: 'monospaced.elastic',
        files: ['/Content/StoremeyTheme/ANGULARJS/bower_components/angular-elastic/elastic.js']
    }, {
        name: 'ngMap',
        files: ['/Content/StoremeyTheme/ANGULARJS/bower_components/ngmap/build/scripts/ng-map.min.js']
    }, {
        name: 'chart.js',
            files: ['/Content/StoremeyTheme/ANGULARJS/bower_components/angular-chart.js/dist/angular-chart.min.js', '/Content/StoremeyTheme/ANGULARJS/bower_components/angular-chart.js/dist/angular-chart.min.css']
    }, {
        name: 'flow',
        files: ['/Content/StoremeyTheme/ANGULARJS/bower_components/ng-flow/dist/ng-flow-standalone.min.js']
    }, {
        name: 'ckeditor',
        files: ['/Content/StoremeyTheme/ANGULARJS/bower_components/angular-ckeditor/angular-ckeditor.min.js']
    }, {
        name: 'mwl.calendar',
        files: ['/Content/StoremeyTheme/ANGULARJS/bower_components/angular-bootstrap-calendar/dist/js/angular-bootstrap-calendar-tpls.js', '/Content/StoremeyTheme/ANGULARJS/bower_components/angular-bootstrap-calendar/dist/css/angular-bootstrap-calendar.min.css', '/assets/js/config/config-calendar.js']
    }, {
        name: 'ng-nestable',
        files: ['/Content/StoremeyTheme/ANGULARJS/bower_components/angular-nestable/src/angular-nestable.js']
    }, {
        name: 'ngNotify',
        files: ['/Content/StoremeyTheme/ANGULARJS/bower_components/ng-notify/dist/ng-notify.min.js', '/Content/StoremeyTheme/ANGULARJS/bower_components/ng-notify/dist/ng-notify.min.css']
    }, {
        name: 'xeditable',
        files: ['/Content/StoremeyTheme/ANGULARJS/bower_components/angular-xeditable/dist/js/xeditable.min.js', '/Content/StoremeyTheme/ANGULARJS/bower_components/angular-xeditable/dist/css/xeditable.css', '/assets/js/config/config-xeditable.js']
    }, {
        name: 'checklist-model',
        files: ['/Content/StoremeyTheme/ANGULARJS/bower_components/checklist-model/checklist-model.js']
    }, {
        name: 'ui.knob',
        files: ['/Content/StoremeyTheme/ANGULARJS/bower_components/ng-knob/dist/ng-knob.min.js']
    }, {
        name: 'ngAppear',
        files: ['/Content/StoremeyTheme/ANGULARJS/bower_components/angular-appear/build/angular-appear.min.js']
    }, {
        name: 'countTo',
        files: ['/Content/StoremeyTheme/ANGULARJS/bower_components/angular-filter-count-to/dist/angular-filter-count-to.min.js']
    }, {
        name: 'angularSpectrumColorpicker',
        files: ['/Content/StoremeyTheme/ANGULARJS/bower_components/angular-spectrum-colorpicker/dist/angular-spectrum-colorpicker.min.js']
    }]
});