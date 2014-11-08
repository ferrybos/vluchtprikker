﻿angular.module('app').controller('AvailabilityCtrl', function ($scope, $rootScope, $log, $filter, $routeParams, Constants, availabilityService) {
    $scope.constants = Constants;
    $scope.event = null;
    $scope.activeFlightTabIndex = 0;
    $scope.eventUrl = "";
    $rootScope.infoMessage = "";
    $rootScope.errorMessage = "";

    getData();
    
    function getData() {
        availabilityService.get($routeParams.participantid)
            .success(function(data) {
                $log.log("event = " + JSON.stringify(data));
                $scope.event = data;
                $scope.eventUrl = "#/event/" + $scope.event.id;
            })
            .error(function(error) {
                $rootScope.errorMessage = error.message + " " + error.exceptionMessage;
                $scope.event = null;
            });
    };
    
    $scope.SelectFlightTab = function (value) {
        $scope.activeFlightTabIndex = value;
    };

    //$scope.UpdateAvailability = function(availability) {
    //    $log.log("UpdateAvailability: Value=" + availability.value + ", CommentText=" + availability.commentText);
    //};
});

angular.module('app').controller('AvailabilityItemCtrl', function ($scope, $rootScope, $log, $timeout, $filter, $routeParams, Constants, availabilityService) {
    var timeout = null;
    
    var saveUpdates = function () {
        availabilityService.update($scope.flight.availabilities[0])
            .success(function(data) {
                $rootScope.infoMessage = "Gegevens zijn opgeslagen";
                $timeout(function () {
                    $rootScope.infoMessage = "";
                }, 3000);
            })
            .error(function(error) {
                $rootScope.errorMessage = error.message + " " + error.exceptionMessage;
            });
    };
    
    var debounceUpdate = function (newVal, oldVal) {
        if (newVal != oldVal) {
            if (timeout) {
                $timeout.cancel(timeout);
            }
            timeout = $timeout(saveUpdates, 500);
        }
    };

    $scope.toggleComment = function(flight) {
        flight.isCommentExpanded = !flight.isCommentExpanded;
    };

    $scope.$watch('flight.availabilities[0].value', debounceUpdate);
    $scope.$watch('flight.availabilities[0].commentText', debounceUpdate);
});