app.service("FinalsAppService", function ($http) {

    this.loadUsersData = function () {
        return $http({
            method: "GET",
            url: "/Home/GetUsers"
        });
    };

    this.loadAdminsData = function () {
        return $http({
            method: "GET",
            url: "/Home/GetAdmins"
        });
    };

    
});
