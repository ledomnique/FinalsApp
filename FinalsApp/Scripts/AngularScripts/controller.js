app.controller("FinalsController", function ($scope, FinalsAppService) {
    // Initialize user object
    $scope.user = JSON.parse(sessionStorage.getItem("userCredentials")) || {};

    // Function to load user profile
    $scope.loadUserProfile = function () {
        const userId = sessionStorage.getItem("userID");
        console.log("Fetching user profile for ID:", userId); // Debug log

        if (userId) {
            FinalsAppService.getUserById(userId).then(
                function (response) {
                    console.log("User data received:", response.data); // Debug log
                    $scope.user = response.data;
                    sessionStorage.setItem("userCredentials", JSON.stringify(response.data));
                },
                function (error) {
                    console.error("Error fetching user profile:", error); // Debug log
                    $scope.user = {}; // Fallback to empty user
                }
            );
        } else {
            console.warn("No userID found in sessionStorage."); // Debug log
        }
    };

    // Call function on load
    $scope.loadUserProfile();

    $scope.loadUsers = function () {
        var getData = FinalsAppService.loadUsersData();
        getData.then(function (ReturnedData) {
            $scope.usersData = ReturnedData.data;
            $(document).ready(function () {
                $('#myTable').DataTable();
            });
        });
    };

    $scope.cancelFunc = function () {
        $scope.firstName = null;
        $scope.middleName = null;
        $scope.lastName = null;
        $scope.userEmail = null;
        $scope.userPassword = null;
        $scope.userAddress = null;
        $scope.userPhone = null;
    };

    $scope.cleanFunc = function () {
        $scope.firstName = null;
        $scope.middleName = null;
        $scope.lastName = null;
        $scope.userEmail = null;
        $scope.userPassword = null;
        $scope.userAddress = null;
        $scope.userPhone = null;
    };

    $scope.signupUser = function () {
        var newUser = {
            firstName: $scope.firstName,
            lastName: $scope.lastName,
            password: $scope.userPassword,
            email: $scope.userEmail
        };
        var response = FinalsAppService.SignupUser(newUser);
        response.then((result) => {
            console.log("Signup successful, userID:", result.data); // Debug log
            sessionStorage.setItem("userID", result.data);
            window.location.href = "/Home/LoginPage";
        });
    };

    $scope.loginUser = function () {
        var response = FinalsAppService.LoginUser($scope.userLoginEmail, $scope.userLoginPassword);
        response.then((result) => {
            if (result.data == 0) {
                alert("Incorrect email or password");
            } else {
                console.log("Login successful, userID:", result.data); // Debug log
                sessionStorage.setItem("userID", result.data);
                window.location.href = "/Home/Homepage";
            }
        });
    };

    // Logout function
    $scope.logoutUser = function () {
        sessionStorage.clear(); // Clear all session storage data
        window.location.href = "/Home/LoginPage"; // Redirect to login page
    };

    // Change password function
    $scope.changePassword = function () {
        if ($scope.newPassword !== $scope.confirmPassword) {
            alert("New password and confirm password do not match!");
            return;
        }

        const userId = sessionStorage.getItem("userID");
        if (!userId) {
            alert("User not logged in!");
            return;
        }

        const passwordData = {
            userId: userId,
            currentPassword: $scope.currentPassword,
            newPassword: $scope.newPassword
        };

        FinalsAppService.changePassword(passwordData).then(
            function (response) {
                if (response.data.success) {
                    alert("Password updated successfully!");
                    $scope.currentPassword = "";
                    $scope.newPassword = "";
                    $scope.confirmPassword = "";
                    document.getElementById("update-password-form").classList.add("hidden");
                } else {
                    alert(response.data.message || "Error updating password.");
                }
            },
            function (error) {
                console.error("Error changing password:", error);
                alert("An error occurred while updating the password.");
            }
        );
    };

});
