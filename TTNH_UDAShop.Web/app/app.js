/// <reference path="../assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('TTNH_UDAShop',
        ['TTNH_UDAShop.products',
         'TTNH_UDAShop.product_categories',

            'TTNH_UDAShop.common'])
        .config(config)
        // config phần đăng nhập gọi hàm ở dưới
        .config(configAuthentication);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('base', {
                url: '',
                templateUrl: '/app/shared/views/baseView.html',
                // các template lồng nhau. (quan hệ kế thừa )
                abstract: true
            }).state('login', {
                url: "/login",
                templateUrl: "/app/components/login/loginView.html",
                controller: "loginController"
            })
            .state('home', {
                url: "/admin",
                parent: 'base',
                templateUrl: "/app/components/home/homeView.html",
                controller: "homeController"
            });
        $urlRouterProvider.otherwise('/login');
    }

    // hàm config đăng nhập được gọi vào config 
    function configAuthentication($httpProvider) {
        // push vào interceptors - tức là việc quản trị sự tương tác giữa client và server
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {

                    return config;
                },
                requestError: function (rejection) {

                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401") {
                        $location.path('/login');
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {

                    if (rejection.status == "401") {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }
})();