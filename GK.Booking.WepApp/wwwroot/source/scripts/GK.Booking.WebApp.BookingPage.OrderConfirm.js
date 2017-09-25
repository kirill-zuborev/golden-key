angular.module('GK.Booking.WebApp.BookingPage.OrderConfirm', [
	'GK.Booking.WebApp.VisualTimer',
	'GK.Booking.LockScreen',
])
	.directive('gkBookingWebAppBookingPageOrderConfirm', function () {
		var definitionObject = {
			templateUrl: 'wwwroot/GK.Booking.WebApp.BookingPage.OrderConfirm.html',
			scope: {
				orderValue: "=orderValue",
				successCallback: "&onConfirmed",
				returnToCreatedCallback: "&onReturnToCreated",
				returnToNewCallback: "&onReturnToNew"
			},
			controller: ['$scope', 'OrderService',
				function ($scope, OrderService) {

					$scope.message;
					$scope.isRequestInProgress = false;
					$scope.isOrderExpired = false;

					$scope.onOrderExpired = function () {
						$scope.isOrderExpired = true;
					};

					$scope.confirmOrder = function () {
						$scope.isRequestInProgress = true;
						$scope.orderValue.customerSecretCode = this.secretCode;
						$scope.message = "Код проверяется";

						var promiseObj = OrderService.confirmCreatedOrder($scope.orderValue)

						promiseObj.then(function (value) {
							if (value == true) {
								$scope.message = "Код проверен";
								$scope.successCallback.call();
							}
							else {
								$scope.message = "Неправильный код";
							}
							$scope.isRequestInProgress = false;
						},
						function (reason) {
							if (reason.errorCode == ServerErrorCodes.ORDER_EXPIRED) {
								$scope.isRequestInProgress = false;
								$scope.isOrderExpired = true;
							}
						});
					};
				}]
		};
		return definitionObject;
	});