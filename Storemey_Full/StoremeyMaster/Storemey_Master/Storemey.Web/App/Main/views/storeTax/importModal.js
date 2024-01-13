(function () {
    angular.module('app').controller('importstoreTaxCTR', [
        '$http', '$scope', '$rootScope', '$uibModalInstance', 'abp.services.app.storeTax',
        function ($http, $scope, $rootScope, $uibModalInstance, storeTaxervices) {


            $scope.uploadFile = function (files) {
                //storeTaxervices.importToCSV( files ).then(function (result) {
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
                        storeTaxervices.importFromCSV(Firastresult).then(function (result) {
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