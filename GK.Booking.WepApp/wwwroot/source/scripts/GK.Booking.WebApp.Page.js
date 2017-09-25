angular.module('GK.Booking.WebApp.Page', [])
	.directive('gkBookingWebAppPage', function () {
		var definitionObject = {
			transclude: true,
			templateUrl: 'wwwroot/GK.Booking.WebApp.Page.html'
		};

		return definitionObject;
	});