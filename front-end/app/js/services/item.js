logisticsApp.factory('itemService', ['$http', function ($http) {
  return {
    all: function (params) {
      queryString = '';

      if (params.Name != undefined) {
        queryString += 'substringof(tolower(\''+params.Name+'\'), tolower(Name)) eq true'
      }
      queryString += (queryString != '' && params.LocationId != undefined) ? ' and ' : ''
      if (params.LocationId != undefined) {
        queryString += "LocationId eq guid'" + params.LocationId + "'"
      }
      queryString += (queryString != '' && params.CategoryId != undefined) ? ' and ' : ''
      if (params.CategoryId != undefined) {
        queryString += "Categories/any(cat: cat eq guid'" + params.CategoryId + "')"
      }
      if (queryString == '') {
        return $http.get("http://tools.ligaac.ro/oData/Item");
      } else {
        return $http.get("http://tools.ligaac.ro/oData/Item?$filter="+queryString);
      }
    }
  };
}]);
