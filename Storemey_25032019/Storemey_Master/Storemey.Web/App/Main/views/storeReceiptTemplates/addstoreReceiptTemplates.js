(function () {
    angular.module('app').controller('app.views.storeReceiptTemplates', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeReceiptTemplates',
        function ($scope, $state, $stateParams, _thisServices) {

            $.validator.addMethod("regx", function (value, element, regexpr) {
                return regexpr.test(value);
            }, "Please enter a valid pasword.");


            $scope.validationOptions = {
                rules: {
                    name: {
                        required: true,
                    },
                    logo: {
                        required: true,
                    },
                    headerText: {
                        required: true,
                    },
                    invoiceNoPrefix: {
                        required: true,
                    },
                    inoviceHeading: {
                        required: true,
                    },
                    servedByLabel: {
                        required: true,
                    },
                    discountLabel: {
                        required: true,
                    },
                    subtotallabel: {
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

            var GetstoreReceiptTemplatesInputDto = {
                Id: 0
            };

            $scope.GetstoreReceiptTemplatesOutputDto = {};

            function getstoreReceiptTemplates(Id) {
                GetstoreReceiptTemplatesInputDto.Id = Id;
                _thisServices.getById(GetstoreReceiptTemplatesInputDto)
                    .then(function (result) {
                        $scope.GetstoreReceiptTemplatesOutputDto = result.data;

                    });
            }
            if ($scope.Id !== 0) {
                getstoreReceiptTemplates($scope.Id);
            }
            else {
                getstoreReceiptTemplates($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                debugger;
                if ($scope.GetstoreReceiptTemplatesOutputDto.id === undefined || $scope.GetstoreReceiptTemplatesOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreReceiptTemplatesOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeReceiptTemplates', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreReceiptTemplatesOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeReceiptTemplates', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeReceiptTemplates');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('storeReceiptTemplates', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();