(function () {
    angular.module('app').controller('app.views.storeRegisters', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeRegisters', 'abp.services.app.masterService',
        function ($scope, $state, $stateParams, _thisServices, _masterService) {




            function getMasters() {
                if ($scope.Countries === undefined) {

                    _masterService.listAlloutlets()
                        .then(function (result) {
                            $scope.Outlets = result.data.items;


                            _masterService.listAllReceiptTemplates()
                                .then(function (result3) {
                                    $scope.ReceiptTemplates = result3.data.items;

                                    if ($scope.Id !== 0) {
                                        getstoreRegisters($scope.Id);
                                    }
                                    else {
                                        getstoreRegisters($scope.Id);
                                    }

                                });

                        });
                }
            }
            getMasters();



            var stateobj = $stateParams.SearchDto;
            if ($stateParams.SearchDto !== undefined && $stateParams.SearchDto !== null && $stateParams.SearchDto !== '') {
                $scope.Id = stateobj.tempID;
            }
            else {
                $stateParams.SearchDto = null;
            }

            var GetstoreRegistersInputDto = {
                Id: 0
            };

            $scope.GetstoreRegistersOutputDto = {};

            function getstoreRegisters(Id) {
                GetstoreRegistersInputDto.Id = Id;
                _thisServices.getById(GetstoreRegistersInputDto)
                    .then(function (result) {
                        $scope.GetstoreRegistersOutputDto = result.data;


                        var result2 = $scope.Outlets.filter(function (element) {
                            if (parseInt(element.id) == parseInt($scope.GetstoreRegistersOutputDto.outletId)) {
                                return true;
                            } else {
                                return false;
                            }
                        });
                        if (result2[0] !== undefined) {
                            console.log(result2[0]);
                            $scope.GetstoreRegistersOutputDto.outletId = result2[0];
                        }

                        var result3 = $scope.ReceiptTemplates.filter(function (element) {
                            if (parseInt(element.id) == parseInt($scope.GetstoreRegistersOutputDto.receiptTemplateId)) {
                                return true;
                            } else {
                                return false;
                            }
                        });
                        if (result3[0] !== undefined) {
                            console.log(result3[0]);
                            $scope.GetstoreRegistersOutputDto.receiptTemplateId = result3[0];
                        }


                    });
            }
            //if ($scope.Id !== 0) {
            //    getstoreRegisters($scope.Id);
            //}
            //else {
            //    getstoreRegisters($scope.Id);
            //}



            $scope.saveAddEdit = function () {


                $scope.GetstoreRegistersOutputDto.outletId = $scope.GetstoreRegistersOutputDto.outletId.id;
                $scope.GetstoreRegistersOutputDto.receiptTemplateId = $scope.GetstoreRegistersOutputDto.receiptTemplateId.id;


                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }

                if ($scope.GetstoreRegistersOutputDto.id === undefined || $scope.GetstoreRegistersOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreRegistersOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeRegisters', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreRegistersOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeRegisters', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeRegisters');
            //};

            $scope.cancelAddEdit = function () {
                window.scrollTo(0, 0);
                $state.go('storeRegisters', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();