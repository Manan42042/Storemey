(function () {
    angular.module('app').controller('app.views.storeUsers', [
        '$scope', '$state', '$stateParams', 'abp.services.app.storeUsers','$ngConfirm',
        function ($scope, $state, $stateParams, _thisServices, $ngConfirm) {

            $.validator.addMethod("regx", function (value, element, regexpr) {
                return regexpr.test(value);
            }, "Please enter a valid pasword.");

            $scope.validationOptions = {
                rules: {
                    firstName: {
                        required: true,
                    },
                    lastName: {
                        required: true,
                    },
                    phoneNumber: {
                        required: true,
                    },
                    userName: {
                        required: true,
                    },
                    email: {
                        required: true,
                        email: true
                    },
                    password: {
                        required: true,
                        //change regexp to suit your needs
                        regx: /(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}/,
                    }
                }
            }

            var stateobj = $stateParams.SearchDto;
            if ($stateParams.SearchDto !== undefined && $stateParams.SearchDto !== null && $stateParams.SearchDto !== '') {
                $scope.Id = stateobj.tempID;
            }
            else {
                $stateParams.SearchDto = null;
            }

            var GetstoreUsersInputDto = {
                Id: 0
            };

            $scope.GetstoreUsersOutputDto = {};

            function getstoreUsers(Id) {
                GetstoreUsersInputDto.Id = Id;
                _thisServices.getById(GetstoreUsersInputDto)
                    .then(function (result) {
                        $scope.GetstoreUsersOutputDto = result.data;
                        if ($scope.GetstoreUsersOutputDto.image == null) {
                            $scope.GetstoreUsersOutputDto.image = "/images/noimage320@320.jpg";
                        }

                    });
            }
            if ($scope.Id !== 0) {
                getstoreUsers($scope.Id);
            }
            else {
                getstoreUsers($scope.Id);
            }



            $scope.saveAddEdit = function () {

                var $loginForm = $('#frmCountry');
                if (!$loginForm.valid()) {
                    return;
                }
                debugger;
                if ($scope.GetstoreUsersOutputDto.id === undefined || $scope.GetstoreUsersOutputDto.id === 0) {
                    _thisServices.create($scope.GetstoreUsersOutputDto)
                        .then(function () {

                            abp.notify.success("Saved Successfully.");
                            $state.go('storeUsers', { SearchDto: $stateParams.SearchDto });

                        });
                }
                else {
                    _thisServices.update($scope.GetstoreUsersOutputDto)
                        .then(function () {

                            abp.notify.success("Updated Successfully.");
                            $state.go('storeUsers', { SearchDto: $stateParams.SearchDto });

                        });
                }
            };


            //$scope.saveAddEdit = function () {
            //    $state.go('storeUsers');
            //};



            $('#fileInput').on('change', function () {
                if (this.files && this.files[0]) {
                    var FR = new FileReader();
                    FR.addEventListener("load", function (e) {
                        $scope.GetstoreUsersOutputDto.image = e.target.result;
                        $scope.openCrop();
                    });
                    FR.readAsDataURL(this.files[0]);
                }
            });

            $scope.openCrop = function () {
                $scope.data = {};
                $scope.data.src = $scope.GetstoreUsersOutputDto.image;
                $scope.data.result = '';
                $scope.cropmodalwindow = $ngConfirm({
                    boxWidth: '50%',
                    type: 'red',
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
                                $scope.GetstoreUsersOutputDto.image = scope.data.result;
                                $scope.cropmodalwindow.close();
                                return false; // prevent close;
                            }
                        },
                        //close: function (scope, button) {

                        //},
                    }
                });
            };






            $scope.cancelAddEdit = function () { window.scrollTo(0, 0);
                $state.go('storeUsers', { SearchDto: $stateParams.SearchDto });
            };
        }
    ]);
})();