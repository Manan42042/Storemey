(function () {



    angular.module('app').factory('myService', function (input, http) {
        return {
            // 1st function
            serverCall: function () {
                return $http.post('/ServerRequest/GetData', input).then(function (response) {
                    alert(response.data);
                    return response.data;
                });
            },
            // 2nd function
            anotherFunctionCall: function () {
                alert("Hi");
            }
        };
    });

    angular.module('app').controller('app.views.storeProducts', [
        '$http', '$scope', '$state', '$stateParams', 'abp.services.app.productService', '$ngConfirm', '$timeout', 'cfpLoadingBar', 'abp.services.app.masterService', '$aside', 'SweetAlert', '$uibModal', '$q',
        function ($http, $scope, $state, $stateParams, _thisServices, $ngConfirm, $timeout, cfpLoadingBar, _masterService, $aside, SweetAlert, $uibModal, $q) {

            cfpLoadingBar.start();
            //#region Main product iamge upload and delete

            //$scope.image2 = '';
            $scope.images = [];
            $scope.Mastercate = [];
            $scope.Masterbrand = [];
            $scope.Mastertag = [];
            $scope.MasterSeasons = [];
            $('#fileInput').on('change', function () {
                cfpLoadingBar.start();
                if ($scope.GetStoreProductsDto.listProductImages != undefined && $scope.GetStoreProductsDto.listProductImages != null && (($scope.GetStoreProductsDto.listProductImages.length) + (this.files.length)) > 10) {
                    abp.notify.error("Maximum 10 image files are valid.");
                    return;
                }

                console.log(URL.createObjectURL(this.files[0]));
                $scope.mainsrc = URL.createObjectURL(this.files[0]);
                $scope.numberLoaded = false;
                //if (this.files && this.files[0]) {
                //    var FR = new FileReader();
                //    FR.addEventListener("load", function (e) {

                //        //$scope.image = {
                //        //    image: e.target.result, isDefault: false,
                //        //};
                //        $scope.image2 = e.target.result;


                //        //$scope.images.push($scope.image);
                //        //$scope.openCropProduct($scope.images[$scope.images.length - 1]);

                //        //if ($scope.images.length === 1) {
                //        //    $scope.change($scope.image);
                //        //}

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
                }).then(function (data) {
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

                        $scope.images.push($scope.image);
                        $scope.openCropProduct($scope.images[$scope.images.length - 1]);

                    }
                });

                $timeout(function () {
                    $scope.numberLoaded = true;
                }, 1000);


            });



            $scope.change = function (img) {
                img.isDefault = true;
                $($scope.GetStoreProductsDto.listProductImages).each(function (index, value) {
                    value.isDefault = false;
                    if (value.image === img.image) {
                        value.isDefault = true;
                    }
                });
                $($scope.images).each(function (index, value) {
                    value.isDefault = false;
                    if (value.image === img.image) {
                        value.isDefault = true;
                    }
                });

                $scope.numberLoaded = false;

                $timeout(function () {
                    $scope.numberLoaded = true;
                }, 1000);
            };


            $scope.remove = function (item) {
                cfpLoadingBar.start();
                $scope.numberLoaded = false;
                var index = $scope.images.indexOf(item);
                $scope.images.splice(index, 1);

                $scope.objimag = {};

                $($scope.GetStoreProductsDto.listProductImages).each(function (index, value) {

                    if (value.image === item.image) {
                        $scope.GetStoreProductsDto.listProductImages.splice(index, 1);
                    }

                });

                if ($scope.images.length > 0) {
                    $scope.images[0].isDefault = true;
                    $scope.GetStoreProductsDto.listProductImages[0].isDefault = true;
                }

                $timeout(function () {
                    cfpLoadingBar.complete();
                    $scope.numberLoaded = true;
                }, 1000);
            };

            $scope.slickConfig = {
                enabled: true,
                autoplay: true,
                draggable: false,
                autoplaySpeed: 3000,
                method: {},
                event: {
                    beforeChange: function (event, slick, currentSlide, nextSlide) {
                    },
                    afterChange: function (event, slick, currentSlide, nextSlide) {
                    }
                }
            };

            $scope.openCropProduct = function (img) {
                $scope.data = {};
                $scope.data.src = img.image;
                $scope.data.result = '';
                var found = false;
                $scope.listProductImagesobj = {};
                $($scope.GetStoreProductsDto.listProductImages).each(function (index, value) {

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

                    onOpen: function () {
                        // after the modal is displayed.
                        //alert('onOpen');
                        cfpLoadingBar.complete();
                    },
                    buttons: {
                        Save: {
                            text: 'Save',
                            btnClass: 'btn-blue',
                            action: function (scope, button) {


                                var $dataX = $('#dataX');
                                var $dataY = $('#dataY');
                                var $dataHeight = $('#dataHeight');
                                var $dataWidth = $('#dataWidth');
                           

                                //var res3 = '';
                                //if ($scope.image2 != '') {

                                //    var lenth1 = $scope.image2.length;
                                //    var lenthvalue1 = lenth1 / 4;

                                //    res3 = $scope.image2.substring(0, lenthvalue1);
                                //    $scope.image2 = '';
                                //}


                                var lenth = scope.data.result.length;
                                var lenthvalue = lenth / 2;

                                var res = scope.data.result.substring(1, lenthvalue);
                                var res1 = scope.data.result.substring(lenthvalue, lenth);
                    
                                //var datapara = { image: res, image2: res1, x1: $dataX.val(), y1: $dataY.val(), height: $dataHeight.val(), width: $dataWidth.val() };
                                var datapara = { image: scope.data.result, x1: $dataX.val(), y1: $dataY.val(), height: $dataHeight.val(), width: $dataWidth.val() };
                                $scope.paramters = {};
                                $scope.paramters.image = scope.data.result;
                                $scope.paramters.x1 = scope.data.result;
                                $scope.paramters.y1 = scope.data.result;
                                $scope.paramters.height = scope.data.result;
                                $scope.paramters.width = scope.data.result;



                                var deferred = $q.defer();
                                $http.post('/Image/cropimage', datapara, {
                                    //withCredentials: true,
                                    //headers: { 'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8' },
                                    //transformRequest: angular.identity
                                }).then(function (data) {
                                    //scope.data.result = data.data;
                                    //deferred.resolve(scope.data.result);

                                    img.image = data.data;
                                    $scope.listProductImagesobj.image = data.data;
                                   if (!found) {
                                        $scope.badabaseimageobject = {
                                            id: 0, image: data.data, size1: 0, size2: 0, size3: 0, productId: null, isVariantProductImage: false, isDefault: img.isDefault,
                                        };
                                        $scope.GetStoreProductsDto.listProductImages.push($scope.badabaseimageobject);
                                    }
                                    $scope.cropmodalwindow.close();
                                });




                                return false; // prevent close;
                            }
                        },
                        //close: function (scope, button) {

                        //},
                    }
                });


                //$scope.cropmodalwindow = $uibModal.open({
                //    templateUrl: '/App/Main/views/popups/cropimage/cropimage.cshtml',
                //    controller: 'app.views.cropimagenew as vm',
                //    backdrop: 'static',
                //    size: 'lg',
                //    scope: $scope,
                //    animation: true,
                //    keyboard: false,
                //    resolve: {
                //        //id: function () {
                //        //    return user.id;
                //        //}
                //    }
                //});
            };


            //#endregion Main product iamge upload and delete


            //#region CKEditor
            $scope.options = {
                language: 'en',
                allowedContent: true,
                entities: false
            };


            $scope.onReady = function () {
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

            var GetStoreProductsDto = {
                Id: 0
            };

            $scope.GetStoreProductsDto = { isVariant: false };
            function getstoreProducts(Id) {
                cfpLoadingBar.start();

                GetStoreProductsDto.Id = Id;
                _thisServices.getProductById(GetStoreProductsDto)
                    .then(function (result) {
                        $scope.GetStoreProductsDto = result.data;


                        console.log($scope.GetStoreProductsDto);
                        if ($scope.GetStoreProductsDto.id === 0) {
                            cfpLoadingBar.complete();
                        }

                        var result2 = $scope.Supplier.filter(function (element) {

                            if (parseInt(element.id) == parseInt($scope.GetStoreProductsDto.supplierId)) {
                                return true;
                            } else {
                                return false;
                            }
                        });
                        if (result2[0] !== undefined) {
                            $scope.GetStoreProductsDto.uisuppId = result2[0];
                        }


                        $scope.GetStoreProductsDto.listProductPricing.filter(function (element) {
                            $($scope.Tax).each(function (index1, value) {
                                if (value.id === element.taxId) {
                                    element.uitaxId = value;

                                }
                            });
                        });



                        if ($scope.GetStoreProductsDto.isVariant == null || $scope.GetStoreProductsDto.isVariant == undefined) {
                            $scope.GetStoreProductsDto.isVariant = false;
                        }
                        if ($scope.GetStoreProductsDto.listProductImages[0].image === null || $scope.GetStoreProductsDto.listProductImages[0].image === '') {
                            $scope.GetStoreProductsDto.listProductImages = [];
                        }

                        $($scope.GetStoreProductsDto.listProductImages).each(function (index, value) {
                            if (value.image != null && value.image != '') {

                                $scope.image = {
                                    image: value.image,
                                    isDefault: value.isDefault,
                                };
                                $scope.images.push($scope.image);
                            }
                        });
                        $scope.Mastercate.ui = [];
                        $scope.Masterbrand.ui = [];
                        $scope.Mastertag.ui = [];
                        $scope.MasterSeasons.ui = [];

                        $($scope.GetStoreProductsDto.listCategory).each(function (index, value) {
                            $($scope.Masters.listCategories.items).each(function (index1, value1) {
                                if (value.categoryId === value1.id) {
                                    $scope.Mastercate.ui.push(value1);
                                    debugger;
                                }
                            });
                        });

                        $($scope.GetStoreProductsDto.listBrands).each(function (index, value) {
                            $($scope.Masters.listBrands.items).each(function (index1, value1) {
                                if (value.brandId === value1.id) {
                                    $scope.Masterbrand.ui.push(value1);
                                }
                            });
                        });

                        $($scope.GetStoreProductsDto.listTags).each(function (index, value) {
                            $($scope.Masters.listTags.items).each(function (index1, value1) {
                                if (value.tagId === value1.id) {
                                    $scope.Mastertag.ui.push(value1);
                                }
                            });
                        });

                        $($scope.GetStoreProductsDto.listSeason).each(function (index, value) {
                            $($scope.Masters.listSeasons.items).each(function (index1, value1) {
                                if (value.seasonId === value1.id) {
                                    $scope.MasterSeasons.ui.push(value1);
                                }
                            });
                        });




                        $timeout(function () {
                            if ($scope.images != undefined && $scope.images != null && $scope.images.length > 0) {
                                $scope.numberLoaded = true;
                            }
                            cfpLoadingBar.complete();
                        }, 1000);

                    });

                $scope.getMastersdata();
            }


            $scope.getMastersdata = function () {
                _thisServices.getMastersdata()
                    .then(function (result) {
                        $scope.Masters = result.data;
                        console.log(result.data);


                        $scope.Supplier = result.data.listSuppliers.items;
                        $scope.Tax = result.data.listTax.items;

                    });
            };

            if ($scope.Id !== 0) {
                getstoreProducts($scope.Id);
            }
            else {
                getstoreProducts($scope.Id);
            }

            $scope.addNewCategories = function () {
                $aside.open({
                    templateUrl: '/App/Main/views/storeCategories/modal-addeditstoreCategories.cshtml',
                    //controller: 'app.views.modal-storeCategories',
                    placement: 'right',
                    size: '45perWidth',
                    scope: $scope,

                });
            };



            $scope.addNewBrands = function () {
                $aside.open({
                    templateUrl: '/App/Main/views/storeBrands/modal-addeditstoreBrands.cshtml',
                    //controller: 'app.views.modal-storeBrands',
                    placement: 'right',
                    size: '45perWidth',
                    scope: $scope,

                });
            };



            $scope.addNewTags = function () {
                $aside.open({
                    templateUrl: '/App/Main/views/storeTags/modal-addeditstoreTags.cshtml',
                    //controller: 'app.views.modal-storeTags',
                    placement: 'right',
                    size: '45perWidth',
                    scope: $scope,

                });
            };


            $scope.addNewSeason = function () {
                $aside.open({
                    templateUrl: '/App/Main/views/storeSeasons/modal-addeditstoreSeasons.cshtml',
                    //controller: 'app.views.modal-storeSeasons',
                    placement: 'right',
                    size: '45perWidth',
                    scope: $scope,

                });
            };

            $scope.AddVariants = function () {

                if ($scope.GetStoreProductsDto.productName != null && $scope.GetStoreProductsDto.productName != '') {
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



            $scope.uiselectblurorenter = function (value) {

                if (value === 'addNewCategories')
                    $scope.addNewCategories();
                else if (value === 'addNewBrands')
                    $scope.addNewBrands();
                else if (value === 'addNewTags')
                    $scope.addNewTags();
                else if (value === 'addNewSeason')
                    $scope.addNewSeason();

            };





            $scope.deleteVariant = function (value) {
                var index = $scope.GetStoreProductsDto.listProductVariants.indexOf(value);
                $scope.GetStoreProductsDto.listProductVariants.splice(index, 1);

                abp.notify.error("Delete Successfully.");

            };


            $scope.editVariant = function (value) {
                //alert('editVariant');
                debugger;
                $scope.obj = value;
                $aside.open({
                    templateUrl: '/App/Main/views/storeProducts/addeditstoreProductsVariants.cshtml',
                    //controller: 'app.views.variantslider',
                    placement: 'right',
                    size: '70perWidth',
                    scope: $scope,

                });

            };












            $scope.saveAddEdit = function () {
                cfpLoadingBar.start();



                $scope.GetStoreProductsDto.supplierId = $scope.GetStoreProductsDto.uisuppId.id;


                if ($scope.GetStoreProductsDto.isVariant === false) {
                    var result2 = $scope.GetStoreProductsDto.listProductPricing.filter(function (element) {
                        element.taxId = element.uitaxId.id;
                    });
                }


                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }

                $scope.GetStoreProductsDto.listCategory = [];
                $scope.GetStoreProductsDto.listBrands = [];
                $scope.GetStoreProductsDto.listTags = [];
                $scope.GetStoreProductsDto.listSeason = [];
                debugger;
                $($scope.Mastercate.ui).each(function (index, value) {
                    $scope.GetStoreProductsDto.listCategory.push(value);
                });

                $($scope.Masterbrand.ui).each(function (index, value) {
                    $scope.GetStoreProductsDto.listBrands.push(value);
                });

                $($scope.Mastertag.ui).each(function (index, value) {
                    $scope.GetStoreProductsDto.listTags.push(value);
                });

                $($scope.MasterSeasons.ui).each(function (index, value) {
                    $scope.GetStoreProductsDto.listSeason.push(value);
                });


                _thisServices.saveOrUpdateProduct($scope.GetStoreProductsDto)
                    .then(function () {

                        abp.notify.success("Saved Successfully.");
                        window.scrollTo(0, 0);

                        $state.go('storeProducts', { SearchDto: $stateParams.SearchDto });

                    });


            };

            $scope.cancelAddEdit = function () {
                window.scrollTo(0, 0);
                $state.go('storeProducts', { SearchDto: $stateParams.SearchDto });
            };


            $scope.CopyPrices = function (newValue) {
                if (newValue !== undefined && newValue !== "" && newValue !== 0) {
                    $scope.GetStoreProductsDto.listProductPricing.filter(function (element) {
                        element.price = newValue;
                    });
                }
                if ($scope.GetStoreProductsDto.isVariant == true || $scope.GetStoreProductsDto.isVariant === 'true') {
                    $($scope.GetStoreProductsDto.listProductVariants).each(function (index1, value1) {
                        $(value1.listProductPricing).each(function (index2, value2) {
                            value2.price = newValue;
                        });
                    });
                }
            };

            $scope.CopymarkUp = function (newValue) {
                if (newValue !== undefined && newValue !== "" && newValue !== 0) {
                    $scope.GetStoreProductsDto.listProductPricing.filter(function (element) {
                        element.markUp = newValue;
                    });
                }
                if ($scope.GetStoreProductsDto.isVariant == true || $scope.GetStoreProductsDto.isVariant === 'true') {
                    $($scope.GetStoreProductsDto.listProductVariants).each(function (index1, value1) {
                        $(value1.listProductPricing).each(function (index2, value2) {
                            value2.markUp = newValue;
                        });
                    });
                }
            };

            $scope.CopypriceExcludingTax = function (newValue) {
                if (newValue !== undefined && newValue !== "" && newValue !== 0) {
                    $scope.GetStoreProductsDto.listProductPricing.filter(function (element) {
                        element.priceExcludingTax = newValue;
                    });

                }
                if ($scope.GetStoreProductsDto.isVariant == true || $scope.GetStoreProductsDto.isVariant === 'true') {
                    $($scope.GetStoreProductsDto.listProductVariants).each(function (index1, value1) {
                        $(value1.listProductPricing).each(function (index2, value2) {
                            value2.priceExcludingTax = newValue;
                        });
                    });
                }
            };

            $scope.CopyuitaxId = function (newValue) {
                if (newValue !== undefined && newValue !== "" && newValue !== 0) {
                    $scope.GetStoreProductsDto.listProductPricing.filter(function (element) {
                        element.uitaxId = newValue;
                    });

                }
                if ($scope.GetStoreProductsDto.isVariant == true || $scope.GetStoreProductsDto.isVariant === 'true') {
                    $($scope.GetStoreProductsDto.listProductVariants).each(function (index1, value1) {
                        $(value1.listProductPricing).each(function (index2, value2) {
                            value2.uitaxId = newValue;
                            value2.taxId = newValue.id;
                        });
                    });
                }
            };

            $scope.CopypriceIncludingTax = function (newValue) {
                if (newValue !== undefined && newValue !== "" && newValue !== 0) {
                    $scope.GetStoreProductsDto.listProductPricing.filter(function (element) {
                        element.priceIncludingTax = newValue;
                    });

                }

                if ($scope.GetStoreProductsDto.isVariant == true || $scope.GetStoreProductsDto.isVariant === 'true') {
                    $($scope.GetStoreProductsDto.listProductVariants).each(function (index1, value1) {
                        $(value1.listProductPricing).each(function (index2, value2) {
                            value2.priceIncludingTax = newValue;
                        });
                    });
                }
            };

            $scope.copyprice = {};
            $scope.$watch("copyprice.price", function (newValue, oldValue) {
                $scope.CopyPrices(newValue);
            });

            $scope.$watch("copyprice.markUp", function (newValue, oldValue) {
                $scope.CopymarkUp(newValue);
            });

            $scope.$watch("copyprice.priceExcludingTax", function (newValue, oldValue) {
                $scope.CopypriceExcludingTax(newValue);

            });

            $scope.$watch("copyprice.uitaxId", function (newValue, oldValue) {
                $scope.CopyuitaxId(newValue);
            });

            $scope.$watch("copyprice.priceIncludingTax", function (newValue, oldValue) {
                $scope.CopypriceIncludingTax(newValue);

            });



        }
    ]);
})();