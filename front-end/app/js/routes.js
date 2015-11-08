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
      when('/locations', {
        templateUrl: 'views/locations.html',
        controller: 'LocationController'
      }).
      when('/category', {
        templateUrl: 'views/categories.html',
        controller: 'CategoryController'
      }).
      when('/home', {
        templateUrl: 'views/home.html'
      }).
      when('/qr', {
        templateUrl: 'views/qr.html',
        controller: 'qrController'
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
