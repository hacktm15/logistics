logisticsApp.config(['$routeProvider',
  function($routeProvider) {
    $routeProvider.
      when('/login', {
        templateUrl: 'views/login.html',
        controller: 'LoginController'
      }).
      when('/items', {
        templateUrl: 'views/items.html',
        controller: 'ItemsController'
      }).
      when('/warnings', {
        templateUrl: 'views/warnings.html',
        controller: 'WarningsController'
      }).
      otherwise({
        redirectTo: '/login'
      });
  }
]);
