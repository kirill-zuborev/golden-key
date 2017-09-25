(function () {
	'use strict';

	angular
		.module('GK.Booking.Interceptors.ServerErrorInterceptor', ['GK.Booking.Services.RedirectService'])
		.factory('ServerErrorInterceptor', ServerErrorInterceptor);

	ServerErrorInterceptor.$inject = ['$q', '$rootScope', 'RedirectService'];

	function ServerErrorInterceptor($q, $rootScope, RedirectService) {
		var interceptor = {
			responseError: responseError
		};

		return interceptor;

		function responseError(response) {
			//ErrorInfoDTO validation errors
			if (response.status === 400) {
				response.data = {
					errorCode: response.data.ErrorCode,
					errorMessage: response.data.ErrorMessage
				};
			}
			else if (response.status === 500) {
				RedirectService.toServerErrorPage();
			}

			else if (response.status === 503) {
				RedirectService.toDisabledPage();
			}

			return $q.reject(response);
		};
	}
})();