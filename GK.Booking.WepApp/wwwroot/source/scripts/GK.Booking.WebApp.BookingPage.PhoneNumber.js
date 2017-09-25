angular.module('GK.Booking.WebApp.BookingPage.PhoneNumber', ['GK.Booking.Services.ContactService', 'GK.Booking.Filters.PhoneNumber'])
	.directive('gkBookingWebAppBookingPagePhoneNumber', function () {
		var definitionObject = {
			templateUrl: 'wwwroot/GK.Booking.WebApp.BookingPage.PhoneNumber.html',
			scope: {
				value: "=",
				submitCallback: '&onSubmit'
			},
			controller: ['$scope', 'ContactService',
				function ($scope, ContactService) {

					$scope.phoneNumber;
					$scope.isTrustedNumber = false;
					$scope.message = "";

					$scope.checkPhoneNumber = function () {

						$scope.message = "Номер проверяется";
						var promiseObj = ContactService.isPhoneLocked($scope.phoneNumber);

						promiseObj.then(function (value) {
							if (value === true) {
								$scope.message = "Номер в чёрном списке";
							}
							else {
								$scope.message = "Номер проверен";
								$scope.isTrustedNumber = true;
							}
						});
					};

					$scope.changePhoneValue = function (isFormValid) {
						$scope.isTrustedNumber = false;
						if (isFormValid) {
							$scope.value = $scope.phoneNumber;
							$scope.checkPhoneNumber();
						}
					};

					var activate = function () {
						if ($scope.value != null) {
							$scope.phoneNumber = $scope.value;
							$scope.checkPhoneNumber();
						}
					};

					activate(); //Check model phoneNumber on startup
				}]
		};
		return definitionObject;
	});