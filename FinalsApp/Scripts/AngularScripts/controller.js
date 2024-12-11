app.controller("FinalsController", function ($scope, FinalsAppService) {

    // User Credentials
    function getUserCredentials() {
        const credentials = sessionStorage.getItem('userCredentials');
        return credentials ? JSON.parse(credentials) : [];
    }

    // Save user credentials to session storage
    function saveUserCredentials(userCredentials) {
        sessionStorage.setItem('userCredentials', JSON.stringify(userCredentials));
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

    // Load User Data in Admin Dashboard



  /*
    getData.then(function (ReturnedData) {
        $scope.usersData = ReturnedData.data;
           setTimeout(() => {
               if ($.fn.DataTable.isDataTable('#users_tbl')) {
                   $('#users_tbl').DataTable().destroy();
               }
               $('#users_tbl').DataTable();
           }, 0);
        }, function (error) {
            console.error("Error loading user data:", error);
        });
    

   

    var DataTable = require('datatables.net');
    require('datatables.net-responsive');

    $(document).ready(function () {
        $('#users_tbl').DataTable();
    });
*/
  
    $scope.loadUsers = function () {
        var getData = FinalsAppService.loadUsersData();
        getData.then(function (ReturnedData) {
            console.log("Loaded Data: ", ReturnedData.data); // Debugging
            $scope.usersData = ReturnedData.data;

            // Reinitialize DataTable
            $timeout(function () {
                if ($.fn.DataTable.isDataTable('#users_tbl')) {
                    $('#users_tbl').DataTable().destroy();
                }
                $('#users_tbl').DataTable();
            }, 0);

        }).catch(function (error) {
            console.error("Error loading users:", error);
        });
    };

    // Call the function to load users
    $scope.loadUsers();

    
});

