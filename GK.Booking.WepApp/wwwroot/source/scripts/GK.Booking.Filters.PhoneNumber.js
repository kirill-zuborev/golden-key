//usage: number | PhoneNumber: [hide_digits_count]
(function () {
	'use strict';

	angular
		.module('GK.Booking.Filters.PhoneNumber', [])
		.filter('PhoneNumber', PhoneNumber);

	function PhoneNumber() {
		return function PhoneNumber(pNumber, hideCount) {

			if (!pNumber || hideCount < 0) {
				return '';
			}

			if (hideCount > 12) {
				hideCount = 12;
			}


			var value = pNumber.toString().trim().replace(/^\+/, '');

			if (value.match(/[^0-9]/)) {
				return pNumber;
			}

			//hide symbols
			if ((value.length + hideCount) >= 12) {
				var prefix = value.slice(0, (12 - hideCount));
				var suffix = value.slice(hideCount * -1);

				suffix = suffix.replace(/./gi, "X");

				value = prefix + suffix;
			}


			var country, city, number;

			switch (value.length) {
				case 10: // +1PPP####### -> C (PPP) ###-##-##
					country = 1;
					city = value.slice(0, 3);
					number = value.slice(3);
					break;

				case 11: // +CPPP####### -> CCC (PP) ###-##-##
					country = value[0];
					city = value.slice(1, 4);
					number = value.slice(4);
					break;

				case 12: // +CCCPP####### -> CCC (PP) ###-##-##
					country = value.slice(0, 3);
					city = value.slice(3, 5);
					number = value.slice(5);
					break;

				default:
					return pNumber;
			}

			if (country == 1) {
				country = "";
			}

			if (country.length > 0) {
				country = '+' + country;
			}

			number = number.slice(0, 3) + '-' + number.slice(3, 5) + '-' + number.slice(5);

			return (country + " (" + city + ") " + number).trim();
		};
	}
})();