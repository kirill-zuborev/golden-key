angular.module('GK.Booking.WebApp.FeedbackPage', ['GK.Booking.LockScreen', 'GK.Booking.Services.NotifyService', 'ngCookies'])
	.controller('GK.Booking.WebApp.FeedbackPageController', ['$scope', '$cookies', 'NotifyService',
		function ($scope, $cookies, NotifyService) {

			$scope.FEEDBACK_SENT_COOKIES_NAME = "FEEDBACK_SENT";
			$scope.requestInProgress = false;

			$scope.isFeedbackSent = function () {
				return $cookies.get($scope.FEEDBACK_SENT_COOKIES_NAME) != null;
			};

			$scope.feedbackAlreadySent = function () {

				var expireDate = new Date();
				expireDate.setDate(expireDate.getDate() + 1);

				$cookies.put($scope.FEEDBACK_SENT_COOKIES_NAME, true, { 'expires': expireDate });
				$scope.isFeedbackSent = true;
			};

			$scope.isFeedbackSent = $scope.isFeedbackSent();

			$scope.sendFeedback = function () {
				$scope.requestInProgress = true;
				var promiseObj = NotifyService.notifyFeedback(this.feedbackText);

				promiseObj.then(function (value) {
					$scope.feedbackAlreadySent();
					$scope.requestInProgress = false;
				});
			};
		}]);
