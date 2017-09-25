angular
	.module('GK.Booking.WebApp.HomePage', ['GK.Booking.Services.ConfigurationService'])
	.controller('GK.Booking.WebApp.HomePageController', ['$scope', '$rootScope', 'ConfigurationService',
		function ($scope, $rootScope, ConfigurationService) {

			$scope.model = {};
			$scope.model.activeScreenName = 'opened';

			$scope.applicationParameters = {};

			$scope.isOpenedScreenShowed = function () {
				return $scope.activeScreenName === 'opened';
			};

			$scope.isClosedScreenShowed = function () {
				return $scope.activeScreenName === 'closed';
			};

			$scope.loadOpenedScreen = function () {
				$scope.activeScreenName = 'opened';
			};

			$scope.loadClosedScreen = function () {
				$scope.activeScreenName = 'closed';
			};

			function activate() {
				ConfigurationService.getApplicationParameters()
					.then(function (value) {
						if (value.isWorkingDayEnd) {
							$scope.loadClosedScreen();
						}
						else {
							$scope.loadOpenedScreen();
						}
					});
			};
			activate();
		}]);