logisticsApp.factory('locationService', ['$http', function ($http) {
  return {
    all: function () {
      return $http.get("http://tools.ligaac.ro/oData/Location");
    }
  };
}]);
