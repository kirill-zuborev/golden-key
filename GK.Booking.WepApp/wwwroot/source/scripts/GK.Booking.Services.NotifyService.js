(function () {
	'use strict';

	angular
		.module('GK.Booking.Services.NotifyService', [])
		.factory('NotifyService', NotifyService);

	NotifyService.$inject = ['$http', '$q'];

	function NotifyService($http, $q) {
		var service = {
			notifyFeedback: notifyFeedback
		};

		return service;

		function NOTIFY_FEEDBACK_URL() { return '/Notify/NotifyFeedback'; };

		function notifyFeedback(message) {
			var deferred = $q.defer();

			$http({
				url: NOTIFY_FEEDBACK_URL(),
				method: "POST",
				data: {
					messageData: message
				}
			})
            .then(function (response) {
            	deferred.resolve(response.data);
            },
            function (response) {
            	deferred.reject(response.data);
            });

			return deferred.promise;
		};
	}
})();