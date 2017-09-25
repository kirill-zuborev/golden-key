(function () {
	'use strict';

	angular
		.module('GK.Booking.Services.RedirectService', [])
		.factory('RedirectService', RedirectService);

	RedirectService.$inject = ['$window', '$location'];

	function RedirectService($window, $location) {
		var service = {
			toLoginPage: toLoginPage,
			toCustomLocation: toCustomLocation,
			toLinkPage: toLinkPage,
			toKitchenScreen: toKitchenScreen,
			toManagerScreen: toManagerScreen,
			toAdminPage: toAdminPage,
			toServerErrorPage: toServerErrorPage,
			toDisabledPage: toDisabledPage,
			toBookingPage: toBookingPage,
			toFeedbackPage: toFeedbackPage
		};

		return service;

		function toLoginPage() {
			$window.location.assign('/login');
		};

		function toCustomLocation(location) {
			$window.location.assign(location);
		};

		function toLinkPage() {
			$window.location.assign('/link');
		};

		function toKitchenScreen() {
			$window.location.assign('/kitchen');
		};

		function toManagerScreen() {
			$window.location.assign('/manager');
		};

		function toAdminPage() {
			$window.location.assign('/admin');
		};

		function toServerErrorPage() {
			$window.location.assign('/error');
		};

		function toDisabledPage() {
			$location.path("/disabled");
		};

		function toBookingPage() {
			$window.location.assign('#booking');
		};

		function toFeedbackPage() {
			$window.location.assign('#feedback');
		};
	}
})();