app.service("FinalsAppService", function ($http) {
    this.loadUsersData = function (getData) {
        return $http({
            method: "POST",
            url: "/Home/LoadUsersData",
            data: getData
        });
    };

    this.SignupUser = function (newUser) {
        return $http({
            method: "POST",
            url: "/Home/SignupUser",
            data: newUser
        });
    };

    this.LoginUser = function (email, password) {
        return $http({
            method: "POST",
            url: "/Home/LoginUser",
            data: { email, password }
        });
    };

    this.getUserById = function (userId) {
        return $http({
            method: "GET",
            url: `/Home/GetUserById?userId=${userId}`,
        });
    };

    this.changePassword = function (passwordData) {
        return $http({
            method: "POST",
            url: "/Home/ChangePassword", 
            data: passwordData
        });
    };

    this.RequestBook = function (newBook) {
        return $http({
            method: "POST",
            url: "/Home/RequestBook",
            data: newBook
        });
    }
});
