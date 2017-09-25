angular.module('GK.Booking.WebApp.TimeMap', [])
	.directive('gkBookingWebAppTimeMap', function () {
		var definitionObject = {
			templateUrl: 'wwwroot/GK.Booking.WebApp.TimeMap.html',
			scope: {
				saveCallback: '&onChoise',
				closeCallback: '&onClose',
				emptyCallback: '&onEmpty'
			},
			controller: ['$scope', '$element', '$interval', 'TimeMapService',
				function ($scope, $element, $interval, TimeMapService) {
					$scope.timeMap = {};
					$scope.timeMap.TimeMapTrays = [];

					$scope.isLoading = false;
					$scope.isRequestInProgress = false;

					$scope.getTimeMap = function () {

						//Stops new request when previous not completed
						if ($scope.isRequestInProgress == true) {
							return;
						}

						$scope.isRequestInProgress = true;

						if ($scope.timeMap.TimeMapTrays.length === 0) {
							$scope.isLoading = true;
						}

						var promiseObj = TimeMapService.getTimeMap();

						promiseObj.then(function (value) {
							if (value == null) {
								$scope.timeMap.TimeMapTrays = [];
							}
							else if (value.TimeMapTrays.length === 0) {
								$scope.emptyCallback();
							}
							else {
								if ($scope.timeMap.TimeMapTrays.length === 0) {
									$scope.timeMap = value;
								}
								else {
									updateTimeMap(value.TimeMapTrays);
								}
							}

							$scope.isLoading = false;
							$scope.isRequestInProgress = false;
						});
					};

					var updateTimeMap = function (responceTrays) {

						var timeMapDictionary = {};
						//Convert responce array to dictionary with key: StartTime
						for (i = 0; i < responceTrays.length; i++) {
							timeMapDictionary[responceTrays[i].StartTime] = responceTrays[i];
						}

						for (i = 0; i < $scope.timeMap.TimeMapTrays.length; i++) {

							if (timeMapDictionary[$scope.timeMap.TimeMapTrays[i].StartTime] == null) {
								$scope.timeMap.TimeMapTrays.splice(i, 1);
							}
							else {
								$scope.timeMap.TimeMapTrays[i].IsAvilable = timeMapDictionary[$scope.timeMap.TimeMapTrays[i].StartTime].IsAvilable;
							}
						}
					}

					$scope.getTimeMap(); //Load time queue on startup
					var timer = $interval($scope.getTimeMap, 5000); //Refresh time queue after 10sec. interval

					$element.on('$destroy', function () {   //Destroy timer
						$interval.cancel(timer);
					});
				}],
			link: function (scope) {
				scope.setTimeValue = function (startTime, endTime) {
					var timeMapTrayInterval = { targetStartTime: startTime, targetEndTime: endTime };
					scope.saveCallback({ timeInterval: timeMapTrayInterval });

					scope.closeCallback();
				};
			}
		};
		return definitionObject;
	});