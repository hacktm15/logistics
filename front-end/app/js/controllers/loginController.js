logisticsApp.controller('LoginController',
  function LoginController($scope, loginService, $location) {
    console.log('y');
    $scope.login = function() {
      console.log('x')
      console.log(loginService.login($scope.user, $scope.password));
    }
  }
);
