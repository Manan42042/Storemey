(function () {
    var controllerId = 'app.views.layout.sidebarNav';
    angular.module('app').controller(controllerId, [
        '$rootScope', '$state', 'appSession',
        function ($rootScope, $state, appSession) {
            var vm = this;

            if (vm.parentIndex === undefined) {
                vm.parentIndex = 0;
            }
            vm.Index = 20;
            vm.menuItems = [

                createMenuItem(1000, "Home", "", "Home", "javascript:void()", true, [
                    createMenuItem(2000, "Dashboard", "Pages.dashboard", "icon-dashboard", "home", false),
                ]),

                createMenuItem(1001, "POS and sale history", "", "", "javascript:void()", true, [
                    createMenuItem(2001, "Point of sale", "", "icon-pos", "pos", false),
                    createMenuItem(2002, "Cash register", "Pages.CashRegister", "icon-cash-register", "cashregister", false),
                    createMenuItem(2003, "Sale transactions", "", "icon-sale-history", "SaleTransactions", false),
                ]),


                createMenuItem(1002, "Customers", "", "", "javascript:void()", true, [
                    createMenuItem(2004, "Customers", "Pages.storeCustomers", "storeCustomers", "storeCustomers", false),
                    createMenuItem(2005, "Giftcards", "Pages.storeGiftCards", "storeGiftCards", "storeGiftCards", false),
                ]),

                createMenuItem(1000, "Products", "", "Products", "javascript:void()", true, [
                    createMenuItem(2006, "Products", "Pages.storeProducts", "storeProducts", "storeProducts", false),
                ]),

                createMenuItem(1003, "Product Types/Categories", "", "", "javascript:void()", true, [
                    createMenuItem(2007, "Products types", "", "icon-product", "javascript:void()", true, [
                        createMenuItem(101, "Tags", "Pages.storeTags", "storeTags", "storeTags", false),
                        createMenuItem(102, "Brands", "Pages.storeBrands", "storeBrands", "storeBrands", false),
                        createMenuItem(103, "Categories", "Pages.storeCategories", "storeCategories", "storeCategories", false),
                        createMenuItem(103, "Seasons", "Pages.storeSeasons", "storeSeasons", "storeSeasons", false),
                        createMenuItem(104, "Suppliers", "Pages.storeSuppliers", "storeSuppliers", "storeSuppliers", false),
                    ]),
                ]),

                createMenuItem(1004, "Inventory", "", "", "", true, [
                    createMenuItem(2008, "Store inventory", "", "icon-product", "javascript:void()", true, [
                        createMenuItem(105, "My inventory", "Pages.StoreInventory", "storeInventory", "storeInventory", false),
                        createMenuItem(106, "Inventory purchase", "Pages.StoreInventoryPurchaseOrders", "StoreInventoryPurchaseOrders", "storeInventoryPurchaseOrders", false),
                        createMenuItem(107, "Inventory transfer", "Pages.StoreInventoryTransferOrders", "StoreInventoryTransferOrders", "storeInventoryTransferOrders", false),
                    ]),
                ]),

                createMenuItem(1005, "Features", "", "", "", true, [
                    createMenuItem(2009, "Store setting", "", "icon-system-setup", "javascript:void()", true, [
                        createMenuItem(108, "Warehouses", "Pages.Warehouses", "storeWarehouses", "storeWarehouses", false),
                        createMenuItem(109, "Outlets", "Pages.Outlets", "storeOutlets", "storeOutlets", false),
                        createMenuItem(110, "Registers", "Pages.Registers", "storeRegisters", "storeRegisters", false),
                        createMenuItem(111, "Receipt templates", "Pages.storeReceiptTemplates", "storeReceiptTemplates", "storeReceiptTemplates", false),
                        createMenuItem(112, "Payment types", "Pages.storePaymentTypes", "storePaymentTypes", "storePaymentTypes", false),
                        createMenuItem(113, "Sale Tax", "Pages.storeTax", "storeTax", "storeTax", false),
                        createMenuItem(114, "Tax Group", "Pages.TaxGroup", "Tax Group", "storeTaxGroups", false),
                        createMenuItem(115, "Users", "Pages.storeUsers", "Users", "storeUsers", false),
                    ]),

                    createMenuItem(2010, "Masters", "", "icon-basic-setup", "javascript:void()", true, [
                        createMenuItem(116, "Countries", "Pages.storeCountryMaster", "storeCountryMaster", "storeCountryMaster", false),
                        createMenuItem(117, "States", "Pages.storeStateMaster", "storeStateMaster", "storeStateMaster", false),
                        createMenuItem(118, "Cities", "Pages.storeCityMaster", "storeCityMaster", "storeCityMaster", false),
                        createMenuItem(119, "Currancies", "Pages.storecurrencies", "storecurrencies", "storecurrencies", false),
                        createMenuItem(120, "Timezones", "Pages.storeTimeZones", "storeTimeZones", "storeTimeZones", false),
                    ]),

                ]),


                createMenuItem(1006, "My ecommerce", "", "", "javascript:void()", true, [
                    createMenuItem(2011, "Comming Soon", "Comming Soon", "icon-comming-soon", "comingsoon", false),
                    //createMenuItem(1003, "Ecommerce", "", "icon-ecommarce", "javascript:void()", true, [
                    //    createMenuItem(121, "Header Menu Management", "Pages.storeTags", "storeTags", "storeTags", false),
                    //    createMenuItem(122, "Footer Management", "Pages.storeBrands", "storeBrands", "storeBrands", false),
                    //    createMenuItem(123, "File Management", "Pages.storeCategories", "storeCategories", "storeCategories", false),
                    //    createMenuItem(124, "Email Template", "Pages.storeSuppliers", "storeSuppliers", "storeSuppliers", false),
                    //    createMenuItem(125, "Page Manager", "Pages.storeInventory", "storeInventory", "storeInventory", false),
                    //    createMenuItem(126, "seo management", "Pages.storeProducts", "storeProducts", "storeProducts", false),
                    //    createMenuItem(128, "Advertisement Management", "Pages.storeProducts", "storeProducts", "storeProducts", false),
                    //    createMenuItem(129, "Sale Orders", "Pages.storeProducts", "storeProducts", "storeProducts", false),
                    //    createMenuItem(130, "Drop Order", "Pages.storeProducts", "storeProducts", "storeProducts", false),
                    //]),
                ]),





                createMenuItem(1007, "Integration", "", "", "javascript:void()", true, [
                    createMenuItem(2012, "Comming Soon", "Comming Soon", "icon-comming-soon", "comingsoon", false),
                    //createMenuItem(1108, "Third party integration", "", "icon-third-party", "javascript:void()", true, [
                    //    createMenuItem(134, "XERO", "Pages.XERO", "XERO", "XERO", false),
                    //    createMenuItem(135, "SHOPIFY", "Pages.SHOPIFY", "SHOPIFY", "SHOPIFY", false),
                    //    createMenuItem(136, "QUICKBOOK", "Pages.QUICKBOOK", "QUICKBOOK", "QUICKBOOK", false),
                    //    createMenuItem(137, "BIG COMMERCE", "Pages.BigCOMMERCE", "BigCOMMERCE", "BigCOMMERCE", false),
                    //]),
                ]),


                createMenuItem(1008, "Storemey Billing", "", "", "javascript:void()", true, [
                    createMenuItem(2013, "Billing", "Billing", "icon-comming-soon", "Billing", false)
                ]),


                createMenuItem(1009, "Reports", "", "", "javascript:void()", true, [
                    createMenuItem(2014, "Advance reports", "", "icon-report", "javascript:void()", true, [

                        createMenuItem(138, "Sales", "Pages.Sale", "Sales", "Sales", false),
                        createMenuItem(139, "Customers", "Pages.Customer", "Customer", "Customer", false),
                        createMenuItem(140, "Inventory", "Pages.Inventory", "Inventory", "Inventory", false),
                        createMenuItem(141, "Register Closer", "Pages.RegisterCloser", "RegisterCloser", "RegisterCloser", false),
                        createMenuItem(142, "Tax", "Pages.Tax", "Tax", "Tax", false),
                        createMenuItem(143, "Products", "Pages.Product", "Product", "Product", false),
                    ]),

                ]),

                createMenuItem(1010, "Bug History", "", "", "javascript:void()", true, [
                    createMenuItem(2015, "Bug tracker", "Pages.storeGeneralSettings", "icon-bug-tracker", "storeBugTrackers", false),
                ]),
                createMenuItem(1011, "Settings", "", "", "javascript:void()", true, [
                    createMenuItem(2016, "General settings", "Pages.storeGeneralSettings", "icon-general-setting", "storeGeneralSettings", false),
                ]),
            ];


            vm.showMenuItem = function (menuItem) {
                if (menuItem.permissionName) {
                    return abp.auth.isGranted(menuItem.permissionName);
                }
                return true;
            }

            function createMenuItem(index, name, permissionName, icon, route, isTitle, childItems) {
                return {
                    index: index,
                    name: name,
                    permissionName: permissionName,
                    icon: icon,
                    route: route,
                    items: childItems,
                    isTitle: isTitle
                };
            }
            vm.ActiveCurrentMenu = function (index, route) {
                if (route !== 'javascript:void()') {
                    vm.Index = index;
                    $state.go(route);
                }
            }
        }
    ]);
})();