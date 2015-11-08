// logisticsApp.controller('WarningsController',
//   function WarningsController($scope) {
//     $scope.panelType = 'warning';
//   }
// )



logisticsApp.controller('WarningsController',
  function WarningsController($scope, warningService) {
    var warnings = [];
    console.log('xx')
    $scope.warnings = warnings;
     warningService.all().then(
       function (response) {
         angular.copy(response.data.value, warnings);
       }
     );
  }
);
