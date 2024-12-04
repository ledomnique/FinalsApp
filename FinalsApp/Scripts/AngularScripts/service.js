app.service("FinalsAppService", function ($http) {

    this.loadUsersData = function () {
        return $http.get("/Home/LoadUsersData");
    }
});