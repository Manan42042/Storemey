(function () {
    angular.module('app').controller('app.views.storeGiftCards', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeGiftCards', 'abp.services.app.storeCustomers',
        function ($scope, $state, $stateParams, _thisServices,_customerService) {


            function getMasters() {
                if ($scope.Customers === undefined) {

                    _customerService.listAll()
                        .then(function (result) {
                            $scope.Customers = result.data.items;

                            if ($scope.Id !== 0) {
                                getstoreGiftCards($scope.Id);
                            }
                            else {
                                getstoreGiftCards($scope.Id);
                            }

                        });
                }
            }
            getMasters();



            $.validator.addMethod("regx", function (value, element, regexpr) {
                return regexpr.test(value);
            }, "Please enter a valid pasword.");

            $scope.validationOptions = {
                rules: {
                    giftCardName: {
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

            var GetstoreGiftCardsInputDto = {
                Id: 0
            };

            $scope.GetstoreGiftCardsOutputDto = {};

            function getstoreGiftCards(Id) {
                GetstoreGiftCardsInputDto.Id = Id;
                _thisServices.getById(GetstoreGiftCardsInputDto)
                    .then(function (result) {
                        $scope.GetstoreGiftCardsOutputDto = result.data;


                        var result2 = $scope.Customers.filter(function (element) {
                            if (parseInt(element.id) == parseInt($scope.GetstoreGiftCardsOutputDto.customerId)) {
                                return true;
                            } else {
                                return false;
                            }
                        });
                        if (result2[0] !== undefined) {
                            console.log(result2[0]);
                            $scope.GetstoreGiftCardsOutputDto.uiCustomerId = result2[0];
                        }

                    });
            }
            //if ($scope.Id !== 0) {
            //    getstoreGiftCards($scope.Id);
            //}
            //else {
            //    getstoreGiftCards($scope.Id);
            //}



            $scope.saveAddEdit = function () {


                $scope.GetstoreGiftCardsOutputDto.customerId = $scope.GetstoreGiftCardsOutputDto.uiCustomerId.id;

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                debugger;
                if ($scope.GetstoreGiftCardsOutputDto.id === undefined || $scope.GetstoreGiftCardsOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreGiftCardsOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeGiftCards', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreGiftCardsOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeGiftCards', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeGiftCards');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);

                $state.go('storeGiftCards', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();