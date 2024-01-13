(function () {
    angular.module('app').controller('app.views.variantslider', [
        '$http', '$scope', '$uibModalStack', 'abp.services.app.storeProducts',
        function ($http, $scope, $uibModalStack, storeProductservices) {




            $scope.ChangeVariantValues = function ($item, $model, m) {
                if ($item.id !== 0) {
                    angular.forEach($scope.allVariantValue, function (value, key) {
                        if (value.variantId !== $item.id) {
                            delete $scope.allVariantValue[key];
                        }
                    });
                    m.allVariantValue = $scope.allVariantValue;
                    m.variantValues = [];
                    $scope.fillAlldata();
                }
                $scope.ChangeVariant($item, $model, m);
            };

            $scope.ChangeVariant = function ($item, $model, m) {
                var curselvarid = m.variantNames.id;
                var myvariants = $scope.allVariant;

                angular.forEach($scope.GetStoreProductsDto.selectedVariant, function (m2, key) {
                    angular.forEach(myvariants, function (value, key2) {
                        if (m2.variantNames !== undefined && m2.variantNames.id !== undefined && m2.variantNames.id === value.id) {
                            delete myvariants[key2];
                        }
                    });
                    m2.allVariant = myvariants;
                });

            };

            $scope.fillAlldata = function () {
                $scope.allVariant = [];
                $scope.allVariantValue = [];
                $scope.allVariant = angular.copy($scope.Masters.listProductVariants.items);
                $scope.allVariantValue = angular.copy($scope.Masters.listProductVariantValues.items);
            };

            $scope.init = function () {
                $scope.fillAlldata();

                $scope.GetStoreProductsDto.selectedVariant.filter(function (m) {
                    if (m.variantId !== 0 && m.variantId != null) {
                        $scope.allVariantValue.filter(function (m2) {
                            if (m2.variantId !== m.variantValueId) {
                                var index = $scope.Masters.listProductVariantValues.items.indexOf(m2);
                                $scope.allVariantValue.splice(index, 1);
                            }
                        });
                        m.allVariantValue = $scope.allVariantValue;
                        $scope.fillAlldata();
                    }
                    else {
                        m.allVariant = $scope.allVariant;
                        //m.allVariantValue = $scope.allVariantValue;
                    }
                });
                $scope.fillAlldata();
            };

            $scope.init();

            $scope.cancelAddEditslider = function () {
                window.scrollTo(0, 0);
                $uibModalStack.dismissAll('closing');
            };

            $scope.saveAddEdit = function () {

                debugger;
                $scope.saveVariant = [];
                $scope.varid = [];

                if ($scope.GetStoreProductsDto.selectedVariant[0] !== undefined && $scope.GetStoreProductsDto.selectedVariant[0].variantValues !== undefined && $scope.GetStoreProductsDto.selectedVariant[0].variantNames !== undefined) {
                    $scope.saveVariant.push($scope.GetStoreProductsDto.selectedVariant[0].variantValues);
                    $scope.varid.push($scope.GetStoreProductsDto.selectedVariant[0].variantNames.id);

                    if ($scope.GetStoreProductsDto.selectedVariant[1] !== undefined && $scope.GetStoreProductsDto.selectedVariant[1].variantValues !== undefined && $scope.GetStoreProductsDto.selectedVariant[1].variantNames !== undefined) {
                        $scope.saveVariant.push($scope.GetStoreProductsDto.selectedVariant[1].variantValues);
                        $scope.varid.push($scope.GetStoreProductsDto.selectedVariant[1].variantNames.id);

                        if ($scope.GetStoreProductsDto.selectedVariant[2] !== undefined && $scope.GetStoreProductsDto.selectedVariant[2].variantValues !== undefined && $scope.GetStoreProductsDto.selectedVariant[2].variantNames !== undefined) {
                            $scope.saveVariant.push($scope.GetStoreProductsDto.selectedVariant[2].variantValues);
                            $scope.varid.push($scope.GetStoreProductsDto.selectedVariant[2].variantNames.id);
                        }
                        else {
                            $scope.saveVariant.push([]);
                            $scope.varid.push(0);

                        }
                    }
                    else {
                        $scope.saveVariant.push([]);
                        $scope.varid.push(0);
                    }

                }
                else {
                    $scope.saveVariant.push([]);
                    $scope.varid.push(0);
                }
                debugger;

                //if ($scope.GetStoreProductsDto.listProductVariants.length === 1) {
                //    $scope.GetStoreProductsDto.defaultVariant = angular.copy($scope.GetStoreProductsDto.listProductVariants[0]);
                //}

                var Obj = angular.copy($scope.GetStoreProductsDto.defaultProductVariants);

                
                $scope.GetStoreProductsDto.listProductVariants = [];

                angular.forEach($scope.saveVariant[0], function (m1, key) {

                    if ($scope.saveVariant[1].length === 0) {
                        var VariantJsonObj = angular.copy(Obj);
                        VariantJsonObj.productName = $scope.GetStoreProductsDto.productName + '/' + m1.variantValue;
                        VariantJsonObj.sku = $scope.GetStoreProductsDto.sku;
                        VariantJsonObj.barcode = $scope.GetStoreProductsDto.barcode;
                        VariantJsonObj.variantId1 = $scope.varid[0];
                        VariantJsonObj.variantValueId1 = m1.id;
                        $scope.GetStoreProductsDto.listProductVariants.push(VariantJsonObj);
                    }

                    angular.forEach($scope.saveVariant[1], function (m2, key) {

                        if ($scope.saveVariant[2].length === 0) {
                            var VariantJsonObj = angular.copy(Obj);
                            VariantJsonObj.productName = $scope.GetStoreProductsDto.productName + '/' + m1.variantValue + '/' + m2.variantValue;
                            VariantJsonObj.sku = $scope.GetStoreProductsDto.sku;
                            VariantJsonObj.barcode = $scope.GetStoreProductsDto.barcode;
                            VariantJsonObj.variantId1 = $scope.varid[0];
                            VariantJsonObj.variantValueId1 = m1.id;

                            VariantJsonObj.variantId2 = $scope.varid[1];
                            VariantJsonObj.variantValueId2 = m2.id;

                            $scope.GetStoreProductsDto.listProductVariants.push(VariantJsonObj);
                        }

                        angular.forEach($scope.saveVariant[2], function (m3, key) {
                            var VariantJsonObj = angular.copy(Obj);
                            VariantJsonObj.productName = $scope.GetStoreProductsDto.productName + '/' + m1.variantValue + '/' + m2.variantValue + '/' + m3.variantValue;
                            VariantJsonObj.sku = $scope.GetStoreProductsDto.sku;
                            VariantJsonObj.barcode = $scope.GetStoreProductsDto.barcode;
                            VariantJsonObj.variantId1 = $scope.varid[0];
                            VariantJsonObj.variantValueId1 = m1.id;

                            VariantJsonObj.variantId2 = $scope.varid[1];
                            VariantJsonObj.variantValueId2 = m1.id;

                            VariantJsonObj.variantId3 = $scope.varid[2];
                            VariantJsonObj.variantValueId3 = m1.id;

                            $scope.GetStoreProductsDto.listProductVariants.push(VariantJsonObj);

                        });
                    });
                });


                $scope.CopyPrices($scope.copyprice.price);
                $scope.CopymarkUp($scope.copyprice.markUp);
                $scope.CopypriceExcludingTax($scope.copyprice.priceExcludingTax);
                $scope.CopyuitaxId($scope.copyprice.uitaxId);
                $scope.CopypriceIncludingTax($scope.copyprice.priceIncludingTax);



                $uibModalStack.dismissAll('closing');
                console.log($scope.GetStoreProductsDto.listProductVariants);
            };



        }
    ]);



})();