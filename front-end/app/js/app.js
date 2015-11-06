'use strict';

var logisticsApp = angular.module('logisticsApp', ['ngRoute']);

logisticsApp
    .controller('ItemsController',
      function ItemsController($scope) {

      }
    )

    .controller('LoginController',
      function LoginController($scope) {
      }
    )

    .controller('WarningsController',
      function WarningsController($scope) {
      }
    )

    .directive('searchBar',
      function() {
        return {
          restrict: 'E',
          templateUrl: 'partials/directives/searchBar.html',
          scope: {},
          controller: function SearchBarController($scope) {
          }
        };
      }
    )

    .config(['$routeProvider',
      function($routeProvider) {
        $routeProvider.
          when('/login', {
            templateUrl: 'partials/login.html',
            controller: 'LoginController'
          }).
          when('/items', {
            templateUrl: 'partials/items.html',
            controller: 'ItemsController'
          }).
          when('/warnings', {
            templateUrl: 'partials/warnings.html',
            controller: 'WarningsController'
          }).
          otherwise({
            redirectTo: '/login'
          });
  }]);
