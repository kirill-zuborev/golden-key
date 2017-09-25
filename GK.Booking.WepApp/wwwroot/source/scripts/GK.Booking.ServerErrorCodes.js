(function () {
	'use strict';

	angular
		.module('GK.Booking.ServerErrorCodes', [])
		.constant('ServerErrorCodes', {

			UNDEFINED_ERROR: 0,
			INVALID_PHONE_NUMBER: 10,
			MESSAGE_NOT_SENT: 11,
			PHONE_NUMBER_LOCKED: 12,
			ORDER_MAXIMUM_PRICE_EXCEEDED: 13,
			TARGET_TIME_MAP_TRAY_IS_FULL: 14,
			CURRENT_STATUS_NOT_CORRECT_FOR_OPERATION: 15,
			ORDER_EXPIRED: 16,
			WRONG_USER_CREDENTIALS: 17
		});
})();