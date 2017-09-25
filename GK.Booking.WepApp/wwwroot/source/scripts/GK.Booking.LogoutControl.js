angular.module('GK.Booking.LogoutControl', [])
	.directive('gkBookingLogoutControl', function () {
		var definitionObject = {
			templateUrl: 'wwwroot/GK.Booking.LogoutControl.html',
			scope: {
				onLogoutClick: '&onLogoutClick'
			},
			controller: ['$scope', function ($scope) {
			}
		]};
		return definitionObject;
	});