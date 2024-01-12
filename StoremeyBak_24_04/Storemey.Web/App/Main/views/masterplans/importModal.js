(function () {
    angular.module('app').controller('importmasterPlansCTR', [
        '$http', '$scope', '$rootScope', '$uibModalInstance', 'abp.services.app.masterPlans',
        function ($http, $scope, $rootScope, $uibModalInstance, masterPlansServices) {
            var vm = this;


            $scope.uploadFile = function (files) {
                //masterPlansServices.importToCSV( files ).then(function (result) {
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
                    if (data !== null && data !== undefined && data.data !== null) {
                        var Firastresult = data.data;
                            masterPlansServices.importFromCSV(Firastresult).then(function (result) {
                                $rootScope.$broadcast('updateList', { data: 'passing' });
                                abp.notify.success("Import done! Successfilly.");
                            });
                    }
                });
            };



            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            };

        }
    ]);



})();