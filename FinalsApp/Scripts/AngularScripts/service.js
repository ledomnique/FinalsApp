app.service("FinalsService", function ($http) {

    this.loadUsersData = function () {
        return $http.get("/Home/LoadUsersData");
    }
});