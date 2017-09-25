angular
	.module('GK.Booking.WebApp.AppModule', [
		'ngRoute',
		'ngMask',
		'GK.Booking.WebApp.Nav',
		'GK.Booking.WebApp.Page',
		'GK.Booking.WebApp.BookingPage',
		'GK.Booking.WebApp.FeedbackPage',
		'GK.Booking.WebApp.HomePage',
		'GK.Booking.Interceptors.ServerErrorInterceptor',
		'GK.Booking.Services.ConfigurationService'])
	.config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {
		$routeProvider
			.when('/', { templateUrl: 'wwwroot/GK.Booking.WebApp.HomePage.html', controller: 'GK.Booking.WebApp.HomePageController' })
			.when('/booking', { templateUrl: 'wwwroot/GK.Booking.WebApp.BookingPage.html', controller: 'GK.Booking.WebApp.BookingPageController' })
			.when('/feedback', { templateUrl: 'wwwroot/GK.Booking.WebApp.FeedbackPage.html', controller: 'GK.Booking.WebApp.FeedbackPageController' })
			.when('/disabled', { templateUrl: 'wwwroot/GK.Booking.WebApp.DisabledPage.html' })
			.otherwise({ templateUrl: 'wwwroot/GK.Booking.WebApp.HomePage.html' });;

		$httpProvider.interceptors.push('ServerErrorInterceptor');
	}]);
