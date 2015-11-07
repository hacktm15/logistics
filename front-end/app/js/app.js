'use strict';

var logisticsApp = angular.module('logisticsApp', ['ngRoute']);

logisticsApp.controller('CollapseDemoCtrl', function ($scope) {
  $scope.isCollapsed = false;
});
