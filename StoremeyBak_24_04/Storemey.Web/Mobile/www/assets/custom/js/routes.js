'use strict';

window.routes = [
	{
		path: '/',
		componentUrl: '/Mobile/www/partials/screens/splash.html'
	},
	{
		path: '/walkthrough',
		componentUrl: '/Mobile/www/partials/screens/walkthrough.html'
	},
	{
		path: '/home',
		componentUrl: '/Mobile/www/partials/screens/home.html'
	},
	{
		path: '/components',
		componentUrl: '/Mobile/www/partials/components.html',
		routes: [
			{
				path: '/accordion',
				componentUrl: '/Mobile/www/partials/components/accordion.html'
			},
			{
				path: '/action-sheet',
				componentUrl: '/Mobile/www/partials/components/action-sheet.html'
			},
			{
				path: '/autocomplete',
				componentUrl: '/Mobile/www/partials/components/autocomplete.html'
			},
			{
				path: '/badge',
				componentUrl: '/Mobile/www/partials/components/badge.html'
			},
			{
				path: '/button',
				componentUrl: '/Mobile/www/partials/components/button.html'
			},
			{
				path: '/cards',
				componentUrl: '/Mobile/www/partials/components/cards.html'
			},
			{
				path: '/checkbox',
				componentUrl: '/Mobile/www/partials/components/checkbox.html'
			},
			{
				path: '/chips',
				componentUrl: '/Mobile/www/partials/components/chips.html'
			},
			{
				path: '/content-block',
				componentUrl: '/Mobile/www/partials/components/content-block.html'
			},
			{
				path: '/data-table',
				componentUrl: '/Mobile/www/partials/components/data-table.html'
			},
			{
				path: '/datepicker',
				componentUrl: '/Mobile/www/partials/components/datepicker.html'
			},
			{
				path: '/dialog',
				componentUrl: '/Mobile/www/partials/components/dialog.html'
			},
			{
				path: '/floating-action-button',
				componentUrl: '/Mobile/www/partials/components/floating-action-button.html',
				routes: [
					{
						path: '/default',
						componentUrl: '/Mobile/www/partials/components/fab-default.html'
					},
					{
						path: '/extended',
						componentUrl: '/Mobile/www/partials/components/fab-extended.html'
					},
					{
						path: '/speed-dial',
						componentUrl: '/Mobile/www/partials/components/fab-speed-dial.html'
					},
					{
						path: '/morph-searchbar',
						componentUrl: '/Mobile/www/partials/components/fab-morph-searchbar.html'
					},
					{
						path: '/morph-toolbar',
						componentUrl: '/Mobile/www/partials/components/fab-morph-toolbar.html'
					}
				]
			},
      {
				path: '/form-elements',
				componentUrl: '/Mobile/www/partials/components/form-elements.html'
			},
      {
				path: '/form-validator',
				componentUrl: '/Mobile/www/partials/components/form-validator.html'
			},
			{
				path: '/gauge',
				componentUrl: '/Mobile/www/partials/components/gauge.html'
			},
			{
				path: '/grid',
				componentUrl: '/Mobile/www/partials/components/grid.html'
			},
			{
				path: '/hamburger',
				componentUrl: '/Mobile/www/partials/components/hamburger.html'
			},
			{
				path: '/infinite-scroll',
				componentUrl: '/Mobile/www/partials/components/infinite-scroll.html'
			},
			{
				path: '/keypad',
				componentUrl: '/Mobile/www/partials/components/keypad.html'
			},
			{
				path: '/lazy-load',
				componentUrl: '/Mobile/www/partials/components/lazy-load.html'
			},
			{
				path: '/list-index',
				componentUrl: '/Mobile/www/partials/components/list-index.html'
			},
			{
				path: '/list-view',
				componentUrl: '/Mobile/www/partials/components/list-view.html'
			},
			{
				path: '/navbar',
				componentUrl: '/Mobile/www/partials/components/navbar.html',
				routes: [
					{
						path: '/fixed',
						componentUrl: '/Mobile/www/partials/components/navbar-fixed.html'
					},
					{
						path: '/hide-on-scroll',
						componentUrl: '/Mobile/www/partials/components/navbar-hide-on-scroll.html'
					}
				]
			},
			{
				path: '/note',
				componentUrl: '/Mobile/www/partials/components/note.html'
			},
			{
				path: '/notification',
				componentUrl: '/Mobile/www/partials/components/notification.html'
			},
			{
				path: '/photo-browser',
				componentUrl: '/Mobile/www/partials/components/photo-browser.html'
			},
			{
				path: '/picker',
				componentUrl: '/Mobile/www/partials/components/picker.html'
			},
			{
				path: '/popover',
				componentUrl: '/Mobile/www/partials/components/popover.html'
			},
			{
				path: '/popup',
				componentUrl: '/Mobile/www/partials/components/popup.html'
			},
			{
				path: '/preloader',
				componentUrl: '/Mobile/www/partials/components/preloader.html'
			},
			{
				path: '/progress-bar',
				componentUrl: '/Mobile/www/partials/components/progress-bar.html'
			},
			{
				path: '/pull-to-refresh',
				componentUrl: '/Mobile/www/partials/components/pull-to-refresh.html'
			},
			{
				path: '/radio',
				componentUrl: '/Mobile/www/partials/components/radio.html'
			},
			{
				path: '/range-slider',
				componentUrl: '/Mobile/www/partials/components/range-slider.html'
			},
			{
				path: '/rating',
				componentUrl: '/Mobile/www/partials/components/rating.html'
			},
			{
				path: '/searchbar',
				componentUrl: '/Mobile/www/partials/components/searchbar.html',
				routes: [
					{
						path: '/fixed',
						componentUrl: '/Mobile/www/partials/components/searchbar-fixed.html'
					},
					{
						path: '/static',
						componentUrl: '/Mobile/www/partials/components/searchbar-static.html'
					},
					{
						path: '/expandable',
						componentUrl: '/Mobile/www/partials/components/searchbar-expandable.html'
					}
				]
			},
			{
				path: '/sheet-modal',
				componentUrl: '/Mobile/www/partials/components/sheet-modal.html'
			},
			{
				path: '/side-panel',
				componentUrl: '/Mobile/www/partials/components/side-panel.html'
			},
			{
				path: '/smart-select',
				componentUrl: '/Mobile/www/partials/components/smart-select.html'
			},
			{
				path: '/sortable-list',
				componentUrl: '/Mobile/www/partials/components/sortable-list.html'
			},
			{
				path: '/stepper',
				componentUrl: '/Mobile/www/partials/components/stepper.html'
			},
			{
				path: '/subnavbar',
				componentUrl: '/Mobile/www/partials/components/subnavbar.html'
			},
			{
				path: '/swipeout',
				componentUrl: '/Mobile/www/partials/components/swipeout.html'
			},
			{
				path: '/swiper-slider',
				componentUrl: '/Mobile/www/partials/components/swiper-slider.html',
				routes: [
					{
						path: '/horizontal',
						componentUrl: '/Mobile/www/partials/components/swiper-slider-horizontal.html'
					},
					{
						path: '/vertical',
						componentUrl: '/Mobile/www/partials/components/swiper-slider-vertical.html'
					}
				]
			},
			{
				path: '/tabs',
				componentUrl: '/Mobile/www/partials/components/tabs.html',
				routes: [
					{
						path: '/static',
						componentUrl: '/Mobile/www/partials/components/tabs-static.html'
					},
					{
						path: '/animated',
						componentUrl: '/Mobile/www/partials/components/tabs-animated.html'
					},
					{
						path: '/swipeable',
						componentUrl: '/Mobile/www/partials/components/tabs-swipeable.html'
					},
					{
						path: '/tabbar',
						componentUrl: '/Mobile/www/partials/components/tabbar.html'
					}
				]
			},
			{
				path: '/timeline',
				componentUrl: '/Mobile/www/partials/components/timeline.html',
				routes: [
					{
						path: '/vertical',
						componentUrl: '/Mobile/www/partials/components/timeline-vertical.html'
					},
					{
						path: '/horizontal',
						componentUrl: '/Mobile/www/partials/components/timeline-horizontal.html'
					},
					{
						path: '/calendar',
						componentUrl: '/Mobile/www/partials/components/timeline-calendar.html'
					}
				]
			},
			{
				path: '/timepicker',
				componentUrl: '/Mobile/www/partials/components/timepicker.html'
			},
			{
				path: '/toast',
				componentUrl: '/Mobile/www/partials/components/toast.html'
			},
			{
				path: '/toggle',
				componentUrl: '/Mobile/www/partials/components/toggle.html'
			},
			{
				path: '/toolbar',
				componentUrl: '/Mobile/www/partials/components/toolbar.html',
				routes: [
					{
						path: '/static',
						componentUrl: '/Mobile/www/partials/components/toolbar-static.html'
					},
					{
						path: '/fixed',
						componentUrl: '/Mobile/www/partials/components/toolbar-fixed.html'
					},
					{
						path: '/hide-on-scroll',
						componentUrl: '/Mobile/www/partials/components/toolbar-hide-on-scroll.html'
					}
				]
			},
			{
				path: '/tooltip',
				componentUrl: '/Mobile/www/partials/components/tooltip.html'
			},
			{
				path: '/virtual-list',
				componentUrl: '/Mobile/www/partials/components/virtual-list.html'
			},
		]
	},
	{
		path: '/screens',
		componentUrl: '/Mobile/www/partials/screens.html',
		routes: [
			{
				path: '/404',
				componentUrl: '/Mobile/www/partials/screens/404.html'
			},
			{
				path: '/about',
				componentUrl: '/Mobile/www/partials/screens/about.html'
			},
			{
				path: '/activity-feed',
				componentUrl: '/Mobile/www/partials/screens/activity-feed.html'
			},
			{
				path: '/articles-list',
				componentUrl: '/Mobile/www/partials/screens/articles-list.html'
			},
			{
				path: '/articles-single',
				componentUrl: '/Mobile/www/partials/screens/articles-single.html'
			},
			{
				path: '/business-profile',
				componentUrl: '/Mobile/www/partials/screens/business-profile.html'
			},
			{
				path: '/careers',
				componentUrl: '/Mobile/www/partials/screens/careers.html'
			},
			{
				path: '/cart',
				componentUrl: '/Mobile/www/partials/screens/cart.html'
			},
			{
				path: '/chat',
				componentUrl: '/Mobile/www/partials/screens/chat.html'
			},
			{
				path: '/checkout',
				componentUrl: '/Mobile/www/partials/screens/checkout.html'
			},
			{
				path: '/coming-soon',
				componentUrl: '/Mobile/www/partials/screens/coming-soon.html'
			},
			{
				path: '/contact-us',
				componentUrl: '/Mobile/www/partials/screens/contact-us.html',
				beforeEnter: function(routeTo, routeFrom, resolve, reject) {
					app.preloader.show();
					if (window.google && window.google.maps) {
						app.preloader.hide();
						resolve();
					}
					else {
						var language = app.utils.i18n.getLanguage();
						LazyLoad.js(['https://maps.googleapis.com/maps/api/js?key=' + app.data.config.googleMaps.apiKey + '&language=' + language.lang + '&libraries=places'], function() {
							app.preloader.hide();
							resolve();
						});
					}
				}
			},
			{
				path: '/cookies',
				componentUrl: '/Mobile/www/partials/screens/cookies.html'
			},
			{
				path: '/event',
				componentUrl: '/Mobile/www/partials/screens/event.html'
			},
			{
				path: '/events-calendar',
				componentUrl: '/Mobile/www/partials/screens/events-calendar.html'
			},
			{
				path: '/faq',
				componentUrl: '/Mobile/www/partials/screens/faq.html'
			},
			{
				path: '/feedback',
				componentUrl: '/Mobile/www/partials/screens/feedback.html'
			},
			{
				path: '/forgot-password',
				componentUrl: '/Mobile/www/partials/screens/forgot-password.html'
			},
			{
				path: '/home',
				componentUrl: '/Mobile/www/partials/screens/home.html'
			},
			{
				path: '/invite-friends',
				componentUrl: '/Mobile/www/partials/screens/invite-friends.html'
			},
			{
				path: '/login',
				componentUrl: '/Mobile/www/partials/screens/login.html'
			},
			{
				path: '/notifications',
				componentUrl: '/Mobile/www/partials/screens/notifications.html'
			},
			{
				path: '/otp-verification',
				componentUrl: '/Mobile/www/partials/screens/otp-verification.html'
			},
			{
				path: '/privacy',
				componentUrl: '/Mobile/www/partials/screens/privacy.html'
			},
			{
				path: '/products-list',
				componentUrl: '/Mobile/www/partials/screens/products-list.html'
			},
			{
				path: '/products-single',
				componentUrl: '/Mobile/www/partials/screens/products-single.html'
			},
			{
				path: '/recipe',
				componentUrl: '/Mobile/www/partials/screens/recipe.html'
			},
			{
				path: '/settings',
				componentUrl: '/Mobile/www/partials/screens/settings.html'
			},
			{
				path: '/signup',
				componentUrl: '/Mobile/www/partials/screens/signup.html'
			},
			{
				path: '/signup/email',
				componentUrl: '/Mobile/www/partials/screens/signup-email.html'
			},
			{
				path: '/splash',
				componentUrl: '/Mobile/www/partials/screens/splash.html'
			},
			{
				path: '/team',
				componentUrl: '/Mobile/www/partials/screens/team.html'
			},
			{
				path: '/terms',
				componentUrl: '/Mobile/www/partials/screens/terms.html'
			},
			{
				path: '/testimonials',
				componentUrl: '/Mobile/www/partials/screens/testimonials.html'
			},
			{
				path: '/under-maintenance',
				componentUrl: '/Mobile/www/partials/screens/under-maintenance.html'
			},
			{
				path: '/user-profile',
				componentUrl: '/Mobile/www/partials/screens/user-profile.html'
			},
			{
				path: '/walkthrough',
				componentUrl: '/Mobile/www/partials/screens/walkthrough.html'
			}
		]
	},
	{
		path: '/themes',
		componentUrl: '/Mobile/www/partials/themes.html'
	},
	{
		path: '/web-apis',
		componentUrl: '/Mobile/www/partials/web-apis.html',
		routes: [
			{
				path: '/battery-status',
				componentUrl: '/Mobile/www/partials/web-apis/battery-status.html'
			},
			{
				path: '/clipboard',
				componentUrl: '/Mobile/www/partials/web-apis/clipboard.html'
			},
			{
				path: '/device-memory',
				componentUrl: '/Mobile/www/partials/web-apis/device-memory.html'
			},
			{
				path: '/device-orientation',
				componentUrl: '/Mobile/www/partials/web-apis/device-orientation.html'
			},
			{
				path: '/file',
				componentUrl: '/Mobile/www/partials/web-apis/file.html'
			},
			{
				path: '/fullscreen',
				componentUrl: '/Mobile/www/partials/web-apis/fullscreen.html'
			},
			{
				path: '/geolocation',
				componentUrl: '/Mobile/www/partials/web-apis/geolocation.html'
			},
			{
				path: '/network-information',
				componentUrl: '/Mobile/www/partials/web-apis/network-information.html'
			},
			{
				path: '/online-offline-status',
				componentUrl: '/Mobile/www/partials/web-apis/online-offline-status.html'
			},
			{
				path: '/page-visibility',
				componentUrl: '/Mobile/www/partials/web-apis/page-visibility.html'
			},
			{
				path: '/permissions',
				componentUrl: '/Mobile/www/partials/web-apis/permissions.html'
			},
			{
				path: '/quota-estimation',
				componentUrl: '/Mobile/www/partials/web-apis/quota-estimation.html'
			},
			{
				path: '/screen-orientation',
				componentUrl: '/Mobile/www/partials/web-apis/screen-orientation.html'
			},
			{
				path: '/vibration',
				componentUrl: '/Mobile/www/partials/web-apis/vibration.html'
			},
			{
				path: '/web-share',
				componentUrl: '/Mobile/www/partials/web-apis/web-share.html'
			}
		]
	},
	{
		path: '/cordova-plugins',
		componentUrl: '/Mobile/www/partials/cordova-plugins.html',
		routes: [
			{
				path: '/battery-status',
				componentUrl: '/Mobile/www/partials/cordova-plugins/battery-status.html'
			},
			{
				path: '/build-info',
				componentUrl: '/Mobile/www/partials/cordova-plugins/build-info.html'
			},
			{
				path: '/device',
				componentUrl: '/Mobile/www/partials/cordova-plugins/device.html'
			},
			{
				path: '/dialogs',
				componentUrl: '/Mobile/www/partials/cordova-plugins/dialogs.html'
			},
			{
				path: '/geolocation',
				componentUrl: '/Mobile/www/partials/cordova-plugins/geolocation.html'
			},
			{
				path: '/inappbrowser',
				componentUrl: '/Mobile/www/partials/cordova-plugins/inappbrowser.html'
			},
			{
				path: '/network-information',
				componentUrl: '/Mobile/www/partials/cordova-plugins/network-information.html'
			},
			{
				path: '/social-sharing',
				componentUrl: '/Mobile/www/partials/cordova-plugins/social-sharing.html'
			},
			{
				path: '/splash-screen',
				componentUrl: '/Mobile/www/partials/cordova-plugins/splash-screen.html'
			},
			{
				path: '/status-bar',
				componentUrl: '/Mobile/www/partials/cordova-plugins/status-bar.html'
			},
			{
				path: '/vibration',
				componentUrl: '/Mobile/www/partials/cordova-plugins/vibration.html'
			}
		]
	},
	{
		path: '/integrations',
		componentUrl: '/Mobile/www/partials/integrations.html',
		routes: [
			{
				path: '/algolia-places',
				componentUrl: '/Mobile/www/partials/integrations/algolia-places.html',
				beforeEnter: function(routeTo, routeFrom, resolve, reject) {
					app.preloader.show();
					if (window.places && window.L) {
						app.preloader.hide();
						resolve();
					}
					else {
						LazyLoad.css(['https://cdn.jsdelivr.net/leaflet/1/leaflet.css'], function() {
							LazyLoad.js(['https://cdn.jsdelivr.net/leaflet/1/leaflet.js'], function() {
								LazyLoad.js(['https://cdn.jsdelivr.net/npm/places.js'], function() {
									app.preloader.hide();
									resolve();
								});
							});
						});
					}
				}
			},
			{
				path: '/embedly',
				componentUrl: '/Mobile/www/partials/integrations/embedly.html',
				beforeEnter: function(routeTo, routeFrom, resolve, reject) {
					app.preloader.show();
					if (window.embedly) {
						app.preloader.hide();
						resolve();
					}
					else {
						LazyLoad.js(['https://cdn.embedly.com/widgets/platform.js'], function() {
							app.preloader.hide();
							resolve();
						});
					}
				}
			},
			{
				path: '/google-maps',
				componentUrl: '/Mobile/www/partials/integrations/google-maps.html',
				beforeEnter: function(routeTo, routeFrom, resolve, reject) {
					app.preloader.show();
					if (window.google && window.google.maps) {
						app.preloader.hide();
						resolve();
					}
					else {
						var language = app.utils.i18n.getLanguage();
						LazyLoad.js(['https://maps.googleapis.com/maps/api/js?key=' + app.data.config.googleMaps.apiKey + '&language=' + language.lang + '&libraries=places'], function() {
							app.preloader.hide();
							resolve();
						});
					}
				}
			}
		]
	},
	{
		path: '/utilities',
		componentUrl: '/Mobile/www/partials/utilities.html',
		routes: [
			{
				path: '/brand-icons',
				componentUrl: '/Mobile/www/partials/utilities/brand-icons.html'
			},
			{
				path: '/colors',
				componentUrl: '/Mobile/www/partials/utilities/colors.html'
			},
			{
				path: '/dropcaps',
				componentUrl: '/Mobile/www/partials/utilities/dropcaps.html'
			},
			{
				path: '/elevation',
				componentUrl: '/Mobile/www/partials/utilities/elevation.html'
			},
			{
				path: '/embeds',
				componentUrl: '/Mobile/www/partials/utilities/embeds.html'
			},
			{
				path: '/line-dividers',
				componentUrl: '/Mobile/www/partials/utilities/line-dividers.html'
			}
		]
	},
	{
		path: '/more',
		componentUrl: '/Mobile/www/partials/more.html'
	},
	{
		path: '(.*)',
		componentUrl: '/Mobile/www/partials/screens/404.html'
	}
];