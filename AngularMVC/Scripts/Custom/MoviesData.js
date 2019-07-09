function getallData() {
    //debugger;
    $http.get('../api/Movies/')
        .success(function (data, status, headers, config) {
            $scope.ListMovie = data;
        })
        .error(function (data, status, headers, config) {
            $scope.message = 'Unexpected Error while loading data!!';
            $scope.result = "color-red";
            console.log($scope.message);
        });
};