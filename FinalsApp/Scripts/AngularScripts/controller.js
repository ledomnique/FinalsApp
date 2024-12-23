app.controller("FinalsController", function ($timeout, $scope, FinalsAppService) {

    //Initialize Admin Dashboard
    $scope.welcomeAdmin = function () {
        Swal.fire({
            title: 'Welcome, Admin!',
            icon: 'success',
            text: 'You can now access the Dashboard.',
            confirmButtonText: 'Continue',
            allowOutsideClick: false,
            allowEscapeKey: false
        })
    }

    //Login function that allows admin to access the dashboard (hardcoded credentials)
    $scope.credentials = {
        userEmail: "",
        userPassword: ""
    };

    $scope.login = function () {
        // Hardcoded admin credentials
        const adminEmail = "dominique.nilo@gmail.com";
        const adminPassword = "lewis320!";

        // Check if any fields are empty
        if (!$scope.credentials.userEmail || !$scope.credentials.userPassword) {
            Swal.fire({
                title: "Missing Fields",
                text: "Ensure all fields are filled correctly before proceeding!",
                icon: "warning",
            });
            return; // Stop execution if validation fails
        }

        // Check credentials
        if ($scope.credentials.userEmail === adminEmail && $scope.credentials.userPassword === adminPassword) {
            // Redirect to the admin dashboard
            window.location.href = "/Home/Dashboard";
        } else {
            // Show an error message
            $scope.loginError = "Invalid email or password.";
            Swal.fire({
                title: "Login Failed",
                text: "Invalid email or password. Please try again.",
                icon: "error",
            });
        }
    };


    // User Credentials
    function getUserCredentials() {
        const credentials = sessionStorage.getItem('userCredentials');
        return credentials ? JSON.parse(credentials) : [];
    }

    $scope.submitFunc = function () {
        var userCredentials = getUserCredentials(); // existing credentials

        // If forms are empty
        if (!$scope.firstName || !$scope.lastName || !$scope.userEmail || !$scope.userPassword) {
            Swal.fire({
                title: "Missing Field",
                text: "Ensure all fields are filled correctly before proceeding!",
                icon: "warning",
            });
            return; // Stop execution if validation fails
        }

        // Check if User Exists
        var userSearch = userCredentials.find(UFind => UFind.uEmail === $scope.userEmail);

        if (userSearch === undefined) {
            // User does not exist, proceed to register
            userCredentials.push({
                fName: $scope.firstName,
                mName: $scope.middleName,
                lName: $scope.lastName,
                uEmail: $scope.userEmail,
                uAddress: $scope.userAddress,
                uPhone: $scope.userPhone,
                uPassword: $scope.userPassword
            });

            // Save user credentials to session storage | register
            saveUserCredentials(userCredentials);

            Swal.fire({
                title: "Welcome to Inkling!",
                text: "Click OK to redirect to the Login Portal",
                icon: "success",
            }).then(() => {
                console.log("Redirecting to login page...");
                $scope.cleanFunc();
                $scope.$apply(function () {
                    window.location.href = "/Home/LoginPage"; // Redirect to login page
                });
            });

        } else {
            // User exists, show error alert
            Swal.fire({
                title: "A Familiar Name",
                text: "It seems this user is already registered. Try a different Email.",
                icon: "error",
            }).then(() => {
                $scope.cleanFunc();
            });
        }
    }

    /*
    $scope.loginFunc = function () {
        var userCredentials = getUserCredentials(); // get existing credentials

        // if forms are empty
        if (!$scope.userEmail || !$scope.userPassword) {
            Swal.fire({
                title: "Missing Field",
                text: "Ensure all fields are filled correctly before proceeding!",
                icon: "warning",
            });
            return; // Stop execution if validation fails
        }

        
        // Credentials check
        var userSearch = userCredentials.find(UFind => UFind.uEmail === $scope.userEmail && UFind.uPassword === $scope.userPassword);

        // Check if login credentials match
        if (userSearch) {
            Swal.fire({
                title: "Welcome!",
                text: "Click OK to enter Inkling.",
                icon: "success",
            }).then(() => {
                $scope.cleanFunc();
                $scope.$apply(function () {
                    window.location.href = "/Home/Homepage"; // success login redirect
                });
            });
        } else {
            Swal.fire({
                title: "Incorrect Credentials",
                text: "Check your details.",
                icon: "error",
            }).then(() => {
                $scope.cleanFunc(); // Clear the form after alert
            });
        }
    }
    */

    $scope.cancelFunc = function () {
        // Clear all form fields
        $scope.firstName = null;
        $scope.middleName = null;
        $scope.lastName = null;
        $scope.userEmail = null;
        $scope.userPassword = null;
        $scope.userAddress = null;
        $scope.userPhone = null;
    }

    $scope.cleanFunc = function () {
        // Clear all form fields
        $scope.firstName = null;
        $scope.middleName = null;
        $scope.lastName = null;
        $scope.userEmail = null;
        $scope.userPassword = null;
        $scope.userAddress = null;
        $scope.userPhone = null;
    }

    $scope.logData = function () {

    }

    $scope.loadUsersData = function () {
        FinalsAppService.loadUsersData().then(function (response) {
            console.log("Loaded Data: ", response.data); // Debugging
            $scope.userData = response.data;

            angular.element(document).ready(function () {
                dTable = $('#users_tbl')
                dTable.DataTable();
            });




            //let table = new DataTable('#users_tbl', {
            //    // config options...
            //});



             //If using a table (e.g., DataTables)
            $timeout(function () {
            $('#users_tbl').DataTable();
            }, 0);
        }, function (error) {
            console.error("Error loading user data: ", error);
        });
    }

    $scope.loadBooksData = function () {
        FinalsAppService.loadBooksData().then(function (response) {
            console.log("Loaded Data: ", response.data); // Debugging
            $scope.bookData = response.data;

            angular.element(document).ready(function () {
                dTable = $('#books_tbl')
                dTable.DataTable();
            });

            $timeout(function () {
                $('#users_tbl').DataTable();
            }, 0);

        }, function (error) {
            console.error("Error loading user data: ", error);
        });
    }

    $scope.loadAdminData = function () {
        FinalsAppService.loadAdminsData().then(function (response) {
            console.log("Loaded Data: ", response.data); // Debugging
            $scope.adminData = response.data;

            // If using a table (e.g., DataTables)
            //$timeout(function () {
            //    $('#users_tbl').DataTable();
            //}, 0);
        }, function (error) {
            console.error("Error loading user data: ", error);
        });
    }

    $scope.loadAdminData();

    $scope.loadUsersData();

    $scope.loadBooksData();

    $scope.deleteUser = function (userID, index) {
        if (confirm("Are you sure you want to delete this user?")) {
            FinalsAppService.deleteUser(userID).then(function (response) {
                if (response.data.success) {
                    // Remove the book from the scope (front-end)
                    $scope.userData.splice(index, 1);
                    console.log("User deleted successfully!");
                } else {
                    console.error("Failed to delete user.");
                }
            }, function (error) {
                console.error("Error deleting user: ", error);
            });
        }
    }

    $scope.saveUser = function () {

        FinalsAppService.saveUser();
    }

    function openChangePasswordPopup() {
        Swal.fire({
            title: 'Change Password',
            html: `
            <div>
                <label for="oldPassword">Old Password</label>
                <input id="oldPassword" type="password" class="swal2-input" placeholder="Enter old password">
            </div>
            <div>
                <label for="newPassword">New Password</label>
                <input id="newPassword" type="password" class="swal2-input" placeholder="Enter new password">
            </div>
        `,
            focusConfirm: false,
            showCancelButton: true,
            confirmButtonText: 'Submit',
            preConfirm: () => {
                const oldPassword = document.getElementById('oldPassword').value;
                const newPassword = document.getElementById('newPassword').value;

                if (!oldPassword || !newPassword) {
                    Swal.showValidationMessage('Please fill out both fields.');
                    return false;
                }

                return { oldPassword, newPassword };
            }
        }).then((result) => {
            if (result.isConfirmed) {
                const { oldPassword, newPassword } = result.value;

                // Send the passwords to your server for processing
                // Example using fetch:
                fetch('/Home/ChangePassword', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ oldPassword, newPassword })
                })
                    .then(response => {
                        if (response.ok) {
                            Swal.fire('Success', 'Your password has been changed.', 'success');
                        } else {
                            Swal.fire('Error', 'Failed to change password.', 'error');
                        }
                    })
                    .catch(() => {
                        Swal.fire('Error', 'An error occurred. Please try again.', 'error');
                    });
            }
        });
    }

});

