
app.controller('PlanServicesCtrl', ['$scope', '$ngConfirm', '$state', '$stateParams', '$uibModal', 'abp.services.app.masterPlanServices', '$http', '$q', 'uiGridExporterService', 'uiGridExporterConstants','cfpLoadingBar',
    function ($scope, $ngConfirm, $state, $stateParams, $uibModal, _thisServices, $http, $q, uiGridExporterService, uiGridExporterConstants, cfpLoadingBar) {
        $scope.gridOptions = [];
        $scope.PlanServices = [];
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
            $state.go('addeditmasterplanservices', { 'id': entity.id });
        }

        var DeleteMasterPlanServicesInputDto = {
        }
        $scope.remove = function (entity) {
            //alert("remote " + JSON.stringify(entity));
            DeleteMasterPlanServicesInputDto.id = entity.id;
            $ngConfirm({
                title: 'Remove record ',
                content: 'Service name: <strong>' + entity.serviceName + '</strong> <div> Are you sure you want to delete this record? </div> ',
                scope: $scope,
                buttons: {
                    Yes: {
                        text: 'YES',
                        btnClass: 'btn-blue',
                        action: function (scope, button) {
                            _thisServices.delete(DeleteMasterPlanServicesInputDto).then(function (result) {
                                abp.notify.error("Deleted! Successfilly.");
                                $scope.pagination.refereshGrid();
                            });
                        }
                    },
                    No: {
                        text: 'No',
                        btnClass: 'btn-orange',
                        action: function (scope, button) {
                            //$ngConfirm('Delete process is canceled');
                        }
                    }
                    //},
                    //close: function (scope, button) {
                    //    // closes the modal
                    //}
                }
            });

        };


        $scope.CreateAddEdit = function () {
            $state.go('addeditmasterplanservices');
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
                    templateUrl: '/App/Main/views/masterPlanServices/importModal.cshtml',
                    controller: 'importmasterPlanServicesCTR as vm',
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
            paginationPageSizes: [10, 25, 50, 75, 100, "All"],
            ddlpageSize: 10,
            pageNumber: 1,
            pageSize: 10,
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
                $scope.GetPlanServices();
            },
            firstPage: function () {
                if (this.pageNumber > 1) {
                    this.pageNumber = 1;
                    $scope.GetPlanServices();
                }
            },
            nextPage: function () {
                if (this.pageNumber < this.getTotalPages()) {
                    this.pageNumber++;
                    $scope.GetPlanServices();
                }
            },
            previousPage: function () {
                if (this.pageNumber > 1) {
                    this.pageNumber--;
                    $scope.GetPlanServices();
                }
            },
            lastPage: function () {
                if (this.pageNumber >= 1) {
                    this.pageNumber = this.getTotalPages();
                    $scope.GetPlanServices();
                }
            },
            refereshGrid: function () {
                if (this.pageNumber >= 1) {
                    this.pageNumber = 1;
                    $scope.GetPlanServices();
                }
            }
        };

        //ui-Grid Call
        $scope.GetPlanServices = function () {
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
                enableColumnMenus: false,


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
                    //{ name: "id", displayName: "Number", width: '15%', headerCellClass: $scope.highlightFilteredHeader },
                    { name: "serviceName", title: "Service Name", width: '75%', headerCellClass: $scope.highlightFilteredHeader },
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

            //var apiRoute = 'api/Plan/GetPlanServices/' + NextPage + '/' + NextPageSize;
            //var result = CRUDService.getPlanServices(apiRoute);

            //abp.ui.setBusy(
            //    $('.grid'),

            cfpLoadingBar.start();
                _thisServices.getAdvanceSearch($scope.SearchDto).then(function (result) {
                    if (result.data === null) {
                        $scope.pagination.totalItems = 1;
                        $scope.gridOptions.data = [];
                        $scope.loaderMore = false;
                        //abp.ui.clearBusy('body');

                    }
                    else {
                        $scope.pagination.totalItems = result.data.items[0].recordsTotal;
                        $scope.gridOptions.data = result.data.items;
                        $scope.loaderMore = false;
                        //abp.ui.clearBusy('body');

                    }
                    cfpLoadingBar.complete();
                })
            //);



            //result.then(
            //    function (response) {

            //    },
            //    function (error) {
            //        console.log("Error: " + error);
            //    });
        }

        //Default Load
        $scope.GetPlanServices();
        //$scope.Process();


        //Selected Call
        $scope.GetByID = function (model) {
            $scope.SelectedRow = model;
        };

        $scope.$on("updateList", function (e, a) {
            $scope.pagination.refereshGrid();
        });


    }
]);