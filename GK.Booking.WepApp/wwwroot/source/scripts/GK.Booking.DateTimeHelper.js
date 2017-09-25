(function () {
	'use strict';

	angular
		.module('GK.Booking.DateTimeHelper', [])
		.factory('DateTimeHelper', DateTimeHelper);

	function DateTimeHelper() {
		var service = {
			toClientDateTime: toClientDateTime,
			formatDate: formatDate,
			parseTimeToMilliseconds: parseTimeToMilliseconds,
			parseTimeComponentOfDateToMilliseconds: parseTimeComponentOfDateToMilliseconds
		};

		return service;

		function toClientDateTime(jsonServerDate) {
			var reg = /-?\d+/;
			var clearDate = reg.exec(jsonServerDate);

			var offsetInMilliseconds = new Date().getTimezoneOffset() * 60000;

			return new Date(clearDate - offsetInMilliseconds);
		};

		function formatDate(jsonDate) {

			var reg = /-?\d+/;
			var clearDate = reg.exec(jsonDate);
			var dateOut = new Date(parseInt(clearDate[0]));

			return dateOut;
		};

		function parseTimeToMilliseconds(timeString) {

			var timeComponents = timeString.split(':');
			var hours = parseInt(timeComponents[0]);
			var minutes = parseInt(timeComponents[1]);

			var millisecondsOut = (hours * 60 + minutes) * 60000;

			return millisecondsOut;
		};

		function parseTimeComponentOfDateToMilliseconds(date) {
			var reg = /[: ]/;
			var timeComponents = date.toTimeString().split(reg);
			var hours = parseInt(timeComponents[0]);
			var minutes = parseInt(timeComponents[1]);
			var seconds = parseInt(timeComponents[2]);

			var millisecondsOut = ((hours * 60 + minutes) * 60 + seconds) * 1000;

			return millisecondsOut;
		}
	}
})();