(function (app) {
    // dùng cơ chế inject tuwh động từ service injector không để luôn $state ở trên hàm 
    // như vậy sẽ lỗi liên kết vòng vì mọi tham số đều dùng HTTP
    app.controller('loginController', ['$scope', 'loginService', '$injector', 'notificationService',
        function ($scope, loginService, $injector, notificationService) {

            $scope.loginData = {
                userName: "",
                password: ""
            };

            $scope.loginSubmit = function () {
                // sử dụng loginservice bên service 
                loginService.login($scope.loginData.userName, $scope.loginData.password).then(function (response) {
                    if (response != null && response.error != undefined) {
                        // đăng nhập lỗi thì sẽ báo lỗi
                        notificationService.displayError("Đăng nhập không đúng.");
                    }
                    else {
                        // dùng injector để tiêm vào và truyền ra 1 đối tượng 
                        // inject vào là lấy được mọi thứ không cần truyền tham số trên hàm. là 1 cơ chế của angular
                        var stateService = $injector.get('$state');
                        // tự động go về home khi đăng nhập thành công
                        stateService.go('home');
                    }
                });
            }
        }]);
})(angular.module('TTNH_UDAShop'));