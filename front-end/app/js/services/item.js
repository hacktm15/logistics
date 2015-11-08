logisticsApp.factory('itemService', ['$http', function ($http) {
  return {
    all: function (params) {
      queryString = '';
      queryString += 'substringof(tolower(\''+params.Name+'\'), tolower(Name)) eq true'
      console.log(queryString);

      return $http.get("http://tools.ligaac.ro/oData/Item?$filter="+queryString);
    }
  };
}]);
