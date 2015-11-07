logisticsApp.controller('ItemsController',
  function ItemsController($scope, itemService) {
    var items = [];
    $scope.items = items;
     itemService.all().then(
       function (response) {
         angular.copy(response.data.value, items);
       }
     );
    $scope.panelType = 'info';
  }
);
