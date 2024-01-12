using System.Web.Optimization;

namespace Storemey.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            //ACCOUNT BUNDLES
            bundles.Add(
                new StyleBundle("~/Bundles/account-vendor/css")
                    .Include("~/fonts/roboto/roboto.css", new CssRewriteUrlTransform())
                    .Include("~/fonts/material-icons/materialicons.css", new CssRewriteUrlTransform())
                    .Include("~/lib/bootstrap/dist/css/bootstrap.css", new CssRewriteUrlTransform())
                    .Include("~/lib/toastr/toastr.css", new CssRewriteUrlTransform())
                    .Include("~/lib/famfamfam-flags/dist/sprite/famfamfam-flags.css", new CssRewriteUrlTransform())
                    .Include("~/lib/font-awesome/css/font-awesome.css", new CssRewriteUrlTransform())
                    .Include("~/lib/Waves/dist/waves.css", new CssRewriteUrlTransform())
                    .Include("~/lib/animate.css/animate.css", new CssRewriteUrlTransform())
                    .Include("~/css/materialize.css", new CssRewriteUrlTransform())
                    .Include("~/css/style.css", new CssRewriteUrlTransform())
                    .Include("~/Views/Account/_Layout.css", new CssRewriteUrlTransform())
            );

            bundles.Add(
                new ScriptBundle("~/Bundles/account-vendor/js/bottom")
                    .Include(
                        "~/lib/json2/json2.js",
                        "~/lib/jquery/dist/jquery.js",
                        "~/lib/bootstrap/dist/js/bootstrap.js",
                        "~/lib/moment/min/moment-with-locales.js",
                        "~/lib/jquery-validation/dist/jquery.validate.js",
                        "~/lib/blockUI/jquery.blockUI.js",
                        "~/lib/toastr/toastr.js",
                        "~/lib/sweetalert/dist/sweetalert.min.js",
                        "~/lib/spin.js/spin.js",
                        "~/lib/spin.js/jquery.spin.js",
                        "~/lib/Waves/dist/waves.js",
                        "~/Abp/Framework/scripts/abp.js",
                        "~/Abp/Framework/scripts/libs/abp.jquery.js",
                        "~/Abp/Framework/scripts/libs/abp.toastr.js",
                        "~/Abp/Framework/scripts/libs/abp.blockUI.js",
                        "~/Abp/Framework/scripts/libs/abp.spin.js",
                        "~/Abp/Framework/scripts/libs/abp.sweet-alert.js",
                        "~/js/admin.js",
                        "~/js/main.js"
                    )
            );

            bundles.Add(
                          new ScriptBundle("~/swagger/ui/ext/1-js")
                              .Include(
                                  "~/swagger/ui/ext/1.js"
                              )
                      );


            //VENDOR RESOURCES

            //~/Bundles/App/vendor/css
            bundles.Add(
                new StyleBundle("~/Bundles/App/vendor/css")
                    .Include("~/fonts/roboto/roboto.css", new CssRewriteUrlTransform())
                    .Include("~/fonts/material-icons/materialicons.css", new CssRewriteUrlTransform())
                    .Include("~/lib/bootstrap/dist/css/bootstrap.css", new CssRewriteUrlTransform())
                    .Include("~/lib/toastr/toastr.css", new CssRewriteUrlTransform())
                    .Include("~/lib/famfamfam-flags/dist/sprite/famfamfam-flags.css", new CssRewriteUrlTransform())
                    .Include("~/lib/font-awesome/css/font-awesome.css", new CssRewriteUrlTransform())
                    .Include("~/lib/Waves/dist/waves.css", new CssRewriteUrlTransform())
                    .Include("~/lib/animate.css/animate.css", new CssRewriteUrlTransform())
                //.Include("~/css/materialize.css", new CssRewriteUrlTransform())
                //.Include("~/css/style.css", new CssRewriteUrlTransform())
                //.Include("~/css/themes/all-themes.css", new CssRewriteUrlTransform())
                );

            //~/Bundles/App/vendor/js
            bundles.Add(
                new ScriptBundle("~/Bundles/App/vendor/js")
                    .Include(

                        "~/lib/json2/json2.js",
                        "~/lib/jquery/dist/jquery.js",
                        "~/lib/bootstrap/dist/js/bootstrap.js",
                        "~/lib/moment/min/moment-with-locales.js",
                        "~/lib/jquery-validation/dist/jquery.validate.js",
                        "~/lib/blockUI/jquery.blockUI.js",
                        "~/lib/toastr/toastr.js",
                        "~/lib/sweetalert/dist/sweetalert.min.js",
                        "~/lib/spin.js/spin.js",
                        "~/lib/spin.js/jquery.spin.js",
                        "~/lib/bootstrap-select/dist/js/bootstrap-select.js",
                        "~/lib/jquery-slimscroll/jquery.slimscroll.js",
                        "~/lib/Waves/dist/waves.js",
                        "~/lib/push.js/push.js",
                        "~/lib/jquery.serializejson/jquery.serializejson.js",



                        "~/Scripts/angular.min.js",
                        "~/Scripts/angular-animate.min.js",
                        "~/Scripts/angular-sanitize.min.js",
                        "~/Scripts/angular-ui-router.min.js",
                        "~/Scripts/angular-ui/ui-bootstrap.min.js",
                        "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
                        "~/Scripts/angular-ui/ui-utils.min.js",

                        "~/Abp/Framework/scripts/abp.js",
                        "~/Abp/Framework/scripts/libs/abp.jquery.js",
                        "~/Abp/Framework/scripts/libs/abp.toastr.js",
                        "~/Abp/Framework/scripts/libs/abp.blockUI.js",
                        "~/Abp/Framework/scripts/libs/abp.spin.js",
                        "~/Abp/Framework/scripts/libs/abp.sweet-alert.js",
                        "~/Abp/Framework/scripts/libs/angularjs/abp.ng.js",


                        "~/lib/ui-cropper-master/ui-cropper-master/compile/unminified/ui-cropper.js",

                        
                        "~/lib/ui-select/ui-select-master/ui-select-master/dist/angular-sanitize.js",
                        "~/lib/ui-select/ui-select-master/ui-select-master/dist/select.js",
                        "~/lib/angular-confirm-master/dist/angular-confirm.min.js",
                        "~/lib/angular-validate-master/dist/angular-validate.min.js",
                        "~/lib/offline-master/offline-master/offline.min.js",
                        //"~/lib/offline-master/offline-master/js/snake.js",
                        //"~/lib/angular-loading-spinner-master/angular-loading-spinner-master/dist/loading-spinner.min.js",

                        "~/js/admin.js",
                        "~/Scripts/jquery.signalR-2.2.3.min.js",
                        "~/js/main.js"
                    )
                );

            //Home-Index Bundles
            bundles.Add(
                new ScriptBundle("~/Bundles/home-index")
                    .Include(
                        "~/lib/jquery-countTo/jquery.countTo.js",
                        "~/lib/raphael/raphael.js",
                        "~/lib/morris.js/morris.js",
                        "~/lib/chart.js/dist/Chart.bundle.js",
                        "~/lib/Flot/jquery.flot.js",
                        "~/lib/Flot/jquery.flot.resize.js",
                        "~/lib/Flot/jquery.flot.pie.js",
                        "~/lib/Flot/jquery.flot.categories.js",
                        "~/lib/Flot/jquery.flot.time.js",
                        "~/lib/jquery-sparkline/dist/jquery.sparkline.js"
                    )
            );

            //APPLICATION RESOURCES

            //~/Bundles/App/Main/css
            bundles.Add(
                new StyleBundle("~/Bundles/App/Main/css")
                    .IncludeDirectory("~/App/Main", "*.css", true)
                );

            //~/Bundles/App/Main/js
            bundles.Add(
                new ScriptBundle("~/Bundles/App/Main/js")
                    .IncludeDirectory("~/Common/Scripts", "*.js", true)
                    .IncludeDirectory("~/App/Main", "*.js", true)
                );

            //~/Bundles/App/Main/js
            bundles.Add(
                new ScriptBundle("~/Bundles/App/Main/js")
                    .IncludeDirectory("~/Common/Scripts", "*.js", true)
                    .IncludeDirectory("~/App/Main", "*.js", true)
                );

            //PAKET THEME RESOURCES

            //~/Bundles/App/Main/js
            bundles.Add(
                new ScriptBundle("~/Bundles/App/Main/js")
                    .IncludeDirectory("~/Common/Scripts", "*.js", true)
                    .IncludeDirectory("~/App/Main", "*.js", true)
                );

            #region STOREMEY BUNDLES 

            //HOME - STOREMEYTHEME - CSS
            bundles.Add(
                new StyleBundle("~/Homepage/Bundles")
                    .Include("~/Content/StoremeyTheme/ANGULARJS/bower_components/bootstrap/dist/css/bootstrap.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/StoremeyTheme/ANGULARJS/bower_components/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/StoremeyTheme/ANGULARJS/bower_components/themify-icons/css/themify-icons.css", new CssRewriteUrlTransform())
                    .Include("~/Content/StoremeyTheme/ANGULARJS/bower_components/flag-icon-css/css/flag-icon.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-loading-bar/build/loading-bar.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/StoremeyTheme/ANGULARJS/bower_components/animate.css/animate.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/StoremeyTheme/ANGULARJS/bower_components/perfect-scrollbar/css/perfect-scrollbar.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/StoremeyTheme/ANGULARJS/bower_components/bootstrap-daterangepicker/daterangepicker.css", new CssRewriteUrlTransform())
                    .Include("~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-ui-switch/angular-ui-switch.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/StoremeyTheme/ANGULARJS/bower_components/AngularJS-Toaster/toaster.css", new CssRewriteUrlTransform())
                    .Include("~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-aside/dist/css/angular-aside.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-notification-icons/dist/angular-notification-icons.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/StoremeyTheme/ANGULARJS/bower_components/v-accordion/dist/v-accordion.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/StoremeyTheme/ANGULARJS/bower_components/v-button/dist/v-button.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/StoremeyTheme/ANGULARJS/bower_components/sweetalert/dist/sweetalert.css", new CssRewriteUrlTransform())
                    .Include("~/Content/StoremeyTheme/ANGULARJS/bower_components/ladda/dist/ladda-themeless.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-awesome-slider/dist/css/angular-awesome-slider.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/StoremeyTheme/ANGULARJS/bower_components/slick-carousel/slick/slick.css", new CssRewriteUrlTransform())
                    .Include("~/Content/StoremeyTheme/ANGULARJS/bower_components/slick-carousel/slick/slick-theme.css", new CssRewriteUrlTransform())
                    .Include("~/assets/css/styles.css", new CssRewriteUrlTransform())
                    .Include("~/assets/css/plugins.css", new CssRewriteUrlTransform())

                );



            //HOME - STOREMEYTHEME - JS
            bundles.Add(
                new ScriptBundle("~/Bundles/StoremeythemeJS")
                    .Include(
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/jquery/dist/jquery.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/fastclick/lib/fastclick.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/modernizr/modernizr.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/moment/min/moment.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/perfect-scrollbar/js/min/perfect-scrollbar.jquery.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/bootstrap-daterangepicker/daterangepicker.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/sweetalert/dist/sweetalert.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/spin.js/spin.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/ladda/dist/ladda.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/slick-carousel/slick/slick.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular/angular.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-cookies/angular-cookies.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-animate/angular-animate.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-touch/angular-touch.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-sanitize/angular-sanitize.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-ui-router/release/angular-ui-router.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/ngstorage/ngStorage.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-translate/angular-translate.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-translate-loader-url/angular-translate-loader-url.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-translate-loader-static-files/angular-translate-loader-static-files.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-translate-storage-local/angular-translate-storage-local.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-translate-storage-cookie/angular-translate-storage-cookie.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/oclazyload/dist/ocLazyLoad.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-breadcrumb/dist/angular-breadcrumb.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-swipe/dist/angular-swipe.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-bootstrap/ui-bootstrap-tpls.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-loading-bar/build/loading-bar.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-scroll/angular-scroll.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-fullscreen/src/angular-fullscreen.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/ng-bs-daterangepicker/dist/ng-bs-daterangepicker.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-truncate/src/truncate.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-moment/angular-moment.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-ui-switch/angular-ui-switch.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-ladda/dist/angular-ladda.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/AngularJS-Toaster/toaster.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-aside/dist/js/angular-aside.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/v-accordion/dist/v-accordion.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/v-button/dist/v-button.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/ngSweetAlert/SweetAlert.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-notification-icons/dist/angular-notification-icons.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-awesome-slider/dist/angular-awesome-slider.min.js",
                        "~/Content/StoremeyTheme/ANGULARJS/bower_components/angular-slick-carousel/dist/angular-slick.min.js"
                    //"~/assets/js/app.js"
                    //"~/assets/js/main.js"

                    )
            );




            //LOGIN - STOREMEYTHEME - CSS
            bundles.Add(
                new StyleBundle("~/Bundles/Login")
                    .Include("~/Content/StoremeyLoginScreen/vendor/bootstrap/css/bootstrap.min.css")
                    .Include("~/Content/StoremeyLoginScreen/fonts/font-awesome-4.7.0/css/font-awesome.min.css")
                    .Include("~/Content/StoremeyLoginScreen/fonts/Linearicons-Free-v1.0.0/icon-font.min.css")
                    .Include("~/Content/StoremeyLoginScreen/vendor/animate/animate.css")
                    .Include("~/Content/StoremeyLoginScreen/vendor/css-hamburgers/hamburgers.min.css")
                    .Include("~/Content/StoremeyLoginScreen/vendor/animsition/css/animsition.min.css")
                    .Include("~/Content/StoremeyLoginScreen/vendor/select2/select2.min.css")
                    .Include("~/Content/StoremeyLoginScreen/vendor/daterangepicker/daterangepicker.css")
                    .Include("~/Content/StoremeyLoginScreen/css/util.css")
                    .Include("~/Content/StoremeyLoginScreen/css/main.css")
                    .Include("~/Content/StoremeyLoginScreen/css/style2.css")
                    .Include("~/fonts/roboto/roboto.css")
                    .Include("~/fonts/material-icons/materialicons.css")
                    .Include("~/lib/bootstrap/dist/css/bootstrap.css")
                    .Include("~/lib/toastr/toastr.css")
                    .Include("~/lib/famfamfam-flags/dist/sprite/famfamfam-flags.css")
                    .Include("~/lib/font-awesome/css/font-awesome.css")
                    .Include("~/lib/Waves/dist/waves.css")
                    .Include("~/lib/animate.css/animate.css")
                    .Include("~/css/materialize.css")
                    .Include("~/css/style.css")
                    .Include("~/Views/Account/_Layout.css")

                );



            //LOGIN - STOREMEYTHEME - JS
            bundles.Add(
                new ScriptBundle("~/Bundles/LoginJS")
                    .Include(
                        //"~/Content/StoremeyLoginScreen/vendor/jquery/jquery-3.2.1.min.js",
                        "~/Content/StoremeyLoginScreen/vendor/animsition/js/animsition.min.js",
                        "~/Content/StoremeyLoginScreen/vendor/bootstrap/js/popper.js",
                        "~/Content/StoremeyLoginScreen/vendor/bootstrap/js/bootstrap.min.js",
                        "~/Content/StoremeyLoginScreen/vendor/select2/select2.min.js",
                        "~/Content/StoremeyLoginScreen/vendor/daterangepicker/moment.min.js",
                        "~/Content/StoremeyLoginScreen/vendor/daterangepicker/daterangepicker.js",
                        "~/Content/StoremeyLoginScreen/vendor/countdowntime/countdowntime.js",
                        "~/Content/StoremeyLoginScreen/js/main.js",
                        "~/lib/jquery/dist/jquery.js",
                        "~/lib/jquery-validation/dist/jquery.validate.js",
                        "~/lib/json2/json2.js",
                        "~/lib/bootstrap/dist/js/bootstrap.js",
                        "~/lib/moment/min/moment-with-locales.js",
                        "~/lib/blockUI/jquery.blockUI.js",
                        "~/lib/toastr/toastr.js",
                        "~/lib/sweetalert/dist/sweetalert.min.js",
                        "~/lib/spin.js/spin.js",
                        "~/lib/spin.js/jquery.spin.js",
                        "~/lib/Waves/dist/waves.js",
                        "~/Abp/Framework/scripts/abp.js",
                        "~/Abp/Framework/scripts/libs/abp.jquery.js",
                        "~/Abp/Framework/scripts/libs/abp.toastr.js",
                        "~/Abp/Framework/scripts/libs/abp.blockUI.js",
                        "~/Abp/Framework/scripts/libs/abp.spin.js",
                        "~/Abp/Framework/scripts/libs/abp.sweet-alert.js",
                        "~/js/admin.js",
                        "~/js/main.js"
                    )
            );

            #endregion

            BundleTable.EnableOptimizations = false;
        }
    }
}