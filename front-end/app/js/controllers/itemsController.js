logisticsApp.controller('ItemsController',
  function ItemsController($scope, $http) {

    function fetch() {
      $http.get("http://tools.ligaac.ro/oData/Item")
        .success(function (response) {
          $scope.jsonResponse = response;
          console.log($scope);
        });
    }

    fetch();
    
  }
)
