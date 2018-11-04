(function (app) {
    'use strict';
    app.service('loginService', ['$http', '$q', 'authenticationService', 'authData',
        function ($http, $q, authenticationService, authData) {
            var userInfo;
            var deferred;

            this.login = function (userName, password) {
                deferred = $q.defer();
                // biến data truyền vào username và password
                // grant_type là thuộc tính để đăng nhập bằng password
                var data = "grant_type=password&username=" + userName + "&password=" + password;

                // gọi đến autoken cấu hình ở sevice
                $http.post('/oauth/token', data, {
                    headers:
                        { 'Content-Type': 'application/x-www-form-urlencoded' }
                }).success(function (response) {
                    // đăng nhập thành công sẽ trả về 1 accessToken với giá trị là reponse.access_token 
                    userInfo = {
                        accessToken: response.access_token,
                        userName: userName
                    };
                    // dùng authenticationdata gán vào IsAuthenticated và userName
                    authenticationService.setTokenInfo(userInfo);
                    authData.authenticationData.IsAuthenticated = true;
                    authData.authenticationData.userName = userName;
                    deferred.resolve(null);
                })
                    .error(function (err, status) {
                        authData.authenticationData.IsAuthenticated = false;
                        authData.authenticationData.userName = "";
                        deferred.resolve(err);
                    });
                return deferred.promise;
            }

            this.logOut = function () {
                authenticationService.removeToken();
                authData.authenticationData.IsAuthenticated = false;
                authData.authenticationData.userName = "";
            }
        }]);
})(angular.module('TTNH_UDAShop.common'));