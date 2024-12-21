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

    this.saveUser = function () {
        var url = "/Home/saveUser"

        var data = {
            UserID: 0
            , firstName: 'b'
            , lastName: 'b'
            , email: 'b'
            , password: 'asd123'
        };

        $http.post(url, data, 'contenttype').then(function (response) {

            if (response.data !== null) {
                alert("User saved successfully!");
            }
        },
            function (response) {
                alert("Error saving user.");
            });
    }

    
});
