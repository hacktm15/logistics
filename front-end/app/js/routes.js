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
      when('/home', {
        templateUrl: 'views/home.html'
      }).
      otherwise({
        redirectTo: '/login'
      });
  }
]);
