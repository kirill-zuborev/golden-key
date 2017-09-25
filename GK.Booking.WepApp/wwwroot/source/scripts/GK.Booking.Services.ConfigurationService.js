(function () {
	'use strict';

	angular
		.module('GK.Booking.Services.ConfigurationService', ['GK.Booking.DateTimeHelper'])
		.factory('ConfigurationService', ConfigurationService);

	ConfigurationService.$inject = ['$http', '$q', 'DateTimeHelper'];

	function ConfigurationService($http, $q, DateTimeHelper) {
		var service = {
			getApplicationParameters: getApplicationParameters,
			getBookingClientStatus: getBookingClientStatus,
			enableBookingClient: enableBookingClient,
			disableBookingClient: disableBookingClient
		};

		return service;

		function GET_URL() { return '/Configuration/GetApplicationParameters'; };
		function GET_BOOKING_CLIENT_STATUS_URL() { return '/Configuration/GetBookingClientStatus'; };
		function ENABLE_CLIENT_URL() { return '/Configuration/EnableBookingClient'; };
		function DISABLE_CLIENT_URL() { return '/Configuration/DisableBookingClient'; };

		function getApplicationParameters() {
			var deferred = $q.defer();

			$http.get(GET_URL())
			.then(function (response) {
				var applicationParameters = {
					workingStartTime: DateTimeHelper.formatDate(response.data.WorkingStartTime),
					workingEndTime: DateTimeHelper.formatDate(response.data.WorkingEndTime),
					maxOrderCost: response.data.MaxOrderCost,
					isWorkingDayEnd: response.data.IsWorkingDayEnd
				}

				deferred.resolve(applicationParameters);
			},
			function (response) {
				deferred.reject(response.data);
			});

			return deferred.promise;
		};

		function enableBookingClient() {
			var deferred = $q.defer();

			$http({
				url: ENABLE_CLIENT_URL(),
				method: "POST"
			})
			.then(function (response) {
				deferred.resolve(response.data);
			},
			function (response) {
				deferred.reject(response.data);
			});

			return deferred.promise;
		};

		function disableBookingClient() {
			var deferred = $q.defer();

			$http({
				url: DISABLE_CLIENT_URL(),
				method: "POST"
			})
			.then(function (response) {
				var disabledReasonText = response.data;
				deferred.resolve(disabledReasonText);
			},
			function (response) {
				deferred.reject(response.data);
			});

			return deferred.promise;
		};

		function getBookingClientStatus() {
			var deferred = $q.defer();

			$http.get(GET_BOOKING_CLIENT_STATUS_URL())
			.then(function (response) {
				var result = {};
				result.isBookingClientEnabled = response.data.IsBookingClientEnabled;
				result.disabledReasonText = response.data.DisabledReasonText;

				deferred.resolve(result);
			},
			function (response) {
				deferred.reject(response.data);
			});

			return deferred.promise;
		};
	}
})();