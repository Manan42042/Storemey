
app.controller('PlansCtrl', ['$scope', '$state', '$stateParams', '$uibModal', 'abp.services.app.masterPlans', '$http', '$q', 'uiGridExporterService', 'uiGridExporterConstants',
    function ($scope, $state, $stateParams, $uibModal, _thisServices, $http, $q, uiGridExporterService, uiGridExporterConstants) {
        $scope.gridOptions = [];
        $scope.Plans = [];
        $scope.useDateRangePicker = false;

        $scope.Serachtext = "";
        $scope.dates2 = {
            startDate: moment().subtract(1, 'day'),
            endDate: moment().subtract(1, 'day')
        };
        $scope.ranges = {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 days': [moment().subtract(7, 'days'), moment()],
            'Last 30 days': [moment().subtract(30, 'days'), moment()],
            'This month': [moment().startOf('month'), moment().endOf('month')]
        };

        $scope.SearchDto = {
            SearchText: $scope.Serachtext,
            CurrentPage: $scope.NextPage,
            MaxRecords: $scope.NextPageSize,
            StartDate: null,
            EndDate: null,
            sortColumn: '',
            sortDirection: ''
        };


        $scope.edit = function (entity) {
            //alert("edit " + JSON.stringify(entity));
            $state.go('addeditmasterplans', { 'id': entity.id });
        }

        var DeleteMasterPlansInputDto = {
        }
        $scope.remove = function (entity) {
            //alert("remote " + JSON.stringify(entity));
            DeleteMasterPlansInputDto.id = entity.id;
            //abp.message.confirm(
            //    'User admin will be deleted.',
            //    'Are you sure?',
            //    function (isConfirmed) {
            //        if (isConfirmed) {
            //            _thisServices.delete(DeleteMasterPlansInputDto).then(function (result) {
            //                abp.notify.success("Deleted! Successfilly.");
            //            });
            //        }
            //    }
            //);
 
            _thisServices.delete(DeleteMasterPlansInputDto).then(function (result) {
                abp.notify.success("Deleted! Successfilly.");
                $scope.pagination.refereshGrid();
            });
        }


        $scope.CreateAddEdit = function () {
            $state.go('addeditmasterplans');
        }

        //$scope.ExportCsv = function () {
        //    _thisServices.exportToCSV().then(function (result) {
        //        window.location.href = result.data;
        //    });
        //};

        $scope.DropValueChange = function (item) {

            if (item === 'Export CSV') {
                _thisServices.exportToCSV().then(function (result) {
                    window.location.href = result.data;
                });
            }
            else if (item === 'Import CSV') {

                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/masterPlans/importModal.cshtml',
                    controller: 'importModalCTR as vm',
                    backdrop: 'static'
                });


                modalInstance.rendered.then(function () {
                  
                });

                modalInstance.result.then(function () {
                    //alert('closed');
                    //$scope.pagination.refereshGrid();
                });

                //modalInstance.rendered.then(function () {
                //    $.AdminBSB.input.activate();
                //});

                //modalInstance.result.then(function () {
                //    getUsers();
                //});

            }


        };


        //EXPORT

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
                $scope.GetPlans();
            },
            firstPage: function () {
                if (this.pageNumber > 1) {
                    this.pageNumber = 1;
                    $scope.GetPlans();
                }
            },
            nextPage: function () {
                if (this.pageNumber < this.getTotalPages()) {
                    this.pageNumber++;
                    $scope.GetPlans();
                }
            },
            previousPage: function () {
                if (this.pageNumber > 1) {
                    this.pageNumber--;
                    $scope.GetPlans();
                }
            },
            lastPage: function () {
                if (this.pageNumber >= 1) {
                    this.pageNumber = this.getTotalPages();
                    $scope.GetPlans();
                }
            },
            refereshGrid: function () {
                if (this.pageNumber >= 1) {
                    this.pageNumber = 1;
                    $scope.GetPlans();
                }
            }
        };

        //ui-Grid Call
        $scope.GetPlans = function () {
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
                useExternalPagination: true,
                enableFiltering: false,
                enableSorting: true,
                enableRowSelection: false,
                enableSelectAll: false,
                enableHorizontalScrollbar: 0,
                enableVerticalScrollbar: 0,
                enablePaginationControls: false,
                multiSelect: false,
                enableRowHeaderSelection: false,
                modifierKeysToMultiSelect: false,
                infiniteScrollDown: false,
                infiniteScrollUp: false,
                useExternalSorting: true,
                enableGridMenu: false,
                exporterMenuCsv: false,
                exporterMenuPdf: false,
                gridMenuShowHideColumns: false,



                onRegisterApi: function (gridApi) {

                    $scope.gridApi = gridApi;
                    gridApi.core.on.sortChanged($scope, (grid, sortColumns) => {
                        var sortColumn = sortColumns[0].field;
                        var sortDirection = sortColumns[0].sort.direction;
                        // then I call a method in my controller which hits my server
                        // side code and returns external sorting through a linq query
                        $scope.SearchDto.sortColumn = sortColumn;
                        $scope.SearchDto.sortDirection = sortDirection;

                        $scope.pagination.refereshGrid();
                    });
                }, //onRegisterApi

                columnDefs: [
                    { name: "id", displayName: "Number", width: '15%', headerCellClass: $scope.highlightFilteredHeader },
                    { name: "planName", title: "Plan Name", width: '60%', headerCellClass: $scope.highlightFilteredHeader },
                    //{ name: "currency_Name", title: "currency_Name", width: '23%', headerCellClass: $scope.highlightFilteredHeader },
                    //{ name: "current_Code", title: "current_Code", width: '18%', headerCellClass: $scope.highlightFilteredHeader },
                    { name: "lastModificationTime", displayName: "Updated Date", type: 'date', width: '15%', cellFilter: 'date:\'MM-dd-yyyy hh:mm a\'', headerCellClass: $scope.highlightFilteredHeader },



                    {
                        name: 'Commands',
                        enableFiltering: false,
                        enableSorting: false,
                        width: '10%',
                        enableColumnResizing: false,
                        cellTemplate: '<span class="label-mini">' +
                            '<a href="#" class="btn btn-transparent btn-xs" tooltip-placement="top" uib-tooltip="Edit" ng-click="grid.appScope.edit(row.entity)"> <i class="fa fa-pencil"></i>' +
                            '</a> <a href="#" class="btn btn-transparent btn-xs tooltips" tooltip-placement="top"  ng-click="grid.appScope.remove(row.entity)" uib-tooltip="Remove" style="padding: 0px; margin-left: 3px;" >' +
                            '<i class="fa fa-times fa fa-white"></i>' +
                            '</a>' +
                            '</span>'
                    }
                ],
                //exporterAllDataFn: function () {
                //    return getPage(1, $scope.gridOptions.totalItems, paginationOptions.sort)
                //        .then(function () {
                //            $scope.gridOptions.useExternalPagination = false;
                //            $scope.gridOptions.useExternalSorting = false;
                //            getPage = null;
                //        });
                //},
            };

            $scope.NextPage = (($scope.pagination.pageNumber - 1) * $scope.pagination.pageSize);
            $scope.NextPageSize = $scope.pagination.pageSize;


            if ($scope.useDateRangePicker) {
                $scope.SearchDto = {
                    SearchText: $scope.Serachtext,
                    CurrentPage: $scope.NextPage,
                    MaxRecords: $scope.NextPageSize,
                    StartDate: $scope.dates2.startDate,
                    EndDate: $scope.dates2.endDate,
                    sortColumn: $scope.SearchDto.sortColumn,
                    sortDirection: $scope.SearchDto.sortDirection
                };
            } else {

                $scope.SearchDto = {
                    SearchText: $scope.Serachtext,
                    CurrentPage: $scope.NextPage,
                    MaxRecords: $scope.NextPageSize,
                    StartDate: null,
                    EndDate: null,
                    sortColumn: $scope.SearchDto.sortColumn,
                    sortDirection: $scope.SearchDto.sortDirection
                };
            }

            //var apiRoute = 'api/Plan/GetPlans/' + NextPage + '/' + NextPageSize;
            //var result = CRUDService.getPlans(apiRoute);

            abp.ui.setBusy(
                $('.grid'),

                _thisServices.getAdvanceSearch($scope.SearchDto).then(function (result) {
                    if (result.data === null) {
                        $scope.pagination.totalItems = 1;
                        $scope.gridOptions.data = [];
                        $scope.loaderMore = false;
                    }
                    else {
                        $scope.pagination.totalItems = result.data.items[0].recordsTotal;
                        $scope.gridOptions.data = result.data.items;
                        $scope.loaderMore = false;
                    }

                })
            );



            //result.then(
            //    function (response) {

            //    },
            //    function (error) {
            //        console.log("Error: " + error);
            //    });
        }

        //Default Load
        $scope.GetPlans();

        //Selected Call
        $scope.GetByID = function (model) {
            $scope.SelectedRow = model;
        };

        $scope.$on("updateList", function (e, a) {
            $scope.pagination.refereshGrid();
        });

        
    }
]);