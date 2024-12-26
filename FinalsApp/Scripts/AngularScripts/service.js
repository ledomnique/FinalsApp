app.service("FinalsAppService", function ($http) {

    this.loadUsersData = function () {
        return $http({
            method: "GET",
            url: "/Home/GetUsers"
        });
    };

    this.deleteUser = function () {
        return $http({
            method: "DELETE",
            url: "/Home/DeleteUser",
            params: { id: userID }
        });
    }

    this.loadBooksData = function () {
        return $http({
            method: "GET",
            url: "/Home/GetBooks"
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
            , firstName: 'c'
            , lastName: 'c'
            , email: 'c@icloud.com'
            , password: 'cne123'
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
