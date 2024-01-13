(function () {
    angular.module('app').controller('app.views.modal-storeCategories', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeCategories', '$ngConfirm', '$uibModalStack','cfpLoadingBar',
        function ($scope, $state, $stateParams, _thisServices, $ngConfirm, $uibModalStack, cfpLoadingBar) {

            $.validator.addMethod("regx", function (value, element, regexpr) {
                return regexpr.test(value);
            }, "Please enter a valid pasword.");

            $scope.validationOptions = {
                rules: {
                    categoryName: {
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

            var GetstoreCategoriesInputDto = {
                Id: 0
            };

            $scope.GetstoreCategoriesOutputDto = {};

            function getstoreCategories(Id) {
                GetstoreCategoriesInputDto.Id = Id;
                _thisServices.getById(GetstoreCategoriesInputDto)
                    .then(function (result) {
                        $scope.GetstoreCategoriesOutputDto = result.data;
                        if ($scope.GetstoreCategoriesOutputDto.image == null) {
                            $scope.GetstoreCategoriesOutputDto.image = "/images/noimage320@320.jpg";
                        }
                    });
            }
            if ($scope.Id !== 0) {
                getstoreCategories($scope.Id);
            }
            else {
                getstoreCategories($scope.Id);
            }



            $scope.saveAddEdit = function () {
                debugger;

                //var $loginForm = $('#frmCountry');
                //if (!$loginForm.valid()) {
                //    return;
                //}
                if ($scope.GetstoreCategoriesOutputDto.id === undefined || $scope.GetstoreCategoriesOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreCategoriesOutputDto)
                        .then(function () {
                            cfpLoadingBar.start();
                            $scope.getMastersdata();

                            abp.notify.success("Saved Successfully.");
                            //$state.go('storeCategories', { SearchDto: $stateParams.SearchDto });
                            $uibModalStack.dismissAll('closing');
                            cfpLoadingBar.complete();

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreCategoriesOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            //$state.go('storeCategories', { SearchDto: $stateParams.SearchDto });
                            $uibModalStack.dismissAll('closing');

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeCategories');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                //$state.go('storeCategories', { SearchDto: $stateParams.SearchDto });
                $uibModalStack.dismissAll('closing');
            };


            $('#fileInput').on('change', function () {
                if (this.files && this.files[0]) {
                    var FR = new FileReader();
                    FR.addEventListener("load", function (e) {
                        $scope.GetstoreCategoriesOutputDto.image = e.target.result;
                        $scope.openCropcatemodel();
                    });
                    FR.readAsDataURL(this.files[0]);
                }
            });

            $scope.openCropcatemodel = function () {
                $scope.data = {};
                $scope.data.src = $scope.GetstoreCategoriesOutputDto.image;
                $scope.data.result = '';
                $scope.cropmodalwindowcatemodel = $ngConfirm({
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
                                $scope.GetstoreCategoriesOutputDto.image = scope.data.result;
                                $scope.cropmodalwindowcatemodel.close();
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
})();