angular.module('GK.Booking.WebApp.VisualTimer', [])
	.directive('gkBookingWebAppVisualTimer', function () {
		var definitionObject = {
			templateUrl: 'wwwroot/GK.Booking.WebApp.VisualTimer.html',
			scope: {
				targetMilliseconds: '=targetMilliseconds',
				finishCallback: '&onFinish'
			},
			controller: ['$scope', '$element', '$interval',
				function ($scope, $element, $interval) {

					$scope.displayedDateHours;
					$scope.displayedDateMinutes;
					$scope.displayedDateSeconds;


					$scope.UPDATE_INTERVAL_MILLISECONDS = 1000;
					$scope.remainingMilliseconds = $scope.targetMilliseconds;

					function updateTime() {
						$scope.remainingMilliseconds -= $scope.UPDATE_INTERVAL_MILLISECONDS;
						if ($scope.remainingMilliseconds <= 0) {
							$interval.cancel(timer);
							$scope.finishCallback.call();

							return;
						}

						var seconds = parseInt($scope.remainingMilliseconds / 1000);

						var hours = parseInt(seconds / 3600);
						seconds -= hours * 3600;
						var minutes = parseInt(seconds / 60);
						seconds -= minutes * 60;

						$scope.displayedDateHours = hours;
						$scope.displayedDateMinutes = minutes;
						$scope.displayedDateSeconds = seconds;
					}

					var timer = $interval(updateTime, $scope.UPDATE_INTERVAL_MILLISECONDS);

					$element.on('$destroy', function () {
						$interval.cancel(timer);
					});
				}]
		};
		return definitionObject;
	});