(function () {
	'use strict';

	angular
		.module('GK.Booking.Dialog', ['mm.foundation'])
		.factory('Dialog', Dialog);

	Dialog.$inject = ['$modal'];

	function Dialog($modal) {
		var service = {
			show: show
		};

		return service;

		function show(title, message) {

			var modalInstance = $modal.open({
				templateUrl: 'wwwroot/GK.Booking.Dialog.html',
				controller: ['$scope', '$modalInstance',
					function ($scope, $modalInstance) {

						$scope.message = message;
						$scope.title = title;

						$scope.ok = function () {
							$modalInstance.close();
						};

						$scope.cancel = function () {
							$modalInstance.dismiss();
						};
					}]
			});

			return modalInstance;
		}
	}
})();
