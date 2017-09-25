angular.module('GK.Booking.LoginPage.LoginController', [
	'GK.Booking.Services.SessionService',
	'GK.Booking.Services.RedirectService',
	'GK.Booking.Dialog'])
	.controller('GK.Booking.LoginPage.LoginController', ['$scope', '$http', '$window', 'SessionService', 'RedirectService', 'Dialog',
		function ($scope, $http, $window, SessionService, RedirectService, Dialog) {

			$scope.model = {};
			$scope.model.user = {};
			$scope.model.user = { Login: '', Password: '', IsPersistent: false };
			$scope.DEFAULT_REDIRECT_PUTH = "/link";

			$scope.submit = function () {

				var promiseObj = SessionService.login($scope.model.user);

				promiseObj.then(function (result) {
					if (result != null) {
						RedirectService.toCustomLocation(result.redirectTo);
					}
					else
						RedirectService.toLinkPage
				},
				function (reason) {
					if (reason.data.errorCode === 17) {
						Dialog.show('Ошибка авторизации', 'Введённые учётные данные не верны.');
					}
					else {
						Dialog.show('Ошибка', 'На сервере произошла ошибка.');
					}
				});
			};
		}]);