using System;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// Базовый класс для коллбеков которые сообщают об ошибках через status или errcode
/// </summary>
[Serializable]
public class VKPlayWebEventArgsBase : EventArgs
{
    /// <summary>
    /// "ok" если операция успешна.
    /// </summary>
    public string status;

    /// <summary>
    /// Внутренний код ошибки партнера.
    /// </summary>
    public int errcode;

    /// <summary>
    /// Описание кода ошибки партнера.
    /// </summary>
    public string errmsg;
}

[Serializable]
public class GetLoginStatusCallbackData : VKPlayWebEventArgsBase
{
    /// <summary>
    /// Статус пользователя:
    /// <br/>
    /// 0 - пользователь не авторизован;
    /// <br/>
    /// 1 - пользователь авторизован, не зарегистрирован;
    /// <br/>
    /// 2 - пользователь авторизован, зарегистрирован;
    /// <br/>
    /// 3 - пользователь авторизован, зарегистрирован, и совершил покупку игры
    /// (только для премиум и платных игр с поддержкой внутриигровых транзакций).
    /// </summary>
    public int loginStatus;
}

[Serializable]
public class UserInfoCallbackData : VKPlayWebEventArgsBase
{
    /// <summary>
    /// ID пользователя на платформе.
    /// </summary>
    public int uid;

    /// <summary>
    /// MD5 хэш от почты пользователя.
    /// </summary>
    public string hash;
}

[Serializable]
public class UserProfileCallbackData : VKPlayWebEventArgsBase
{
    /// <summary>
    /// ID пользователя на платформе;
    /// </summary>
    public int uid;

    /// <summary>
    /// Ник пользователя;
    /// </summary>
    public string nick;

    /// <summary>
    /// Ссылка (URL) на изображение для аватара пользователя;
    /// </summary>
    public string avatar;

    /// <summary>
    /// Год рождения пользователя;
    /// </summary>
    public int birthyear;

    /// <summary>
    /// Пол пользователя, "male" или "female";
    /// </summary>
    public string sex;

    /// <summary>
    /// Уникальный строковый идентификатор пользователя.
    /// </summary>
    public string slug;
}

[Serializable]
public class PaymentFrameUrlCallbackData : VKPlayWebEventArgsBase
{
    /// <summary>
    /// Ссылка (URL) на платежный фрейм.
    /// </summary>
    public string url;
}

[Serializable]
public class GetAuthTokenCallbackData : VKPlayWebEventArgsBase
{
    /// <summary>
    /// ID пользователя в платформе;
    /// </summary>
    public int uid;

    /// <summary>
    /// Авторизационный токен.
    /// </summary>
    public string hash;

    /// <summary>
    /// Полный URL-адрес игры.
    /// </summary>
    public string url;
}

[Serializable]
public class PaymentReceivedCallbackData : VKPlayWebEventArgsBase
{
    /// <summary>
    /// ID пользователя в платформе;
    /// </summary>
    public int uid;
}

[Serializable]
public class PaymentWindowClosedCallbackData : VKPlayWebEventArgsBase
{
    // Пусто.
}

[Serializable]
public class ConfirmWindowClosedCallbackData : VKPlayWebEventArgsBase
{
    // Пусто.
}

[Serializable]
public class ApiInitCallbackData : VKPlayWebEventArgsBase
{
    // Пусто.
}

/// <summary>
/// Данный класс не использует status для сообщения об ошибках, используйте code.
/// </summary>
[Serializable]
public class AdsCallbackData : EventArgs
{
    /// <summary>
    /// "adCompleted" - реклама просмотрена;
    /// <br/>
    /// "adDismissed" - реклама пропущена или отсутствует;
    /// <br/>
    /// "adError" - реклама не была показана вследствие возникшей ошибки.
    /// </summary>
    public string type;

    /// <summary>
    /// "UndefinedAdError" - ошибка показа рекламы;
    /// <br/>
    /// "AdblockDetectedAdError" - обнаружен блокировщик рекламы;
    /// <br/>
    /// "WaterfallConfigLoadFailed" - ошибка показа рекламы.
    /// </summary>
    public string code;
}

