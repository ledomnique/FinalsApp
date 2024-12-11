app.service("FinalsAppService", function ($http) {

    this.loadUsersData = function (ReturnedData) {
        return $http({
            method: "GET", // Use GET instead of POST
            url: "/Home/loadUsersData",
            data: ReturnedData
        });
    };

    
});