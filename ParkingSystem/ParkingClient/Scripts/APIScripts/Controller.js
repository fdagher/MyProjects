app.controller('APIController', function ($scope, APIService) {
    getAll();

    function getAll() {
        var servCall = APIService.getUsers();
        servCall.then(function (d) {
            $scope.users = d.data;
        }, function (error) {
            $log.error('Oops! Something went wrong while fetching the data.')
        })
    }

    $scope.saveUser = function () {
        var user = {
            Date: $scope.dateid,
            Name: $scope.nameid,
            Email: $scope.emailid,
            Mobile: $scope.mobileid,
            ParkingToday: $scope.parkingtodayid,
            ParkingSlot: $scope.pslotid
        };

        alert(user);

        var saveUser = APIService.saveUser(user);
        saveUser.then(function (d) {
            getAll();
        }, function (error) {
            alert(error);
            console.log('Oops! Something went wrong while saving the data.')
        })
    };

    $scope.makeEditable = function (obj) {
        obj.target.setAttribute("contenteditable", true);
        obj.target.focus();
    };

    $scope.updateUser = function (user, eve, index) {
        alert(user.Id);
        switch (index)
        {
            case 1:
                user.Name = eve.currentTarget.innerText;
                break;
            case 2:
                user.Email = eve.currentTarget.innerText;
                break;
            case 3:
                user.Mobile = eve.currentTarget.innerText;
                break;
            case 4:
                user.ParkingToday = eve.currentTarget.innerText;
                break;
            case 5:
                user.ParkingSlot = eve.currentTarget.innerText;
                break;
        }
        
        alert(user.Email);

        var upd = APIService.updateUser(user);

        upd.then(function (d) {
            getAll();
        }, function (error) {
            console.log(error)
        })
    };

    $scope.deleteUser = function (userId) {
        var dlt = APIService.deleteUser(userId);
        dlt.then(function (d) {
            getAll();
        }, function (error) {
            
            console.log('Oops! Something went wrong while deleting the data.')
        })
    };
})