var searchControllers = angular.module('searchControllers', []);

searchControllers.controller('searchResultsController', ['$scope', 'Positions', function ($scope, Positions) {
    $scope.items = [];

    $scope.search = function () {
        var items = [];
        Positions.search($scope.keywords, $scope.location).query(function (data) {
            angular.forEach(data.Items, function (item) {
                $scope.items.push(item);
            });
        });

        $scope.items = items;
    };

    $scope.init = function (keywords, location) {
        $scope.keywords = keywords;
        $scope.location = location;
        $scope.search();
    };
}
]);