angular
	.module('GK.Booking.AdminPage.AdminController', [
		'GK.Booking.Filters.PhoneNumber',
		'GK.Booking.LogoutControl',
		'GK.Booking.Services.ContactService',
		'GK.Booking.Interceptors.AuthInterceptor',
		'GK.Booking.Services.SessionService',
		'GK.Booking.Services.RedirectService',
		'GK.Booking.Services.ConfigurationService',
		'GK.Booking.LockScreen',
		'GK.Booking.Dialog',
		'mm.foundation',
		'ngMask'])
	.config(['$httpProvider', function ($httpProvider) {
		$httpProvider.interceptors.push('AuthInterceptor');
	}])
	.controller('GK.Booking.AdminPage.AdminController', ['$scope', '$http', '$interval', '$window', 'ContactService', 'SessionService', 'RedirectService', 'ConfigurationService', 'Dialog',
		function ($scope, $http, $interval, $window, ContactService, SessionService, RedirectService, ConfigurationService, Dialog) {

			$(document).foundation('dropdown', 'reflow');

			$scope.model = {};
			$scope.model.lockedPhones = {};
			$scope.model.lockedPhones.list = [];

			$scope.model.searchParams = {};
			$scope.model.searchParams.searchString = '';
			$scope.model.searchParams.pageIndex = 1;
			$scope.model.searchParams.pageSize = 10;

			$scope.model.unlockCandidate = '';
			$scope.model.isBookingClientEnabled = true;
			$scope.model.stateDescriptionText = '';

			$scope.model.isDataRequestInProgress = false;
			$scope.model.isClientStatusRequestInProgress = false;

			$scope.logout = function () {
				var promiseObj = SessionService.logout();

				promiseObj.then(function (result) {
					RedirectService.toLoginPage();
				});
			};

			$scope.setUnlockCandidate = function (phoneNumber) {
				$scope.model.unlockCandidate = phoneNumber;
			};

			$scope.removeUnlockCandidate = function () {
				$scope.model.unlockCandidate = '';
			};

			$scope.unlockPhone = function () {
				$scope.model.isDataRequestInProgress = true;
				var promiseObj = ContactService.unlockPhone($scope.model.unlockCandidate)

				promiseObj.then(function (value) {
					$scope.getLockedPhones();
					Dialog.show('Готово', 'Номер ' + $scope.model.unlockCandidate + ' был исключён из чёрного списка.');
					$scope.model.unlockCandidate = '';
					$scope.model.isDataRequestInProgress = false;
				},
				function (reason) {
					Dialog.show('Ошибка', 'Номер не был исключён из чёрного списка.');
					$scope.model.isDataRequestInProgress = false;
				});
			};

			$scope.switchClientState = function(state) {
				if ($scope.model.isBookingClientEnabled) {
					$scope.enableBookingClient();
				}
				else {
					$scope.disableBookingClient();
				}
			};

			$scope.enableBookingClient = function () {
				$scope.model.isClientStatusRequestInProgress = true;
				ConfigurationService.enableBookingClient()
					.then(function (value) {
						$scope.model.isBookingClientEnabled = true;
						$scope.model.stateDescriptionText = null;
						$scope.model.isClientStatusRequestInProgress = false;
					},
					function (reason) {
						$scope.model.isClientStatusRequestInProgress = false;
						Dialog.show('Ошибка', 'Ошибка сервера. Действие не было выполнено.');
					});
			};

			$scope.disableBookingClient = function () {
				$scope.model.isClientStatusRequestInProgress = true;
				ConfigurationService.disableBookingClient()
					.then(function (value) {
						$scope.model.isBookingClientEnabled = false;
						$scope.model.stateDescriptionText = value;
						$scope.model.isClientStatusRequestInProgress = false;
					},
					function (reason) {
						$scope.model.isClientStatusRequestInProgress = false;
						Dialog.show('Ошибка', 'Ошибка сервера. Действие не было выполнено.');
					});
			};

			$scope.getBookingClientStatus = function () {
				$scope.model.isClientStatusRequestInProgress = true;
				ConfigurationService.getBookingClientStatus()
					.then(function (value) {
						$scope.model.isBookingClientEnabled = value.isBookingClientEnabled;
						$scope.model.stateDescriptionText = value.disabledReasonText;
						$scope.model.isClientStatusRequestInProgress = false;
					});
			};

			$scope.getLockedPhones = function () {
				$scope.model.isDataRequestInProgress = true;
				var promiseObj = ContactService.getLockedPhones($scope.model.searchParams);

				promiseObj.then(function (value) {
					$scope.model.lockedPhones = value;
					$scope.model.isDataRequestInProgress = false;
				},
				function (reason) {
					Dialog.show('Ошибка', 'Ошибка при обработке запроса сервером.');
					$scope.model.isDataRequestInProgress = false;
				});
			};

			function activate() {
				$scope.getLockedPhones();
				$scope.getBookingClientStatus();
			};

			activate();
		}]);