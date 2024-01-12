﻿
app.controller('storeRegistersCtrl', ['$scope', '$ngConfirm', '$state', '$stateParams', '$uibModal', 'abp.services.app.storeRegisters', 'cfpLoadingBar',
    function ($scope, $ngConfirm, $state, $stateParams, $uibModal, _thisServicesstoreRegisters, cfpLoadingBar) {
        $scope.gridOptions = [];
        $scope.storeRegisters = [];
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
            searchText: $scope.Serachtext,
            currentPage: $scope.NextPage,
            maxRecords: $scope.NextPageSize,
            startDate: null,
            endDate: null,
            sortColumn: '',
            sortDirection: 'desc',
            tempID: 0,
            pageNumber: 1
        };



        $scope.edit = function (entity) {
            //alert("edit " + JSON.stringify(entity));
            //$state.go('addeditstoreRegisters', { 'id': entity.id, 'searchDto': $scope.SearchDto });
            $scope.SearchDto.tempID = entity.id;
            $state.go('addeditstoreRegisters', { SearchDto: $scope.SearchDto });
        }

        var DeletestoreRegistersInputDto = {
        }
        $scope.remove = function (entity) {
            //alert("remote " + JSON.stringify(entity));
            DeletestoreRegistersInputDto.id = entity.id;


            $ngConfirm({
                title: 'Remove record ',
                content: 'Timezone, name: <strong>' + entity.name + '</strong> <div> Are you sure you want to delete this record? </div> ',
                scope: $scope,
                buttons: {
                    Yes: {
                        text: 'YES',
                        btnClass: 'btn-blue',
                        action: function (scope, button) {
                            _thisServicesstoreRegisters.delete(DeletestoreRegistersInputDto).then(function (result) {
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
            $state.go('addeditstoreRegisters', { SearchDto: $scope.SearchDto });
        }

        //$scope.ExportCsv = function () {
        //    _thisServicesstoreRegisters.exportToCSV().then(function (result) {
        //        window.location.href = result.data;
        //    });
        //};

        $scope.DropValueChange = function (item) {

            if (item === 'Export CSV') {
                _thisServicesstoreRegisters.exportToCSV().then(function (result) {
                    window.location.href = result.data;
                });
            }
            else if (item === 'Import CSV') {

                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/storeRegisters/importModal.cshtml',
                    controller: 'importstoreRegistersCTR',
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
                $scope.GetstoreRegisters();
            },
            firstPage: function () {
                if (this.pageNumber > 1) {
                    this.pageNumber = 1;
                    $scope.GetstoreRegisters();
                }
            },
            nextPage: function () {
                if (this.pageNumber < this.getTotalPages()) {
                    this.pageNumber++;
                    $scope.GetstoreRegisters();
                }
            },
            previousPage: function () {
                if (this.pageNumber > 1) {
                    this.pageNumber--;
                    $scope.GetstoreRegisters();
                }
            },
            lastPage: function () {
                if (this.pageNumber >= 1) {
                    this.pageNumber = this.getTotalPages();
                    $scope.GetstoreRegisters();
                }
            },
            refereshGrid: function (pnumber) {
                if (pnumber > 0) {
                    this.pageNumber = pnumber;
                }
                if (this.pageNumber >= 1) {
                    //this.pageNumber = 1;
                    $scope.GetstoreRegisters();
                }
            }
        };

        //ui-Grid Call
        $scope.GetstoreRegisters = function () {
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

                        $scope.pagination.refereshGrid(1);
                    });
                }, //onRegisterApi

                columnDefs: [
                    //{ name: "id", displayName: "Number", width: '7%', headerCellClass: $scope.highlightFilteredHeader },
                    { name: "registerName", displayName: "Register name", width: '15%', headerCellClass: $scope.highlightFilteredHeader },
                    { name: "outletName", displayName: "Outlet name", width: '15%', headerCellClass: $scope.highlightFilteredHeader },
                    { name: "receiptTemplateName", displayName: "Receipt template name", width: '15%', headerCellClass: $scope.highlightFilteredHeader },
                    { name: "receiptNumber", displayName: "R.No.", width: '15%', headerCellClass: $scope.highlightFilteredHeader },
                    { name: "receiptPrefix", displayName: "R.No. prefix", width: '15%', headerCellClass: $scope.highlightFilteredHeader },
                    { name: "receiptSuffix", displayName: "R.No. suffix", width: '15%', headerCellClass: $scope.highlightFilteredHeader },
                    
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

                $scope.SearchDto.searchText = $scope.Serachtext;
                $scope.SearchDto.currentPage = $scope.NextPage;
                $scope.SearchDto.maxRecords = $scope.NextPageSize;
                $scope.SearchDto.startDate = $scope.dates2.startDate;
                $scope.SearchDto.endDate = $scope.dates2.endDate;
                $scope.SearchDto.sortColumn = $scope.SearchDto.sortColumn;
                $scope.SearchDto.sortDirection = $scope.SearchDto.sortDirection;
                $scope.SearchDto.pageNumber = $scope.pagination.pageNumber;

            } else {


                $scope.SearchDto.searchText = $scope.Serachtext;
                $scope.SearchDto.currentPage = $scope.NextPage;
                $scope.SearchDto.maxRecords = $scope.NextPageSize;
                $scope.SearchDto.startDate = null;
                $scope.SearchDto.endDate = null;
                $scope.SearchDto.sortColumn = $scope.SearchDto.sortColumn;
                $scope.SearchDto.sortDirection = $scope.SearchDto.sortDirection;
                $scope.SearchDto.pageNumber = $scope.pagination.pageNumber;

            }



            //if ($stateParams.SearchDto !== undefined && $stateParams.SearchDto !== null && $stateParams.SearchDto !== '') {
            //    if ($stateParams.SearchDto.pageNumber.tempID !== 0) {
            //        $scope.SearchDto.SearchText = $stateParams.SearchDto.SearchText;
            //        $scope.SearchDto.CurrentPage = $stateParams.SearchDto.CurrentPage;
            //        $scope.SearchDto.MaxRecords = $stateParams.SearchDto.MaxRecords;
            //        $scope.SearchDto.StartDate = $stateParams.SearchDto.StartDate;
            //        $scope.SearchDto.EndDate = $stateParams.SearchDto.EndDate;
            //        $scope.SearchDto.sortColumn = $stateParams.SearchDto.sortColumn;
            //        $scope.SearchDto.sortDirection = $stateParams.SearchDto.sortDirection;
            //        $scope.pagination.pageNumber = $stateParams.SearchDto.pageNumber;
            //        //$scope.SearchDto.tempID = 0;
            //    }
            //}

            //var apiRoute = 'api/storeRegisters/GetstoreRegisters/' + NextPage + '/' + NextPageSize;
            //var result = CRUDService.getstoreRegisters(apiRoute);



            //abp.ui.setBusy(
            //    $('.grid'),
            cfpLoadingBar.start();

                _thisServicesstoreRegisters.getAdvanceSearch($scope.SearchDto).then(function (result) {
                    if (result.data === null) {
                        $scope.pagination.totalItems = 1;
                        $scope.gridOptions.data = [];
                        $scope.loaderMore = false;
                        //abp.ui.clearBusy('body');
                        $stateParams.SearchDto = null;
                    }
                    else {
                        $scope.pagination.totalItems = result.data.items[0].recordsTotal;
                        $scope.gridOptions.data = result.data.items;
                        $scope.loaderMore = false;
                        $stateParams.SearchDto = null;
                        //abp.ui.clearBusy('body');
                    }
                    cfpLoadingBar.complete();
                })
            //);


        };

        //Default Load
        $scope.GetstoreRegisters();
        //$scope.Process();


        //Selected Call
        $scope.GetByID = function (model) {
            $scope.SelectedRow = model;
        };

        $scope.$on("updateList", function (e, a) {
            $scope.pagination.refereshGrid(1);
        });

        if ($stateParams.SearchDto !== undefined && $stateParams.SearchDto !== null && $stateParams.SearchDto !== '') {
            // $scope.pagination.refereshGrid($stateParams.SearchDto.pageNumber);
        }
    }
]);