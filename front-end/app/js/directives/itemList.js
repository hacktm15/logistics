logisticsApp.directive('itemList',
  function() {
    return {
      restrict: 'E',
      templateUrl: 'views/directives/itemList.html',
      scope: {},
      controller: function ItemListController($scope) {
        $scope.panelType = $scope.$parent.panelType;
        $scope.items = $scope.$parent.items;
      }
    };
  }
);
