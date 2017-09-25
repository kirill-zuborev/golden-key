(function () {
	'use strict';

	angular
		.module('GK.Booking.Filters.OrderStatus', [])
		.filter('OrderStatus', OrderStatus);

	function OrderStatus() {
		return function OrderStatus(status) {

			if (!status) {
				return '';
			}

			switch (status) {
				case 'Created':
					return 'Создан';

				case 'Confirmed':
					return 'Подтверждён';

				case 'Ready':
					return 'Готов'

				case 'Completed':
					return 'Завершён'

				default:
					return status;
			}
		};
	}
})();