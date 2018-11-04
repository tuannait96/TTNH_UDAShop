(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);
    // inject apiService vào để gọi ajax
    productCategoryAddController.$inject = ['apiService', '$scope', 'notificationService', '$state','commonService'];

    function productCategoryAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }
        // sự kiện khi bấm nút submit
        $scope.AddProductCategory = AddProductCategory;
        // lấy danh mục seo
        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        function AddProductCategory() {
            // create là tên của phương thức định nghĩa trong api 
            
              apiService.post('api/productcategory/create', $scope.productCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    // state điều hướng, là đối tượng của ui-route - go đến tên trong link địa chỉ muốn đến
                    $state.go('product_categories');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
        // load lên sellect thư mục cha trong view
        // phương thức lấy theo phương thức định nghĩa trong apiService
        function loadParentCategory() {
            apiService.get('api/productcategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadParentCategory();
    }

})(angular.module('TTNH_UDAShop.product_categories'));