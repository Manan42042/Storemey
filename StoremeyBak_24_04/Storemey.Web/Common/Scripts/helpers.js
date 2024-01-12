var App = App || {};
(function () {

    var appLocalizationSource = abp.localization.getSource('Storemey');
    App.localize = function () {
        return appLocalizationSource.apply(this, arguments);
    };

})(App);


