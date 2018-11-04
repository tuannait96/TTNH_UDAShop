(function (app) {
    'use strict';
    app.factory('authData', [function () {
        var authDataFactory = {};

        // khai báo mặc định là chưa đăng nhập và username rỗng
        var authentication = {
            IsAuthenticated: false,
            userName: ""
        };
        authDataFactory.authenticationData = authentication;

        return authDataFactory;
    }]);
})(angular.module('TTNH_UDAShop.common'));
// vid đây là sigle page cho nên lưu đăng nhập ở client