(function () {
    var controllerId = 'app.views.layout.sidebarNav1';
    angular.module('app').controller(controllerId, [
        '$rootScope', '$state', 'appSession',
        function ($rootScope, $state, appSession) {
            var vm = this;


            if (vm.parentIndex === undefined) {
                vm.parentIndex = 0;
            }

            vm.menuItems = [
                //createMenuItem(0, "Administration Department", "", "", "", true, [
                //    createMenuItem(1, "Master Countries", "Pages.mastercountries", "mastercountries", "mastercountries", false),
                //    createMenuItem(2, "Master Plans", "Pages.masterplans", "masterplans", "masterplans", false),
                //    createMenuItem(3, "Master Plan Services", "Pages.mastermlanservices", "masterplanservices", "masterplanservices", false),

                //]),
                createMenuItem(1000, "Home", "", "Home", "javascript:void()", true, [
                    createMenuItem(20, "Home", "Pages.home", "icon-dashboard", "Home", false)
                ]),

                createMenuItem(1001, "Admin Stores", "", "adminStores", "javascript:void()", true, [
                    createMenuItem(160, "Stores", "Pages.adminStores", "icon-dashboard", "adminStores", false)
                ]),

                createMenuItem(1002, "Admin Country", "", "AdminCountry", "javascript:void()", true, [
                    createMenuItem(30, "Countries", "Pages.AdminCountry", "icon-dashboard", "adminCountry", false)
                ]),

                createMenuItem(1003, "Admin Currancy", "", "AdminCurrancy", "javascript:void()", true, [
                    createMenuItem(40, "Currancies", "Pages.AdminCurrancy", "icon-dashboard", "adminCurrancy", false)
                ]),

                createMenuItem(1004, "Admin Plans", "", "AdminPlans", "javascript:void()", true, [
                    createMenuItem(50, "Plans", "Pages.AdminPlans", "icon-dashboard", "adminPlans", false),
                    createMenuItem(60, "Plan Services", "Pages.AdminPlanServices", "icon-dashboard", "adminPlanServices", false),
                    createMenuItem(70, "Plan Price", "Pages.AdminPlanPrice", "icon-dashboard", "adminPlanPrice", false)
                    
                ]),

            
                //createMenuItem(1004, "Admin Plan Services", "", "AdminPlanServices", "javascript:void()", true, [
                //]),

                //createMenuItem(1005, "Admin Plan Price", "", "AdminPlanPrice", "javascript:void()", true, [
                //]),

                createMenuItem(1006, "Admin Email Template", "", "adminEmailTemplates", "javascript:void()", true, [
                    createMenuItem(80, "Email Template", "Pages.adminEmailTemplates", "icon-dashboard", "adminEmailTemplates", false)
                ]),

                createMenuItem(1007, "Admin SMTPSetting", "", "adminSMTPsettings", "javascript:void()", true, [
                    createMenuItem(90, "SMTP Setting", "Pages.adminSMTPsettings", "icon-dashboard", "adminSMTPsettings", false)
                ]),

                createMenuItem(1008, "Admin Master Bug Tracker", "", "adminBugTrackers", "javascript:void()", true, [
                    createMenuItem(100, "Bug Tracker", "Pages.adminBugTrackers", "icon-dashboard", "adminBugTrackers", false)
                ]),


                createMenuItem(1009, "Admin Upcoming Expire", "", "AdminUpcomingExpire", "javascript:void()", true, [
                    createMenuItem(110, "Upcoming Expire", "Pages.AdminUpcomingExpire", "icon-dashboard", "adminUpcomingExpire", false)
                ]),


                createMenuItem(1010, "Admin Recently Renew", "", "AdminRecentlyRenew", "javascript:void()", true, [
                    createMenuItem(120, "Recently Renew", "Pages.AdminRecentlyRenew", "icon-dashboard", "adminRecentlyRenew", false)
                ]),


                createMenuItem(1011, "Admin Backup Shcedular", "", "adminStoreScheduler", "javascript:void()", true, [
                    createMenuItem(130, "Backup Shcedular", "Pages.adminStoreScheduler", "icon-dashboard", "adminStoreScheduler", false)
                ]),


                createMenuItem(1012, "Admin Update All DB", "", "adminUpdateAllDatabase", "javascript:void()", true, [
                    createMenuItem(140, "Update All DB", "Pages.adminUpdateAllDatabase", "icon-dashboard", "adminUpdateAllDatabase", false)
                ]),


                createMenuItem(1013, "Admin Reports", "", "AdminReports", "javascript:void()", true, [
                    createMenuItem(150, "Reports", "Pages.AdminReports", "icon-dashboard", "adminReports", false)
                ]),



                //createMenuItem(1001, "POS Managements", "", "", "", true, [

                //    createMenuItem(120, "POS", "", "icon-pos", "javascript:void()", false, [
                //    ]),


                //    createMenuItem(1002, "Sale", "", "icon-sale", "javascript:void()", true, [
                //        createMenuItem(121, "Sales Orders", "Pages.storeCountryMaste", "storeCountryMaste", "storeCountryMaste", false),
                //        createMenuItem(123, "Unfinished Orders", "Pages.storeStateMaste", "storeStateMaste", "storeStateMaste", false),
                //        createMenuItem(124, "Cash Register Open/Close", "Pages.storeOpenCloseRegister", "storeOpenCloseRegister", "storeOpenCloseRegister", false),
                //    ]),

                //    createMenuItem(1003, "Product", "", "icon-product", "javascript:void()", true, [
                //        createMenuItem(40, "Tags", "Pages.storeTags", "storeTags", "storeTags", false),
                //        createMenuItem(41, "Brands", "Pages.storeBrands", "storeBrands", "storeBrands", false),
                //        createMenuItem(42, "Categories", "Pages.storeCategories", "storeCategories", "storeCategories", false),
                //        createMenuItem(43, "Suppliers", "Pages.storeSuppliers", "storeSuppliers", "storeSuppliers", false),
                //        createMenuItem(44, "Inventory", "Pages.storeInventory", "storeInventory", "storeInventory", false),
                //        createMenuItem(45, "Products", "Pages.storeProducts", "storeProducts", "storeProducts", false),
                //    ]),

                //    createMenuItem(1004, "Customer", "", "icon-customer", "javascript:void()", true, [
                //        createMenuItem(60, "Customer Group", "Pages.storeCustomerGroup", "storeCustomerGroup", "storeCustomerGroup", false),
                //        createMenuItem(61, "Customers", "Pages.storeCustomers", "storeCustomers", "storeCustomers", false),
                //    ]),

                //    createMenuItem(1005, "System Setup", "", "icon-system-setup", "javascript:void()", true, [
                //        createMenuItem(81, "Sale Tax", "Pages.storeTax", "storeTax", "storeTax", false),
                //        createMenuItem(82, "Tax Group", "Pages.TaxGroup", "Tax Group", "storeTaxGroups", false),
                //        createMenuItem(83, "Payment Types", "Pages.storePaymentTypes", "storePaymentTypes", "storePaymentTypes", false),
                //        createMenuItem(84, "Outlet and Register", "Pages.storeOutletAndRegister", "storeOutletAndRegister", "storeOutletAndRegister", false),
                //        createMenuItem(85, "Receipt Templates", "Pages.storeReceiptTemplates", "storeReceiptTemplates", "storeReceiptTemplates", false),
                //        createMenuItem(86, "Users", "Pages.storeUsers", "Users", "storeUsers", false),
                //    ]),

                //    createMenuItem(1006, "Basic Confrigration", "", "icon-basic-setup", "javascript:void()", true, [
                //        createMenuItem(100, "Country Master", "Pages.storeCountryMaster", "storeCountryMaster", "storeCountryMaster", false),
                //        createMenuItem(101, "State Master", "Pages.storeStateMaster", "storeStateMaster", "storeStateMaster", false),
                //        createMenuItem(102, "City Master", "Pages.storeCityMaster", "storeCityMaster", "storeCityMaster", false),
                //        createMenuItem(103, "Store Currancy", "Pages.storecurrencies", "storecurrencies", "storecurrencies", false),
                //    ]),

                //]),
                //createMenuItem(0, "Ecommerce Management", "", "", "", true, [

                //]),
                //createMenuItem(0, "Sales Orders Transactions", "", "", "", true, [

                //]),
                //createMenuItem(0, "Bug Tracker", "", "", "", true, [

                //]),
                //createMenuItem(0, "Third Party Integration", "", "", "", true, [
                //    createMenuItem(1101, "Comming Soon", "Comming Soon", "icon-comming-soon", "Comming Soon", false),
                //]),
                //createMenuItem(0, "General Settings", "", "", "", true, [
                //    createMenuItem(141, "General Settings", "Pages.storeGeneralSettings", "icon-general-setting", "storeGeneralSettings", false),
                //]),
                //createMenuItem(0, "Advance Reports", "", "", "", true, [

                //]),
            ];
            vm.Index = 21;

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