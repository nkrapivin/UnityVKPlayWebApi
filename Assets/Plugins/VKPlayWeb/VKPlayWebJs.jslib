mergeInto(LibraryManager.library, {
	/* -- */

	VKPlayWeb_Init: function(gmrIdInt, gameObjectNameStringPtr) {
		var externalApi = null;
		var unityScope = this;
		let sf = (typeof UTF8ToString === 'undefined') ? Pointer_stringify : UTF8ToString;
		var unityGameObjectName = sf(gameObjectNameStringPtr);
		
		let scriptTag = document.createElement('script');
		scriptTag.type = 'text/javascript';
		scriptTag.src = '//vkplay.ru/app/' + gmrIdInt.toString() + '/static/mailru.core.js';
		scriptTag.onload = function(ev) {
			console.log('vkplay api load:');

			let callbacks = {
				appid: gmrIdInt,
				getLoginStatusCallback: function(status) {
					console.log('getLoginStatusCallback'); console.log(status);
					unityScope.SendMessage(unityGameObjectName, 'ThrowGetLoginStatus', JSON.stringify(status));
				},
				userInfoCallback: function(info) {
					console.log('userInfoCallback'); console.log(info);
					unityScope.SendMessage(unityGameObjectName, 'ThrowUserInfo', JSON.stringify(info));
				},
				userProfileCallback: function(profile) {
					console.log('userProfileCallback'); console.log(profile);
					unityScope.SendMessage(unityGameObjectName, 'ThrowUserProfile', JSON.stringify(profile));
				},
				registerUserCallback: function(info) {
					console.log('registerUserCallback'); console.log(info);
					unityScope.SendMessage(unityGameObjectName, 'ThrowRegisterUser', JSON.stringify(info));
				},
				paymentFrameUrlCallback: function(url) {
					console.log('paymentFrameUrlCallback'); console.log(url);
					unityScope.SendMessage(unityGameObjectName, 'ThrowPaymentFrameUrl', url);
				},
				getAuthTokenCallback: function(token) {
					console.log('getAuthTokenCallback'); console.log(token);
					unityScope.SendMessage(unityGameObjectName, 'ThrowGetAuthToken', JSON.stringify(token));
				},
				paymentReceivedCallback: function(data) {
					console.log('paymentReceivedCallback'); console.log(data);
					unityScope.SendMessage(unityGameObjectName, 'ThrowPaymentReceived', JSON.stringify(data));
				},
				paymentWindowClosedCallback: function() {
					console.log('paymentWindowClosedCallback');
					unityScope.SendMessage(unityGameObjectName, 'ThrowPaymentWindowClosed');
				},
				confirmWindowClosedCallback: function() {
					console.log('confirmWindowClosedCallback');
					unityScope.SendMessage(unityGameObjectName, 'ThrowConfirmWindowClosed');
				},
				adsCallback: function(context) {
					console.log('adsCallback'); console.log(context);
					unityScope.SendMessage(unityGameObjectName, 'ThrowAds', JSON.stringify(context));
				},
				userFriendsCallback: function(data) {
					console.log('userFriendsCallback'); console.log(data);
					unityScope.SendMessage(unityGameObjectName, 'ThrowUserFriends', JSON.stringify(data));
				},
				userSocialFriendsCallback: function(data) {
					console.log('userSocialFriendsCallback'); console.log(data);
					unityScope.SendMessage(unityGameObjectName, 'ThrowUserSocialFriends', JSON.stringify(data));
				}
			};
			
			let ifapi = window.iframeApi;
			console.log(ifapi);
			if ((typeof ifapi) === 'undefined') {
				unityScope.SendMessage(unityGameObjectName, 'ThrowApiInit', JSON.stringify({ 'status': 'error', 'errcode': 2, 'errmsg': 'iframeapi error' }));
			} else {
				ifapi(callbacks).then(
					function(eapi) {
						externalApi = eapi;
						console.log('vkplay api full load:'); console.log(externalApi);
						unityScope.SendMessage(unityGameObjectName, 'ThrowApiInit', JSON.stringify({ 'status': 'ok' }));
					},
					function(apierr) {
						console.log('vkplay api load error:'); console.log(apierr);
						unityScope.SendMessage(unityGameObjectName, 'ThrowApiInit', JSON.stringify({ 'status': 'error', 'errcode': 3, 'errmsg': 'promise error ' + apierr }));
					}
				);
				console.log('vkplay called promise for gameobj ' + unityGameObjectName);
			}
		};
		scriptTag.onerror = function(ev) {
			unityScope.SendMessage(unityGameObjectName, 'ThrowApiInit', JSON.stringify({ 'status': 'error', 'errcode': 1, 'errmsg': 'script error' }));
		};
		document.head.appendChild(scriptTag);
	},

	VKPlayWeb_GetLoginStatus: function() {
		if (externalApi) {
			externalApi.getLoginStatus();
		}
	},
	
	VKPlayWeb_RegisterUser: function() {
		if (externalApi) {
			externalApi.registerUser();
		}
	},
	
	VKPlayWeb_GetAuthToken: function() {
		if (externalApi) {
			externalApi.getAuthToken();
		}
	},
	
	VKPlayWeb_AuthUser: function() {
		if (externalApi) {
			externalApi.authUser();
		}
	},
	
	VKPlayWeb_UserProfile: function() {
		if (externalApi) {
			externalApi.userProfile();
		}
	},
	
	VKPlayWeb_UserInfo: function() {
		if (externalApi) {
			externalApi.userInfo();
		}
	},
	
	VKPlayWeb_ReloadWindow: function() {
		if (externalApi) {
			externalApi.reloadWindow();
		}
	},
	
	VKPlayWeb_ShowAds: function(adsConfigJsonString) {
		let sf = (typeof UTF8ToString === 'undefined') ? Pointer_stringify : UTF8ToString;
		
		if (externalApi) {
			externalApi.showAds(JSON.parse(sf(adsConfigJsonString)));
		}
	},
	
	VKPlayWeb_PaymentFrame: function(argsJsonString) {
		let sf = (typeof UTF8ToString === 'undefined') ? Pointer_stringify : UTF8ToString;
		
		if (externalApi) {
			externalApi.paymentFrame(JSON.parse(sf(argsJsonString)));
		}
	},
	
	VKPlayWeb_PaymentFrameItem: function(argsJsonString) {
		let sf = (typeof UTF8ToString === 'undefined') ? Pointer_stringify : UTF8ToString;
		
		if (externalApi) {
			externalApi.paymentFrameItem(JSON.parse(sf(argsJsonString)));
		}
	},
	
	VKPlayWeb_PaymentFrameUrl: function(argsJsonString) {
		let sf = (typeof UTF8ToString === 'undefined') ? Pointer_stringify : UTF8ToString;
		
		if (externalApi) {
			externalApi.paymentFrameUrl(JSON.parse(sf(argsJsonString)));
		}
	},
	
	VKPlayWeb_UserFriends: function() {
		if (externalApi) {
			externalApi.userFriends();
		}
	},
	
	VKPlayWeb_UserSocialFriends: function() {
		if (externalApi) {
			externalApi.userSocialFriends();
		}
	},
	
	VKPlayWeb_GetGameInventoryItems: function() {
		if (externalApi) {
			externalApi.getGameInventoryItems();
		}
	}

	/* -- */
});

