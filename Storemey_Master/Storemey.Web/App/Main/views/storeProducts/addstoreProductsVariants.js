(function() {
    angular.module('app').controller('app.views.storeProductsVariants', [
        '$scope', '$state', '$stateParams', 'abp.services.app.productService', '$ngConfirm', '$timeout', 'cfpLoadingBar', 'abp.services.app.masterService', '$aside', 'SweetAlert', '$uibModalStack', '$http',
        function($scope, $state, $stateParams, _thisServices, $ngConfirm, $timeout, cfpLoadingBar, _masterService, $aside, SweetAlert, $uibModalStack, $http) {

            cfpLoadingBar.start();
            //#region Main product iamge upload and delete


            $scope.variantimages = [];
            $scope.Mastercate = [];
            $scope.Masterbrand = [];
            $scope.Mastertag = [];
            $scope.MasterSeasons = [];
            $('#fileInput').on('change', function() {
                cfpLoadingBar.start();
                debugger;
                if ($scope.obj.listProductImages !== undefined && $scope.obj.listProductImages !== null && (($scope.obj.listProductImages.length) + (this.files.length)) > 10) {
                    abp.notify.error("Maximum 10 image files are valid.");
                    return;
                }
                $scope.varnumberLoaded = false;
                //if (this.files && this.files[0]) {
                //    var FR = new FileReader();
                //    FR.addEventListener("load", function(e) {

                //        //$scope.image = {
                //        //    image: e.target.result, isDefault: false,
                //        //};


                //        //$scope.variantimages.push($scope.image);
                //        //$scope.openCropProductvar($scope.variantimages[$scope.variantimages.length - 1]);

                //        //if ($scope.variantimages.length === 1) {
                //        //    $scope.change($scope.image);
                //        //}

                //        $scope.image2 = e.target.result;

                //    });
                //    FR.readAsDataURL(this.files[0]);
                //}


                ///////new
                var fd = new FormData();
                //Take the first selected file
                fd.append("file", this.files[0]);

                $http.post('/Image/uploadimage', fd, {
                    withCredentials: true,
                    headers: { 'Content-Type': undefined },
                    transformRequest: angular.identity
                }).then(function(data) {
                    //$rootScope.$broadcast('updateList', { data: 'passing' });
                    //abp.notify.success("Import done! Successfilly.");
                    if (data !== null && data !== undefined && data.data !== null) {
                        var Firastresult = data.data;
                        //storeCountryMasterervices.importFromCSV(Firastresult).then(function (result) {
                        //    $rootScope.$broadcast('updateList', { data: 'passing' });
                        //    abp.notify.success("Import done! Successfilly.");
                        //});

                        //alert('hi');
                        //var lenth = $scope.image2.length;
                        //var lenthvalue = lenth / 4;
                        //alert($scope.image2.length);

                        //var res = scope.data.result.substring(1, lenthvalue);

                        $scope.image = {
                            image: '/TempImage/' + Firastresult, isDefault: false,
                        };
                        //$scope.image2 = '';

                        $scope.variantimages.push($scope.image);
                        $scope.openCropProductvar($scope.variantimages[$scope.variantimages.length - 1]);

                    }
                });


                $timeout(function() {
                    $scope.varnumberLoaded = true;
                }, 1000);


            });



            $scope.change = function(img) {
                img.isDefault = true;
                $($scope.obj.listProductImages).each(function(index, value) {
                    value.isDefault = false;
                    if (value.image === img.image) {
                        value.isDefault = true;
                    }
                });
                $($scope.variantimages).each(function(index, value) {
                    value.isDefault = false;
                    if (value.image === img.image) {
                        value.isDefault = true;
                    }
                });

                $scope.varnumberLoaded = false;

                $timeout(function() {
                    $scope.varnumberLoaded = true;
                }, 1000);
            };


            $scope.remove = function(item) {
                cfpLoadingBar.start();
                $scope.varnumberLoaded = false;
                var index = $scope.variantimages.indexOf(item);
                $scope.variantimages.splice(index, 1);

                $scope.objimag = {};

                $($scope.obj.listProductImages).each(function(index, value) {

                    if (value.image === item.image) {
                        $scope.obj.listProductImages.splice(index, 1);
                    }

                });

                if ($scope.variantimages.length > 0) {
                    $scope.variantimages[0].isDefault = true;
                    $scope.obj.listProductImages[0].isDefault = true;
                }

                $timeout(function() {
                    cfpLoadingBar.complete();
                    $scope.varnumberLoaded = true;
                }, 1000);
            };

            $scope.slickConfig = {
                enabled: true,
                autoplay: true,
                draggable: false,
                autoplaySpeed: 3000,
                method: {},
                event: {
                    beforeChange: function(event, slick, currentSlide, nextSlide) {
                    },
                    afterChange: function(event, slick, currentSlide, nextSlide) {
                    }
                }
            };

            $scope.openCropProductvar = function(img) {
                $scope.data = {};
                $scope.data.src = img.image;
                $scope.data.result = '';
                var found = false;
                $scope.listProductImagesobj = {};
                $($scope.obj.listProductImages).each(function(index, value) {

                    if (value.image === img.image) {
                        found = true;
                        $scope.listProductImagesobj = value;
                    }

                });

                $scope.cropmodalwindow = $ngConfirm({
                    boxWidth: '50%',
                    //type: 'red',
                    animation: 'zoom',
                    //closeAnimation: 'scale',

                    typeAnimated: false,
                    animationSpeed: 300,
                    closeIcon: false,
                    useBootstrap: false,
                    title: 'Crop image!',
                    contentUrl: '/App/Main/views/popups/cropimage/cropimage.cshtml',
                    scope: $scope,

                    onOpen: function() {
                        // after the modal is displayed.
                        //alert('onOpen');
                        cfpLoadingBar.complete();
                    },
                    buttons: {
                        Save: {
                            text: 'Save',
                            btnClass: 'btn-blue',
                            action: function(scope, button) {
                                img.image = scope.data.result;
                                //$scope.listProductImagesobj.image = scope.data.result;
                                //debugger;
                                //if (!found) {
                                //    $scope.badabaseimageobject = {
                                //        id: 0, image: scope.data.result, size1: 0, size2: 0, size3: 0, productId: null, isVariantProductImage: false, isDefault: img.isDefault,
                                //    };
                                //    $scope.obj.listProductImages.push($scope.badabaseimageobject);
                                //}
                                //$scope.cropmodalwindow.close();
                                //return false; // prevent close;




                                var $dataX = $('#dataX');
                                var $dataY = $('#dataY');
                                var $dataHeight = $('#dataHeight');
                                var $dataWidth = $('#dataWidth');
                                //alert($dataX.val());
                                //alert($dataY.val());
                                //alert($dataHeight.val());
                                //alert($dataWidth.val());

                                //var res3 = '';
                                //if ($scope.image2 != '') {

                                //    var lenth1 = $scope.image2.length;
                                //    var lenthvalue1 = lenth1 / 4;

                                //    res3 = $scope.image2.substring(0, lenthvalue1);
                                //    $scope.image2 = '';
                                //}



                                //var datapara = { image: res, image2: res1, x1: $dataX.val(), y1: $dataY.val(), height: $dataHeight.val(), width: $dataWidth.val() };
                                var datapara = { image: scope.data.result,x1: $dataX.val(), y1: $dataY.val(), height: $dataHeight.val(), width: $dataWidth.val() };




                                $http.post('/Image/cropimage', datapara, {
                                    //withCredentials: true,
                                    //headers: { 'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8' },
                                    //transformRequest: angular.identity
                                }).then(function(data) {
                                    //scope.data.result = data.data;
                                    //deferred.resolve(scope.data.result);

                                    img.image = data.data;
                                    $scope.listProductImagesobj.image = data.data;
                                    //if (!found) {
                                    //    $scope.badabaseimageobject = {
                                    //        id: 0, image: data.data, size1: 0, size2: 0, size3: 0, productId: null, isVariantProductImage: false, isDefault: img.isDefault,
                                    //    };
                                    //    $scope.GetStoreProductsDto.listProductImages.push($scope.badabaseimageobject);
                                    //}


                                    if (!found) {
                                        $scope.badabaseimageobject = {
                                            id: 0, image: data.data, size1: 0, size2: 0, size3: 0, productId: null, isVariantProductImage: false, isDefault: img.isDefault,
                                        };
                                        $scope.obj.listProductImages.push($scope.badabaseimageobject);
                                    }
                                    $scope.cropmodalwindow.close();

                                });

                            }
                        },
                        //close: function (scope, button) {

                        //},
                    }
                });
            };


            //#endregion Main product iamge upload and delete


            //#region CKEditor
            $scope.options = {
                language: 'en',
                allowedContent: true,
                entities: false
            };


            $scope.onReady = function() {
                // ...
            };
            //#endregion CKEditor

            $scope.validationOptions = {
                rules: {
                    ProductName: {
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

            var obj = {
                Id: 0
            };



            $scope.getMastersdata = function() {


                _thisServices.getMastersdata()
                    .then(function(result) {
                        $scope.Masters = result.data;
                        console.log(result.data);


                        $scope.Supplier = result.data.listSuppliers.items;
                        $scope.Tax = result.data.listTax.items;

                    });


                cfpLoadingBar.complete();

                $scope.obj.listProductPricing.filter(function(element) {
                    $($scope.Tax).each(function(index1, value) {
                        if (value.id === element.taxId) {
                            element.uitaxId = value;

                        }
                    });
                });

                if ($scope.obj.isVariant == null || $scope.obj.isVariant == undefined) {
                    $scope.obj.isVariant = false;
                }


                $($scope.obj.listProductImages).each(function(index, value) {
                    if (value.image != null && value.image != '') {
                        $scope.image = {
                            image: value.image,
                            isDefault: value.isDefault,
                        };
                        $scope.variantimages.push($scope.image);
                    }
                });

                $timeout(function() {
                    if ($scope.variantimages != undefined && $scope.variantimages != null && $scope.variantimages.length > 0) {
                        $scope.varnumberLoaded = true;
                    }
                    cfpLoadingBar.complete();
                }, 1000);

            };

            if ($scope.Id !== 0) {
                $scope.getMastersdata();
            }
            else {
                $scope.getMastersdata();
            }

            $scope.addNewCategories = function() {
                $aside.open({
                    templateUrl: '/App/Main/views/storeCategories/modal-addeditstoreCategories.cshtml',
                    //controller: 'app.views.modal-storeCategories',
                    placement: 'right',
                    size: '45perWidth',
                    scope: $scope,

                });
            };



            $scope.addNewBrands = function() {
                $aside.open({
                    templateUrl: '/App/Main/views/storeBrands/modal-addeditstoreBrands.cshtml',
                    //controller: 'app.views.modal-storeBrands',
                    placement: 'right',
                    size: '45perWidth',
                    scope: $scope,

                });
            };



            $scope.addNewTags = function() {
                $aside.open({
                    templateUrl: '/App/Main/views/storeTags/modal-addeditstoreTags.cshtml',
                    //controller: 'app.views.modal-storeTags',
                    placement: 'right',
                    size: '45perWidth',
                    scope: $scope,

                });
            };


            $scope.addNewSeason = function() {
                $aside.open({
                    templateUrl: '/App/Main/views/storeSeasons/modal-addeditstoreSeasons.cshtml',
                    //controller: 'app.views.modal-storeSeasons',
                    placement: 'right',
                    size: '45perWidth',
                    scope: $scope,

                });
            };

            $scope.AddVariants = function() {

                if ($scope.obj.productName != null && $scope.obj.productName != '') {
                    $aside.open({
                        templateUrl: '/App/Main/views/storeProducts/variantslider.cshtml',
                        //controller: 'app.views.variantslider',
                        placement: 'right',
                        size: '45perWidth',
                        scope: $scope,

                    });
                }
                else {
                    SweetAlert.swal({
                        title: "Product name is required!",
                        text: "",
                        confirmButtonColor: "#c82e29"
                    });
                }
            };



            $scope.uiselectblurorenter = function(value) {

                if (value === 'addNewCategories')
                    $scope.addNewCategories();
                else if (value === 'addNewBrands')
                    $scope.addNewBrands();
                else if (value === 'addNewTags')
                    $scope.addNewTags();
                else if (value === 'addNewSeason')
                    $scope.addNewSeason();

            };





            $scope.deleteVariant = function(value) {
                var index = $scope.obj.listProductVariants.indexOf(value);
                $scope.obj.listProductVariants.splice(index, 1);

                abp.notify.error("Delete Successfully.");

            };


            $scope.deleteVariant = function(value) {

                $aside.open({
                    templateUrl: '/App/Main/views/storeProducts/variantslider.cshtml',
                    //controller: 'app.views.variantslider',
                    placement: 'right',
                    size: '45perWidth',
                    scope: $scope,

                });

            };












            $scope.saveAddEdit = function() {
                cfpLoadingBar.start();



                //$scope.obj.supplierId = $scope.obj.uisuppId.id;

                try {

                    if ($scope.GetStoreProductsDto.isVariant === true) {
                        var result2 = $scope.obj.listProductPricing.filter(function(element) {
                            element.taxId = element.uitaxId.id;
                        });
                    }

                } catch (e) {
                    console.log(e);

                }

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }

                $scope.obj.listCategory = [];
                $scope.obj.listBrands = [];
                $scope.obj.listTags = [];
                $scope.obj.listSeason = [];
                debugger;
                $($scope.Mastercate.ui).each(function(index, value) {
                    $scope.obj.listCategory.push(value);
                });

                $($scope.Masterbrand.ui).each(function(index, value) {
                    $scope.obj.listBrands.push(value);
                });

                $($scope.Mastertag.ui).each(function(index, value) {
                    $scope.obj.listTags.push(value);
                });

                $($scope.MasterSeasons.ui).each(function(index, value) {
                    $scope.obj.listSeason.push(value);
                });


                //abp.notify.success("Saved Successfully.");
                $uibModalStack.dismissAll('closing');

                //_thisServices.saveOrUpdateProduct($scope.obj)
                //    .then(function () {

                //        abp.notify.success("Saved Successfully.");
                //        $state.go('storeProducts', { SearchDto: $stateParams.SearchDto });

                //    });


                //if ($scope.copyprice != undefined) {
                //    $scope.CopyPrices($scope.copyprice.price);
                //    $scope.CopymarkUp($scope.copyprice.markUp);
                //    $scope.CopypriceExcludingTax($scope.copyprice.priceExcludingTax);
                //    $scope.CopyuitaxId($scope.copyprice.uitaxId);
                //    $scope.CopypriceIncludingTax($scope.copyprice.priceIncludingTax);
                //}

            };

            $scope.cancelAddEdit = function() {
                $uibModalStack.dismissAll('closing');
            };


        }
    ]);
})();