(function () {
    'use strict';

    var app = angular.module('app', [
        //'ngAnimate',
        //'ngSanitize',


        'ngAnimate',
        'ngCookies',
        'ngStorage',
        'ngSanitize',
        'ngTouch',
        'ui.router',
        'ui.bootstrap',
        'angularMoment',
        'oc.lazyLoad',
        'swipe',
        'ngBootstrap',
        'truncate',
        'uiSwitch',
        'toaster',
        'ngAside',
        'vAccordion',
        'vButton',
        'oitozero.ngSweetAlert',
        'angular-notification-icons',
        'angular-ladda',
        'angularAwesomeSlider',
        //'slickCarousel',
        'cfp.loadingBar',
        'ncy-angular-breadcrumb',
        'duScroll',
        'pascalprecht.translate',
        'FBAngular',
        'ui.router',
        'ui.bootstrap',
        'ui.jq',
        'ngSanitize',
        'ui.select',
        'cp.ngConfirm',
        'ngValidate',
        'uiCropper',
        'abp',
        'ui.grid', //data grid for AngularJS  
        'ui.grid.pagination', //data grid Pagination  
        'ui.grid.resizeColumns', //data grid Resize column  
        'ui.grid.moveColumns', //data grid Move column  
        'ui.grid.pinning', //data grid Pin column Left/Right  
        'ui.grid.selection', //data grid Select Rows  
        'ui.grid.autoResize', //data grid Enabled auto column Size  
        'ui.grid.exporter', //data grid Export Data  
        'ckeditor',
        'slick',
        'ui.bootstrap',
        'mj.scrollingTabs',
        'counter'
    ]);


    //Configuration for Angular UI routing.
    app.config([
        '$stateProvider', '$urlRouterProvider', '$locationProvider', '$qProvider',
        function ($stateProvider, $urlRouterProvider, $locationProvider, $qProvider) {
            $locationProvider.hashPrefix('page');
            //$locationProvider.baseHref = "pages";
            $urlRouterProvider.otherwise('/home');
            $qProvider.errorOnUnhandledRejections(false);
            //$locationProvider.html5Mode(true);
            //$locationProvider.hashPrefix('!');


            // MASTER COUNTRY
            if ($('#paneltype').val() === 'user') {

                $stateProvider
                    .state('mastercountries', {
                        url: '/mastercountries',
                        templateUrl: '/App/Main/views/mastercountries/mastercountries.cshtml',
                        menu: 'Master Countries', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditmastercountries', {
                        url: '/addeditmastercountries?id',
                        templateUrl: '/App/Main/views/mastercountries/addeditmastercountries.cshtml',
                        menu: 'addeditmastercountries', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });

                // MASTER PLANS


                $stateProvider
                    .state('masterplans', {
                        url: '/masterplans?id',
                        templateUrl: '/App/Main/views/masterplans/masterplans.cshtml',
                        menu: 'Master Plans', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });
                $stateProvider.state('addeditmasterplans', {
                    url: '/addeditmasterplans?id',
                    templateUrl: '/App/Main/views/masterplans/addeditmasterplans.cshtml',
                    menu: 'addeditmasterplans', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                    params: { SearchDto: null }
                });


                // MASTER PLAN SERVICE

                $stateProvider
                    .state('masterplanservices', {
                        url: '/masterplanservices?id',
                        templateUrl: '/App/Main/views/masterplanservices/masterplanservices.cshtml',
                        menu: 'Master Plans', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });

                $stateProvider.state('addeditmasterplanservices', {
                    url: '/addeditmasterplanservices?id',
                    templateUrl: '/App/Main/views/masterplanservices/addeditmasterplanservices.cshtml',
                    menu: 'addeditmasterplanservices', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                    params: { SearchDto: null }
                });

                // STORE CURRENCY 

                $stateProvider
                    .state('storecurrencies', {
                        url: '/currencies',
                        templateUrl: '/App/Main/views/storecurrencies/storecurrencies.cshtml',
                        menu: 'Store Currencies', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstorecurrencies', {
                        url: '/managecurrencies',
                        templateUrl: '/App/Main/views/storecurrencies/addeditstorecurrencies.cshtml',
                        menu: 'Store Currencies', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });

                // STORE GENERAL SETTINGS

                $stateProvider
                    .state('storeGeneralSettings', {
                        url: '/generalsettings',
                        templateUrl: '/App/Main/views/storeGeneralSettings/storeGeneralSettings.cshtml',
                        menu: 'General Settings', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // STORE USERES

                $stateProvider
                    .state('storeUsers', {
                        url: '/users',
                        templateUrl: '/App/Main/views/storeUsers/storeUsers.cshtml',
                        menu: 'Store Users', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreUsers', {
                        url: '/manageusers',
                        templateUrl: '/App/Main/views/storeUsers/addeditstoreUsers.cshtml',
                        menu: 'Store Users', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // Tax Group

                $stateProvider
                    .state('storeTaxGroups', {
                        url: '/taxgroups',
                        templateUrl: '/App/Main/views/storeTaxGroups/storeTaxGroups.cshtml',
                        menu: 'Store Users', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreTaxGroups', {
                        url: '/managetaxgroups',
                        templateUrl: '/App/Main/views/storeTaxGroups/addeditstoreTaxGroups.cshtml',
                        menu: 'Store Users', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // Tax 
                $stateProvider
                    .state('storeTax', {
                        url: '/tax',
                        templateUrl: '/App/Main/views/storeTax/storeTax.cshtml',
                        menu: 'Store Users', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreTax', {
                        url: '/managetax',
                        templateUrl: '/App/Main/views/storeTax/addeditstoreTax.cshtml',
                        menu: 'Store Users', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // Tags
                $stateProvider
                    .state('storeTags', {
                        url: '/tags',
                        templateUrl: '/App/Main/views/storeTags/storeTags.cshtml',
                        menu: 'Store Users', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreTags', {
                        url: '/managetags',
                        templateUrl: '/App/Main/views/storeTags/addeditstoreTags.cshtml',
                        menu: 'store Tags', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });


                // StoreSuppliers
                $stateProvider
                    .state('storeSuppliers', {
                        url: '/suppliers',
                        templateUrl: '/App/Main/views/storeSuppliers/storeSuppliers.cshtml',
                        menu: 'store Suppliers', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreSuppliers', {
                        url: '/managesuppliers',
                        templateUrl: '/App/Main/views/storeSuppliers/addeditstoreSuppliers.cshtml',
                        menu: 'store Tags', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });





                // storeStateMaster
                $stateProvider
                    .state('storeStateMaster', {
                        url: '/statemaster',
                        templateUrl: '/App/Main/views/storeStateMaster/storeStateMaster.cshtml',
                        menu: 'store Suppliers', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreStateMaster', {
                        url: '/managestatemaster',
                        templateUrl: '/App/Main/views/storeStateMaster/addeditstoreStateMaster.cshtml',
                        menu: 'store Tags', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // storeReceiptTemplates
                $stateProvider
                    .state('storeReceiptTemplates', {
                        url: '/receipttemplates',
                        templateUrl: '/App/Main/views/storeReceiptTemplates/storeReceiptTemplates.cshtml',
                        menu: 'store Suppliers', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreReceiptTemplates', {
                        url: '/managereceipttemplates',
                        templateUrl: '/App/Main/views/storeReceiptTemplates/addeditstoreReceiptTemplates.cshtml',
                        menu: 'store Tags', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // storePaymentTypes
                $stateProvider
                    .state('storePaymentTypes', {
                        url: '/paymenttypes',
                        templateUrl: '/App/Main/views/storePaymentTypes/storePaymentTypes.cshtml',
                        menu: 'store Suppliers', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstorePaymentTypes', {
                        url: '/managepaymenttypes',
                        templateUrl: '/App/Main/views/storePaymentTypes/addeditstorePaymentTypes.cshtml',
                        menu: 'store Tags', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });


                // storeCustomers
                $stateProvider
                    .state('storeCustomers', {
                        url: '/customers',
                        templateUrl: '/App/Main/views/storeCustomers/storeCustomers.cshtml',
                        menu: 'store Suppliers', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreCustomers', {
                        url: '/managecustomers',
                        templateUrl: '/App/Main/views/storeCustomers/addeditstoreCustomers.cshtml',
                        menu: 'store Tags', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });





                // storeCountryMaster
                $stateProvider
                    .state('storeCountryMaster', {
                        url: '/countrymaster',
                        templateUrl: '/App/Main/views/storeCountryMaster/storeCountryMaster.cshtml',
                        menu: 'store Suppliers', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreCountryMaster', {
                        url: '/managecountrymaster',
                        templateUrl: '/App/Main/views/storeCountryMaster/addeditstoreCountryMaster.cshtml',
                        menu: 'store Tags', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // storeCityMaster
                $stateProvider
                    .state('storeCityMaster', {
                        url: '/citymaster',
                        templateUrl: '/App/Main/views/storeCityMaster/storeCityMaster.cshtml',
                        menu: 'store Suppliers', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreCityMaster', {
                        url: '/managecitymaster',
                        templateUrl: '/App/Main/views/storeCityMaster/addeditstoreCityMaster.cshtml',
                        menu: 'store Tags', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });


                // storeCategories
                $stateProvider
                    .state('storeCategories', {
                        url: '/categories',
                        templateUrl: '/App/Main/views/storeCategories/storeCategories.cshtml',
                        menu: 'store Suppliers', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreCategories', {
                        url: '/managecategories',
                        templateUrl: '/App/Main/views/storeCategories/addeditstoreCategories.cshtml',
                        menu: 'store Tags', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // storeBrands
                $stateProvider
                    .state('storeBrands', {
                        url: '/brands',
                        templateUrl: '/App/Main/views/storeBrands/storeBrands.cshtml',
                        menu: 'store brands', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreBrands', {
                        url: '/managebrands',
                        templateUrl: '/App/Main/views/storeBrands/addeditstoreBrands.cshtml',
                        menu: 'store brands', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });




                // storeProductVariants
                $stateProvider
                    .state('storeProductVariants', {
                        url: '/variant',
                        templateUrl: '/App/Main/views/storeProductVariants/storeProductVariants.cshtml',
                        menu: 'store variant', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreProductVariants', {
                        url: '/managevariant',
                        templateUrl: '/App/Main/views/storeProductVariants/addeditstoreProductVariants.cshtml',
                        menu: 'store variant', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });





                // storeProductVariantValues
                $stateProvider
                    .state('storeProductVariantValues', {
                        url: '/variantvalue',
                        templateUrl: '/App/Main/views/storeProductVariantValues/storeProductVariantValues.cshtml',
                        menu: 'store variant', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreProductVariantValues', {
                        url: '/managevariantvalue',
                        templateUrl: '/App/Main/views/storeProductVariantValues/addeditstoreProductVariantValues.cshtml',
                        menu: 'store variant', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });


                  // storeGiftcards
                $stateProvider
                    .state('storeGiftCards', {
                        url: '/storeGiftCards',
                        templateUrl: '/App/Main/views/storeGiftCards/storeGiftCards.cshtml',
                        menu: 'store giftcards', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });

                $stateProvider
                    .state('addeditstoreGiftCards', {
                        url: '/managegiftcards',
                        templateUrl: '/App/Main/views/storeGiftcards/addeditstoreGiftCards.cshtml',
                        menu: 'store giftcards', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // storeSeasons
                $stateProvider
                    .state('storeSeasons', {
                        url: '/Seasons',
                        templateUrl: '/App/Main/views/storeSeasons/storeSeasons.cshtml',
                        menu: 'store Seasons', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreSeasons', {
                        url: '/manageSeasons',
                        templateUrl: '/App/Main/views/storeSeasons/addeditstoreSeasons.cshtml',
                        menu: 'store Seasons', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // HOME  PAGE
                $stateProvider
                    .state('home', {
                        url: '/home',
                        templateUrl: '/App/Main/views/home/home.cshtml',
                        menu: 'Home', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });


                // Point of sale  PAGE
                $stateProvider
                    .state('pos', {
                        url: '/pos',
                        templateUrl: '/App/Main/views/pos/pos.cshtml',
                        menu: 'pos', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // CASH REGISTER
                $stateProvider
                    .state('cashregister', {
                        url: '/cashregister',
                        templateUrl: '/App/Main/views/cashregister/cashregister.cshtml',
                        menu: 'cashregister', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // Sale Transaction
                $stateProvider
                    .state('SaleTransactions', {
                        url: '/SaleTransactions',
                        templateUrl: '/App/Main/views/SaleTransactions/SaleTransactions.cshtml',
                        menu: 'CompleteSalesOrders', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });


                // Giftcards
                $stateProvider
                    .state('Giftcards', {
                        url: '/Giftcards',
                        templateUrl: '/App/Main/views/Giftcards/Giftcards.cshtml',
                        menu: 'Giftcards', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // HOME  PAGE
                $stateProvider
                    .state('comingsoon', {
                        url: '/comingsoon',
                        templateUrl: '/App/Main/views/comingsoon/comingsoon.cshtml',
                        menu: 'comingsoon', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });


                // HOME  PAGE
                $stateProvider
                    .state('Billing', {
                        url: '/Billing',
                        templateUrl: '/App/Main/views/Billing/Billing.cshtml',
                        menu: 'Billing', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // storeProducts
                $stateProvider
                    .state('storeProducts', {
                        url: '/storeProducts',
                        templateUrl: '/App/Main/views/storeProducts/storeProducts.cshtml',
                        menu: 'storeProducts', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreProducts', {
                        url: '/managestoreProducts',
                        templateUrl: '/App/Main/views/storeProducts/addeditstoreProducts.cshtml',
                        menu: 'storeProducts', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });






                // storeBugTrackers
                $stateProvider
                    .state('storeBugTrackers', {
                        url: '/storeBugTrackers',
                        templateUrl: '/App/Main/views/storeBugTrackers/storeBugTrackers.cshtml',
                        menu: 'storeBugTrackers', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreBugTrackers', {
                        url: '/managestoreBugTrackers',
                        templateUrl: '/App/Main/views/storeBugTrackers/addeditstoreBugTrackers.cshtml',
                        menu: 'storeBugTrackers', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });




                // storeInventory
                $stateProvider
                    .state('storeInventory', {
                        url: '/storeInventory',
                        templateUrl: '/App/Main/views/storeInventory/storeInventory.cshtml',
                        menu: 'storeInventory', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreInventory', {
                        url: '/managestoreInventory',
                        templateUrl: '/App/Main/views/storeInventory/addeditstoreInventory.cshtml',
                        menu: 'storeInventory', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });


                // storeInventoryPurchaseOrders
                $stateProvider
                    .state('storeInventoryPurchaseOrders', {
                        url: '/storeInventoryPurchaseOrders',
                        templateUrl: '/App/Main/views/storeInventoryPurchaseOrders/storeInventoryPurchaseOrders.cshtml',
                        menu: 'storeInventoryPurchaseOrders', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreInventoryPurchaseOrders', {
                        url: '/managestoreInventoryPurchaseOrders',
                        templateUrl: '/App/Main/views/storeInventoryPurchaseOrders/addeditstoreInventoryPurchaseOrders.cshtml',
                        menu: 'storeInventoryPurchaseOrders', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });


                // storeInventoryTransferOrders
                $stateProvider
                    .state('storeInventoryTransferOrders', {
                        url: '/storeInventoryTransferOrders',
                        templateUrl: '/App/Main/views/storeInventoryTransferOrders/storeInventoryTransferOrders.cshtml',
                        menu: 'storeInventoryTransferOrders', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreInventoryTransferOrders', {
                        url: '/managestoreInventoryTransferOrders',
                        templateUrl: '/App/Main/views/storeInventoryTransferOrders/addeditstoreInventoryTransferOrders.cshtml',
                        menu: 'storeInventoryTransferOrders', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });




                // storeTimeZone
                $stateProvider
                    .state('storeTimeZones', {
                        url: '/storeTimeZones',
                        templateUrl: '/App/Main/views/storeTimeZones/storeTimeZones.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreTimeZones', {
                        url: '/managestoreTimeZone',
                        templateUrl: '/App/Main/views/storeTimeZones/addeditstoreTimeZones.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // storeTimeZone
                $stateProvider
                    .state('storeWarehouses', {
                        url: '/storeWarehouses',
                        templateUrl: '/App/Main/views/storeWarehouses/storeWarehouses.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreWarehouses', {
                        url: '/managestoreWarehouses',
                        templateUrl: '/App/Main/views/storeWarehouses/addeditstoreWarehouses.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // storeOutlets
                $stateProvider
                    .state('storeOutlets', {
                        url: '/storeOutlets',
                        templateUrl: '/App/Main/views/storeOutlets/storeOutlets.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreOutlets', {
                        url: '/managestoreOutlets',
                        templateUrl: '/App/Main/views/storeOutlets/addeditstoreOutlets.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // storeRegisters
                $stateProvider
                    .state('storeRegisters', {
                        url: '/storeRegisters',
                        templateUrl: '/App/Main/views/storeRegisters/storeRegisters.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditstoreRegisters', {
                        url: '/managestoreRegisters',
                        templateUrl: '/App/Main/views/storeRegisters/addeditstoreRegisters.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });




                $urlRouterProvider.otherwise('/home');

            }

            if ($('#paneltype').val() === 'admin') {

                // HOME  PAGE
                $stateProvider
                    .state('home', {
                        url: '/home',
                        templateUrl: '/App/Main/Admin/home/home.cshtml',
                        menu: 'Home', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });




                // adminCountry
                $stateProvider
                    .state('adminCountry', {
                        url: '/adminCountry',
                        templateUrl: '/App/Main/Admin/adminCountry/adminCountry.cshtml',
                        menu: 'store Suppliers', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditadminCountry', {
                        url: '/manageadminCountry',
                        templateUrl: '/App/Main/Admin/adminCountry/addeditadminCountry.cshtml',
                        menu: 'store Tags', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // admincURRANCY
                $stateProvider
                    .state('adminCurrancy', {
                        url: '/adminCurrancy',
                        templateUrl: '/App/Main/Admin/adminCurrancy/adminCurrancy.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditadminCurrancy', {
                        url: '/manageadminCurrancy',
                        templateUrl: '/App/Main/Admin/adminCurrancy/addeditadminCurrancy.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });


                // adminPlanPrice
                $stateProvider
                    .state('adminPlanPrice', {
                        url: '/adminPlanPrice',
                        templateUrl: '/App/Main/Admin/adminPlanPrice/adminPlanPrice.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditadminPlanPrice', {
                        url: '/manageadminPlanPrice',
                        templateUrl: '/App/Main/Admin/adminPlanPrice/addeditadminPlanPrice.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // adminPlans
                $stateProvider
                    .state('adminPlans', {
                        url: '/adminPlans',
                        templateUrl: '/App/Main/Admin/adminPlans/adminPlans.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditadminPlans', {
                        url: '/manageadminPlans',
                        templateUrl: '/App/Main/Admin/adminPlans/addeditaadminPlans.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // adminPlanServices
                $stateProvider
                    .state('adminPlanServices', {
                        url: '/adminPlanServices',
                        templateUrl: '/App/Main/Admin/adminPlanServices/adminPlanServices.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditadminPlanServices', {
                        url: '/manageadminPlanServices',
                        templateUrl: '/App/Main/Admin/adminPlanServices/addeditadminPlanServices.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });


                // adminEmailTemplates
                $stateProvider
                    .state('adminEmailTemplates', {
                        url: '/adminEmailTemplates',
                        templateUrl: '/App/Main/Admin/adminEmailTemplates/adminEmailTemplates.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditadminEmailTemplates', {
                        url: '/manageadminEmailTemplates',
                        templateUrl: '/App/Main/Admin/adminEmailTemplates/addeditadminEmailTemplates.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // adminSMTPsettings
                $stateProvider
                    .state('adminSMTPsettings', {
                        url: '/adminSMTPsettings',
                        templateUrl: '/App/Main/Admin/adminSMTPsettings/adminSMTPsettings.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditadminSMTPsettings', {
                        url: '/manageadminSMTPsettings',
                        templateUrl: '/App/Main/Admin/adminSMTPsettings/addeditadminSMTPsettings.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // adminStoreScheduler
                $stateProvider
                    .state('adminStoreScheduler', {
                        url: '/adminStoreScheduler',
                        templateUrl: '/App/Main/Admin/adminStoreScheduler/adminStoreScheduler.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditadminStoreScheduler', {
                        url: '/manageadminStoreScheduler',
                        templateUrl: '/App/Main/Admin/adminStoreScheduler/addeditadminStoreScheduler.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });



                // adminUpdateAllDatabase
                $stateProvider
                    .state('adminUpdateAllDatabase', {
                        url: '/adminUpdateAllDatabase',
                        templateUrl: '/App/Main/Admin/adminUpdateAllDatabase/adminUpdateAllDatabase.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditadminUpdateAllDatabase', {
                        url: '/manageadminUpdateAllDatabase',
                        templateUrl: '/App/Main/Admin/adminUpdateAllDatabase/addeditadminUpdateAllDatabase.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });




                // adminBugTrackers
                $stateProvider
                    .state('adminBugTrackers', {
                        url: '/adminBugTrackers',
                        templateUrl: '/App/Main/Admin/adminBugTrackers/adminBugTrackers.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditadminBugTrackers', {
                        url: '/manageadminBugTrackers',
                        templateUrl: '/App/Main/Admin/adminBugTrackers/addeditadminBugTrackers.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });


                // adminStores
                $stateProvider
                    .state('adminStores', {
                        url: '/adminStores',
                        templateUrl: '/App/Main/Admin/adminStores/adminStores.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Tenants' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }

                    });


                $stateProvider
                    .state('addeditadminStores', {
                        url: '/manageadminStores',
                        templateUrl: '/App/Main/Admin/adminStores/addeditadminStores.cshtml',
                        menu: 'admin Currancy', //Matches to name of 'Home' menu in StoremeyNavigationProvider
                        params: { SearchDto: null }
                    });






                $urlRouterProvider.otherwise('/home');
            }



        }
    ]);


})();