
logisticsApp.directive('filters',
  function() {
    return {
      restrict: 'E',
      templateUrl: 'views/directives/filters.html',
      scope: {},
      controller: function FiltersController($scope, locationService, categoriesService) {
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
      }
    };
  }
);
