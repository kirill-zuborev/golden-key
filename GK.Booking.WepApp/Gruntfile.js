/// <binding AfterBuild='cssmin, sass, uglify' />
module.exports = function (grunt) {
	grunt.initConfig({
		root: 'wwwroot',
		jsSource: 'wwwroot/source/scripts',
		sassSource: 'wwwroot/source/styles',

		pkg: grunt.file.readJSON('package.json'),
		sass: {
			styles: {
				files: {
					'<%= root %>/GK.Booking.WebApp.Styles.css': '<%= sassSource %>/GK.Booking.WebApp.Styles.scss',
					'<%= root %>/GK.Booking.KitchenScreen.Styles.css': '<%= sassSource %>/GK.Booking.KitchenScreen.Styles.scss',
					'<%= root %>/GK.Booking.ManagerScreen.Styles.css': '<%= sassSource %>/GK.Booking.ManagerScreen.Styles.scss',
					'<%= root %>/GK.Booking.LoginPage.Styles.css': '<%= sassSource %>/GK.Booking.LoginPage.Styles.scss',
					'<%= root %>/GK.Booking.LinkPage.Styles.css': '<%= sassSource %>/GK.Booking.LinkPage.Styles.scss',
					'<%= root %>/GK.Booking.AdminPage.Styles.css': '<%= sassSource %>/GK.Booking.AdminPage.Styles.scss',
					'<%= root %>/GK.Booking.NotFoundErrorPage.Styles.css': '<%= sassSource %>/GK.Booking.NotFoundErrorPage.Styles.scss',
					'<%= root %>/GK.Booking.ServerErrorPage.Styles.css': '<%= sassSource %>/GK.Booking.ServerErrorPage.Styles.scss',
					'<%= root %>/GK.Booking.WebApp.DisabledPage.css': '<%= sassSource %>/GK.Booking.WebApp.DisabledPage.scss'
				}
			}
		},
		cssmin: {
			styles: {
				files: [{
					src: ['<%= root %>/GK.Booking.WebApp.Styles.css'],
					dest: '<%= root %>/GK.Booking.WebApp.Styles.css'
				},
					{
						src: ['<%= root %>/GK.Booking.WebApp.DisabledPage.css'],
						dest: '<%= root %>/GK.Booking.WebApp.DisabledPage.css'
					},
					{
						src: ['<%= root %>/GK.Booking.KitchenScreen.Styles.css'],
						dest: '<%= root %>/GK.Booking.KitchenScreen.Styles.css'
					},
					{
						src: ['<%= root %>/GK.Booking.ManagerScreen.Styles.css'],
						dest: '<%= root %>/GK.Booking.ManagerScreen.Styles.css'
					},
					{
						src: ['<%= root %>/GK.Booking.LoginPage.Styles.css'],
						dest: '<%= root %>/GK.Booking.LoginPage.Styles.css'
					},
					{
						src: ['<%= root %>/GK.Booking.LinkPage.Styles.css'],
						dest: '<%= root %>/GK.Booking.LinkPage.Styles.css'
					},
					{
						src: ['<%= root %>/GK.Booking.AdminPage.Styles.css'],
						dest: '<%= root %>/GK.Booking.AdminPage.Styles.css'
					},
					{
						src: ['<%= root %>/GK.Booking.NotFoundErrorPage.Styles.css'],
						dest: '<%= root %>/GK.Booking.NotFoundErrorPage.Styles.css'
					},
					{
						src: ['<%= root %>/GK.Booking.ServerErrorPage.Styles.css'],
						dest: '<%= root %>/GK.Booking.ServerErrorPage.Styles.css'
					}
				]
			}
		},
		uglify: {
			options: {
				banner: '/*! <%= pkg.name %> - v<%= pkg.version %> - ' +
					'<%= grunt.template.today("yyyy-mm-dd") %> */'
			},
			webApp: {
				files: {
					'<%= root %>/GK.Booking.BookingApp.min.js': [
						'<%= jsSource %>/GK.Booking.WebApp.AppModule.js',
						'<%= jsSource %>/GK.Booking.DateTimeHelper.js',
						'<%= jsSource %>/GK.Booking.LockScreen.js',
						'<%= jsSource %>/GK.Booking.Services.TimeMapService.js',
						'<%= jsSource %>/GK.Booking.Services.ContactService.js',
						'<%= jsSource %>/GK.Booking.Services.OrderService.js',
						'<%= jsSource %>/GK.Booking.Services.ConfigurationService.js',
						'<%= jsSource %>/GK.Booking.Services.NotifyService.js',
						'<%= jsSource %>/GK.Booking.Dialog.js',
						'<%= jsSource %>/GK.Booking.WebApp.Nav.js',
						'<%= jsSource %>/GK.Booking.WebApp.VisualTimer.js',
						'<%= jsSource %>/GK.Booking.WebApp.TimeMap.js',
						'<%= jsSource %>/GK.Booking.WebApp.BookingPage.PhoneNumber.js',
						'<%= jsSource %>/GK.Booking.WebApp.BookingPage.OrderConfirm.js',
						'<%= jsSource %>/GK.Booking.WebApp.Page.js',
						'<%= jsSource %>/GK.Booking.WebApp.HomePage.js',
						'<%= jsSource %>/GK.Booking.WebApp.BookingPage.js',
						'<%= jsSource %>/GK.Booking.WebApp.FeedbackPage.js',
						'<%= jsSource %>/GK.Booking.Filters.PhoneNumber.js',
						'<%= jsSource %>/GK.Booking.Constant.js',
						'<%= jsSource %>/GK.Booking.ServerErrorCodes.js',
						'<%= jsSource %>/GK.Booking.Interceptors.ServerErrorInterceptor.js',
						'<%= jsSource %>/GK.Booking.Services.RedirectService.js'
					]
				}
			},
			adminApp: {
				files: {
					'<%= root %>/GK.Booking.AdminApp.min.js': [
						'<%= jsSource %>/GK.Booking.Filters.PhoneNumber.js',
						'<%= jsSource %>/GK.Booking.AdminPage.AdminController.js',
						'<%= jsSource %>/GK.Booking.DateTimeHelper.js',
						'<%= jsSource %>/GK.Booking.LogoutControl.js',
						'<%= jsSource %>/GK.Booking.Services.ContactService.js',
						'<%= jsSource %>/GK.Booking.Services.SessionService.js',
						'<%= jsSource %>/GK.Booking.Services.RedirectService.js',
						'<%= jsSource %>/GK.Booking.Services.ConfigurationService.js',
						'<%= jsSource %>/GK.Booking.Interceptors.AuthInterceptor.js',
						'<%= jsSource %>/GK.Booking.Dialog.js',
						'<%= jsSource %>/GK.Booking.LockScreen.js'
					]
				}
			},
			kitchenApp: {
				files: {
					'<%= root %>/GK.Booking.KitchenApp.min.js': [
						'<%= jsSource %>/GK.Booking.Constant.js',
						'<%= jsSource %>/GK.Booking.DateTimeHelper.js',
						'<%= jsSource %>/GK.Booking.KitchenScreen.OrdersController.js',
						'<%= jsSource %>/GK.Booking.Services.OrderService.js',
						'<%= jsSource %>/GK.Booking.Filters.PhoneNumber.js',
						'<%= jsSource %>/GK.Booking.Filters.OrderStatus.js',
						'<%= jsSource %>/GK.Booking.LogoutControl.js',
						'<%= jsSource %>/GK.Booking.Services.SessionService.js',
						'<%= jsSource %>/GK.Booking.Services.RedirectService.js',
						'<%= jsSource %>/GK.Booking.Interceptors.AuthInterceptor.js'
					]
				}
			},
			managerApp: {
				files: {
					'<%= root %>/GK.Booking.ManagerApp.min.js': [
						'<%= jsSource %>/GK.Booking.Constant.js',
						'<%= jsSource %>/GK.Booking.DateTimeHelper.js',
						'<%= jsSource %>/GK.Booking.Dialog.js',
						'<%= jsSource %>/GK.Booking.ServerErrorCodes.js',
						'<%= jsSource %>/GK.Booking.ManagerScreen.OrdersController.js',
						'<%= jsSource %>/GK.Booking.Services.OrderService.js',
						'<%= jsSource %>/GK.Booking.Services.ContactService.js',
						'<%= jsSource %>/GK.Booking.Filters.PhoneNumber.js',
						'<%= jsSource %>/GK.Booking.Filters.OrderStatus.js',
						'<%= jsSource %>/GK.Booking.LogoutControl.js',
						'<%= jsSource %>/GK.Booking.Services.SessionService.js',
						'<%= jsSource %>/GK.Booking.Services.RedirectService.js',
						'<%= jsSource %>/GK.Booking.Interceptors.AuthInterceptor.js',
						'<%= jsSource %>/GK.Booking.LockScreen.js'
					]
				}
			},
			linkApp: {
				files: {
					'<%= root %>/GK.Booking.LinkApp.min.js': [
						'<%= jsSource %>/GK.Booking.LinkPage.LinkController.js',
						'<%= jsSource %>/GK.Booking.LogoutControl.js',
						'<%= jsSource %>/GK.Booking.Services.SessionService.js',
						'<%= jsSource %>/GK.Booking.Services.RedirectService.js',
						'<%= jsSource %>/GK.Booking.Interceptors.AuthInterceptor.js',
						'<%= jsSource %>/GK.Booking.Dialog.js'
					]
				}
			},
			loginApp: {
				files: {
					'<%= root %>/GK.Booking.LoginApp.min.js': [
						'<%= jsSource %>/GK.Booking.Constant.js',
						'<%= jsSource %>/GK.Booking.DateTimeHelper.js',
						'<%= jsSource %>/GK.Booking.Services.SessionService.js',
						'<%= jsSource %>/GK.Booking.Services.RedirectService.js',
						'<%= jsSource %>/GK.Booking.LoginPage.LoginController.js',
						'<%= jsSource %>/GK.Booking.Dialog.js'
					]
				}
			}
		},
		watch: {
			styles: {
				files: ['<%= sassSource %>/*.scss'],
				tasks: ['sass', 'cssmin'],
				options: {
					spawn: false,
				}
			},
			scripts: {
				files: ['<%= jsSource %>/GK.Booking.*js'],
				tasks: ['uglify'],
				options: {
					spawn: false,
				}
			}
		}
	});

	grunt.loadNpmTasks('grunt-contrib-sass');
	grunt.loadNpmTasks('grunt-contrib-cssmin');
	grunt.loadNpmTasks('grunt-contrib-uglify');
	grunt.loadNpmTasks('grunt-contrib-watch');

	grunt.registerTask('dev', ['sass', 'cssmin', 'uglify', 'watch']);
	grunt.registerTask('debug', ['sass', 'cssmin']);
	grunt.registerTask('build', ['sass', 'cssmin', 'uglify']);
	grunt.registerTask('default', ['dev']);
};