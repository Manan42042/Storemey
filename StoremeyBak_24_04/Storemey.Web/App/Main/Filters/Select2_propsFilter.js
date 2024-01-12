(function () {

    //var i = 1;
    angular.module('app').filter('propsFilter', function () {
        return function (items, props) {


            if (props.countryCode !== undefined && props.countryCode === "") {
                return items;
            }
            if (props.stateName !== undefined && props.stateName === "") {
                return items;
            }
            if (props.cityName !== undefined && props.cityName === "") {
                return items;
            }
            if (props.currency !== undefined && props.currency === "") {
                return items;
            }
            if (props.name !== undefined && props.name === "") {
                return items;
            }
            //debugger;

            //$('.ng-confirm-title').html(i);
            //i = i + 1;

            var out = [];
            if (angular.isArray(items)) {
                var keys = Object.keys(props);

                items.forEach(function (item) {
                    var itemMatches = false;
                    for (var i = 0; i < keys.length; i++) {
                        var prop = keys[i];
                        var text = props[prop].toLowerCase();
                        if (item[prop] !== undefined && item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                            itemMatches = true;
                            break;
                        }
                    }

                    if (itemMatches) {
                        out.push(item);
                    }
                });
            } else {
                // Let the output be the input untouched
                out = items;
            }

            return out;
        };
    });

})();