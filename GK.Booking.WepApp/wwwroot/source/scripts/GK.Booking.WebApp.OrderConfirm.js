angular.module('GK.Booking.WebApp.OrderConfirm', [])
	.directive('gkBookingWebAppOrderConfirm', function () {
		var definitionObject = {
			templateUrl: 'wwwroot/GK.Booking.WebApp.OrderConfirm.html',
			scope: {
				timeMap: '=',
				getTimeMapMethodLink: '&',
				chosenTime: '&'
			},
			link: function ($scope) {
				$(document).foundation('reveal', 'reflow');

				//$scope.formatDate = function (jsonDate) {

				//	var reg = /-?\d+/;
				//	var clearDate = reg.exec(jsonDate);
				//	var dateOut = new Date(parseInt(clearDate[0]));

				//	return dateOut;
				//};

				$scope.setTimeValue = function (startTime, endTime) {
					$scope.chosenTime.StartTime = startTime;
					$scope.chosenTime.EndTime = endTime;
				};
			}
		};
		return definitionObject;
	});