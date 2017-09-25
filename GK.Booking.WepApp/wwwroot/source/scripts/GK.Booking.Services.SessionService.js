(function () {
	'use strict';

	angular
		.module('GK.Booking.Services.SessionService', [])
		.factory('SessionService', SessionService);

	SessionService.$inject = ['$http', '$q'];

	function SessionService($http, $q) {
		var service = {
			login: login,
			logout: logout,
			LOGIN_PAGE_ROUTE: '/login'
		};

		return service;

		function login(loginData) {
			var deferred = $q.defer();
			$http({
				url: '/Login/Login',
				method: "POST",
				data: loginData
			})
			.then(function (response) {

				if (response.data.ErrorCode != null) {

					response.data = {
						errorCode: response.data.ErrorCode,
						errorMessage: response.data.ErrorMessage
					};

					return deferred.reject(response);
				}
				else {
					deferred.resolve(response.data);
				}

			},
			function (reason) {
				deferred.reject(reason.data);
			});

			return deferred.promise;
		};

		function logout() {
			var deferred = $q.defer();
			$http({
				url: '/Login/Logout',
				method: "POST"
			})
			.then(function (response) {
				deferred.resolve();
			},
			function (reason) {
				deferred.reject(reason.data);
			});

			return deferred.promise;
		};
	}
})();