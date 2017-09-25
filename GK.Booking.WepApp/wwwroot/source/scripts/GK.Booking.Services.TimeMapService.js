(function () {
	'use strict';

	angular
		.module('GK.Booking.Services.TimeMapService', ['GK.Booking.DateTimeHelper'])
		.factory('TimeMapService', TimeMapService);

	TimeMapService.$inject = ['$http', '$q', 'DateTimeHelper'];

	function TimeMapService($http, $q, DateTimeHelper) {
		var service = {
			getTimeMap: getTimeMap,
			isTimeMapTrayAvilable: isTimeMapTrayAvilable
		};

		return service;

		function getTimeMap() {

			var deferred = $q.defer();

			$http({
				url: '/TimeMap/GetTimeMap',
				method: 'GET'
			})
			.then(function (response) {
				deferred.resolve(response.data);
			},
			function (response) {
				deferred.reject(response.data);
			});

			return deferred.promise;
		}

		function isTimeMapTrayAvilable(startTime, endTime) {

			var deferred = $q.defer();
			$http({
				url: '/TimeMap/CheckTimeMapTrayState',
				method: "POST",
				data: {
					startTime: startTime,
					endTime: endTime
				}
			})
			.then(function (response) {
				if (response.data === "Avilable") {
					deferred.resolve(true);
				}
				else {
					deferred.resolve(false);
				}
			},
			function (response) {
				deferred.reject(response.data);
			});

			return deferred.promise;
		};
	}
})();