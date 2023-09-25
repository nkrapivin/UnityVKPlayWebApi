using UnityEngine;

public class ApiExample : MonoBehaviour
{
    private VKPlayWeb api;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("before everything");

        api = VKPlayWeb.instance;

        var gmrId = 1; // <<< заменить на свой!!!! См. Кабинет -> Системные свойства -> ID Игры (GMRID)

        // Самый главный коллбек
        api.apiInitCallback += (sender, args) =>
        {
            Debug.Log(args);

            if (args.status == "ok")
            {
                // Получить инфу о пользователе, она придёт в getLoginStatusCallback...
                api.getLoginStatus();
            }
            else
            {
                // Что-то пошло не так с JS API
                // Возможные причины:
                // адблокер,
                // косяк на стороне вк
                // косяк на стороне разраба
                // странный косяк ифрейма или скрипта
                Debug.Log($"api init fail, reason = {args.errmsg}");
            }
        };

        // Второй по важности
        api.getLoginStatusCallback += (sender, args) =>
        {
            Debug.Log(args);
            
            if (args.loginStatus == 0)
            {
                // Нужно авторизироваться
                api.authUser();
            }
            else if (args.loginStatus == 1)
            {
                // Нужно зарегестрировать пользователя в игре
                // (после успешной реги нужно будет перегрузить ифрейм,
                //  это сделает коллбек ниже)
                // Если пользователь уже зареган то этот код не выполнится.
                api.registerUser();
            }
            else if (args.loginStatus == 2 || args.loginStatus == 3)
            {
                // Если loginStatus == 2 или == 3 можете вызывать все остальные методы JS API:
                api.getAuthToken(); // ответ в getAuthTokenCallback
                api.userFriends(); // ответ в userFriendsCallback
                api.userSocialFriends(); // ответ в userSocialFriendsCallback
                api.userInfo(); // ответ в userInfoCallback
                api.userProfile(); // ответ в userProfileCallback
                // .... надеюсь понятно что сразу это делать не нужно, особенно showAds :DDDDD

                var adsConfig = new VKPlayWebAdsConfig()
                {
                    interstitial = false
                };
                api.showAds(adsConfig);
            }
        };

        // Выполняется только если loginStatus был 1, нужно перезагрузить ифрейм
        api.registerUserCallback += (sender, args) =>
        {
            Debug.Log(args);

            if (args.status != "ok")
            {
                // Что-то пошло не так...? Дать пользователю повторить регу.
                return;
            }

            /*
            https://documentation.vkplay.ru/f2p_vkp/f2pb_js_vkp#registerUser
                Чтобы все параметры после вызова метода registerUser, гарантированно, передались в iframe с игрой,
                требуется перезагрузить страницу с игрой, используя метод reloadWindow.
                Сделать это можно, например,
                сделав следующий вызов сразу после вызова метода registerUser:
            */
            api.reloadWindow();
        };

        api.userFriendsCallback += (sender, args) =>
        {
            Debug.Log(args);
        };

        api.userSocialFriendsCallback += (sender, args) =>
        {
            Debug.Log(args);
        };

        api.userInfoCallback += (sender, args) =>
        {
            Debug.Log(args);

            if (args.status == "ok")
            {
                Debug.Log($"uid={args.uid}, hash={args.hash}");
            }
        };

        api.userProfileCallback += (sender, args) =>
        {
            Debug.Log(args);

            if (args.status == "ok")
            {
                Debug.Log($"Hello {args.nick}!");
            }
        };

        // Тоже важный коллбек
        api.adsCallback += (sender, args) =>
        {
            Debug.Log(args);

            // здесь нет поля .status, нужно использовать .code
            // См. https://documentation.vkplay.ru/f2p_vkp/f2pb_adbreak_vkp
            if (args.type == "adError")
            {
                // что-то очень плохо с рекламой!
            }
        };

        api.confirmWindowClosedCallback += (sender, args) =>
        {
            Debug.Log(args);
        };

        api.getAuthTokenCallback += (sender, args) =>
        {
            Debug.Log(args);

            if (args.status == "ok")
            {
                Debug.Log($"token data, uid={args.uid}, token={args.hash}");
            }
        };

        Debug.Log($"before api init, gmr id = {gmrId}");

        api.init(gmrId);

        Debug.Log("api init called, wait for apiInitCallback...");
        // после этого ждём вызов apiInitCallback... будет он сильно позже, не сразу
    }

    // Update is called once per frame
    void Update()
    {
        // Вот как раз таки showAds лучше здесь дёргать когда уже loginStatus==2 или 3
        // в ПОДХОДЯЩИЕ моменты...
    }
}
