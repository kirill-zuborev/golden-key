angular.module('GK.Booking.LockScreen', [])
	.directive('gkBookingLockScreen', function () {
		var definitionObject = {
			templateUrl: 'wwwroot/GK.Booking.LockScreen.html',
			scope: {
				isShown: '=isShown',
				lockText: '@lockText'
			}
		};
		return definitionObject;
	});