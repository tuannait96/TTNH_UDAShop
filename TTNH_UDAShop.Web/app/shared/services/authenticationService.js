(function (app) {
    'use strict';
    // $q là service giúp sử dụng parten promise
    // $http và $window là section service giúp lưu secton vào JSON
    app.service('authenticationService', ['$http', '$q', '$window',
        function ($http, $q, $window) {
            var tokenInfo;

            // tạo token gán vào tokenInfo
            this.setTokenInfo = function (data) {
                tokenInfo = data;
                $window.sessionStorage["TokenInfo"] = JSON.stringify(tokenInfo);
            }

            // lấy token 
            this.getTokenInfo = function () {
                return tokenInfo;
            }

            //xóa token
            this.removeToken = function () {
                tokenInfo = null;
                $window.sessionStorage["TokenInfo"] = null;
            }

            this.init = function () {
                if ($window.sessionStorage["TokenInfo"]) {
                    tokenInfo = JSON.parse($window.sessionStorage["TokenInfo"]);
                }
            }

            // gán token vào header
            this.setHeader = function () {
                delete $http.defaults.headers.common['X-Requested-With'];
                if ((tokenInfo != undefined) && (tokenInfo.accessToken != undefined) && (tokenInfo.accessToken != null) && (tokenInfo.accessToken != "")) {
                    $http.defaults.headers.common['Authorization'] = 'Bearer ' + tokenInfo.accessToken;
                    $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
                }
            }

            // kiểm tra đăng nhập chưa
            this.validateRequest = function () {
                var url = 'api/home/TestMethod';
                var deferred = $q.defer();
                // then là phương thuuwsc thuuuwcj hiện sau phương thức trước, tức là rẽ nhánh ra 2 function
                $http.get(url).then(function () {
                    // sau khi kiểm tra đăng nhập đúng
                    deferred.resolve(null);
                }, function (error) {
                    // kiểm tra đăng nhập sai trả về reject
                    deferred.reject(error);
                    });
                // parten promise giúp chúng ta đảm bảo các phương thức trước sau từ phương thức then ở trên
                return deferred.promise;
            }

            this.init();
        }
    ]);
})(angular.module('TTNH_UDAShop.common'));