angular.module('GK.Booking.WebApp.Nav', ['GK.Booking.Services.RedirectService'])
	.directive('gkBookingWebAppNav', function () {
		var definitionObject = {
			templateUrl: 'wwwroot/GK.Booking.WebApp.Nav.html',
			link: function () {
				$(document).foundation('topbar', 'reflow');
			},
			controller: ['$scope', 'RedirectService',
				function ($scope, RedirectService) {
					$scope.toBookingPage = function () {
						RedirectService.toBookingPage();
					};

					$scope.toFeedbackPage = function () {
						RedirectService.toFeedbackPage();
					};
				}]
		};

		return definitionObject;
	});