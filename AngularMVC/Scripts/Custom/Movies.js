var app = angular.module('movieModule', []);

// Defining angularjs Controller and injecting CustomersService
app.controller('movieCtrl', function ($scope, $http, MoviesService) {

    $scope.moviesData = null;
    // Fetching records from the factory created at the bottom of the script file
    MoviesService.GetAllRecords().then(function (d) {
        $scope.moviesData = d.data; // Success
    }, function () {
        alert('Unable to Get Movies Data !!!'); // Failed
    });

    $scope.total = function () {
        var total = 0;
        angular.forEach($scope.moviesData, function (item) {
            total++;
        });
        return total;
    };
    $scope.Movie = {
        ID: '',
        Name: '',
        Year: '',
        ProducerId: '',

        //arrays for storing CustomerNumber stuff
        ProducersActors: [],
    };
    console.log('Movie lalala' + JSON.stringify($scope.Movie));

    $scope.ProducersActors = {
        ID: 0,
        Name: '',
    }
});