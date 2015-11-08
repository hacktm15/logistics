logisticsApp.controller('qrController',
  function qrController($scope, $location) {
  	
  	$scope.submit = function() {
  		$location.path('/items').search({EntityId: $scope.scannedQr});
  	}

    // var items = [];
    // $scope.items = items;
    //  itemService.all().then(
    //    function (response) {
    //      angular.copy(response.data.value, items);
    //    }
    //  );
    // $scope.panelType = 'info';


  }
);
