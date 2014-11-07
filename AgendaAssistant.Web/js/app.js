﻿var app = angular.module('app', ['ngRoute', 'ngResource', 'ngAnimate', 'uiSlider', 'mgcrea.ngStrap', 'angular.filter', 'angular-appinsights']);

app.config(function($datepickerProvider) {
    angular.extend($datepickerProvider.defaults, {
        dateFormat: 'dd-MMM-yyyy',
        useNative: true,
        autoClose: true,
        startWeek: 1
    });
});

// Configure routes
app.config(['$routeProvider', '$locationProvider', 'insightsProvider', function ($routeProvider, $locationProvider, insightsProvider) {
    //$locationProvider.html5Mode(true);

    $routeProvider.
        when('/', { templateUrl: 'views/home.html', controller: 'HomeCtrl' }).
        when('/newevent', { templateUrl: 'views/newevent.html', controller: 'NewEventCtrl' }).
        when('/event/:id', { templateUrl: 'views/event.html', controller: 'EventCtrl' }).
        when('/confirm/:id', { templateUrl: 'views/confirm.html', controller: 'ConfirmCtrl' }).
        when('/availability/:eventid/:personid', { templateUrl: 'views/availability.html', controller: 'AvailabilityCtrl' }).
        when('/participant/:eventid/:personid', { templateUrl: 'views/participant.html', controller: 'ParticipantCtrl' }).
        otherwise({ redirectTo: '/' });
    
    // Add application insights id here
    insightsProvider.start('5845b2871415a79a1ee36e1f6326b3d7aefbe63c678950a');
}]);
