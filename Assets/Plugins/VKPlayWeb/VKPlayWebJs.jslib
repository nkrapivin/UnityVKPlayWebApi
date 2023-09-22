mergeInto(LibraryManager.library, {
	/* -- */

	VKPlayWeb_Init: function(gmrIdInt, gameObjectNameStringPtr) {
		if (typeof externalApi !== 'undefined') {
			console.log('vkplay api is already initialized');
			return;
		}
		
		let sf = (typeof UTF8ToString === 'undefined') ? Pointer_stringify : UTF8ToString;
		var unityGameObjectName = sf(gameObjectNameStringPtr);
		var externalApi = null;
		
		let scriptTag = document.createElement('script');
		scriptTag.type = 'text/javascript';
		scriptTag.src = '//vkplay.ru/app/' + gmrIdInt.toString() + '/static/mailru.core.js';
		scriptTag.onload = function(ev) {
			console.log('vkplay api load:');
			console.log(ev);

			let callbacks = {
				appid: gmrIdInt,
				getLoginStatusCallback: function(status) {
					console.log('getLoginStatusCallback'); console.log(status);
					Module.SendMessage(unityGameObjectName, 'ThrowGetLoginStatus', JSON.stringify(status));
				},
				userInfoCallback: function(info) {
					console.log('userInfoCallback'); console.log(info);
					Module.SendMessage(unityGameObjectName, 'ThrowUserInfo', JSON.stringify(info));
				},
				userProfileCallback: function(profile) {
					console.log('userProfileCallback'); console.log(profile);
					Module.SendMessage(unityGameObjectName, 'ThrowUserProfile', JSON.stringify(profile));
				},
				registerUserCallback: function(info) {
					console.log('registerUserCallback'); console.log(info);
					Module.SendMessage(unityGameObjectName, 'ThrowRegisterUser', JSON.stringify(info));
				},
				paymentFrameUrlCallback: function(url) {
					console.log('paymentFrameUrlCallback'); console.log(url);
					Module.SendMessage(unityGameObjectName, 'ThrowPaymentFrameUrl', url);
				},
				getAuthTokenCallback: function(token) {
					console.log('getAuthTokenCallback'); console.log(token);
					Module.SendMessage(unityGameObjectName, 'ThrowGetAuthToken', JSON.stringify(token));
				},
				paymentReceivedCallback: function(data) {
					console.log('paymentReceivedCallback'); console.log(data);
					Module.SendMessage(unityGameObjectName, 'ThrowPaymentReceived', JSON.stringify(data));
				},
				paymentWindowClosedCallback: function() {
					console.log('paymentWindowClosedCallback');
					Module.SendMessage(unityGameObjectName, 'ThrowPaymentWindowClosed');
				},
				confirmWindowClosedCallback: function() {
					console.log('confirmWindowClosedCallback');
					Module.SendMessage(unityGameObjectName, 'ThrowConfirmWindowClosed');
				},
				adsCallback: function(context) {
					console.log('adsCallback'); console.log(context);
					Module.SendMessage(unityGameObjectName, 'ThrowAds', JSON.stringify(context));
				},
				userFriendsCallback: function(data) {
					console.log('userFriendsCallback'); console.log(data);
					Module.SendMessage(unityGameObjectName, 'ThrowUserFriends', JSON.stringify(data));
				},
				userSocialFriendsCallback: function(data) {
					console.log('userSocialFriendsCallback'); console.log(data);
					Module.SendMessage(unityGameObjectName, 'ThrowUserSocialFriends', JSON.stringify(data));
				}
			};
			
			console.log('callback struct:');
			console.log(callbacks);
			
			let ifapi = window.iframeApi;
			console.log('iframe api:');
			console.log(ifapi);
			
			if ((typeof ifapi) === 'undefined') {
				Module.SendMessage(unityGameObjectName, 'ThrowApiInit', JSON.stringify({ 'status': 'error', 'errcode': 2, 'errmsg': 'iframeapi error' }));
			} else {
				ifapi(callbacks).then(
					function(eapi) {
						console.log('vkplay api full load:'); console.log(eapi);
						externalApi = eapi;
						Module.SendMessage(unityGameObjectName, 'ThrowApiInit', JSON.stringify({ 'status': 'ok' }));
					},
					function(apierr) {
						console.log('vkplay api load error:'); console.log(apierr);
						Module.SendMessage(unityGameObjectName, 'ThrowApiInit', JSON.stringify({ 'status': 'error', 'errcode': 3, 'errmsg': 'promise error ' + apierr }));
					}
				);
				console.log('vkplay called promise for gameobj ' + unityGameObjectName);
			}
		};
		scriptTag.onerror = function(ev) {
			console.log('vk api script error:'); console.log(ev);
			Module.SendMessage(unityGameObjectName, 'ThrowApiInit', JSON.stringify({ 'status': 'error', 'errcode': 1, 'errmsg': 'script error' }));
		};
		document.head.appendChild(scriptTag);
	},

	VKPlayWeb_GetLoginStatus: function() {
		if (typeof externalApi !== 'undefined') {
			externalApi.getLoginStatus();
		}
	},
	
	VKPlayWeb_RegisterUser: function() {
		if (typeof externalApi !== 'undefined') {
			externalApi.registerUser();
		}
	},
	
	VKPlayWeb_GetAuthToken: function() {
		if (typeof externalApi !== 'undefined') {
			externalApi.getAuthToken();
		}
	},
	
	VKPlayWeb_AuthUser: function() {
		if (typeof externalApi !== 'undefined') {
			externalApi.authUser();
		}
	},
	
	VKPlayWeb_UserProfile: function() {
		if (typeof externalApi !== 'undefined') {
			externalApi.userProfile();
		}
	},
	
	VKPlayWeb_UserInfo: function() {
		if (typeof externalApi !== 'undefined') {
			externalApi.userInfo();
		}
	},
	
	VKPlayWeb_ReloadWindow: function() {
		if (typeof externalApi !== 'undefined') {
			externalApi.reloadWindow();
		}
	},
	
	VKPlayWeb_ShowAds: function(adsConfigJsonString) {
		if (typeof externalApi !== 'undefined') {
			let sf = (typeof UTF8ToString === 'undefined') ? Pointer_stringify : UTF8ToString;
			externalApi.showAds(JSON.parse(sf(adsConfigJsonString)));
		}
	},
	
	VKPlayWeb_PaymentFrame: function(argsJsonString) {
		if (typeof externalApi !== 'undefined') {
			let sf = (typeof UTF8ToString === 'undefined') ? Pointer_stringify : UTF8ToString;
			externalApi.paymentFrame(JSON.parse(sf(argsJsonString)));
		}
	},
	
	VKPlayWeb_PaymentFrameItem: function(argsJsonString) {
		if (typeof externalApi !== 'undefined') {
			let sf = (typeof UTF8ToString === 'undefined') ? Pointer_stringify : UTF8ToString;
			externalApi.paymentFrameItem(JSON.parse(sf(argsJsonString)));
		}
	},
	
	VKPlayWeb_PaymentFrameUrl: function(argsJsonString) {
		if (typeof externalApi !== 'undefined') {
			let sf = (typeof UTF8ToString === 'undefined') ? Pointer_stringify : UTF8ToString;
			externalApi.paymentFrameUrl(JSON.parse(sf(argsJsonString)));
		}
	},
	
	VKPlayWeb_UserFriends: function() {
		if (typeof externalApi !== 'undefined') {
			externalApi.userFriends();
		}
	},
	
	VKPlayWeb_UserSocialFriends: function() {
		if (typeof externalApi !== 'undefined') {
			externalApi.userSocialFriends();
		}
	},
	
	VKPlayWeb_GetGameInventoryItems: function() {
		if (typeof externalApi !== 'undefined') {
			externalApi.getGameInventoryItems();
		}
	}

	/* -- */
});

