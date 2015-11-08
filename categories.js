logisticsApp.factory('categoriesService', ['$http', function ($http) {
  return {
    all: function () {
      return $http.get("http://tools.ligaac.ro/oData/Category");
    }
  };
}]);
