(function () {
	'use strict';

	angular
		.module('GK.Booking.Services.OrderService', ['GK.Booking.Constant', 'GK.Booking.DateTimeHelper'])
		.factory('OrderService', OrderService);

	OrderService.$inject = ['$http', '$q', 'Constant', 'DateTimeHelper'];

	function OrderService($http, $q, Constant, DateTimeHelper) {
		var service = {
			pushOrder: pushOrder,
			confirmCreatedOrder: confirmCreatedOrder,
			getTodayConfirmedOrders: getTodayConfirmedOrders,
			setOrderStatus: setOrderStatus
		};

		return service;

		function GET_URL() { return '/Orders/GetTodayActiveOrders'; };
		function PUSH_URL() { return '/Orders/PushOrder'; };
		function CONFIRM_CREATED_URL() { return '/Orders/ConfirmOrder'; };
		function READY_URL() { return '/Orders/ConfirmOrderToReady'; };
		function CONFIRM_URL() { return '/Orders/ReadyOrderToConfirm'; };
		function COMPLETE_URL() { return '/Orders/CompleteOrder'; };
		function DENY_URL() { return '/Orders/DenyOrder'; };

		function pushOrder(order) {

			var pushOrderCommand = buildPushOrderCommand(order);

			var deferred = $q.defer();

			$http({
				url: PUSH_URL(),
				method: "POST",
				data: pushOrderCommand
			})
			.then(function (response) {

				order.orderId = response.data.OrderID;
				order.orderStatus = response.data.OrderStatus;
				order.creationTime = response.data.CreationTime;
				order.orderLifeTimeMilliseconds = response.data.OrderLifeTimeMilliseconds;

				deferred.resolve(true);
			},
			function (response) {
				deferred.reject(response.data);
			});

			return deferred.promise;
		}

		function confirmCreatedOrder(order) {

			var deferred = $q.defer();

			$http({
				url: CONFIRM_CREATED_URL(),
				method: "POST",
				data: { orderId: order.orderId, customerSecretCode: order.customerSecretCode }
			})
			.then(function (response) {
				if (response.data === 'Done') {
					deferred.resolve(true);
				}
				else {
					deferred.resolve(false);
				}
			},
			function (response) {
				deferred.reject(response.data);
			});

			return deferred.promise;
		}

		function setOrderStatus(orderId, status) {

			var deferred = $q.defer();

			$http({
				url: getURLByAction(status),
				method: "POST",
				data: { orderId: orderId }
			})
			.then(function (response) {
				if (response.data === 'Done') {
					deferred.resolve(true);
				}
				else {
					deferred.resolve(false);
				}
			},
			function (response) {
				deferred.reject(response.data);
			});

			return deferred.promise;
		};

		function getTodayConfirmedOrders() {
			var deferred = $q.defer();

			$http.get(GET_URL())
			.then(function (response) {
				deferred.resolve(response.data);
			},
			function (response) {
				deferred.reject(response.data);
			});

			return deferred.promise;
		}

		function getURLByAction(action) {
			switch (action) {
				case Constant.ORDER_CONFIRMED: return CONFIRM_URL();
				case Constant.ORDER_READY: return READY_URL();
				case Constant.ORDER_COMPLETED: return COMPLETE_URL();
				case Constant.ORDER_DENIED: return DENY_URL();
				default:
					console.error('Can not get URL by action');
					return null;
			};
		};

		function buildPushOrderCommand(order) {
			var choisedMenuItemsNames = [];

			for (var i = 0; i < order.tray.length; i++) {
				choisedMenuItemsNames.push(order.tray[i].product.name);
			}

			choisedMenuItemsNames.push()

			var pushOrderCommand = {
				PhoneNumber: order.phoneNumber,
				TargetStartTime: order.selectedTime.targetStartTime,
				TargetEndTime: order.selectedTime.targetEndTime,
				MenuItemsNames: choisedMenuItemsNames
			};

			return pushOrderCommand;
		}
	}
})();