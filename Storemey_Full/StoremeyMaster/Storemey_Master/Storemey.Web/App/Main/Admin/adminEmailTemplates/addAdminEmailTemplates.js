(function () {
    angular.module('app').controller('app.views.adminEmailTemplates', [
        '$scope', '$state', '$stateParams', 'abp.services.app.adminEmailTemplates', '$log',
        function ($scope, $state, $stateParams, _thisServices, $log) {


            //.controller('DatepickerDemoCtrl', ["$scope", "$log",
            //    function ($scope, $log) {
                    $scope.today = function () {
                        $scope.dt = new Date();
                    };
                    $scope.today();
                    $scope.start = $scope.minDate;
                    $scope.end = $scope.maxDate;

                    $scope.clear = function () {
                        $scope.dt = null;
                    };
                    $scope.datepickerOptions = {
                        showWeeks: false,
                        startingDay: 1
                    };
                    $scope.dateDisabledOptions = {
                        dateDisabled: disabled,
                        showWeeks: false,
                        startingDay: 1
                    };
                    $scope.startOptions = {
                        showWeeks: false,
                        startingDay: 1,
                        minDate: $scope.minDate,
                        maxDate: $scope.maxDate
                    };
                    $scope.endOptions = {
                        showWeeks: false,
                        startingDay: 1,
                        minDate: $scope.minDate,
                        maxDate: $scope.maxDate
                    };
                    // Disable weekend selection
                    function disabled(data) {
                        var date = data.date, mode = data.mode;
                        return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
                    }


                    $scope.setDate = function (year, month, day) {
                        $scope.dt = new Date(year, month, day);
                    };
                    $scope.toggleMin = function () {
                        $scope.datepickerOptions.minDate = $scope.datepickerOptions.minDate ? null : new Date();
                        $scope.dateDisabledOptions.minDate = $scope.dateDisabledOptions.minDate ? null : new Date();
                    };
                    $scope.maxDate = new Date(2020, 5, 22);
                    $scope.minDate = new Date(1970, 12, 31);

                    $scope.open = function () {
                        $scope.opened = !$scope.opened;
                    };


                    $scope.endOpen = function () {
                        $scope.endOptions.minDate = $scope.start;
                        $scope.startOpened = false;
                        $scope.endOpened = !$scope.endOpened;
                    };
                    $scope.startOpen = function () {
                        $scope.startOptions.maxDate = $scope.end;
                        $scope.endOpened = false;
                        $scope.startOpened = !$scope.startOpened;
                    };

                    $scope.dateOptions = {
                        formatYear: 'yy',
                        startingDay: 1
                    };

                    $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
                    $scope.format = $scope.formats[0];

                    $scope.hstep = 1;
                    $scope.mstep = 15;

                    // Time Picker
                    $scope.options = {
                        hstep: [1, 2, 3],
                        mstep: [1, 5, 10, 15, 25, 30]
                    };

                    $scope.ismeridian = true;
                    $scope.toggleMode = function () {
                        $scope.ismeridian = !$scope.ismeridian;
                    };

                    $scope.update = function () {
                        var d = new Date();
                        d.setHours(14);
                        d.setMinutes(0);
                        $scope.dt = d;
                    };

                    $scope.changed = function () {
                        $log.log('Time changed to: ' + $scope.dt);
                    };

                    $scope.clear = function () {
                        $scope.dt = null;
                    };

                //}])



            $.validator.addMethod("regx", function (value, element, regexpr) {
                return regexpr.test(value);
            }, "Please enter a valid pasword.");

            $scope.validationOptions = {
                rules: {
                    tagName: {
                        required: true,
                    },
                    description: {
                        required: true,
                    }
                }
            };

            // Editor options.
            $scope.options = {
                language: 'en',
                allowedContent: true,
                entities: false
            };

            // Called when the editor is completely ready.
            $scope.onReady = function () {
                // ...
            };

               // Editor options.
            $scope.options = {
                language: 'en',
                allowedContent: true,
                entities: false
            };

            // Called when the editor is completely ready.
            $scope.onReady = function () {
                // ...
            };


            var stateobj = $stateParams.SearchDto;
            if ($stateParams.SearchDto !== undefined && $stateParams.SearchDto !== null && $stateParams.SearchDto !== '') {
                $scope.Id = stateobj.tempID;
            }
            else {
                $stateParams.SearchDto = null;
            }

            var GetadminEmailTemplatesInputDto = {
                Id: 0
            };

            $scope.GetadminEmailTemplatesOutputDto = {};

            function getadminEmailTemplates(Id) {
                GetadminEmailTemplatesInputDto.Id = Id;
                _thisServices.getById(GetadminEmailTemplatesInputDto)
                    .then(function (result) {
                        $scope.GetadminEmailTemplatesOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getadminEmailTemplates($scope.Id);
            }
            else {
                getadminEmailTemplates($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                if ($scope.GetadminEmailTemplatesOutputDto.id === undefined || $scope.GetadminEmailTemplatesOutputDto.id === 0) {
                    _thisServices.create($scope.GetadminEmailTemplatesOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('adminEmailTemplates', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetadminEmailTemplatesOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('adminEmailTemplates', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('adminEmailTemplates');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('adminEmailTemplates', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();