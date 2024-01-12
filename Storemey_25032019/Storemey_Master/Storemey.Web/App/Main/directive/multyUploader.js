(function () {

    var app = angular.module("app");

    app.directive("imgUpload", function ($http, $compile, $ngConfirm) {
        return {
            restrict: 'AE',
            scope: {
                url: "@",
                method: "@"
            },
            template: '<input class="fileUpload" type="file" multiple />' +
                '<div class="dropzone">' +
                '<p class="msg">Click or Drag and Drop images files to upload</p>' +
                '</div>' +
                '<div class="preview clearfix">' +
                '<div class="previewData clearfix col-md-6" ng-repeat="data in previewData track by $index">' +
                '<div class="radio clip-radio radio-primary"> Is default: ' +
                '   <input type="radio" id="radio{{$index + 1}}" name="inline" value="{{$index}}" ng-checked="({{$index == 0}})"  ng-model="ch" ng-change="changeImg()" class="ng-valid ng-not-empty ng-dirty ng-valid-parse ng-touched" style="">' +
                '       <label for="radio{{$index + 1}}"> Image {{$index + 1}} </label>' +
                '			</div>  <img src="/assets/Hardware_Design/Untitled.png" style="width: 30px;height: 30px;" />' +
                '<img src={{data.src}} ng-click="openCrop(data)"></img>' +
                //'<div class="previewDetails">' +
                //'<div class="detail"><b>Name : </b>{{data.name}}</div>' +
                //'<div class="detail"><b>Type : </b>{{data.type}}</div>' +
                //'<div class="detail"><b>Size : </b> {{data.size}}</div>' +
                //'</div>' +
                '<div class="previewControls">' +
                //'<span ng-click="upload(data)" class="circle upload">' +
                //'<i class="fa fa-check"></i>' +
                //'</span>' +
                '<span ng-click="remove(data)" class="circle remove">' +
                '<i class="fa fa-close"></i>' +
                '</span>' +
                '</div>' +
                '</div>' +
                '</div>'
            ,
            link: function (scope, elem, attrs) {
                var formData = new FormData();
                scope.previewData = [];
                scope.ch = 0;

                function previewFile(file) {
                    var reader = new FileReader();
                    var obj = new FormData().append('file', file);
                    reader.onload = function (data) {
                        var src = data.target.result;
                        var size = ((file.size / (1024 * 1024)) > 1) ? (file.size / (1024 * 1024)) + ' mB' : (file.size / 1024) + ' kB';
                        scope.$apply(function () {
                            scope.previewData.push({
                                'name': file.name, 'size': size, 'type': file.type,
                                'src': src, 'data': obj
                            });
                        });
                        console.log(scope.previewData);
                    }
                    reader.readAsDataURL(file);
                }

                function uploadFile(e, type) {
                    e.preventDefault();
                    var files = "";
                    if (type == "formControl") {
                        files = e.target.files;
                    } else if (type === "drop") {
                        files = e.originalEvent.dataTransfer.files;
                    }
                    for (var i = 0; i < files.length; i++) {
                        var file = files[i];
                        if (file.type.indexOf("image") !== -1) {
                            previewFile(file);
                        } else {
                            alert(file.name + " is not supported");
                        }
                    }
                }
                elem.find('.fileUpload').bind('change', function (e) {
                    uploadFile(e, 'formControl');
                });

                elem.find('.dropzone').bind("click", function (e) {
                    $compile(elem.find('.fileUpload'))(scope).trigger('click');
                });

                elem.find('.dropzone').bind("dragover", function (e) {
                    e.preventDefault();
                });

                elem.find('.dropzone').bind("drop", function (e) {
                    uploadFile(e, 'drop');
                });
                scope.upload = function (obj) {
                    $http({
                        method: scope.method, url: scope.url, data: obj.data,
                        headers: { 'Content-Type': undefined }, transformRequest: angular.identity
                    }).success(function (data) {

                    });
                }

                scope.openCrop = function (data) {
                    scope.data = data;
                    scope.NewValue = '';
                    scope.cropmodalwindow = $ngConfirm({
                        boxWidth: '50%',
                        type: 'red',
                        typeAnimated: true,
                        closeIcon: true,
                        useBootstrap: false,
                        title: 'Crop image!',
                        contentUrl: '/App/Main/views/popups/cropimage/cropimage.cshtml',
                        scope: scope,
                        buttons: {
                            Save: {
                                text: 'Save',
                                btnClass: 'btn-blue',
                                action: function (scope, button) {
                                    scope.data.src = scope.data.result;
                                    scope.cropmodalwindow.close();
                                    return false; // prevent close;
                                }
                            },
                            close: function (scope, button) {

                            },
                        }
                    });
                };

                scope.remove = function (data) {
                    var index = scope.previewData.indexOf(data);
                    scope.previewData.splice(index, 1);
                }
            }
        }
    });



})();