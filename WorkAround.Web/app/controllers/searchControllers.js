var searchControllers = angular.module('searchControllers', []);

searchControllers.controller('searchResultsController', ['$scope', 'Positions', function ($scope, Positions) {
    $scope.items = [];

    $scope.search = function () {
        var items = [];

        window.history.pushState({}, 'Search', '/?k=' + $scope.keywords + "&l=" + $scope.location);
        $scope.busy = true;
        Positions.search($scope.keywords, $scope.location).query(function (data) {
            $scope.busy = false;
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