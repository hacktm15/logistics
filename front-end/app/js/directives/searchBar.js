
logisticsApp.directive('searchBar',
  function() {
    return {
      restrict: 'E',
      templateUrl: 'views/directives/searchBar.html',
      scope: {},
      controller: function SearchBarController($scope, $routeParams, $location) {

        $scope.searchContent = function() {
          $location.path('/items').search(angular.extend($routeParams, {Name: $scope.searchText}));
        };

      }
    };
  }
);
