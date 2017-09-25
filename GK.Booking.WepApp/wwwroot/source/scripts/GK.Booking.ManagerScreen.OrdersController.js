angular.module('GK.Booking.ManagerScreen.OrdersController', [
	'ngAnimate',
	'ngRoute',
	'ngAudio',
	'GK.Booking.Services.OrderService',
	'GK.Booking.Services.ContactService',
	'GK.Booking.Filters.PhoneNumber',
	'GK.Booking.Filters.OrderStatus',
	'GK.Booking.ServerErrorCodes',
	'GK.Booking.LogoutControl',
	'GK.Booking.Services.SessionService',
	'GK.Booking.Services.RedirectService',
	'GK.Booking.Interceptors.AuthInterceptor',
	'GK.Booking.Dialog',
	'GK.Booking.LockScreen',
	'GK.Booking.Constant',
	'GK.Booking.DateTimeHelper'])
	.config(['$httpProvider', function ($httpProvider) {
		$httpProvider.interceptors.push('AuthInterceptor');
	}])
	.controller('GK.Booking.ManagerScreen.OrdersController', [
		'$scope', '$element', '$http', '$interval', '$route', 'ngAudio', 'OrderService', 'ContactService', 'ServerErrorCodes', 'Dialog', 'Constant', 'SessionService', 'RedirectService', 'DateTimeHelper',
		function ($scope, $element, $http, $interval, $route, ngAudio, OrderService, ContactService, ServerErrorCodes, Dialog, Constant, SessionService, RedirectService, DateTimeHelper) {

			$(document).foundation('dropdown', 'reflow');

			$scope.orders = [];
			$scope.lockCandidate = null;
			$scope.isRequestInProgress = false;
			$scope.isGetDataRequestInProgress = false;
			$scope.wrongRequestCount = 0;

			$scope.beepSignal = ngAudio.load('wwwroot/GK.Booking.ManagerScreen.OrderReceive.mp3');

			$scope.removeOrder = function (order) {
				for (var i = 0; i < $scope.orders.length; i++) {
					var deletionCandidat = $scope.orders[i];
					if (deletionCandidat === order) {
						$scope.orders.splice(i, 1);
					}
				}
			};

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

			$scope.Constant = Constant;

			cancelationToken = $interval(function () {

				if ($scope.isGetDataRequestInProgress == true) {
					return;
				}

				$scope.isGetDataRequestInProgress = true;

				var promiseObj = OrderService.getTodayConfirmedOrders();

				promiseObj.then(function (value) {
					if (value != null) {
						$scope.removeNonexistentOrders(value);
						angular.merge($scope.orders, value);
					}
					else {
						$scope.orders = [];
					}
					$scope.isGetDataRequestInProgress = false;
					$scope.wrongRequestCount = 0;
				},
				function (reason) {
					if ($scope.wrongRequestCount > 10) {
						Dialog.show('Ошибка', 'Приложение потеряло связь с сервером. Попробуйте обновить страницу. Если это сообщение появится повторно, свяжитесь с администратором.');
						$scope.wrongRequestCount = 0;
					}
					$scope.wrongRequestCount++;
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

			$scope.getRemainingTimeInMinutes = function (targetTime) {
				var targetTimeInMilliseconds = DateTimeHelper.parseTimeToMilliseconds(targetTime);

				var miliseccondsDiff = targetTimeInMilliseconds - DateTimeHelper.parseTimeComponentOfDateToMilliseconds(new Date());

				if (miliseccondsDiff <= 0) {
					return 0;
				}

				var seconds = parseInt(miliseccondsDiff / 1000);

				return parseInt(seconds / 60);
			}
			var updateInterval = setInterval(function () { $scope.$apply(); }, 1000);   //timer tick


			$scope.setOrderStatus = function (order, status) {
				$scope.isRequestInProgress = true;
				var orderId = order.OrderID;
				var promiseObj = OrderService.setOrderStatus(orderId, status)

				promiseObj.then(function (value) {
					if (value == true) {
						order.OrderStatus = status;

						if (status != Constant.ORDER_READY && status != Constant.ORDER_CONFIRMED) {
							$scope.removeOrder(order);
						};
					}
					else {
						Dialog.show('Ошибка', 'Статус заказа не был изменён.');
					}
					$scope.isRequestInProgress = false;
				},
				function (reason) {
					if (reason != null && 'errorCode' in reason) {
						if (reason.errorCode == ServerErrorCodes.CURRENT_STATUS_NOT_CORRECT_FOR_OPERATION) {
							Dialog.show('Ошибка', 'Текущий статус заказа не позволяет выполнить данную операцию. Страница обновлена.');
							$route.reload();
						}
						else {
							Dialog.show('Ошибка', 'Статус заказа не был изменён.');
							$scope.isRequestInProgress = false;
						}
					}
					else {
						Dialog.show('Ошибка', 'Статус заказа не был изменён.');
						$scope.isRequestInProgress = false;
					}
				});
			};

			$scope.lockOrderPhone = function (order) {
				$scope.isRequestInProgress = true;
				var phoneNumber = order.CustomerPhoneNumber;
				var promiseObj = ContactService.lockPhone(phoneNumber)

				promiseObj.then(function (value) {
					$scope.setOrderStatus(order, Constant.ORDER_DENIED);
					$scope.isRequestInProgress = false;
					Dialog.show('Готово', 'Номер ' + order.CustomerPhoneNumber + ' был добавлен в чёрный список.');
				},
				function (reason) {
					$scope.isRequestInProgress = false;
					Dialog.show('Ошибка', 'Номер не был добавлен в чёрный список.');
				});
			};

			$scope.logout = function () {
				var promiseObj = SessionService.logout();

				promiseObj.then(function (result) {
					RedirectService.toLoginPage();
				},
				function (reason) {
					Dialog.show('Ошибка', 'На сервере произошла ошибка.');
				});
			};

			$scope.playBeep = function () {
				$scope.beepSignal.play();
			};

			$scope.setLockCandidate = function (order) {
				$scope.lockCandidate = order;
			};

			$scope.removeLockCandidate = function () {
				$scope.lockCandidate = null;
			};

			$element.on('$destroy', function () {
				$interval.cancel(cancelationToken);
				clearInterval(updateInterval);
			});
		}]);