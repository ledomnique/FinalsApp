app.service("FinalsAppService", function ($http) {

    this.loadUsersData = function (getData) {
        return $http({
            method: "POST",
            url: "/Home/LoadUsersData",
            data: getData
        });
    }
});