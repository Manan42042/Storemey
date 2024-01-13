(function () {
    angular.module('app').controller('app.views.storeReceiptTemplates', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeReceiptTemplates', '$ngConfirm',
        function ($scope, $state, $stateParams, _thisServices, $ngConfirm) {

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


            $scope.PaymentType = [
                { name: '80-90mm thermal', id: 0 },
                { name: 'Standard Invoice', id: 1 }
            ];

            var GetstoreReceiptTemplatesInputDto = {
                Id: 0
            };

            $scope.GetstoreReceiptTemplatesOutputDto = {};

            function getstoreReceiptTemplates(Id) {
                GetstoreReceiptTemplatesInputDto.Id = Id;
                _thisServices.getById(GetstoreReceiptTemplatesInputDto)
                    .then(function (result) {
                        $scope.GetstoreReceiptTemplatesOutputDto = result.data;

                        
                        var result2 = $scope.PaymentType.filter(function (element) {
                            if (element.name == $scope.GetstoreReceiptTemplatesOutputDto.templateType) {
                                return true;
                            } else {
                                return false;
                            }
                        });
                        if (result2[0] !== undefined) {
                            console.log(result2[0]);
                            $scope.GetstoreReceiptTemplatesOutputDto.uiTemplateType = result2[0];
                        }

                    });
            }
            if ($scope.Id !== 0) {
                getstoreReceiptTemplates($scope.Id);
            }
            else {
                getstoreReceiptTemplates($scope.Id);
            }





$scope.saveAddEdit = function () {


    $scope.GetstoreReceiptTemplatesOutputDto.templateType = $scope.GetstoreReceiptTemplatesOutputDto.uiTemplateType.name;


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

$scope.cancelAddEdit = function () {
    window.scrollTo(0, 0);
    $state.go('storeReceiptTemplates', { SearchDto: $stateParams.SearchDto });
};



$('#fileInput').on('change', function () {
    if (this.files && this.files[0]) {
        var FR = new FileReader();
        FR.addEventListener("load", function (e) {
            $scope.GetstoreReceiptTemplatesOutputDto.logo = e.target.result;
            $scope.openCropreceipt();
        });
        FR.readAsDataURL(this.files[0]);
    }
});

$scope.openCropreceipt = function () {
    $scope.data = {};
    $scope.data.src = $scope.GetstoreReceiptTemplatesOutputDto.logo;
    $scope.data.result = '';
    $scope.cropmodalwindowreceipt = $ngConfirm({
        boxWidth: '50%',
        //type: 'red',
        typeAnimated: true,
        closeIcon: false,
        useBootstrap: false,
        title: 'Crop image!',
        contentUrl: '/App/Main/views/popups/cropimage/cropimage.cshtml',
        scope: $scope,
        buttons: {
            Save: {
                text: 'Save',
                btnClass: 'btn-blue',
                action: function (scope, button) {
                    $scope.GetstoreReceiptTemplatesOutputDto.logo = scope.data.result;
                    $scope.cropmodalwindowreceipt.close();
                    return false; // prevent close;
                }
            },
            //close: function (scope, button) {

            //},
        }
    });
};



        }
    ]);
}) ();