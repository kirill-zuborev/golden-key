angular.module('GK.Booking.KitchenScreen.OrdersController', [
	'ngAnimate',
	'GK.Booking.Services.OrderService',
	'GK.Booking.Filters.PhoneNumber',
	'GK.Booking.Filters.OrderStatus',
	'GK.Booking.LogoutControl',
	'GK.Booking.Interceptors.AuthInterceptor',
	'GK.Booking.Services.SessionService',
	'GK.Booking.Services.RedirectService',
	'GK.Booking.Constant',
	'GK.Booking.DateTimeHelper'])
	.config(['$httpProvider', function ($httpProvider) {
		$httpProvider.interceptors.push('AuthInterceptor');
	}])
	.controller('GK.Booking.KitchenScreen.OrdersController', ['$scope', '$element', '$http', '$interval', '$window', 'OrderService', 'Constant', 'SessionService', 'RedirectService', 'DateTimeHelper',
		function ($scope, $element, $http, $interval, $window, OrderService, Constant, SessionService, RedirectService, DateTimeHelper) {

			$scope.orders = [];

			$scope.Constant = Constant;
			$scope.isGetDataRequestInProgress = false;

			$scope.removeNonexistentOrders = function (actualOrders) {

				for (var i = 0; i < $scope.orders.length; i++) {

					var existFlag = false;

					for (var j = 0; j < actualOrders.length; j++) {
						if ($scope.orders[i].OrderID === actualOrders[j].OrderID) {
							existFlag = true;
							break;
						}
					}

					if (existFlag === false) {
						$scope.orders.splice(i, 1);
					}
				}
			};

			cancelationToken = $interval(function () {
				var promiseObj = OrderService.getTodayConfirmedOrders();

				if ($scope.isGetDataRequestInProgress == true) {
					return;
				}

				$scope.isGetDataRequestInProgress = true;

				promiseObj.then(function (value) {
					if (value != null) {
						$scope.removeNonexistentOrders(value);
						angular.merge($scope.orders, value);
					}
					else {
						$scope.orders = [];
					}

					$scope.isGetDataRequestInProgress = false;
				},
				function (reason) {
					$scope.isGetDataRequestInProgress = false;
				});
			}, 5000);

			$scope.getRemainingTime = function (targetTime) {
				var targetTimeInMilliseconds = DateTimeHelper.parseTimeToMilliseconds(targetTime);

				var miliseccondsDiff = targetTimeInMilliseconds - DateTimeHelper.parseTimeComponentOfDateToMilliseconds(new Date());
				if (miliseccondsDiff <= 0) {
					return '0:00:00';
				}

				var seconds = parseInt(miliseccondsDiff / 1000);

				var hours = parseInt(seconds / 3600);
				seconds -= hours * 3600;
				var minutes = parseInt(seconds / 60);
				seconds -= minutes * 60;

				if (seconds < 10) {
					seconds = '0' + seconds;
				}

				if (minutes < 10) {
					minutes = '0' + minutes;
				}

				return hours + ':' + minutes + ':' + seconds;
			};

			$scope.logout = function () {
				var promiseObj = SessionService.logout();

				promiseObj.then(function (result) {
					RedirectService.toLoginPage();
				});
			};

			$scope.getRemainingTimeInMinutes = function (targetTime) {
				var targetTimeInMilliseconds = DateTimeHelper.parseTimeToMilliseconds(targetTime);

				var miliseccondsDiff = targetTimeInMilliseconds - DateTimeHelper.parseTimeComponentOfDateToMilliseconds(new Date());

				if (miliseccondsDiff <= 0) {
					return 0;
				}

				var seconds = parseInt(miliseccondsDiff / 1000);

				return parseInt(seconds / 60);
			}

			var updateInterval = setInterval(function () { $scope.$apply(); }, 1000);

			$element.on('$destroy', function () {
				$interval.cancel(cancelationToken);
				clearInterval(updateInterval);
			});
		}]);