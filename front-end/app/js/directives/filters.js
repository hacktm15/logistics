
logisticsApp.directive('filters',
  function() {
    return {
      restrict: 'E',
      templateUrl: 'views/directives/filters.html',
      scope: {},
      controller: function FiltersController($scope, locationService, categoriesService, $routeParams, $location) {
        var locations = [];
        var categories = [];
        $scope.categories = categories;
        categoriesService.all().then(
          function (response) {
            angular.copy(response.data.value, categories);
          }
        );
        $scope.locations = locations;
        locationService.all().then(
          function (response) {
            angular.copy(response.data.value, locations);
          }
        );

        $scope.submitLocation = function() {
          $location.path('/items').search(angular.extend($routeParams, {LocationId: $scope.locationId}));
        }

        $scope.submitCategory = function() {
          $location.path('/items').search(angular.extend($routeParams, {CategoryId: $scope.categoryId}));
        }

        $scope.resetFilters = function() {
          $location.path('/items').search({});
        }
      }
    };
  }
);
