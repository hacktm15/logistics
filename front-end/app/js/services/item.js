logisticsApp.factory('itemService', ['$http', function ($http) {
  return {
    all: function (params) {
      queryString = '';

      if (params.Name != undefined) {
        queryString += 'substringof(tolower(\''+params.Name+'\'), tolower(Name)) eq true'
      }
      if (params.Location != undefined) {

      }
      console.log(queryString);
      if (queryString == '') {
      return $http.get("http://tools.ligaac.ro/oData/Item");

      } else {
      return $http.get("http://tools.ligaac.ro/oData/Item?$filter="+queryString);

      }

    }
  };
}]);
