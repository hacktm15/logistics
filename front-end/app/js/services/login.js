logisticsApp.factory('loginService', ['$http', function ($http) {
  return {
    login: function (user, pass) {
      return $http({ url: 'http://tools.ligaac.ro/api/Auth/GetToken',
        method: "POST",
        data: JSON.stringify({"User": user,
          "Password": pass
        }),
        headers: {'Content-Type': 'application/json'}})

    }
  };
}]);
