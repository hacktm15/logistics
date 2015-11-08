logisticsApp.controller('categoryController',
  function LocationController($scope, locationService) {
    var categories = [];
    $scope.categories = categories;
     locationService.all().then(
       function (response) {
         angular.copy(response.data.value, categories);
       }
     );
  }
);
