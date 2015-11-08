logisticsApp.controller('categoryController',
  function categoryController($scope, categoriesService) {
    var categories = [];
    $scope.categories = categories;
      categoriesService.all().then(
       function (response) {
         angular.copy(response.data.value, categories);
       }
     );
  }
);
