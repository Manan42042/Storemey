(function () {

    //angular.module('app').controller('app.views.temp.index', [
    //    '$scope', '$timeout', '$uibModal', 'abp.services.app.user',
    //    function ($scope, $timeout, $uibModal, userService) {
    //        $scope.data = [
    //            { name: 'Bob', title: 'CEO' },
    //            { name: 'Frank', title: 'Lowly Developer' },
    //            { name: 'Bob', title: 'CEO' },
    //            { name: 'Frank', title: 'Lowly Developer' },
    //            { name: 'Bob', title: 'CEO' },
    //            { name: 'Frank', title: 'Lowly Developer' },
    //            { name: 'Bob', title: 'CEO' },
    //            { name: 'Frank', title: 'Lowly Developer' },

    //        ];

    //    }
    //]);


    angular.module('app').controller('Products_controller', ['$scope', 'abp.services.app.user', '$http', '$q',
        function ($scope, userService, $http, $q) {
            $scope.gridOptions = [];
            alert('hi');

            vm = this;
            var users = [];

            //ervice('asyncService', function ($http, $q) {
            //    return {
            //        loadDataFromUrls: function (urls) {
            //            var deferred = $q.defer();
            //            var urlCalls = [];
            //            angular.forEach(urls, function (url) {
            //                urlCalls.push($http.get(url.url));
            //            });
            //             they may, in fact, all be done, but this
            //             executes the callbacks in then, once they are
            //             completely finished.
            //            $q.all(urlCalls)
            //                .then(
            //                    function (results) {
            //                        deferred.resolve(
            //                            JSON.stringify(results))
            //                    },
            //                    function (errors) {
            //                        deferred.reject(errors);
            //                    },
            //                    function (updates) {
            //                        deferred.update(updates);
            //                    });
            //            return deferred.promise;
            //        }
            //    };
            //});


            function getUsers() {
                var deferred = $q.defer();

                userService.getAll({}).then(function (result) {
                    users = result.data.items;

                    deferred.resolve(users);

                });
            }

            getUsers();


            //Pagination  
            $scope.pagination = {
                paginationPageSizes: [15, 25, 50, 75, 100, "All"],
                ddlpageSize: 15,
                pageNumber: 1,
                pageSize: 15,
                totalItems: 0,

                getTotalPages: function () {
                    return Math.ceil(this.totalItems / this.pageSize);
                },
                pageSizeChange: function () {
                    if (this.ddlpageSize == "All")
                        this.pageSize = $scope.pagination.totalItems;
                    else
                        this.pageSize = this.ddlpageSize

                    this.pageNumber = 1
                    $scope.GetProducts();
                },
                firstPage: function () {
                    if (this.pageNumber > 1) {
                        this.pageNumber = 1
                        $scope.GetProducts();
                    }
                },
                nextPage: function () {
                    if (this.pageNumber < this.getTotalPages()) {
                        this.pageNumber++;
                        $scope.GetProducts();
                    }
                },
                previousPage: function () {
                    if (this.pageNumber > 1) {
                        this.pageNumber--;
                        $scope.GetProducts();
                    }
                },
                lastPage: function () {
                    if (this.pageNumber >= 1) {
                        this.pageNumber = this.getTotalPages();
                        $scope.GetProducts();
                    }
                }
            };

            vm.getProductsList = function (apiRoute) {

                userService.getAll({}).then(function (result) {
                    return result.data.items;
                });
            }

            //ui-Grid Call  
            $scope.GetProducts = function () {
                $scope.loaderMore = true;
                $scope.lblMessage = 'loading please wait....!';
                $scope.result = "color-green";

                $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
                    if (col.filters[0].term) {
                        return 'header-filtered';
                    } else {
                        return '';
                    }
                };
                $scope.gridOptions = {
                    useExternalPagination: true,
                    useExternalSorting: true,
                    enableFiltering: true,
                    enableSorting: true,
                    enableRowSelection: true,
                    enableSelectAll: true,
                    enableGridMenu: true,

                    columnDefs: [
                        { name: "userName", displayName: "User Name", width: '10%', headerCellClass: $scope.highlightFilteredHeader },
                        { name: "surname", title: "Sur Name", width: '40%', headerCellClass: $scope.highlightFilteredHeader },
                        { name: "fullName", title: "fullName", headerCellClass: $scope.highlightFilteredHeader },
                        { name: "emailAddress", title: "emailAddress", headerCellClass: $scope.highlightFilteredHeader },
                        { name: "creationTime", title: "creationTime", headerCellClass: $scope.highlightFilteredHeader },
                        //{
                        //    name: "Price", title: "Price", cellFilter: 'number',
                        //    //filters: [{ condition: uiGridConstants.filter.GREATER_THAN, placeholder: 'Minimum' }, { condition: uiGridConstants.filter.LESS_THAN, placeholder: 'Maximum' }],
                        //    headerCellClass: $scope.highlightFilteredHeader
                        //},
                        //{ name: "CreatedOn", displayName: "Created On", cellFilter: 'date:"short"', headerCellClass: $scope.highlightFilteredHeader },
                        //{
                        //    name: 'Commands',
                        //    enableFiltering: false,
                        //    enableSorting: false,
                        //    width: '5%',
                        //    enableColumnResizing: false,
                        //    cellTemplate: '<span class="label label-warning label-mini">' +
                        //        '<a href="" style="color:white" title="Select" ng-click="grid.appScope.GetByID(row.entity)">' +
                        //        '<i class="fa fa-check-square" aria-hidden="true"></i>' +
                        //        '</a>' +
                        //        '</span>'
                        //}
                    ],
                    exporterAllDataFn: function () {
                        return getPage(1, $scope.gridOptions.totalItems, paginationOptions.sort)
                            .then(function () {
                                $scope.gridOptions.useExternalPagination = false;
                                $scope.gridOptions.useExternalSorting = false;
                                getPage = null;
                            });
                    },
                };

                var NextPage = (($scope.pagination.pageNumber - 1) * $scope.pagination.pageSize);
                var NextPageSize = $scope.pagination.pageSize;
                var apiRoute = 'api/Product/GetProducts/' + NextPage + '/' + NextPageSize;
                //var result = CRUDService.getProducts(apiRoute);
                //result = vm.users; 

                

                //getUsers().then(
                    //function (response) {
                debugger;
                        $scope.pagination.totalItems = 2000;
                        $scope.gridOptions.data = users;
                        $scope.loaderMore = false;
                    //},
                    //function (error) {
                    //    console.log("Error: " + error);
                    //});
            }



            //Default Load  
            $scope.GetProducts();

            //Selected Call  
            $scope.GetByID = function (model) {
                $scope.SelectedRow = model;
            };

           
        }

    ]);
    //JS - Service
    //angular.module('app').service('abp.services.app.user', function (userService) {
    //**********----Get Record----***************  

    //});  

})();