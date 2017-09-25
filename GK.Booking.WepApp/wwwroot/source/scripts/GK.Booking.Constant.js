(function () {
	'use strict';

	angular
		.module('GK.Booking.Constant', [])
		.constant('Constant', {
			/*Order status*/
			ORDER_CREATED: 'Created',
			ORDER_CONFIRMED: 'Confirmed',
			ORDER_READY: 'Ready',
			ORDER_COMPLETED: 'Completed',
			ORDER_DENIED: 'Denied'
			/********************************/
		});
})();