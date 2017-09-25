(function () {
	'use strict';

	angular
		.module('GK.Booking.Interceptors.AuthInterceptor', [])
		.factory('AuthInterceptor', AuthInterceptor);

	AuthInterceptor.$inject = ['$q', '$location', 'RedirectService'];

	function AuthInterceptor($q, $location, RedirectService) {
		var service = {
			request: request,
			responseError: responseError
		};

		return service;

		function request(request) {
			//Ask server that ajax request
			request.headers['X-Requested-With'] = 'XMLHttpRequest';

			return request;
		};

		function responseError(response) {
			if (response.status === 401) {
				RedirectService.toLoginPage();
			}

			return $q.reject(response);
		};
	}
})();