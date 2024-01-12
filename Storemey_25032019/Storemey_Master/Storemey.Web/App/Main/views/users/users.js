
app.controller('UsersCtrl', ['$scope', 'abp.services.app.user', '$http', '$q',
    function ($scope, usersServices, $http, $q) {
        $scope.gridOptions = [];
        var vm = this;

        vm.users = [];


        //vm.dates2 = {
        //    startDate: moment().subtract(1, 'day'),
        //    endDate: moment().subtract(1, 'day')
        //};
        vm.ranges = {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 days': [moment().subtract(7, 'days'), moment()],
            'Last 30 days': [moment().subtract(30, 'days'), moment()],
            'This month': [moment().startOf('month'), moment().endOf('month')]
        };



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
                    this.pageSize = this.ddlpageSize;

                this.pageNumber = 1;
                $scope.GetUsers();
            },
            firstPage: function () {
                if (this.pageNumber > 1) {
                    this.pageNumber = 1;
                    $scope.GetUsers();
                }
            },
            nextPage: function () {
                if (this.pageNumber < this.getTotalPages()) {
                    this.pageNumber++;
                    $scope.GetUsers();
                }
            },
            previousPage: function () {
                if (this.pageNumber > 1) {
                    this.pageNumber--;
                    $scope.GetUsers();
                }
            },
            lastPage: function () {
                if (this.pageNumber >= 1) {
                    this.pageNumber = this.getTotalPages();
                    $scope.GetUsers();
                }
            }
        };

        //ui-Grid Call
        $scope.GetUsers = function () {
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
            $scope.getTableHeight = function () {
                var rowHeight = 30; // your row height
                var headerHeight = 30; // your header height
                return {
                    height: ($scope.gridData.data.length * rowHeight + headerHeight) + "px"
                };
            };
            $scope.gridOptions = {
                useExternalPagination: false,
                useExternalSorting: false,
                enableFiltering: false,
                enableSorting: true,
                enableRowSelection: false,
                enableSelectAll: false,
                enableGridMenu: false,
                enableHorizontalScrollbar: 0,
                enableVerticalScrollbar: 0,
                enablePaginationControls: true,
                multiSelect: false,
                enableRowHeaderSelection: false,
                modifierKeysToMultiSelect: false,
                infiniteScrollDown: false,
                infiniteScrollUp: false,
                
                columnDefs: [
                    { name: "userName", displayName: "User Name", width: '20%', headerCellClass: $scope.highlightFilteredHeader },
                    { name: "surname", title: "Sur Name", width: '20%', headerCellClass: $scope.highlightFilteredHeader },
                    { name: "fullName", title: "fullName", width: '20%', headerCellClass: $scope.highlightFilteredHeader },
                    { name: "emailAddress", title: "emailAddress", width: '20%', headerCellClass: $scope.highlightFilteredHeader },
                    { name: "creationTime", displayName: "creation Time", width: '20%', cellFilter: 'date:"short"', headerCellClass: $scope.highlightFilteredHeader },
               
                    //{
                    //    name: "Price", title: "Price", cellFilter: 'number',
                    //    filters: [{ condition: uiGridConstants.filter.GREATER_THAN, placeholder: 'Minimum' }, { condition: uiGridConstants.filter.LESS_THAN, placeholder: 'Maximum' }],
                    //    headerCellClass: $scope.highlightFilteredHeader
                    //},
                    //
                    //{
                    //    name: 'Edit',
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

            vm.NextPage = (($scope.pagination.pageNumber - 1) * $scope.pagination.pageSize);
            vm.NextPageSize = $scope.pagination.pageSize;

            //var apiRoute = 'api/User/GetUsers/' + NextPage + '/' + NextPageSize;
            //var result = CRUDService.getUsers(apiRoute);

            var SearchDto = {
                CurrentPage: vm.NextPage,
                MaxRecords: vm.NextPageSize
            }

            usersServices.getAll({}).then(function (result) {
                console.log(result.data.items);

                $scope.pagination.totalItems = result.data.recordsTotal;
                $scope.gridOptions.data = result.data.items;
                $scope.loaderMore = false;

            });

            //result.then(
            //    function (response) {

            //    },
            //    function (error) {
            //        console.log("Error: " + error);
            //    });
        }

        //Default Load
        $scope.GetUsers();

        //Selected Call
        $scope.GetByID = function (model) {
            $scope.SelectedRow = model;
        };
    }
]);