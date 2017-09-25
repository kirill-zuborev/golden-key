angular
	.module('GK.Booking.LinkPage.LinkController', [
	'GK.Booking.LogoutControl',
	'GK.Booking.Interceptors.AuthInterceptor',
	'GK.Booking.Services.SessionService',
	'GK.Booking.Services.RedirectService',
	'GK.Booking.Dialog'
	])
	.config(['$httpProvider', function ($httpProvider) {
		$httpProvider.interceptors.push('AuthInterceptor');
	}])
	.controller('GK.Booking.LinkPage.LinkController', ['$scope', 'SessionService', 'RedirectService', 'Dialog',
		function ($scope, SessionService, RedirectService, Dialog) {

			$scope.logout = function () {
				var promiseObj = SessionService.logout();

				promiseObj.then(function (result) {
					RedirectService.toLoginPage();
				},
				function (reason) {
					Dialog.show('Ошибка', 'На сервере произошла ошибка.');
				});
			};

			$scope.toKitchenScreen = function () {
				RedirectService.toKitchenScreen();
			};

			$scope.toManagerScreen = function () {
				RedirectService.toManagerScreen();
			};

			$scope.toAdminPage = function () {
				RedirectService.toAdminPage();
			};

		}]);