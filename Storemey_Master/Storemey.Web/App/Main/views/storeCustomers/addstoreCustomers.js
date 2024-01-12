(function () {
    angular.module('app').controller('app.views.storeCustomers', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeCustomers',
        function ($scope, $state, $stateParams, _thisServices) {

            $.validator.addMethod("regx", function (value, element, regexpr) {
                return regexpr.test(value);
            }, "Please enter a valid pasword.");
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

            $scope.validationOptions = {
                rules: {
                    firstName: {
                        required: true,
                    },
                    lastName: {
                        required: true,
                    },
                    gender: {
                        required: true,
                    },
                    dateofbirth: {
                        required: true,
                    },
                    companyName: {
                        required: true,
                    },
                    bill_Email: {
                        required: true,
                    },
                    bill_Phone: {
                        required: true,
                    },
                    bill_address: {
                        required: true,
                    }
                }
            };

            var stateobj = $stateParams.SearchDto;
            if ($stateParams.SearchDto !== undefined && $stateParams.SearchDto !== null && $stateParams.SearchDto !== '') {
                $scope.Id = stateobj.tempID;
            }
            else {
                $stateParams.SearchDto = null;
            }

            var GetstoreCustomersInputDto = {
                Id: 0
            };

            $scope.GetstoreCustomersOutputDto = {};

            function getstoreCustomers(Id) {
                GetstoreCustomersInputDto.Id = Id;
                _thisServices.getById(GetstoreCustomersInputDto)
                    .then(function (result) {
                        $scope.GetstoreCustomersOutputDto = result.data;
                        $scope.GetstoreCustomersOutputDto.dateOfBirth = new Date($scope.GetstoreCustomersOutputDto.dateOfBirth);
                    });
            }
            if ($scope.Id !== 0) {
                getstoreCustomers($scope.Id);
            }
            else {
                getstoreCustomers($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                debugger;
                if ($scope.GetstoreCustomersOutputDto.id === undefined || $scope.GetstoreCustomersOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreCustomersOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeCustomers', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreCustomersOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeCustomers', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeCustomers');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('storeCustomers', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();