[Serializable]
public class VKPlayWebAdsConfig
{
    /// <summary>
    /// "admanSource,admanagerSource" ИЛИ
    /// <br/>
    /// "admanSource" ИЛИ
    /// <br/>
    /// "admanagerSource" ИЛИ
    /// <br/>
    /// ничего (null), будет выбран лучший источник рекламы.
    /// </summary>
    public string sources;

    /// <summary>
    /// true - ролик будет показан без футера и сообщений, связанных с получением вознаграждения
    /// <br/>
    /// false - будет выбрано стандартное поведение (т.е. будет показана реклама в формате advideoreward)
    /// </summary>
    public bool interstitial;
}

[Serializable]
public class UserSocialFriendsCallbackDataFriend
{
    /// <summary>
    /// Ник пользователя
    /// </summary>
    public string nick;

    /// <summary>
    /// Количество по
    /// </summary>
    public int online;

    /// <summary>
    /// Уникальный идентификатор пользователя
    /// </summary>
    public string slug;

    /// <summary>
    /// Ссылка на изображение для аватара пользователя
    /// </summary>
    public string avatar;
}

[Serializable]
public class UserSocialFriendsCallbackData : VKPlayWebEventArgsBase
{
    /// <summary>
    /// Список друзей
    /// </summary>
    public UserSocialFriendsCallbackDataFriend[] friends;
}

[Serializable]
public class UserFriendsCallbackDataFriend
{
    /// <summary>
    /// ID пользователя на платформе
    /// </summary>
    public int uid;

    /// <summary>
    /// Ник пользователя
    /// </summary>
    public string nick;

    /// <summary>
    /// Уникальный идентификатор пользователя
    /// </summary>
    public string slug;

    /// <summary>
    /// Ссылка на изображение для аватара пользователя
    /// </summary>
    public string avatar;
}

[Serializable]
public class UserFriendsCallbackData : VKPlayWebEventArgsBase
{
    /// <summary>
    /// Список друзей
    /// </summary>
    public UserFriendsCallbackDataFriend[] friends;
}

[Serializable]
public class VKPlayWebMerchantParamBase
{
    /// <summary>
    /// Количество
    /// </summary>
    public int amount;

    /// <summary>
    /// Описание предмета
    /// </summary>
    public string description;

    /// <summary>
    /// Валюта, как правило "RUB"
    /// </summary>
    public string currency;

    // наследуйте от этого класса чтобы добавить доп. опции...
}

[Serializable]
public class VKPlayPaymentFrameArgs
{
    public string scenario;
    public VKPlayWebMerchantParamBase merchant_param;
}

[Serializable]
public class VKPlayPaymentFrameItemArgs
{
    /// <summary>
    /// Список id-ов вида "1,2,3,4"
    /// </summary>
    public string ids;
    public object merchant_param;

    public VKPlayPaymentFrameItemArgs(int[] idsIntArray)
    {
        var s = "";
        for (int i = 0, l = idsIntArray.Length; i < l; ++i)
        {
            s += idsIntArray[i].ToString();
            if (i < l - 1)
            {
                s += ",";
            }
        }

        ids = s;
    }
}

public class VKPlayWeb : MonoBehaviour
{
    private static GameObject instance_;
    private static VKPlayWeb componentInstance_;

    /// <summary>
    /// Синглтон API, все методы должны вызываться через него
    /// </summary>
    public static VKPlayWeb instance
    {
        get
        {
            if (instance_ == null && componentInstance_ == null)
            {
                instance_ = new GameObject("VKPlayWeb");
                componentInstance_ = instance_.AddComponent<VKPlayWeb>();
            }

            return componentInstance_;
        }
    }

