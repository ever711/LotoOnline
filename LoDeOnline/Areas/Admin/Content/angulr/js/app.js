'use strict';


angular.module('app', [
    'ngAnimate',
    'ngCookies',
    'ngResource',
    'ngSanitize',
    'ngTouch',
    'ngStorage',
    'ui.router',
    'ui.bootstrap',
    'ui.utils',
    'ui.load',
    'ui.jq',
    'oc.lazyLoad',
    'pascalprecht.translate',
    'kendo.directives',
    'toastr',
    'blockUI',
    'angular.filter'
]);

angular.module('app')
    .config(function ($httpProvider) {
        $httpProvider.interceptors.push('globalInterceptor');
    })
    .factory('globalInterceptor', function ($q, $injector) {
        return {
            // optional method
            'request': function (config) {
                return config;
            },

            // optional method
            'requestError': function (rejection) {
                return $q.reject(rejection);
            },

            // optional method
            'response': function (response) {
                // do something on success
                return response;
            },

            // optional method
            'responseError': function (rejection) {
                var toastr = $injector.get('toastr');
                if (rejection.data && rejection.data.error) {
                    toastr.error(rejection.data.error.message);
                } else {
                    toastr.error("Error");
                }
             
                return $q.reject(rejection);
            }
        };
    });
   