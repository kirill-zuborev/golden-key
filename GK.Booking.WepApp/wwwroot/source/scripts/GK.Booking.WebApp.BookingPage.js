angular
	.module('GK.Booking.WebApp.BookingPage', [
		'ngAnimate',
		'GK.Booking.LockScreen',
		'GK.Booking.WebApp.TimeMap',
		'GK.Booking.WebApp.BookingPage.PhoneNumber',
		'GK.Booking.WebApp.BookingPage.OrderConfirm',
		'GK.Booking.Services.OrderService',
		'GK.Booking.Services.TimeMapService',
		'GK.Booking.Services.ConfigurationService',
		'GK.Booking.ServerErrorCodes',
		'GK.Booking.Dialog'])
	.controller('GK.Booking.WebApp.BookingPageController', [
		'$scope', '$http', '$location', '$route', 'TimeMapService', 'OrderService', 'ConfigurationService', 'ServerErrorCodes', 'Dialog',
		function ($scope, $http, $location, $route, TimeMapService, OrderService, ConfigurationService, ServerErrorCodes, Dialog) {

			var selectedMenuCategory;

			$scope.applicationParameters = {};

			function activate() {
				ConfigurationService.getApplicationParameters()
					.then(function (value) {

						if (value.isWorkingDayEnd) {
							$scope.goHomePage();
						}
						else {
							$scope.applicationParameters = value;
						}
					});
			};
			activate();

			$scope.setSelectedMenuCategory = function (category) {
				selectedMenuCategory = category;
			};

			$scope.getSelectedMenuCategory = function () {
				return selectedMenuCategory;
			};

			$scope.products = [];
			$scope.categories = [];

			$scope.order = {};
			$scope.isScreenLocked = false;
			$scope.orderDescDialogText = {};


			$scope.setOrderTime = function (timeInterval) {
				$scope.order.selectedTime = timeInterval;
			};

			$http.get('/Menu')
				.then(function (response) {
					$scope.menu = response.data;

					$scope.products = [];

					for (var i = 0; i < $scope.menu.Items.length; i++) {
						var item = $scope.menu.Items[i];
						$scope.products.push({ name: item.Name, desc: item.Desc, price: item.Price, category: item.Category });
					}

					$scope.categories = {};
					for (i = 0; i < $scope.menu.Items.length; i++) {
						$scope.categories[$scope.menu.Items[i].Category] = true;
					}

					$scope.categories = Object.getOwnPropertyNames($scope.categories);

					if ($scope.categories.length > 0) {
						$scope.setSelectedMenuCategory($scope.categories[0]);
					}
				});

			$scope.order.tray = [];
			$scope.addProduct = function (product) {
				var newTrayItem = {
					id: Math.random(),
					product: product
				};
				$scope.order.tray.push(newTrayItem);
			};
			$scope.removeProduct = function (trayItem) {
				for (var i = 0; i < $scope.order.tray.length; i++) {
					var deletionCandidat = $scope.order.tray[i];
					if (deletionCandidat === trayItem) {
						$scope.order.tray.splice(i, 1);
					}
				}
			};
			$scope.getProductTotalPrice = function () {
				var totalPrice = 0;
				for (var i = 0; i < $scope.order.tray.length; i++) {
					totalPrice += $scope.order.tray[i].product.price;
				}
				return Math.round(totalPrice * 100) / 100;
			};

			$scope.getRemainingAmount = function () {
				return $scope.applicationParameters.maxOrderCost - $scope.getProductTotalPrice();
			};

			$scope.doWorkByTimeMapTrayState = function (successCallback) {
				$scope.isScreenLocked = true;
				var promiseObj = TimeMapService.isTimeMapTrayAvilable($scope.order.selectedTime.targetStartTime, $scope.order.selectedTime.targetEndTime);

				promiseObj.then(function (value) {
					if (value == true) {
						successCallback.call();
					}
					else {
						$scope.previousScreenName = $scope.activeScreenName;
						$scope.loadCorrectTimeScreen();
					}
					$scope.isScreenLocked = false;
				});
			};

			$scope.pushOrder = function () {
				$scope.isScreenLocked = true;
				var promiseObj = TimeMapService.isTimeMapTrayAvilable($scope.order.selectedTime.targetStartTime, $scope.order.selectedTime.targetEndTime);

				promiseObj.then(function (value) {
					if (value == true) {
						promiseObj = OrderService.pushOrder($scope.order);

						promiseObj.then(function (value) {
							$scope.isScreenLocked = false;
							$scope.loadSecretCodeScreen();
						},
						function (reason) {
							if (reason.errorCode == ServerErrorCodes.TARGET_TIME_MAP_TRAY_IS_FULL) {
								$scope.previousScreenName = $scope.activeScreenName;
								$scope.loadCorrectTimeScreen();
							}
							else if (reason.errorCode == ServerErrorCodes.PHONE_NUMBER_LOCKED) {
								Dialog.show('Ошибка', 'Ваш номер телефона был внесён в чёрный список. Пожалуйста обратитесь к администрации магазина для разблокировки номера.');
								$scope.goHomePage();
							}
							else if (reason.errorCode == ServerErrorCodes.INVALID_PHONE_NUMBER) {
								Dialog.show('Ошибка', 'Вы ввели номер телефона в неправильном формате. Пожалуйста, введите номер телефона в формате: 9 цифр без разделителей.');
							}
							else if (reason.errorCode == ServerErrorCodes.ORDER_MAXIMUM_PRICE_EXCEEDED) {
								Dialog.show('Ошибка', 'Превышена максимальная цена одного заказа. Страница обновлена. Пожалуйста, повторите свой заказ.');
								$route.reload();
							}

							$scope.isScreenLocked = false;
						});
					}
					else {
						$scope.previousScreenName = $scope.activeScreenName;
						$scope.loadCorrectTimeScreen();
						$scope.isScreenLocked = false;
					}
				});
			};

			$scope.goHomePage = function () {
				$location.path("/");
			};

			$scope.loadNewOrderScreen = function () {
				$scope.order = {};
				$scope.order.tray = [];
				$scope.previousScreenName = 'order';
				$scope.loadOrderScreen();
			};

			$scope.loadPartiallyCreatedOrderScreen = function () {
				$scope.order.orderId = null;
				$scope.order.selectedTime = null;
				$scope.order.orderStatus = null;
				$scope.order.creationDate = null;
				$scope.order.expireDate = null;

				$scope.previousScreenName = 'order';
				$scope.loadOrderScreen();
			};

			//Navigation--->
			$scope.activeScreenName = 'order';
			$scope.previousScreenName = 'order';

			$scope.isOrderScreenShowed = function () {
				return $scope.activeScreenName === 'order';
			};

			$scope.isTimeScreenShowed = function () {
				return $scope.activeScreenName === 'selectTime';
			};

			$scope.isPhoneNumberScreenShowed = function () {
				return $scope.activeScreenName === 'number';
			};

			$scope.isCorrectTimeScreenShowed = function () {
				return $scope.activeScreenName === 'correctTime';
			};

			$scope.isSecretScreenShowed = function () {
				return $scope.activeScreenName === 'secret';
			};

			$scope.isConfirmedScreenShowed = function () {
				return $scope.activeScreenName === 'confirm';
			};

			$scope.loadOrderScreen = function () {
				$scope.activeScreenName = 'order';
			};

			$scope.loadSelectTimeScreen = function () {
				$scope.activeScreenName = 'selectTime';
			};

			$scope.loadCorrectTimeScreen = function () {
				$scope.activeScreenName = 'correctTime';
			};

			$scope.loadPhoneNumberScreen = function () {
				$scope.activeScreenName = 'number';
			};

			$scope.loadSecretCodeScreen = function () {
				$scope.activeScreenName = 'secret';
			};

			$scope.loadConfirmedScreen = function () {
				$scope.activeScreenName = 'confirm';
			};

			$scope.loadPreviousScreen = function () {
				$scope.activeScreenName = $scope.previousScreenName;
			};
			//<--------------------------------------------------
		}]);