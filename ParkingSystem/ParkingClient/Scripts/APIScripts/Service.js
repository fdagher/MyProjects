app.service("APIService", function ($http)
{
    this.getUsers = function () {
        return $http.get("http://localhost:60261/api/users")
    }

    this.saveUser = function (user) {
        return $http(
        {
            method: 'post',
            data: user,
            url: 'http://localhost:60261/api/users'
        });
    }

    this.updateUser = function (user) {
        return $http(
        {
            method: 'put',
            data: user,
            url: 'http://localhost:60261/api/users/' + user.Id
        });
    }

    this.deleteUser = function (userId) {
        var url = 'http://localhost:60261/api/users/' + userId;
        return $http(
        {
            method: 'delete',
            data: userId,
            url: url
        });
    }
});

