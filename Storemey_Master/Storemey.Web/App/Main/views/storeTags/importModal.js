﻿(function () {
    angular.module('app').controller('importstoreTagsCTR', [
        '$http', '$scope', '$rootScope', '$uibModalInstance', 'abp.services.app.storeTags',
        function ($http, $scope, $rootScope, $uibModalInstance, storeTagservices) {


            $scope.uploadFile = function (files) {
                //storeTagservices.importToCSV( files ).then(function (result) {
                //    window.location.href = result.data;
                //});

                var fd = new FormData();
                //Take the first selected file
                fd.append("file", files[0]);

                $http.post('/Home/ImportToCSV', fd, {
                    withCredentials: true,
                    headers: { 'Content-Type': undefined },
                    transformRequest: angular.identity
                }).then(function (data) {
                    //$rootScope.$broadcast('updateList', { data: 'passing' });
                    //abp.notify.success("Import done! Successfilly.");
                    if (data !== null && data !== undefined && data.data !== null) {
                        var Firastresult = data.data;
                        storeTagservices.importFromCSV(Firastresult).then(function (result) {
                            $rootScope.$broadcast('updateList', { data: 'passing' });
                            abp.notify.success("Import done! Successfilly.");
                        });
                    }
                });
            };



            $scope.cancel = function () {
                $uibModalInstance.dismiss({});
            };

        }
    ]);



})();