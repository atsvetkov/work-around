var searchControllers = angular.module('searchControllers', []);

searchControllers.controller('searchResultsController', ['$scope', 'Positions', function ($scope, Positions) {
    $scope.items = [];

    $scope.search = function () {
        var items = [];

        window.history.replaceState({}, 'Search', '/?k=' + $scope.keywords + "&l=" + $scope.location);
        Positions.search($scope.keywords, $scope.location).query(function (data) {
            angular.forEach(data, function (item) {
                $scope.items.push(item);
            });
        });

        $scope.items = items;
    };

    $scope.init = function (keywords, location) {
        $scope.keywords = keywords;
        $scope.location = location;

        if (keywords && location) {
            $scope.search();
        }
    };
}
]);