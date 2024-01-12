(function () {


    angular.module('app')
        .directive('tagOnBlur', function ($timeout) {
            return {
                require: 'uiSelect',
                link: function (scope, elm, attrs, ctrl) {

                    scope.isTab = false;
                    ctrl.searchInput.bind("keydown keypress", function (event) {
                        if ((event.which === 9 || event.which === 13) && ctrl.searchInput.val() != '') {
                            event.preventDefault();
                            scope.isTab = true;
                            scope.uiselectblurorenter(attrs.functionValue);
                        }
                    });

                    ctrl.searchInput.bind('click', function (event) {
                        scope.isTab = true;
                    });

                    ctrl.searchInput.on('blur', function () {

                        if (scope.isTab && ctrl.searchInput.val() != '') {
                            scope.uiselectblurorenter(attrs.functionValue);
                            scope.isTab = false;
                            return;
                        }
                        if ((ctrl.items.length > 0 || ctrl.tagging.isActivated)) {
                            $timeout(function () {
                                ctrl.searchInput.triggerHandler('tagged');
                                var newItem = ctrl.search;
                                if (ctrl.tagging.fct) {
                                    newItem = ctrl.tagging.fct(newItem);
                                }
                                if (newItem) ctrl.select(newItem, true);
                            });
                        }
                    });
                }
            };
        });


})();