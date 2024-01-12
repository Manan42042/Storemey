(function () {
    angular.module('app').controller('app.views.storeCategories', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeCategories','$ngConfirm',
        function ($scope, $state, $stateParams, _thisServices, $ngConfirm) {

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

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                debugger;
                if ($scope.GetstoreCategoriesOutputDto.id === undefined || $scope.GetstoreCategoriesOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreCategoriesOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeCategories', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreCategoriesOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeCategories', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeCategories');
            //};

            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('storeCategories', { SearchDto: $stateParams.SearchDto });
            };


            $('#fileInput').on('change', function () {
                if (this.files && this.files[0]) {
                    var FR = new FileReader();
                    FR.addEventListener("load", function (e) {
                        $scope.GetstoreCategoriesOutputDto.image = e.target.result;
                        $scope.openCropcate();
                    });
                    FR.readAsDataURL(this.files[0]);
                }
            });

            $scope.openCropcate = function () {
                $scope.data = {};
                $scope.data.src = $scope.GetstoreCategoriesOutputDto.image;
                $scope.data.result = '';
                $scope.cropmodalwindowcate = $ngConfirm({
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
                                $scope.cropmodalwindowcate.close();
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