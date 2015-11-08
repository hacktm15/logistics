logisticsApp.controller('LocationController',
  function LocationController($scope, locationService) {
    var locations = [];
    $scope.locations = locations;
     locationService.all().then(
       function (response) {
         angular.copy(response.data.value, locations);
       }
     );
  }
);
