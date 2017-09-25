(function () {
	'use strict';

	angular
		.module('GK.Booking.Services.ContactService', ['GK.Booking.DateTimeHelper'])
		.factory('ContactService', ContactService);

	ContactService.$inject = ['$http', '$q', 'DateTimeHelper'];

	function ContactService($http, $q, DateTimeHelper) {
		var service = {
			isPhoneLocked: isPhoneLocked,
			lockPhone: lockPhone,
			unlockPhone: unlockPhone,
			getLockedPhones: getLockedPhones
		};

		return service;

		function CHECK_LOCKED_URL() { return '/Contact/CheckPhoneStatus'; };
		function LOCK_URL() { return '/Contact/LockPhone'; };
		function UNLOCK_URL() { return '/Contact/UnlockPhone'; };
		function GET_LOCKED_LIST_URL() { return '/Contact/GetLockedPhones'; };

		function isPhoneLocked(number) {

			var deferred = $q.defer();

			$http({
				url: CHECK_LOCKED_URL(),
				method: "POST",
				data: {
					phoneNumber: number
				}
			})
			.then(function (response) {
				if (response.data === 'locked') {
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
		}

		function lockPhone(number) {

			var deferred = $q.defer();

			$http({
				url: LOCK_URL(),
				method: "POST",
				data: {
					phoneNumber: number
				}
			})
            .then(function (response) {
            	deferred.resolve(true);
            },
            function (reason) {
            	deferred.reject(reason.data);
            });

			return deferred.promise;
		}

		function unlockPhone(number) {

			var deferred = $q.defer();

			$http({
				url: UNLOCK_URL(),
				method: "POST",
				data: {
					phoneNumber: number
				}
			})
            .then(function (response) {
            	deferred.resolve(true);
            },
            function (reason) {
            	deferred.reject(reason.data);
            });

			return deferred.promise;
		}

		function getLockedPhones(searchParams) {

			var deferred = $q.defer();

			$http({
				url: GET_LOCKED_LIST_URL(),
				method: "POST",
				data: {
					pageNumber: searchParams.pageIndex,
					pageSize: searchParams.pageSize,
					searchString: searchParams.searchString
				}
			})
			.then(function (response) {

				for (var i = 0; i < response.data.PhonesList.length; i++) {
					response.data.PhonesList[i].LockDate = DateTimeHelper.formatDate(response.data.PhonesList[i].LockDate);
				}

				var result = {};
				result.list = response.data.PhonesList;
				result.total = response.data.TotalCount;

				deferred.resolve(result);
			},
			function (reason) {
				deferred.reject(reason.data);
			});

			return deferred.promise;
		}
	}
})();