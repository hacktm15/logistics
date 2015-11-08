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
      queryString += (queryString != '' && params.EntityId != undefined) ? ' and ' : ''
      if (params.EntityId != undefined) {
        queryString += "EntityId eq guid'" + params.EntityId + "'"
      }
      if (queryString == '') {
        return $http.get("http://tools.ligaac.ro/oData/Item");
      } else {
        return $http.get("http://tools.ligaac.ro/oData/Item?$filter="+queryString);
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
