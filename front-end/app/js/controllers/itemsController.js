logisticsApp.controller('ItemsController',
  function ItemsController($scope, itemService) {
    $scope.getItems = function() {
      var items = [];
      $scope.items = items;
      itemService.all().then(
        function (response) {
          angular.copy(response.data.value, items);
        }
      );
    }
    $scope.getItems();
    $scope.panelType = 'info';
  }
);