    void Awake()
    {
        var i = instance;
        if (i != null && i != this)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);
    }

    // functions
    [DllImport("__Internal")]
    private static extern void VKPlayWeb_Init(int gmrIdInt, string gameObjectName);

    /// <summary>
    /// Данный метод инициализирует JS API, нужно вызвать только ОДИН раз.
    /// <br/>
    /// Вызывает коллбек <see cref="apiInitCallback"/>
    /// </summary>
    /// <param name="gmrIdInt">Ваш GMR ID из Системных свойств</param>
    public void init(int gmrIdInt) => VKPlayWeb_Init(gmrIdInt, gameObject.name);

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_GetLoginStatus();
    /// <summary>
    /// Данный метод получает информацию о текущем статусе пользователя.
    /// <br/>
    /// Вызывает коллбек <see cref="getLoginStatusCallback"/>
    /// </summary>
    public void getLoginStatus() => VKPlayWeb_GetLoginStatus();

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_RegisterUser();
    /// <summary>
    /// Данный метод позволяет вызвать форму регистрации пользователя
    /// <br/>
    /// Вызывает коллбек <see cref="registerUserCallback"/>
    /// </summary>
    public void registerUser() => VKPlayWeb_RegisterUser();

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_GetAuthToken();
    /// <summary>
    /// Данный метод позволяет получить авторизационный токен для текущего пользователя
    /// <br/>
    /// Вызывает коллбек <see cref="getAuthTokenCallback"/>
    /// </summary>
    public void getAuthToken() => VKPlayWeb_GetAuthToken();

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_AuthUser();
    /// <summary>
    /// Данный метод вызывает форму авторизации пользователя и перезагружает страницу
    /// </summary>
    public void authUser() => VKPlayWeb_AuthUser();

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_UserProfile();
    /// <summary>
    /// Данный метод позволяет получить более подробную информацию о пользователе
    /// <br/>
    /// Вызывает коллбек <see cref="userProfileCallback"/>
    /// </summary>
    public void userProfile() => VKPlayWeb_UserProfile();

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_UserInfo();
    /// <summary>
    /// Данный метод позволяет получить информацию о пользователе (например для расчёта подписи)
    /// <br/>
    /// Вызывает коллбек <see cref="userInfoCallback"/>
    /// </summary>
    public void userInfo() => VKPlayWeb_UserInfo();

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_ReloadWindow();
    /// <summary>
    /// Данный метод перезагружает окно игры и обновляет параметры iframe
    /// </summary>
    public void reloadWindow() => VKPlayWeb_ReloadWindow();

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_ShowAds(string adsConfigJsonString);
    /// <summary>
    /// Данный метод позволяет показать игроку рекламу
    /// <br/>
    /// См. <see cref="VKPlayWebAdsConfig"/>
    /// </summary>
    /// <param name="adsConfig">Настройки показа рекламы</param>
    public void showAds(VKPlayWebAdsConfig adsConfig) => VKPlayWeb_ShowAds(JsonUtility.ToJson(adsConfig));

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_UserFriends();
    /// <summary>
    /// Данный метод позволяет получить список друзей пользователя в проекте
    /// <br/>
    /// Вызывает коллбек <see cref="userFriendsCallback"/>
    /// </summary>
    public void userFriends() => VKPlayWeb_UserFriends();

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_UserSocialFriends();
    /// <summary>
    /// Данный метод позволяет получить список друзей пользователя (не привязан к проекту)
    /// <br/>
    /// Вызывает коллбек <see cref="userSocialFriendsCallback"/>
    /// </summary>
    public void userSocialFriends() => VKPlayWeb_UserSocialFriends();

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_GetGameInventoryItems();
    /// <summary>
    /// Данный метод позволяет получить информацию о внутриигровых предметах
    /// <br/>
    /// TODO: реализовать коллбек
    /// </summary>
    public void getGameInventoryItems() => VKPlayWeb_GetGameInventoryItems();

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_PaymentFrame(string argsJsonString);
    /// <summary>
    /// Показывает платёжную форму
    /// <br/>
    /// Вызывает коллбеки <see cref="paymentReceivedCallback"/> и <see cref="paymentWindowClosedCallback"/>
    /// </summary>
    /// <param name="args">Параметры показа платёжной формы</param>
    public void paymentFrame(VKPlayPaymentFrameArgs args) => VKPlayWeb_PaymentFrame(JsonUtility.ToJson(args));

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_PaymentFrameItem(string argsJsonString);
    public void paymentFrameItem(VKPlayPaymentFrameItemArgs args) => VKPlayWeb_PaymentFrameItem(JsonUtility.ToJson(args));

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_PaymentFrameUrl(string argsJsonString);
    /// <summary>
    /// Генерирует ссылку на платёжную форму
    /// <br/>
    /// Вызывает коллбек <see cref="paymentFrameUrlCallback"/>
    /// </summary>
    /// <param name="args">Параметры платёжной формы</param>
    public void paymentFrameUrl(VKPlayPaymentFrameArgs args) => VKPlayWeb_PaymentFrameUrl(JsonUtility.ToJson(args));

    // callbacks
    public event EventHandler<ApiInitCallbackData> apiInitCallback;
    public void ThrowApiInit(string a) =>
        apiInitCallback?.Invoke(this, JsonUtility.FromJson<ApiInitCallbackData>(a));

    public event EventHandler<GetLoginStatusCallbackData> getLoginStatusCallback;
    public void ThrowGetLoginStatus(string a) =>
        getLoginStatusCallback?.Invoke(this, JsonUtility.FromJson<GetLoginStatusCallbackData>(a));

    public event EventHandler<UserInfoCallbackData> userInfoCallback;
    public void ThrowUserInfo(string a) =>
        userInfoCallback?.Invoke(this, JsonUtility.FromJson<UserInfoCallbackData>(a));

    public event EventHandler<UserProfileCallbackData> userProfileCallback;
    public void ThrowUserProfile(string a) =>
        userProfileCallback?.Invoke(this, JsonUtility.FromJson<UserProfileCallbackData>(a));

    public event EventHandler<UserInfoCallbackData> registerUserCallback;
    public void ThrowRegisterUser(string a) =>
        registerUserCallback?.Invoke(this, JsonUtility.FromJson<UserInfoCallbackData>(a));

    public event EventHandler<PaymentFrameUrlCallbackData> paymentFrameUrlCallback;
    public void ThrowPaymentFrameUrl(string a) =>
        paymentFrameUrlCallback?.Invoke(this, new PaymentFrameUrlCallbackData() { status = "ok", url = a });

    public event EventHandler<GetAuthTokenCallbackData> getAuthTokenCallback;
    public void ThrowGetAuthToken(string a) =>
        getAuthTokenCallback?.Invoke(this, JsonUtility.FromJson<GetAuthTokenCallbackData>(a));

    public event EventHandler<PaymentReceivedCallbackData> paymentReceivedCallback;
    public void ThrowPaymentReceived(string a) =>
        paymentReceivedCallback?.Invoke(this, JsonUtility.FromJson<PaymentReceivedCallbackData>(a));

    public event EventHandler<PaymentWindowClosedCallbackData> paymentWindowClosedCallback;
    public void ThrowPaymentWindowClosed() =>
        paymentWindowClosedCallback?.Invoke(this, new PaymentWindowClosedCallbackData { status = "ok" });

    public event EventHandler<ConfirmWindowClosedCallbackData> confirmWindowClosedCallback;
    public void ThrowConfirmWindowClosed() =>
        confirmWindowClosedCallback?.Invoke(this, new ConfirmWindowClosedCallbackData { status = "ok" });

    public event EventHandler<AdsCallbackData> adsCallback;
    public void ThrowAds(string a) =>
        adsCallback?.Invoke(this, JsonUtility.FromJson<AdsCallbackData>(a));

    public event EventHandler<UserFriendsCallbackData> userFriendsCallback;
    public void ThrowUserFriends(string a) =>
        userFriendsCallback?.Invoke(this, JsonUtility.FromJson<UserFriendsCallbackData>(a));

    public event EventHandler<UserSocialFriendsCallbackData> userSocialFriendsCallback;
    public void ThrowUserSocialFriends(string a) =>
        userSocialFriendsCallback?.Invoke(this, JsonUtility.FromJson<UserSocialFriendsCallbackData>(a));
}
