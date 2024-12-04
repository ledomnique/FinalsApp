app.controller("FinalsController", function ($scope, FinalsService) {

    $scope.loadGenres = function () {
        var getData = FinalsService.loadGenresData();
        getData.then(function (ReturnedData) {

        })
    }
});